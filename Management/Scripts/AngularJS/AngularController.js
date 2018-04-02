

app.controller('LoginController', ['$scope', '$state', 'BaseServices', function ($scope, $state, BaseServices) {

    $scope.login = function () {
        var acc = "grant_type=password&username=" + $scope.username + "&password=" + $scope.password;
        var isAdmin = 1;
        BaseServices.check('/Token', acc).then(function (response) {
            sessionStorage['token'] = response.data.access_token;
            var roles = JSON.parse(response.data.roles);
            for (var i = 0; i <= roles.length; i++) {
                if (roles[i] === "Reader") {
                    isAdmin = 0;
                };
            };
            if (isAdmin === 1) {
                $state.go("layoutAdmin.dashboard");
            }
            else {
                $state.go("user.homepage");

            };
        }).catch(function (error) {
            var errorlogin = [];
            errorlogin.push("The username or password is incorrect.");
            $scope.loginerror = errorlogin;
        });
    };

    $scope.register = function () {
        var info = "username=" + $scope.username + "&email="
            + $scope.email + "&password=" + $scope.password + "&confirmpassword=" + $scope.confirmpassword
            + "&fullname= " + $scope.firstname + " " + $scope.secondname;
        BaseServices.register('/api/Account/Register', info).then(function (response) {
            $scope.errors = "Đăng kí thành công";
            $state.go("login");
        }).catch(function (error) {
            $scope.errors = parseErrors(error);
            console.log($scope.errors);
        });
    };

    $scope.change = function () {
        $scope.loginerror = {};
        $scope.errors = {};
    };

}]);

function parseErrors(response) {
    var errors = [];
    for (var key in response.data.ModelState) {
        for (var i = 0; i < response.data.ModelState[key].length; i++) {
            errors.push(response.data.ModelState[key][i]);
        }
    }
    return errors;
}