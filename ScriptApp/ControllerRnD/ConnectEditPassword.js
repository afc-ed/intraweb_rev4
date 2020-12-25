
app.controller('RnD/ConnectEditPasswordController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = header;
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Password, $scope.input.Webactive))
        {
            appFactory.postRequest('/RnD/ConnectSavePassword', $scope.input)
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