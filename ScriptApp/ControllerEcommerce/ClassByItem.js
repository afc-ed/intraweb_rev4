
app.controller('Ecommerce/ClassByItemController', function ($scope, appFactory ) {
    window.scope = $scope;
    //initialize
    $scope.input = {};

    $scope.setDefault = function ()
    {
        try
        {
            Spinner($scope, 'on');
            appFactory.postRequest('/Ecommerce/ProductIds')
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        $scope.productlist = response.data[0];
                        ShowSearch($scope, "on");
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
            ErrorMsg(e, 'File = ClassByItem.js | Function = setDefault()');
        }
    };

    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.Product))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/Ecommerce/ClassByItemData', $scope.input)
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
            ErrorMsg(e, 'File = ClassByItem.js | Function = submit()');
        }
    };
    
    $scope.setDefault();
   

});