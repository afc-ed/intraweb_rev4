﻿
app.controller('Distribution/PriceListController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.result = {};
    $scope.input = { PriceLevel: "STD" };

    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Distribution/PriceListLevelData', $scope.input)
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
            ErrorMsg(e, 'File = PriceList.js | Function = setDefault()');
        }
    };

    $scope.setDefault();

   

});