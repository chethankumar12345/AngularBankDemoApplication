app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/CustomerList', { templateUrl: '/BankView/Customer/List.html', controller: 'CustomerListController' })
        .when('/CustomerCreate', { templateUrl: '/BankView/Customer/Create.html', controller: 'CustomerCreateController' })
        .when('/CustomerEdit/:id', { templateUrl: '/BankView/Customer/Edit.html', controller: 'CustomerEditController' })
        .otherwise({ redirectTo: '/CustomerList', controller: "CustomerListController" });
    }]);
app.constant("CustomerApiUrl", "http://localhost:53853/api/CustomersData");

