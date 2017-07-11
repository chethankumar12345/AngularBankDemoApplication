app.factory("accountTypeAccessor", function ($http, AccountTypeApiUrl) {
    var tokenHeader = {
        'Authorization': "Bearer " + sessionStorage.getItem('accessToken'),
        'Accept': 'application/json;odata=verbose'
    };

    var GetAll = function () {
     
        return $http.get(AccountTypeApiUrl, { headers: tokenHeader});
    };
    var GetById = function (id) {
        return $http.get(AccountTypeApiUrl + '?' + id, { headers: tokenHeader });
    };
    var Save = function (account) {
        return $http.post(AccountTypeApiUrl, account, { headers: tokenHeader });
    }
    var Update = function (id,account) {
        return $http.put(AccountTypeApiUrl + '?Id=' + id, account, { headers: tokenHeader });
    }
    var Delete = function (id) {
        return $http.delete(AccountTypeApiUrl + '?id=' + id, { headers: tokenHeader });
    }
    var UniqueCheck = function (accountName) {
        return $http.get(AccountTypeApiUrl + '/CheckAccountTypeUniqueness?accountTypeName=' + accountName, { headers: tokenHeader });
    }
    
    return {
        GetAll: GetAll,
        GetById: GetById,
        Save: Save,
        Update: Update,
        Delete: Delete,
        UniqueCheck: UniqueCheck
    };
});