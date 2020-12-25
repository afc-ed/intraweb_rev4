
app.controller('Distribution/ExternalDistributionCenterBatchController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    //initialize
    $scope.header = {};
    $scope.input = { Location : "" };

    $scope.setDefault = function ()
    {
        Spinner($scope, 'off');
        ShowSearch($scope, "on"); 
        $scope.input = { Location: "" };
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.Location))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/ExternalDistributionCenterBatchRecords', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.output = response.data[0];
                            //$scope.filelink = response.data[1];
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
        }
        catch (e)
        {
            ErrorMsg(e, 'File = ExternalDistributionCenter.js | Function = submit()');
        }
    };

    $scope.details = function (list)
    {
        $scope.header = list;
        $scope.openModal('/Distribution/ExternalDistributionCenterBatchDetail', 'Distribution/ExternalDistributionCenterBatchDetailController', 'large');
    };

    $scope.openModal = function (url, controller, size)
    {
        var modalInstance = $modal.open(
        {
            templateUrl: url,
            controller: controller,
            backdrop: 'static',
            windowClass: size,
            resolve:
            {
                header: function ()
                {
                    return $scope.header;
                }
            }
        });        
    };

    $scope.setDefault();
   

});