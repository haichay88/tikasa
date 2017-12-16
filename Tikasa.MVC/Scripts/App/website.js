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

    $scope.GA = {
        Visits: 0,
        Pageviews:0
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

   
    function authorize(event) {
        // Handles the authorization flow.
        // `immediate` should be false when invoked from the button click.
        var useImmdiate = event ? false : true;
        var authData = {
            client_id: CLIENT_ID,
            scope: SCOPES,
            immediate: useImmdiate
        };
       
        debugger
        gapi.auth.authorize(authData, function (response) {
            //var authButton = document.getElementById('auth-button');
            if (response.error) {
                // authButton.hidden = false;
            }
            else {
                queryAccounts();
            }
        });
        //queryCoreReportingApi($scope.Website.GAAccountId);
    };
   
    function queryAccounts() {
        // Load the Google Analytics client library.
        gapi.client.load('analytics', 'v3').then(function () {

            // Get a list of all Google Analytics accounts for this user
            gapi.client.analytics.management.accounts.list().then(handleAccounts);
        });
    };
    function handleAccounts(response) {
        // Handles the response from the accounts list method.
        if (response.result.items && response.result.items.length) {
            // Get the first Google Analytics account.
            var firstAccountId = response.result.items[0].id;

            // Query for properties.
            queryProfiles($scope.Website.GAAccountId, $scope.Website.GAPropertyId);
        } else {
            console.log('No accounts found for this user.');
        }
    };
    function queryProfiles(accountId, propertyId) {
        // Get a list of all Views (Profiles) for the first property
        // of the first Account.

        gapi.client.analytics.management.profiles.list({
            'accountId': accountId,
            'webPropertyId': propertyId
        })
            .then(handleProfiles)
            .then(null, function (err) {
                // Log any errors.
                console.log(err);
            });
    };

    function handleProfiles(response) {
        // Handles the response from the profiles list method.
        if (response.result.items && response.result.items.length) {
            // Get the first View (Profile) ID.
            var firstProfileId = response.result.items[0].id;

            // Query the Core Reporting API.
            queryCoreReportingApi(firstProfileId);
        } else {
            console.log('No views (profiles) found for this user.');
        }
    };

    function queryCoreReportingApi(profileId) {
        debugger
        // Query the Core Reporting API for the number sessions for
        // the past seven days.
     //var pageviews=   gapi.client.analytics.data.ga.get({
     //       'ids': 'ga:' + profileId,
     //       'start-date': '30daysAgo',
     //       'end-date': 'today',
     //       'metrics': 'ga:users,ga:pageviews'
     //   })
     //       .then(function (response) {
                
     //           $scope.GA.Visits = response.result.rows[0][0];
                   
     //           $scope.GA.Pageviews = response.result.rows[0][1];
     //           $scope.$apply();
     //       })
     //       .then(null, function (err) {
     //           // Log any errors.
     //           console.log(err);
     //       });

        var pageviews = gapi.client.analytics.data.ga.get({
            'ids': 'ga:' + profileId,
            'start-date': '30daysAgo',
            'end-date': 'today',
            'metrics': 'ga:users,ga:pageviews'
        });

        var datachart = gapi.client.analytics.data.ga.get({
            'ids': 'ga:' + profileId,
            'start-date': '180daysAgo',
            'end-date': 'today',
            'dimensions': 'ga:month',
            'metrics': 'ga:users,ga:pageviews'
        });

        Promise.all([pageviews, datachart])
            .then(function (response) {
                debugger
                //var data1 = response[0].result.rows.map(function (row) { return +row[2]; });
                //var data2 = response[1].result.rows.map(function (row) { return +row[2]; });
                //var labels = response[1].result.rows.map(function (row) { return +row[0]; });
                var ctx = $("#gachartline");
                var chart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
                        datasets: [{
                            label: '# of Votes',
                            data: [12, 19, 3, 5, 2, 3],
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(153, 102, 255, 0.2)',
                                'rgba(255, 159, 64, 0.2)'
                            ],
                            borderColor: [
                                'rgba(255,99,132,1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });
              
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