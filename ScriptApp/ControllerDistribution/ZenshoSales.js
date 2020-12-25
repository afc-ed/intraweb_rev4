
app.controller('Distribution/ZenshoSalesController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.sale = {};
    $scope.input = {};

    $scope.setDefault = function ()
    {
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');
    };

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.startdate, $scope.input.enddate))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/ZenshoSalesData', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.sale.filelink = response.data[0];
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