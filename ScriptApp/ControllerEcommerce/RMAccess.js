
app.controller('Ecommerce/RMAccessController', function ($scope, appFactory ) {
    window.scope = $scope;
    
    $scope.setDefault = function ()
    {
        try
        {
            //initialize
            $scope.input = { Type: "append"};
            Spinner($scope, 'on');
            appFactory.postRequest('/Ecommerce/RMAccessDroplist')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.regionlist = response.data[0];
                        $scope.loginlist = response.data[1];
                        ShowSearch($scope, 'on');
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
            ErrorMsg(e, 'File = RMAccess.js | Function = setDefault()');
        }
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.Region, $scope.input.Login))
            {
                // login multi-select.
                if ($scope.input.Login != undefined)
                {
                    var convertLogin = angular.fromJson($scope.input.Login).toString();
                    $scope.input.Login = convertLogin;
                }
                // region multi-select.
                if ($scope.input.Region != undefined)
                {
                    var convertRegion = angular.fromJson($scope.input.Region).toString();
                    $scope.input.Region = convertRegion;
                }
                Spinner($scope, 'on');
                appFactory.postRequest('/Ecommerce/UpdateRMAccess', $scope.input)
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
            ErrorMsg(e, 'File = RMAccess.js | Function = submit()');
        }
    };
    
    $scope.setDefault();
   

});