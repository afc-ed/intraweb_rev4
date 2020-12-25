
app.controller('Distribution/ItemLevelController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.level = {};
    $scope.input = { Location: "1" };

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
            appFactory.postRequest('/Distribution/ItemLevelData', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.level =
                        {
                            output: response.data[0][0],
                            totalsales: response.data[0][1],
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
    };

    $scope.setDefault();
   

});