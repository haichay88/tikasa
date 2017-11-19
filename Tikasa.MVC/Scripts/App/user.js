app.service("UserService", function ($http) {
  

    this.Register = function (model) {
        var request = $http({
            method: "post",
            url: "/User/Register",
            data: model,
            contentType: "application/json; charset=UTF-8",
        });
        return request;
    };

    this.Login = function (model) {
        var request = $http({
            method: "post",
            url: "/User/Login",
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };

});

app.controller("UserController", function ($scope, UserService) {
    $scope.Message = undefined;
    $scope.Register = function () {
        if(!$scope.User) return;
        if (!$scope.User.Email || !$scope.User.UserName) {
           // toastr.error("Bạn chưaa nhập tên khách sạn !");
            return;
        }
        if (!$scope.User.Password) {
            //toastr.error("Bạn chưa nhập số phòng hoặc số tầng của khách sạn !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = UserService.Register($scope.User);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                window.location.href = '/Home';
                //toastr.success("Chúc mừng bạn đã đăng ký thành công! Bạn có thể đăng nhập để sử dụng ngay và luôn ! ");
                // $scope.GetHotels();
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });


    };

    $scope.Login = function () {
        if (!$scope.User) return;
        if (!$scope.User.UserName) {
            // toastr.error("Bạn chưaa nhập tên khách sạn !");
            return;
        }
        if (!$scope.User.Password) {
            //toastr.error("Bạn chưa nhập số phòng hoặc số tầng của khách sạn !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = UserService.Login($scope.User);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                window.location.href = '/Home';

            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });


    };

});