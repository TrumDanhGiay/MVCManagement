app.controller('BookController', ['$scope', '$filter', '$ocLazyLoad', 'BaseServices', function ($scope, $filter, $ocLazyLoad, BaseServices) {

    BaseServices.AuthencationKeyGet('/api/Book', sessionStorage['token']).then(function (response) {
        $scope.listbook = response.data;
        console.log(response);
    }).catch(function (error) {
        console.log(error);
    })


    $scope.reload = function () {
        BaseServices.AuthencationKeyGet('/api/Book', sessionStorage['token']).then(function (response) {
        $scope.listbook = response.data;
        console.log(response);
    }).catch(function (error) {
        console.log(error);
    })
    }
}]);
