
app.controller('Ecommerce/MaintenanceController', function ($scope, appFactory ) {
    window.scope = $scope;
    $scope.input = {};

    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Ecommerce/MaintenanceMenu')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.menuoutput = response.data[0];
                    ShowSearch($scope, "on");
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
        try
        {
            // get orders that are checked, save to array then convert to string.
            var itemsChecked = [];
            angular.forEach($scope.menuoutput, function (list)
            {
                if (!!list.checked)
                {
                    itemsChecked.push(list.Id);
                }
            })
            // string of order numbers passed in with object.
            $scope.input.item = itemsChecked.toString();
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Ecommerce/MaintenanceRun', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.message = response.data[0];
                        ShowSearch($scope, "off");
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
        catch (e)
        {
            ErrorMsg(e, 'File = Maintenance.js | Function = setDefault()');
        }
    };

    $scope.setDefault();

   

});