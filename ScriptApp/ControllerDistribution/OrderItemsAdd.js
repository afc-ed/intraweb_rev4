
app.controller('Distribution/OrderItemsAddController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    //initialize
    $scope.header = header;
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Distribution/PriceListData')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.output = response.data[0]; 
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
            ErrorMsg(e, 'File = OrderItemsAdd.js | Function = setDefault()');
        }
    };

    $scope.save = function (item)
    {
        try
        {
            item.OrderNumber = header.Number;
            item.Location = header.Location;
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
                appFactory.postRequest('/Distribution/OrderItemsAddUpdate', item)
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
            else
            {
                alert("Quantity cannot be 0.");
            }
        }
        catch (e)
        {
            ErrorMsg(e, 'File = OrderItemsAdd.js | Function = save()');
        }
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    }; 

    $scope.setDefault();

   

});