
app.controller('Distribution/PromoEditController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.input = {}; 
   
    $scope.setDefault = function (id)
    {        
        $scope.input.id = id;
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/PromoEditRecord', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.input = response.data[0][0];
                    $scope.statelist = response.data[1];
                    $scope.itemlist = response.data[2];
                    $scope.filelink_item = response.data[3];
                    $scope.orderlist = response.data[4];
                    $scope.filelink_order = response.data[5];
                    $scope.invoicelist = response.data[6];
                    $scope.filelink_invoice = response.data[7];
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
        if (RequiredFields($scope.input.Startdate, $scope.input.Enddate, $scope.input.Description))
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/PromoSave', $scope.input)
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
            appFactory.postRequest('/Distribution/PromoDelete', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.open('/Distribution/Promo');
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
    
   

});