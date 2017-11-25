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
    this.WebsiteCreate = function (model) {
        var request = $http({
            method: "post",
            url: "/Website/Create",
            data: model,
            contentType: "application/json; charset=UTF-8",
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
        if (!$scope.User) return;
        if (!$scope.User.UserName) {
            return;
        }
        if (!$scope.User.Password) {
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = HomeService.Login($scope.User);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                debugger
                $scope.WebsiteCreate();
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.WebsiteCreate = function () {
        if (!$scope.Sell.Domain) return;
        $scope.Website = {
            Name: $scope.Sell.Domain
        };
      
        CommonUtils.showWait(true);
        var promiseGet = HomeService.WebsiteCreate($scope.Website);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                window.location.href = '/Website/Detail/' + pl.data.Data;
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.ChangeTypeSellMethod = function (val) {
        if (val == 'website')
            $scope.Sell.IsWebsite = true;
        else
            $scope.Sell.IsWebsite = false;
    };

    $scope.Create = function () {
        var userid = $("#userId").val();
        if (userid <= 0) {
            $("#loginBeforeModal").modal("show");
            return;
        } else {
            $scope.WebsiteCreate();
        }
        


    };

});