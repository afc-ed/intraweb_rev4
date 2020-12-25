
app.controller('RnD/GPCustomerClassListController', function ($scope, appFactory ) {
    window.scope = $scope;
    $scope.result = {};
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/RnD/GPCustomerClassListData')
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
            ErrorMsg(e, 'File = GPCustomerClassList.js | Function = setDefault()');
        }
    };

    $scope.setDefault();

   

});