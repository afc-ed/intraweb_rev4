
app.controller('Distribution/WarehouseInvoiceReconcileController', function ($scope, appFactory)
{
    window.scope = $scope;

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = {};
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');       
    };

    $scope.submit = function ()
    {
        if (RequiredFields($scope.input.startdate, $scope.input.enddate, $scope.input.Location))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/WarehouseInvoiceReconcileData', $scope.input)
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