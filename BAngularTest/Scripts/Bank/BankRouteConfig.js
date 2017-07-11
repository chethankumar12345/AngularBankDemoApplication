app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/BankList', { templateUrl: '/BankView/Bank/List.html', controller: 'BankListController' })
        .when('/BankCreate', { templateUrl: '/BankView/Bank/Create.html', controller: 'BankCreateController' })
        .when('/BankEdit/:id', { templateUrl: '/BankView/Bank/Edit.html', controller: 'BankEditController' })
        .otherwise({ redirectTo: '/BankList', controller: "BankListController" });
    }]);
app.constant("BankApiUrl", "http://localhost:53853/api/BanksData");

app.directive('requiredUniquebank', function ($http, bankAccessor) {
    return {
        require: 'ngModel',
        link: function (scope, element, attribute, ctrl) {
            scope.$watch(attribute.ngModel, function () {

                if (attribute.requiredUniquebank == undefined) {
                    ctrl.$setValidity('requiredUniquebank', true);

                }
                else {
                    bankAccessor.UniqueCheck(attribute.requiredUniquebank).then(function (res) {
                      
                        if (res.data) {

                            ctrl.$setValidity('requiredUniquebank', false);
                        } else {
                            ctrl.$setValidity('requiredUniquebank', true);
                        }

                    }, function (err) {
                        ctrl.$setValidity('requiredUniquebank', false);
                    });
                }
            });
        }
    }
});