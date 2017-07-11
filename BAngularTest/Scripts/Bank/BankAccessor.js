app.factory("bankAccessor", function ($http, BankApiUrl) {
    var GetAll = function () {
        return $http.get(BankApiUrl);
    };
    var GetById = function (id) {
        return $http.get(BankApiUrl + '?' + id);
    };
    var Save = function (bank) {
        return $http.post(BankApiUrl, bank);
    }
    var Update = function (id, bank) {
        return $http.put(BankApiUrl + '?Id=' + id, bank);
    }
    var Delete = function (id) {
        return $http.delete(BankApiUrl + '?id=' + id);
    }
    var UniqueCheck = function (bankName) {
        return $http.get(BankApiUrl + '/CheckBankUniqueness?bankName=' + bankName);
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