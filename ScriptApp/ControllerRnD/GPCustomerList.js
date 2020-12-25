
app.controller('RnD/GPCustomerListController', function ($scope, appFactory ) {
    window.scope = $scope;
    $scope.result = {};
    
    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/RnD/GPCustomerListData')
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
            ErrorMsg(e, 'File = GPCustomerList.js | Function = setDefault()');
        }
    };

    $scope.setDefault();

   

});