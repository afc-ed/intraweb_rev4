
app.controller('Distribution/ItemBinNewController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    // initialize form inputs.    
    $scope.input = { Id: 0, Item: "", Location: "", BinCap: "", Secondary: "" };
    // turn off spinner
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

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

   
   

});