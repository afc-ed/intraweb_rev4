
app.controller('Distribution/OrderItemsController', function ($scope, appFactory, $modalInstance, header, $modal)
{
    window.scope = $scope;
    // initialize form inputs.
    $scope.header = header;
    
    $scope.setDefault = function ()
    {        
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/OrderItemsList', $scope.header)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.itemlist = response.data[0];
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

    $scope.openModal = function (url, controller, size, list = null)
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
                        return $scope.header;
                    },
                    item: function ()
                    {
                        return list;
                    }
                }
        });
        //when modal is closed, the main page is updated.
        modalInstance.result.then(function ()
        {
            $scope.setDefault();
        });
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

    $scope.setDefault();
   

});