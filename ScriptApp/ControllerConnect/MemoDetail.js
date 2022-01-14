
app.controller('Connect/MemoDetail', function ($scope, appFactory, $modalInstance, $modal, header)
{
    window.scope = $scope;    
    $scope.input = header;
    Spinner($scope, 'off');
    // Editor options.
    $scope.options =
    {   
        language: 'en',
        allowedContent: true,
        entities: false,
        height: '200'
    };

    $scope.setDefault = function ()
    {
        appFactory.postRequest('/Connect/MemoDetailGetData', $scope.input)
            .then(function (response) {
                if (!appFactory.errorCheck(response)) {
                    $scope.input = response.data[0];
                }
            })
            .catch(function (reason) {
                alert('Error: ' + reason.status + ' - ' + reason.statusText);
            })
            .finally(function () {
                Spinner($scope, 'off');
            });
    };


    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Title))
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Connect/MemoSave', $scope.input)
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

    $scope.delete = function () {
        if (ConfirmDelete()) {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Connect/MemoDelete', $scope.input)
                .then(function (response) {
                    if (!appFactory.errorCheck(response)) {
                        $modalInstance.close();
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

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };

    $scope.openModal = function (url, controller, size) {
        var modalInstance = $modal.open(
            {
                templateUrl: url,
                controller: controller,
                backdrop: 'static',
                windowClass: size,
                resolve:
                {
                    header: function () {
                        // set parent, this is used to save filter settings based on id.
                        $scope.input.Parent = "memo";
                        return $scope.input;
                    }
                }
            });
        // when modal is closed, refresh filter results on page.
        modalInstance.result.then(function (filter) {
            $scope.input.Region = filter[0];
            $scope.input.Storegroup = filter[1];
            $scope.input.State = filter[2];
        });
    };

    $scope.preview = function () {
        $scope.openModal("/Connect/Preview", "Connect/Preview", "medium");
    };
   
    $scope.setDefault();

});