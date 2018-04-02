

app.factory('BaseServices', ['$http', function ($http) {

    //Login
    var dataFactory = {};
    dataFactory.check = function (url, acc) {
        return $http.post(url,
            acc,
            {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
    };

    //Dang ki
    dataFactory.register = function (url, info) {
        return $http.post(url,
            info,
            {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
    };
  

    //Call google to get location name
    dataFactory.getCityName = function (url) {
        return $http.get(url);
    };

    //Authorize Request Logout
    dataFactory.logout = function (url, key) {
        return $http.post(url, key, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + key
            }
        });
    };

    //Authorize Request post
    dataFactory.AuthencationKey = function (url, object, key) {
        return $http.post(url, object, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + key
            }
        });
    };

    //Authorize Request get
    dataFactory.AuthencationKeyGet = function (url, key) {
        return $http.get(url, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + key
            }
        });
    };

    return dataFactory;

}]);