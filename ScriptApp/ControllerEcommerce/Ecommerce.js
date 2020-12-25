
app.controller('EcommerceController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.ecom = {};
    
    $scope.setDefault = function ()
    {
        try
        {            
            appFactory.postRequest('/Ecommerce/LoadMenu')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.ecom = response;
                    }
                })
                .catch(function (reason)
                {
                    alert("Error: " + reason.status + " : " + reason.statusText);
                });
        }
        catch (e)
        {
            ErrorMsg(e, 'File = Ecommerce.js | Function = setDefault()');
        }
    };

    $scope.open = function (item)
    {
        appFactory.openPage("/Ecommerce/" + item.Id);
    };

    $scope.setDefault();

   

});