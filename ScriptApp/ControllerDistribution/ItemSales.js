
app.controller('Distribution/ItemSalesController', function ($scope, appFactory ) {
    window.scope = $scope;

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = { type: "item_sales", Location: "1" };
        $scope.isTurnover = false;        
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');       
    };

    $scope.setControl = function ()
    {
        switch ($scope.input.type)
        {
            case "item_sales":
                $scope.isTurnover = false;
                break;
            case "item_turnover":
                $scope.isTurnover = true;
                break;
        }
    };

    $scope.submit = function ()
    {
        // if not turnover then check for date range.
        if (!$scope.isTurnover)
        {
            if (!RequiredFields($scope.input.startdate, $scope.input.enddate))
            {
                return false;
            }
        }
        Spinner($scope, 'on');
        appFactory.postRequest('/Distribution/ItemSalesData', $scope.input)
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.filelink = response.data[0];
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
    };

    $scope.setDefault();
   

});