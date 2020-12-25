
app.controller('RnD/CustomerClassEditController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = header;
    
    $scope.save = function ()
    {
        appFactory.postRequest('/RnD/CustomerClassEditSave', $scope.input)
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
            });        
    };

    $scope.cancel = function ()
    {
        $modalInstance.close();
    };  

   
});