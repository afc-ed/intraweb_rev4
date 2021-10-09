
app.controller('Distribution/TunaShipController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.result = {};
    

    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/TunaShipData', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.result =
                        {
                            output: response.data[0],
                            filelink: response.data[1]
                        };
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
            ErrorMsg(e, 'File = TunaShip.js | Function = setDefault()');
        }
    };

    $scope.updateQty = function (item)
    {
        try
        {
            var qtyInput = prompt("Enter Tuna ship quantity for " + item.Storecode, "0");
            if (qtyInput != null)
            {
                if (!CheckNumbers(qtyInput))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            item.QtyEntered = qtyInput;
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/TunaShipQtyUpdate', item)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        // refresh page.
                        $scope.setDefault();
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
        catch (e) {
            ErrorMsg(e, 'File = TunaShip.js | Function = updateQty()');
        }
    };


    $scope.setDefault();

   

});