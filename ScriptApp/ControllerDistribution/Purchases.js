
app.controller('Distribution/PurchasesController', function ($scope, appFactory ) {
    window.scope = $scope;

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = { type: "1", Location: "1" };
        ShowSearch($scope, 'on');
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/VendorDroplist')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.vendorlist = response.data[0];
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

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.startdate, $scope.input.enddate))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/PurchaseList', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.filelink = response.data[0];
                        ShowSearch($scope, 'off');
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

    $scope.setDefault();
   

});