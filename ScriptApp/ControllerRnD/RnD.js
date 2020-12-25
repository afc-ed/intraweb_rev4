
app.controller('RnDController', function ($scope, appFactory)
{
    window.scope = $scope;
    
    $scope.setDefault = function ()
    {
        try
        {            
            appFactory.postRequest('/RnD/LoadMenu')
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
            ErrorMsg(e, 'File = RnD.js | Function = setDefault()');
        }
    };

    $scope.open = function (item)
    {
        appFactory.openPage("/RnD/" + item.Id);
    };

    $scope.setDefault();

   

});