app.controller("DashboardController", ['$scope', 'BaseServices', function ($scope, BaseServices) {

    $scope.location = "Đang xác định ...";
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $scope.lat = position.coords.latitude;
            $scope.lng = position.coords.longitude;
        });
    }
    var interval = setInterval(function () {
        if ($scope.lat != null) {
            BaseServices.getCityName("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + $scope.lat + "," + $scope.lng + "&sensor=true").then(function (response) {
                var location = response.data.results[0].formatted_address;
                $scope.location = location;
                clearInterval(interval);
            }).catch(function (error) {
                $scope.location = "Không xác định";
            })
        }
    }, 1000);


}]);