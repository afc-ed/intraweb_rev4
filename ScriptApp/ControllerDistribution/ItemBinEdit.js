
app.controller('Distribution/ItemBinEditController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    $scope.input = header;
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Item, $scope.input.Location, $scope.input.BinCap))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/ItemBinSave', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $modalInstance.close();
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

    $scope.delete = function ()
    {
        if (ConfirmDelete())
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/ItemBinDelete', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $modalInstance.close();
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

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };

 
});