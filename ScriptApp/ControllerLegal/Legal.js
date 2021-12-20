
app.controller('LegalController', function ($scope, appFactory ) {
    window.scope = $scope;
    
    $scope.setDefault = function ()
    {
        try
        {            
            appFactory.postRequest('/Legal/LoadMenu')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.output = response.data[0];
                    }
                })
                .catch(function (reason)
                {
                    alert("Error: " + reason.status + " : " + reason.statusText);
                });
        }
        catch (e)
        {
            ErrorMsg(e, 'File = Legal.js | Function = setDefault()');
        }
    };

    $scope.open = function (item)
    {
        appFactory.openPage("/Legal/" + item.Id);
    };

    $scope.setDefault();

   

});