
app.controller('Distribution/RecallController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.recall = {};
    $scope.input = { Location: "1" };

    $scope.setDefault = function ()
    {
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.lot, $scope.input.item))
            {
                // save scope
                appFactory.setPageScope($scope.recall);
                // get data.
                Spinner($scope, 'on');
                appFactory.postRequest('/Distribution/RecallData', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.recall =
                            {
                                output: response.data[0],
                                filelink: response.data[1]                                
                            };
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
            ErrorMsg(e, 'File = Recall.js | Function = submit()');
        }
    };

    $scope.setDefault();
   

});