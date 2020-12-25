
app.controller('Ecommerce/ItemStatusController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    //initialize
    $scope.header = {};
    $scope.result = {};
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Ecommerce/ItemStatusData')
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
            ErrorMsg(e, 'File = ItemStatus.js | Function = setDefault()');
        }
    };

    $scope.openEdit = function (list)
    {
        $scope.header = list;
        $scope.openModal("/Ecommerce/ItemStatusEdit", "Ecommerce/ItemStatusEditController", "small");
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
                        return $scope.header;
                    }
                }
        });
        //when modal is closed, the main page is updated.
        modalInstance.result.then(function ()
        {
            $scope.setDefault();
        });
    };

    $scope.setDefault();

   

});