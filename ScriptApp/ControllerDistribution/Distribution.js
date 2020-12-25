
app.controller('DistributionController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.dist = {};
    
    $scope.setDefault = function ()
    {
        appFactory.postRequest('/Distribution/LoadMenu')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.dist = response;
                }
            })
            .catch(function (reason)
            {
                alert("Error: " + reason.status + " : " + reason.statusText);
            });
    };

    $scope.open = function (item)
    {
        appFactory.openPage("/Distribution/" + item.Id);
    };

    $scope.setDefault();

   

});