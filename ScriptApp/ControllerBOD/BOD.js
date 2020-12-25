
app.controller('BODController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.ecom = {};
    
    $scope.setDefault = function ()
    {
        try
        {            
            appFactory.postRequest('/BOD/LoadMenu')
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
            ErrorMsg(e, 'File = BOD.js | Function = setDefault()');
        }
    };

    $scope.open = function (item)
    {
        appFactory.openPage("/BOD/" + item.Id);
    };

    $scope.setDefault();

   

});