
app.controller('Distribution/DropshipNewController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = { Id: 0, Description: "", RateType: "", Rate: 0, Batch: "", CompanyId: ""};
    
    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/DropshipCompany')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.companyList = response.data[0];
                    $scope.copyfromList = response.data[1];
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
        if (RequiredFields($scope.input.Rate, $scope.input.RateType, $scope.input.Description, $scope.input.CompanyId))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/DropshipSave', $scope.input)
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