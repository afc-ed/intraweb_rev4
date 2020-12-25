
app.controller('Ecommerce/PriceListController', function ($scope, appFactory)
{
    window.scope = $scope;
    $scope.result = {};
    $scope.input = { Type: "" };
   
    $scope.setDefault = function (type)
    {
        try
        {
            $scope.input.Type = type;
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Ecommerce/PriceListData', $scope.input)
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

    $scope.setDefault("all");

   

});