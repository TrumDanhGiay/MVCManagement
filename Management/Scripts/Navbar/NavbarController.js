app.controller('NavbarController',['$scope', '$state', 'BaseServices', function ($scope, $state, BaseServices) {

    $scope.logout = function () {
        BaseServices.logout('api/Account/Logout', sessionStorage['token'])
            .then(function (response) {
                sessionStorage.clear();
                $state.go('login');
            })
            .catch(function (error) {
                console.log(error);
            });
    };
}]);