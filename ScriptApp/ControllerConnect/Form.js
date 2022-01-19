
app.controller('Connect/Form', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    $scope.input = {};
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Connect/FormGrid')
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
            ErrorMsg(e, 'File = Form.js | Function = setDefault()');
        }
    };

    $scope.editRecord = function (list)
    {
        $scope.input = list;
        $scope.openModal('/Connect/FormDetail', 'Connect/FormDetail', 'large');
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
            $scope.setDefault();
        });
    };

    $scope.setDefault();

   

});