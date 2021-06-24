
app.controller('Distribution/LanterReconcileController', function ($scope, appFactory)
{
    window.scope = $scope;

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = {};
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');       
    };

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.startdate, $scope.input.enddate))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/LanterReconcileData', $scope.input)
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

    $scope.setDefault();
   

});