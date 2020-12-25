
app.controller('Distribution/DropLabelNewController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    // initialize form inputs.    
    $scope.input = { Id: 0, City: "", State: "" };
    // turn off spinner
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.City, $scope.input.State))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/DropLabelSave', $scope.input)
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