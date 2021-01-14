
app.controller('Ecommerce/ItemStatusNewController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    $scope.input = {};
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Code, $scope.input.Description, $scope.input.IsActive))
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