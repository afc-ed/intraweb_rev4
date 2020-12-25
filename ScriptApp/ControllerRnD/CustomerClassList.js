
app.controller('RnD/CustomerClassListController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;
    $scope.input = {};

    $scope.setDefault = function ()
    {
        try
        {   
            Spinner($scope, 'on'); 
            appFactory.postRequest('/RnD/CustomerClassListData')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.output = response.data[0];
                        $scope.filelink = response.data[1];
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
            ErrorMsg(e, 'File = CustomerClassList.js | Function = setDefault()');
        }
    };

    $scope.edit = function (list)
    {
        $scope.header = list;
        $scope.openModal('/RnD/CustomerClassEdit', 'RnD/CustomerClassEditController', 'small');
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