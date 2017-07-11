/// <reference path="angular.js" />

app.controller('CustomerCreateController', function ($scope, $http, customerAccessor, $location) {

    $scope.customer = { Name: "", MobileNumber: "", EmailId: "" };
    $scope.error = $scope.customer;
    $scope.error = { Name: true, MobileNumber: true, EmailId: true };
    $scope.Save = function () {
        var validated = true;
        $scope.error = $scope.customer;
  
        if ($scope.customer != null)
            {

                if (Object.keys($scope.error).length==3)
                {
                    validated = true;
                    angular.forEach($scope.error, function (value, key) {
                        if (value == "") {
                            validated = false;
                        }
                    })
                  
                }
                else
                {
                    validated = false;
                }
   
        }
        else
        {
            $scope.error = { Name: false, MobileNumber: false, EmailId: false };
             validated = false;
        }

        if ($scope.frmCustomer.MobileNumber.$error.pattern == true || $scope.frmCustomer.EmailId.$error.pattern == true)
            validated = false;

        if (validated) {
            customerAccessor.Save($scope.customer).then(function (response) {
                $location.path("#!/CustomerList");
            }, function (response) {

                if (response.status == 409) {
                    $scope.Message = "Enter Different Customer name  to save";
                }
            })
        }
    };
    $scope.Cancel = function () {
        $location.path("#!/CustomerList");
    };

});
app.controller('CustomerEditController', function ($scope, $http, customerAccessor, $routeParams, $location) {


    $scope.customer = { Name: "", MobileNumber: "", EmailId: "" };
    $scope.error = $scope.customer;
    $scope.error = { Name: true, MobileNumber: true, EmailId: true };

    customerAccessor.GetById($routeParams.id).then(function (response) {

        $scope.customer = response.data;

    });
    $scope.Save = function () {
        var validated = true;

        $scope.error = $scope.customer;
        if ($scope.customer != null) {

            if (Object.keys($scope.error).length == 3) {
                validated = true;
                angular.forEach($scope.error, function (value, key) {
                    if (value == "") {
                        validated = false;
                    }
                })


            }
            else {
                validated = false;
            }

        }
        else {
            $scope.error = { Name: false, MobileNumber: false, EmailId: false };
            validated = false;
        }

        if ($scope.frmCustomer.MobileNumber.$error.pattern == true || $scope.frmCustomer.EmailId.$error.pattern == true)
            validated = false;
     
        if (validated) {
            customerAccessor.Update($scope.customer.CustomerId, $scope.customer)
               .then(function (response) {

                   $location.path("#!/CustomerList");
               }, function (response) {

                   if (response.status == 409) {
                       $scope.Message = "Enter Different Customer name  to save";
                   }
               })
        }
    };
    $scope.Cancel = function () {
        $location.path("#!/CustomerList");
    };
});
app.controller('CustomerListController', function ($rootScope, $scope, customerAccessor) {
    $rootScope.loading = true;
    customerAccessor.GetAll()
        .then(function (response) {
            $scope.Customers = response.data;
            $rootScope.loading = false;
        }, function (response) {
            $scope.Message = response.Message;
            $rootScope.loading = false;
        });

    $scope.deleteAccountType = function ($event, id) {
        var ans = confirm('Are you sure to delete it?');
        if (ans) {
            customerAccessor.Delete(id)
            .then(function () {
                var element = $event.currentTarget;
                $(element).closest('div[class^="col-lg-12"]').hide();

                customerAccessor.GetAll()
           .then(function (response) {
               $scope.Message = null;
               $scope.Customers = response.data;
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