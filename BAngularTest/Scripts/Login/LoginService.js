var app = angular.module('MyApp', []);
app.service('loginservice', function ($http) {

    this.register = function (userInfo) {
        console.log(userInfo);
        var resp = $http({
            url: "/api/Account/Register",
            method: "POST",
            data: userInfo
        });
        return resp;
    };

    this.login = function (userlogin) {

        var resp = $http({
            url: "/TOKEN",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return resp;
    };

    this.isUserRegistered = function (accessToken) {

        var resp = $http({
            url: "/api/Account/UserInfo",
            method: "GET",
            headers: {
                'Content-Type': 'application/JSON',
                'Authorization': 'Bearer ' + accessToken
            }
        }).then(
            function(response) {
                if (response.HasRegistered) {
                    console.log('existing uer');
                    sessionStorage.setItem('userName', response.Email);
                    sessionStorage.setItem('accessToken', accessToken);
                    window.location.href = '/BankApplication/Index';
                } else {
                    console.log('external uer');
                    signUpExternalUser(accessToken);
                }
            }
            );
        return resp;
    };
         function signUpExternalUser(accessToken) {

        var resp = $http({
            url: "/api/Account/RegisterExternal",
            method: "POST",
            headers: {
                'Accept': 'application/json;odata=verbose',
                'Authorization': 'Bearer ' + accessToken
            }
        }).then(
            function () {
                window.location.href = '/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A53853%2FBankApplication%2FRegister&state=OGV9nfOXlXI4fQBvArWZZbKiU9veyAXJf0th-vy5NGk1';

            }
        );
        return resp;
    };
});