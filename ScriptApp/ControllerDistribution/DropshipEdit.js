
app.controller('Distribution/DropshipEditController', function ($scope, appFactory, $modal, $modalInstance, header)
{
    window.scope = $scope;
   
    $scope.setDefault = function ()
    {
         // initialize form inputs is done here because of the page reload when vendor conversion is deleted.
        $scope.input = header; 
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/DropshipEditRecord', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.input = response.data[0];
                    $scope.vendorList = response.data[1];                    
                    $scope.companyList = response.data[2];
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
        if (RequiredFields($scope.input.Description, $scope.input.RateType, $scope.input.Rate, $scope.input.Batch, $scope.input.CompanyId, $scope.input.Customer))
        {
            appFactory.postRequest('/Distribution/DropshipSave', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        // reload.
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
    };

    $scope.delete = function ()
    {
        if (ConfirmDelete())
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/DropshipDelete', $scope.input)
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

    $scope.deleteVendor = function (list)
    {
        if (ConfirmDelete())
        {
            $scope.input = list;
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/DropshipVendorDelete', $scope.input)
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
    };

    $scope.openModal = function (url, controller, size)
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
                        return $scope.input;
                    }
                }
        });
        //when modal is closed, the main page is updated.
        modalInstance.result.then(function ()
        {
            $scope.setDefault($scope.input.id);
        });
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };

    $scope.customerCheck = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/DropshipCustomerCheck', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    alert(response.data[0]);
                }
            })
            .catch(function (reason)
            {
                alert('Error: ' + reason.status + ' - ' + reason.statusText);
            })
            .finally(function () {
                Spinner($scope, 'off');
            });
    };

    $scope.gpIntegration = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/DropshipGPIntegration', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    alert(response.data);
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

    $scope.setDefault();
    

});