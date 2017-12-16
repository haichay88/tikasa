app.service("WebsiteService", function ($http) {

    this.GetWebsite = function (model) {
        var request = $http({
            method: "POST",
            url: "/Website/GetWebsite",
            contentType: "application/json; charset=UTF-8",
            data: model
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
    this.UpdateBaseInfo = function (model) {
        var request = $http({
            method: "POST",
            url: "/Website/UpdateBaseInfo",
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };
    this.UpdateMoreInfo = function (model) {
        var request = $http({
            method: "POST",
            url: "/Website/UpdateMoreInfo",
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };

    this.GetWebsites = function (model) {
        var request = $http({
            method: "POST",
            url: "/Website/GetWebsites",
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };

    this.PublicWebsite = function (model) {
        var request = $http({
            method: "POST",
            url: "/Website/PublicWebsite",
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };
});

app.controller("WebsiteController", function ($scope, WebsiteService) {
    $scope.Message = undefined;
    $scope.Sell = {
        IsWebsite: true,
        hasTraffic: true,
        hasRevenue: true
        
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
    var validationField = function () {
        if (!$scope.Website.TypeOfCategoryId) return false;
        if (!$scope.Website.TypeOfWebsiteId) return false;
        if (!$scope.Website.GoliveMonth) return false;
        if (!$scope.Website.GoliveYear) return false;
    };



    $scope.UpdateBaseInfo = function () {
      
        CommonUtils.showWait(true);
        $scope.Website.Step = 2;
        var promiseGet = WebsiteService.UpdateBaseInfo($scope.Website);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };
    $scope.UpdateMoreInfo = function () {

        CommonUtils.showWait(true);
        $scope.Website.Step =3;
        var promiseGet = WebsiteService.UpdateMoreInfo($scope.Website);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.PublicWebsite = function () {

        CommonUtils.showWait(true);
        $scope.Website.Step = 3;
        var promiseGet = WebsiteService.PublicWebsite($scope.Website);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
            }
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.GetWebsites = function () {

        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetWebsites($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Websites = pl.data.Data.Data;
            }
       
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

    $scope.GetWebsite = function () {
        var webisteId = $("#WebsiteId").val();
        if (!webisteId) return;
        var model = {
            Id:webisteId
        };
        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetWebsite(model);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Website = pl.data.Data;
                $scope.cssStep();
            }
            $scope.Message = pl.data.Message;
            CommonUtils.showWait(false);
        },
            function (errorPl) {

            });
    };

   
    $scope.ShowDetail = function () {
        var webisteId = $("#WebsiteId").val();
        if (!webisteId) return;
        var model = {
            Id: webisteId
        };
        CommonUtils.showWait(true);
        var promiseGet = WebsiteService.GetWebsite(model);
        promiseGet.then(function (pl) {
            CommonUtils.showWait(false);
            if (!pl.data.IsError) {
                $scope.Website = pl.data.Data;
                
            }
           
        },
            function (errorPl) {

            });
    };
    $scope.Traffics = {};
    $scope.Top5Countries = {};
    $scope.Top5Channels = {};
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
    $scope.changeReview = function (val) {
        if ($scope.Website.IsCertificated) {
            if (val==0)
                $scope.Website.Step = 1;
            else
                $scope.Website.Step = val;
        } else {
            $scope.Website.Step = 0;
        }   
        $scope.cssStep();
        
    };

    $scope.cssStep = function (val) {
        if (!$scope.Website) return;
        $scope.ClassStep = {
            Step0: undefined,
            Step1: undefined,
            Step2: undefined,
            Step3: undefined,
        }
        if ($scope.Website.Step == 0) {
            $scope.ClassStep.Step0 = "progression-bar__step progression-bar__step--next";
            $scope.ClassStep.Step1 = "progression-bar__step";
            $scope.ClassStep.Step2 = "progression-bar__step";
            $scope.ClassStep.Step3 = "progression-bar__step";
        }
        if ($scope.Website.Step == 1) {
            $scope.ClassStep.Step0 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step1 = "progression-bar__step progression-bar__step--next";
            $scope.ClassStep.Step2 = "progression-bar__step";
            $scope.ClassStep.Step3 = "progression-bar__step";
        }

        if ($scope.Website.Step == 2) {
            $scope.ClassStep.Step0 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step1 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step2 = "progression-bar__step progression-bar__step--next";
            $scope.ClassStep.Step3 = "progression-bar__step";
        }
        if ($scope.Website.Step == 3) {
            $scope.ClassStep.Step0 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step1 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step2 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
            $scope.ClassStep.Step3 = "progression-bar__step progression-bar__step--active progression-bar__step--complete";
        }


    };

});
app.directive('ckEditor', [function () {
    return {
        require: '?ngModel',
        link: function ($scope, elm, attr, ngModel) {

            var ck = CKEDITOR.replace(elm[0]);

            ck.on('pasteState', function () {
                $scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$modelValue);
            };
        }
    };
}]);