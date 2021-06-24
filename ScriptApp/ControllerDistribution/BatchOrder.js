
app.controller('Distribution/BatchOrderController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    // returns list of batch ids, for user selection.
    $scope.setDefault = function ()
    {
        $scope.input = {batch: "", order: "", newbatch: "", selectedbatch: ""};
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/BatchOrderIds')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.batchlist = response.data[0];
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
    // returns order data based on batch selected.
    $scope.getBatch = function (list)
    {
        // this is done for passing in form input to controller.
        $scope.input.batch = list.Id;
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/BatchOrderData', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.batchoutput = response.data[0];
                    $scope.filelink_order = response.data[1];
                    $scope.filelink_pickticket_lanter = response.data[2];
                    $scope.filelink_pickticket = response.data[3];
                    $scope.filelink_pickticket_phoenix = response.data[4];
                    // show the pickticket lanter button only for lanter batches.
                    $scope.isLanterBatch = (/lanter/i.test($scope.input.batch)) ? true : false;
                    // show the pickticket phoenix button only for phoenix batches.
                    $scope.isPhoenixBatch = (/phoenix/i.test($scope.input.batch)) ? true : false;
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
    };
    // move selected orders to new/existing batch.
    $scope.submit = function ()
    {
        var ret = {Id: ""};
        // get orders that are selected, save to array.
        var ordersChecked = [];
        angular.forEach($scope.batchoutput, function (list)
        {
            if (list.checked)
            {
                ordersChecked.push(list.Number);
            }
        })
        // convert array of order numbers to string because it will be passed in with JSON object.
        $scope.input.order = ordersChecked.toString();
        // check if either batch was entered or selected.
        if (IsEmpty($scope.input.newbatch) && IsEmpty($scope.input.selectedbatch))
        {
            alert("Neither New or Selected Batch ID was found.");
            return false;
        }
        if (!IsEmpty($scope.input.order))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/BatchOrderUpdate', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        alert(response.data[0]);                        
                        // check for batch id.
                        if (!IsEmpty($scope.input.newbatch))
                        {
                            ret.Id = $scope.input.newbatch;
                        }
                        else if (!IsEmpty($scope.input.selectedbatch))
                        {
                            ret.Id = $scope.input.selectedbatch;
                        }
                        else
                        {
                            ret.Id = $scope.input.batch;
                        }
                        // reset.
                        $scope.input.newbatch = "";
                        $scope.input.selectedbatch = "";
                        $scope.input.allocate = false;
                        $scope.getBatch(ret);                      
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
            alert("No orders were selected.");
        }
    };
    // handles modal box view.
    $scope.openModal = function (url, controller, size, list)
    {
        var modalInstance = $modal.open(
        {
            templateUrl: url,
            controller: controller,
            backdrop: 'static',
            windowClass: size,
            resolve:
                {
                    header: function ()
                    {
                        return list;
                    }
                }
        });
    };
    // changes site id for the orders by batch id, the orders cannot be allocated/fulfilled.
    $scope.changeSiteID = function ()
    {
        var input = prompt("Enter new SiteID for Batch: " + $scope.input.batch + ", orders cannot be allocated.", "");
        if (input != null && input != "")
        {
            // save user input to location parameter.
            $scope.input.Location = input;
        }
        else
        {
            return false;
        }
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/BatchOrderChangeSiteID', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    alert("Done");
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

    $scope.modifyQtyToInvoice = function ()
    {
        if (window.confirm("Set the Qty to Invoice value to [Qty fulfilled] instead of [QTY Allocated].")) {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/BatchOrderModifyQtyToInvoice', $scope.input)
                .then(function (response) {
                    if (!appFactory.errorCheck(response)) {
                        alert("Done");
                        ShowSearch($scope, "on");
                    }
                })
                .catch(function (reason) {
                    alert('Error: ' + reason.status + ' - ' + reason.statusText);
                })
                .finally(function () {
                    Spinner($scope, 'off');
                });
        }
    };

    $scope.setDefault();
   

});