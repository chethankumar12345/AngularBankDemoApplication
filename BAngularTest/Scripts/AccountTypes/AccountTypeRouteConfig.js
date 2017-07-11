app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/AccountTypeList', { templateUrl: '/BankView/AccountType/List.html', controller: 'AccountTypeListController' })
        .when('/AccountTypeCreate', { templateUrl: '/BankView/AccountType/Create.html', controller: 'AccountTypeCreateController' })
        .when('/AccountTypeEdit/:id', { templateUrl: '/BankView/AccountType/Edit.html', controller: 'AccountTypeEditController' })
        .otherwise({ redirectTo: '/AccountTypeList', controller: "AccountTypeListController" });
    }]);




app.constant("AccountTypeApiUrl", "http://localhost:53853/api/AccountTypesData");

app.directive('requiredUniqueaccounttype', function ($http, accountTypeAccessor) {
    return {
        require: 'ngModel',
        link: function (scope, element, attribute, ctrl) {
            scope.$watch(attribute.ngModel, function () {

                if (attribute.requiredUniqueaccounttype == undefined || attribute.requiredUniqueaccounttype == "")
                {
                    ctrl.$setValidity('requiredUniqueaccounttype', true);

                }
                else
                    {
                        accountTypeAccessor.UniqueCheck(attribute.requiredUniqueaccounttype).then(function (res) {
                                if (res.data) {
                                 
                                    ctrl.$setValidity('requiredUniqueaccounttype', false);
                                } else {
                                    ctrl.$setValidity('requiredUniqueaccounttype', true);
                                }
                    
                        },function (err) {
                            ctrl.$setValidity('requiredUniqueaccounttype', false);
                        });
                }
            });
        }
    }
});
