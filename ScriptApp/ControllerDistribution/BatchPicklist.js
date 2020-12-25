
app.controller('Distribution/BatchPicklistController', function ($scope, appFactory)
{
    window.scope = $scope;
    //initialize
    $scope.input = {};

    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/BatchPicklistIds', $scope.level)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.batchlist = response.data[0];
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

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.Batch, $scope.input.Type))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/BatchPicklistData', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {                        
                        $scope.filelink_batch = response.data[0];
                        $scope.filelink_pickticket = response.data[1];
                        $scope.filelink_tag_frozen = response.data[2];
                        $scope.filelink_tag_dry = response.data[3];
                        $scope.filelink_palletcount = response.data[4];                       
                        $scope.filelink = response.data[5];
                        $scope.filelink_billoflading = response.data[6];
                        //$scope.filelink_pickticket_export = response.data[7];
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
        }
    };

    $scope.refresh = function ()
    {
        ShowSearch($scope, 'on');
    };

    $scope.setDefault();
   

});