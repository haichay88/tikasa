app.service("HomeService", function ($http) {
  


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

app.controller("HomeController", function ($scope, HomeService) {
    $scope.Message = undefined;
    $scope.Sell = {
        IsWebsite: true,
        
    }
    $scope.LoginToContinue = function () {
        debugger
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
        var promiseGet = HomeService.Login($scope.User);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                if ($scope.Sell.IsWebsite)
                    window.location.href = '/Website/Create';
                else
                    window.location.href = '/Domain/Create';

            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });


    };

    $scope.ChangeVerifycationMethod = function () {
        debugger
       var a= $scope.Verifycation.Method;
    };

    $scope.Create = function () {
        debugger
        var userid = $("#userId").val();
        if (userid <= 0) {
            $("#loginBeforeModal").modal("show");
        } else {
            if ($scope.Sell.IsWebsite)
                window.location.href = '/Website/Create';
            else
                window.location.href = '/Domain/Create';
        }
        


    };

});