
app.controller('Distribution/ExternalDistributionCenterBatchDetailController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.output = {};
    
    $scope.setDefault = function ()
    {
        
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/ExternalDistributionCenterBatchDetailData', header)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.output = response.data[0];
                    $scope.filelink = response.data[1];
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

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

    $scope.setDefault();
   

});