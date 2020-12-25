
app.controller('Distribution/IntransitBillOfLadingController', function ($scope, appFactory)
{
    window.scope = $scope;
    $scope.input = {};

    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/IntransitBillOfLadingIds')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.output= response.data[0];
                    ShowSearch($scope, "on");                    
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

    $scope.submit = function (list)
    {        
        $scope.input = list;
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/InTransitBillofLadingData', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {                        
                    $scope.filelink = response.data[0];
                    ShowSearch($scope, "off");
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

    $scope.setDefault();
   

});