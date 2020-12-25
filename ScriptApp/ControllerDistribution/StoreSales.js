
app.controller('Distribution/StoreSalesController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.input = { Location: "1" };

    $scope.setDefault = function ()
    {
        ShowSearch($scope, 'on');
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/StoreSalesDroplist')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.storelist = response.data[0];
                }
            })
            .catch(function (reason)
            {
                alert('Error: ' + reason.status + ' - ' + reason.statusText);
            })
            .finally(function ()
            {
                Spinner($scope, 'off');
            });
    };

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.startdate, $scope.input.enddate))
        {
            // deserialize json and convert to string.
            if ($scope.input.store != undefined)
            { 
                var convertStore = angular.fromJson($scope.input.store).toString();
                $scope.input.store = convertStore;
            }
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/StoreSalesData', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.filelink = response.data[0];
                        ShowSearch($scope, 'off');
                    }
                })
                .catch(function (reason)
                {
                    alert('Error: ' + reason.status + ' - ' + reason.statusText);
                })
                .finally(function ()
                {
                    Spinner($scope, 'off');
                });
        }
    };

    $scope.refresh = function ()
    {
        ShowSearch($scope, 'on');
    };

    $scope.setDefault();
   

});