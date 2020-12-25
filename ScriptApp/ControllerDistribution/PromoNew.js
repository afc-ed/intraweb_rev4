
app.controller('Distribution/PromoNewController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = { id: 0, Storeprefix: "", State: "", IsActive: "1" };
    Spinner($scope, 'off');

    $scope.setDefault = function ()
    {
        
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/StateList')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.statelist = response.data[0];
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

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Startdate, $scope.input.Enddate, $scope.input.Description))
        {
            appFactory.postRequest('/Distribution/PromoSave', $scope.input)
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

    $scope.setDefault();
   

});