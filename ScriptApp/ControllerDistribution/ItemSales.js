
app.controller('Distribution/ItemSalesController', function ($scope, appFactory ) {
    window.scope = $scope;

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = { type: "item_sales", Location: "1" };
        $scope.isDateFilter = true;
        $scope.isTurnover = false;         
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');       
    };

    $scope.setControl = function ()
    {
        switch ($scope.input.type)
        {
            case "item_sales":
                $scope.isDateFilter = true;
                $scope.isTurnover = false;                
                break;
            case "item_turnover":
                $scope.isDateFilter = false;
                $scope.isTurnover = true;               
                break;
        }
    };

    $scope.submit = function ()
    {
        // if date filter required then check for dates entered.
        if ($scope.isDateFilter)
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