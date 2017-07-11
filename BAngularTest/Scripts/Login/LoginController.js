

app.controller('logincontroller', function ($scope, loginservice) {

    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "";
    $scope.userRegistrationPassword = "";
    $scope.userRegistrationConfirmPassword = "";

    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here

    //Function to register user
    $scope.registerUser = function () {

        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.userRegisterEmail,
            Password: $scope.userRegisterPassword,
            ConfirmPassword: $scope.userRegisterConfirmPassword
        };
        console.log(userRegistrationInfo);
        var promiseregister = loginservice.register(userRegistrationInfo);

        promiseregister.then(function (resp) {
        
            $scope.divSuccess = true;
        }, function (err) {
            console.log(err.data.Message);
            $scope.divErrorText = err.data.Message;
            $scope.divError = true;
        });
    };
    $.CloseSuccessPopUp=function() {
        $scope.divSuccess = false;
    }

    $scope.redirect = function () {
        window.location.href = '/BankApplication/Index';
    };

    //Function to Login. This will generate Token 
    $scope.login = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = loginservice.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.userName = resp.data.userName;
            console.log('Toekn ' + resp.data.access_token);
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/BankApplication/Index';
        }, function (err) {

            $scope.responseData = "Error " + err.status;
        });

    };
    $scope.googleLogin = function() {
        window.location.href = '/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A53853%2FBankApplication%2FRegister&state=1AKFI1JeEVys4-8ohlVEY9tXmBZskjLxJmdOK0m4edY1';
    };
});