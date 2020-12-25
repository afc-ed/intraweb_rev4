
app.controller('Distribution/DropshipVendorController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = { Id: 0, Source: "", Destination: "" };
    Spinner($scope, 'off');
    
    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Source, $scope.input.Destination))
        {
            $scope.input.DropId = header.Id;
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/DropshipVendorSave', $scope.input)
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