
app.controller('Distribution/TunaShipController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.result = {};
    $scope.report = {};

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
                        $scope.result.output = response.data[0];
                        $scope.isCreateList = true;
                        $scope.isFileLink = false;
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
            var qtyInput = prompt("Enter Tuna ship quantity for " + item.Storecode, "");
            if (qtyInput != null && qtyInput != "")
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
            //Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/TunaShipQtyUpdate', item)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        // refresh page. - we don't want page to reload, user wants page position static, so update values only.
                        //$scope.setDefault();
                        item.Qty = qtyInput;
                        item.ModifiedOn = new Date().toLocaleDateString();
                    }
                })
                .catch(function (reason) {
                    alert('Error: ' + reason.status + ' - ' + reason.statusText);
                });
                //.finally(function ()
                //{
                //    Spinner($scope, 'off');
                //});
        }
        catch (e) {
            ErrorMsg(e, 'File = TunaShip.js | Function = updateQty()');
        }
    };

    $scope.createList = function ()
    {
        try
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/TunaShipDownload')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.report.output = response.data[0];
                        $scope.isCreateList = false;
                        $scope.isFileLink = true;
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
            ErrorMsg(e, 'File = TunaShip.js | Function = createList()');
        }
    };

    $scope.setDefault();

   

});