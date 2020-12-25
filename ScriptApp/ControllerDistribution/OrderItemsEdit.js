
app.controller('Distribution/OrderItemsEditController', function ($scope, appFactory, $modalInstance, header, item)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.header = header;
    $scope.item = item;
    

    // retrieve lot list.
    $scope.setDefault = function ()
    {
        try
        {
            item.OrderNumber = header.Number;
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/OrderItemsLotList', item)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        if (!IsEmpty(response.data[1]))
                        {
                            $scope.item.ExtendedQty = item.UOMQty * item.Sold;
                            $scope.item.LotsSelected = response.data[0][0];
                            $scope.item.Difference = $scope.item.ExtendedQty - $scope.item.LotsSelected;
                            $scope.assignedLot = response.data[0][1];
                            $scope.availableLot = response.data[1];
                            $scope.isLot = true;
                        }
                        else
                        {
                            $scope.isLot = false;
                        }
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
            ErrorMsg(e, 'File=  OrderItemsEdit.js | Function= setDefault()');
        } 
    };

    $scope.addLot = function (list)
    {
        try
        {   
            var qtyInput = prompt("Enter quantity for Lot Number: " + list.Id, "0");
            if (qtyInput != null) {
                // check for numeric value.
                if (!CheckNumbers(qtyInput)) {
                    return false;
                }
                // check for zero.
                if (qtyInput <= 0) {
                    alert("Quantity must be greater than 0.");
                }
            }
            else {
                return false;
            }
            // quantity entered cannot exceed onhand.
            if (qtyInput > list.Onhand) {
                alert("The quantity entered exceeds the Onhand amount for the selected Lot.");
                return false;
            }
            // quantity entered cannot exceed lots needed.
            if (qtyInput > $scope.item.Difference) {
                alert("Quantity entered cannot exceed Lots Needed.");
                return false;
            }
            // make sure quantity entered <= than lots needed and selected lot onhand amount.
            if (qtyInput <= $scope.item.Difference && qtyInput <= list.Onhand) {
                item.OrderNumber = header.Number;
                item.Lot = list.Id;
                item.LotQty = qtyInput;
                item.LotDateReceived = list.DateReceived;
                item.LotDateSequence = list.DateSequence;
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/OrderItemsLotAdd', item)
                    .then(function (response) {
                        if (!appFactory.errorCheck(response)) {
                            $scope.setDefault();

                        }
                    })
                    .catch(function (reason) {
                        alert('Error: ' + reason.status + ' - ' + reason.statusText);
                    })
                    .finally(function () {
                        Spinner($scope, 'off');
                    });
            }
            else {
                alert("Error on Lot update.  Please contact support.");
            }
        }
        catch (e)
        {
            ErrorMsg(e, 'File=  OrderItemsEdit.js | Function= addLot()');
        } 
    };

    $scope.deleteLot = function (list)
    {
        try
        {
            if (ConfirmDelete())
            {
                item.OrderNumber = header.Number;
                item.Lot = list.Id;
                item.LotQty = list.Qty;
                item.LotDateReceived = list.DateReceived;
                item.LotDateSequence = list.DateSequence;
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/OrderItemsLotDelete', item)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
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
        }
        catch (e) {
            ErrorMsg(e, 'File=  OrderItemsEdit.js | Function= deleteLot()');
        }
    };

    $scope.changeQuantity = function ()
    {
        try
        {
            item.OrderNumber = header.Number;
            var qtyInput = prompt("Enter quantity for " + item.Number + " : " + item.Description, "0");
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
            if (qtyInput > 0)
            {
                item.QtyEntered = qtyInput;
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/OrderItemsChangeQuantity', item)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.setDefault();
                            item.Sold = item.QtyEntered;
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
            else
            {
                alert("Quantity cannot be 0.");
            }
        }
        catch (e)
        {
            ErrorMsg(e, 'File=  OrderItemsEdit.js | Function= changeQuantity()');
        }
    };


    $scope.delete = function ()
    {
        try
        {
            if (ConfirmDelete())
            {
                item.OrderNumber = header.Number;
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/OrderItemsDelete', item)
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
        }
        catch (e)
        {
            ErrorMsg(e, 'File=  OrderItemsEdit.js | Function= delete()');
        } 
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

    $scope.setDefault();
   

});