
app.controller('Ecommerce/ItemStatusEditController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    $scope.input = header;
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Code, $scope.input.Description))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Ecommerce/ItemStatusSave', $scope.input)
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
        $modalInstance.close();
    };

 
});