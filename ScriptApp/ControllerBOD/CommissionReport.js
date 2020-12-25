
app.controller('BOD/CommissionReportController', function ($scope, appFactory ) {
    window.scope = $scope;
       

    $scope.setDefault = function ()
    {
        //initialize
        $scope.input = { FromDate: "", Id: "" }; 
        Spinner($scope, 'off');
        ShowSearch($scope, 'on');
    };


    $scope.submit = function ()
    {
        try
        {
            if (RequiredFields($scope.input.FromDate))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/BOD/CommissionReportData', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {
                            $scope.filelink = response.data[0];
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
            ErrorMsg(e, 'File = CommissionReport.js | Function = submit()');
        }
    };

    $scope.setDefault();
   

});