
app.controller('RnD/CustomerClassCreateDeleteController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.input = { copyfromid : 0 };
    Spinner($scope, 'off');
    
    $scope.setDefault = function ()
    {
        $scope.isCopyFrom = false;
        Spinner($scope, 'on');
        appFactory.postRequest('/RnD/CustomerClassDroplist')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.classList = response.data[0];
                    ShowSearch($scope, 'on');
                }
            })
            .catch(function (reason) {
                alert('Error: ' + reason.status + ' - ' + reason.statusText);
            })
            .finally(function () {
                Spinner($scope, 'off');
            });
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.action, $scope.input.class))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/RnD/CustomerClassCreateDeleteRun', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.message = response.data[0];
                            ShowSearch($scope, 'off');
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
        }
        catch (e)
        {
            ErrorMsg(e, 'File = CustomerClassCreateDelete.js | Function = submit()');
        }
    };

    $scope.setControl = function ()
    {
        try
        {
            $scope.isCopyFrom = false;
            if ($scope.input.action == "create")
            {
                $scope.isCopyFrom = true;
            }
        }
        catch (e)
        {
            ErrorMsg(e, 'File = CustomerClassCreateDelete.js | Function = setControl()');
        }
    };

    $scope.setDefault();

});