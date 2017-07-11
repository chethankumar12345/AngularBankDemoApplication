app.factory("customerAccessor", function ($http, CustomerApiUrl) {
    var GetAll = function () {
        return $http.get(CustomerApiUrl);
    };
    var GetById = function (id) {
        return $http.get(CustomerApiUrl + '?' + id);
    };
    var Save = function (bank) {
        return $http.post(CustomerApiUrl, bank);
    }
    var Update = function (id, bank) {
        return $http.put(CustomerApiUrl + '?Id=' + id, bank);
    }
    var Delete = function (id) {
        return $http.delete(CustomerApiUrl + '?id=' + id);
    }


    return {
        GetAll: GetAll,
        GetById: GetById,
        Save: Save,
        Update: Update,
        Delete: Delete
    };
});