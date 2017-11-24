app.service("WebsiteService", function ($http) {
  


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

app.controller("WebsiteController", function ($scope, WebsiteService) {
    $scope.Message = undefined;
    $scope.Sell = {
        IsWebsite: true,
        
    }
    

    $scope.ChangeVerifycationMethod = function () {
        if ($scope.Verifycation.Method != "html")
            $scope.Verifycation.IsHtml = false;
        else {
            $scope.Verifycation.IsHtml = true;
        }
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