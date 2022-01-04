
app.controller('Connect/Memo', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    $scope.input = {};
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Connect/MemoGrid')
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
            ErrorMsg(e, 'File = Memo.js | Function = setDefault()');
        }
    };

    $scope.editRecord = function (list)
    {
        appFactory.setPageScope = list;
        $scope.openModal('/Connect/MemoEdit', 'Connect/MemoEdit', 'large');
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
                        return appFactory.getPageScope();
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