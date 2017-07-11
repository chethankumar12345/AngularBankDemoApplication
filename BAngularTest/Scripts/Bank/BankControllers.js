/// <reference path="angular.js" />

app.controller('BankCreateController', function ($scope, $http, bankAccessor, $location) {
    $scope.Save = function () {
        var validated = true;
        $scope.IFSCRequired = false;
        $scope.BankNameRequired = false;

        if ($scope.bank == undefined) {

            $scope.IFSCRequired = true;
                $scope.BankNameRequired = true;
                validated = false;
        }
        else {
            if ($scope.bank.Name == undefined || $scope.bank.Name == "")
            { $scope.BankNameRequired = true; validated = false; }
            if ($scope.bank.IFSC == undefined || $scope.bank.IFSC == "")
            { $scope.IFSCRequired = true; validated = false; }

        }
        if (validated)
            {
                bankAccessor.Save($scope.bank).then(function (response) {
                    $location.path("#!/BankList");
                }, function (response) {

                    if (response.status == 409) {
                        $scope.Message = "Enter Different Bank name  to save";
                    }
                })
        }
 };
 $scope.Cancel = function () {
     $location.path("#!/BankList");
 };

});
app.controller('BankEditController', function ($scope, $http, bankAccessor, $routeParams, $location) {


    bankAccessor.GetById($routeParams.id).then(function (response) {

        $scope.bank = response.data;

    });
    $scope.Save = function () {
        var validated = true;
        $scope.IFSCRequired = false;
        $scope.BankNameRequired = false;

        if ($scope.bank == undefined) {

                $scope.IFSCRequired = true;
                $scope.BankNameRequired = true;
                validated = false;
        }
        else {
                if ($scope.bank.Name == undefined || $scope.bank.Name == "")
                { $scope.BankNameRequired = true; validated = false; }
                if ($scope.bank.IFSC == undefined || $scope.bank.IFSC == "")
                { $scope.IFSCRequired = true; validated = false; }

        }
        if (validated) {
             bankAccessor.Update($scope.bank.BankId, $scope.bank)
                .then(function (response) {

                    $location.path("#!/BankList");
                }, function (response) {

                    if (response.status == 409) {
                        $scope.Message = "Enter Different Bank name  to save";
                    }
                })
        }
    };
    $scope.Cancel = function () {
        $location.path("#!/BankList");
    };
});
app.controller('BankListController', function ($rootScope, $scope, bankAccessor) {
    $rootScope.loading = true;
    bankAccessor.GetAll()
        .then(function (response) {
            $scope.Banks = response.data;
            $rootScope.loading = false;
        }, function (response) {
            $scope.Message = response.Message;
            $rootScope.loading = false;
        });

    $scope.deleteAccountType = function ($event, id) {
        var ans = confirm('Are you sure to delete it?');
        if (ans) {
            bankAccessor.Delete(id)
            .then(function () {
                var element = $event.currentTarget;
                $(element).closest('div[class^="col-lg-12"]').hide();

                bankAccessor.GetAll()
           .then(function (response) {
               $scope.Message = null;
               $scope.Banks = response.data;
               $rootScope.loading = false;
           }, function (response) {
               $scope.Message = response.Message;
               $rootScope.loading = false;
           });

            }, function (response) {
                if (response.status == 500) {
                    $scope.Message = "Error Occured While Deleting Entry";
                }
                

            })
        }
    };
});