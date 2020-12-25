
app.controller('Distribution/SalesController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.sales = {};
    $scope.input = { Location: "1" };

    $scope.setDefault = function ()
    {
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.startdate, $scope.input.enddate))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/SalesData', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.sales =
                            {
                                output: response.data[0][0],
                                total: response.data[0][1],
                                filelink: response.data[1]                                
                            };
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
        }
        catch (e)
        {
            ErrorMsg(e, 'File = Sales.js | Function = submit()');
        }
    };

    $scope.setDefault();
   

});