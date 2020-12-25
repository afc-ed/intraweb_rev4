
app.controller('Distribution/InventoryQuantityController', function ($scope, appFactory)
{
    window.scope = $scope;
    //initialize
    $scope.input = { Location: "0" };
    $scope.result = {};
    Spinner($scope, 'off');

    $scope.getData = function (isRemoveZero = 'false')
    {
        $scope.input.RemoveZeroAmount = isRemoveZero;
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/InventoryQuantityData', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.result = {
                        output: response.data[0],
                        filelink: response.data[1]
                    };
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

   
   

});