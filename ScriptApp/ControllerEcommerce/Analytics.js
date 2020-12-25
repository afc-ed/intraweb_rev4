
app.controller('Ecommerce/AnalyticsController', function ($scope, appFactory ) {
    window.scope = $scope;
       

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = { Type: "" }; 
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');
    };


    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.Type))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/Ecommerce/AnalyticsRun', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.output = response.data[0];
                            $scope.filelink = response.data[1];
                            ShowSearch($scope, "off");
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
            ErrorMsg(e, 'File = Analytics.js | Function = submit()');
        }
    };

    $scope.setDefault();
   

});