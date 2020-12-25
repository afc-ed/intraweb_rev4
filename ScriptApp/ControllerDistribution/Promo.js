
app.controller('Distribution/PromoController', function ($scope, appFactory, $modal)
{
    window.scope = $scope;

    // load current records from db.
    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/PromoRecords')
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
    };
    // opens page to edit.
    $scope.edit = function (list)
    {
        appFactory.openPage('/Distribution/PromoEdit?id=' + list.Id);
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