/// <reference path="angular.js" />

app.controller('AccountTypeCreateController', function ($scope, $http, accountTypeAccessor, $location) {

    $http.get('http://localhost:53853/api/BanksData').then(function (response) {
        $scope.Banks = response.data;
    });
    $scope.Save = function () {
        var validated = true;
        $scope.AccountTypeRequired = false;
        $scope.BankRequired = false;

        if ($scope.account == undefined) {

            $scope.AccountTypeRequired = true;
            $scope.BankRequired = true;
            validated = false;
        }
        else {
            if ($scope.account.AccountType == undefined || $scope.account.AccountType == "")
            { $scope.AccountTypeRequired = true; validated = false; }
            if ($scope.account.BankId == undefined || $scope.account.BankId == "")
            { $scope.BankRequired = true; validated = false; }
           
        }
        if (validated)
            {
        accountTypeAccessor.Save($scope.account).then(function (response) {
                $location.path("#!/AccountTypeList");
        })
        }
    };
    $scope.Cancel = function () {
        $location.path("#!/AccountTypeList");
    };
   
});
app.controller('AccountTypeEditController', function ($scope,$http, accountTypeAccessor, $routeParams, $location) {
    


    $http.get('http://localhost:53853/api/BanksData').then(function (response) {
        $scope.Banks = response.data;
    });
    accountTypeAccessor.GetById($routeParams.id).then(function (response) {
        
        $scope.account = response.data;
    });
   
    $scope.Save = function () {
        var validated = true;
        $scope.AccountTypeRequired = false;
        $scope.BankRequired = false;

        if ($scope.account == undefined) {

            $scope.AccountTypeRequired = true;
            $scope.BankRequired = true;
            validated = false;
        }
        else {
            if ($scope.account.AccountType == undefined || $scope.account.AccountType == "")
            { $scope.AccountTypeRequired = true; validated = false; }
            if ($scope.account.BankId == undefined || $scope.account.BankId == "")
            { $scope.BankRequired = true; validated = false; }
            
        }
        if (validated) {
            accountTypeAccessor.Update($scope.account.AccountTypeId, $scope.account)
                .then(function (response) {

                    $location.path("#!/AccountTypeList");
                })
        }
        };
    
    $scope.Cancel = function () {
        $location.path("#!/AccountTypeList");
    };

});
app.controller('AccountTypeListController', function ($rootScope, $scope, accountTypeAccessor) {
    $rootScope.loading = true;
    accountTypeAccessor.GetAll()
        .then(function (response) {
        $scope.AccountTypes = response.data;
        $rootScope.loading = false;
    }, function (response) {
        $scope.Message = response.Message;
        $rootScope.loading = false;
    });

    $scope.deleteAccountType = function ($event, id) {
        var ans = confirm('Are you sure to delete it?');
        if (ans) {
            accountTypeAccessor.Delete(id)
            .then(function () {
                var element = $event.currentTarget;
                $(element).closest('div[class^="col-lg-12"]').hide();

           accountTypeAccessor.GetAll()
      .then(function (response) {
                  $scope.Message = null;
                  $scope.AccountTypes = response.data;
                  $rootScope.loading = false;
      }, function (response) {
                  $scope.Message = response.Message;
                  $rootScope.loading = false;
      });

       }, function (response) {
         if (response.status == 500)
         {
                    $scope.Message = "Error Occured While Deleting Entry";
          }
         if (response.status == 409) {
             $scope.Message = "Enter Different Account Type  to save";
         }
            })
        }
    };
});