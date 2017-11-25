app.service("WebsiteService", function ($http) {
  


    this.GetWebsite = function (val) {
        var request = $http({
            method: "GET",
            url: "/Website/GetWebsite/" + val,
            contentType: "application/json; charset=UTF-8",
           
        });
        return request;
    };
    this.GetCategories = function () {
        var request = $http({
            method: "GET",
            url: "/Website/GetCategories",
            contentType: "application/json; charset=UTF-8",

        });
        return request;
    };
    this.GetTypeOfWebsite = function () {
        var request = $http({
            method: "GET",
            url: "/Website/GetTypeOfWebsite",
            contentType: "application/json; charset=UTF-8",

        });
        return request;
    };
});

app.controller("WebsiteController", function ($scope, WebsiteService) {
    $scope.Message = undefined;
    $scope.Sell = {
        IsWebsite: true,
        
    }
    $scope.InitMonth = function () {
        $scope.Months = [];
        for (var i = 1; i <= 12; i++) {
            $scope.Months.push({ Key: i, Value: "Tháng " + i });
        }
    };

    $scope.InitYear = function () {
        $scope.Years = [];
        var today = new Date();
        var thisYear = today.getFullYear();
        for (var i = 10; i >= 0; i--) {
            var data = thisYear - i;
            $scope.Years.push({ Key: data, Value:data});
        }
    };
   
    $scope.ChangeVerifycationMethod = function () {
        if ($scope.Verifycation.Method != "html")
            $scope.Verifycation.IsHtml = false;
        else {
            $scope.Verifycation.IsHtml = true;
        }
    };

    $scope.GetWebsite = function () {
        var webisteId = $("#WebsiteId").val();
        if (!webisteId) return;
        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetWebsite(webisteId);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Website = pl.data.Data;
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.GetCategories = function () {
        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetCategories();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Categories = pl.data.Data;
            }
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };
    $scope.GetTypeOfWebsite = function () {
        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetTypeOfWebsite();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.TypeOfWebsites = pl.data.Data;
            }
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };
    $scope.changeTypeOfWebsite = function () {
        var data = $.grep($scope.TypeOfWebsites, function (n) {
            return n.Id == $scope.Website.TypeOfWebsiteId;
        });
        if (data.length > 0)
            $scope.SubTypeOfWebsites = data[0].Childs;
        else
            $scope.SubTypeOfWebsites = [];
    };
    $scope.changeTypeOfCatefgory = function () {
        var data = $.grep($scope.Categories, function (n) {
            return n.Id == $scope.Website.TypeOfCategoryId;
        });
        if (data.length > 0)
            $scope.SubCategories = data[0].Childs;
        else
            $scope.SubCategories = [];
    };

});