
app.controller('Ecommerce/ItemByClassController', function ($scope, appFactory)
{
    window.scope = $scope;
    //initialize
    $scope.input = {};
    
    $scope.setDefault = function ()
    {
        Spinner($scope, 'on');
        appFactory.postRequest('/Ecommerce/CustomerClassIds')
            .then(function (response)
            {
                if (!appFactory.errorCheck(response))
                {
                    $scope.classlist = response.data[0];
                    ShowSearch($scope, 'on'); 
                    $scope.isDataFileOnly = false;
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

    $scope.submit = function (type)
    {
        try
        {
            if (type == 'all')
            {
                $scope.input.Class = type;
            }
            if (RequiredFields($scope.input.Class))
            {
                Spinner($scope, 'on');
                appFactory.postRequest('/Ecommerce/ItemByClassData', $scope.input)
                    .then(function (response)
                    {
                        if (!appFactory.errorCheck(response))
                        {                            
                            if (type != 'all')
                            {
                                $scope.output = response.data[0];
                                $scope.filelink = response.data[1];
                                ShowSearch($scope, 'off');
                                $scope.isDataFileOnly = false;
                            }
                            else
                            {                                
                                $scope.filelink = response.data[0];
                                $scope.isSearch = false;
                                $scope.isData = false;
                                $scope.isDataFileOnly = true;
                            }
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
            ErrorMsg(e, 'File = ItemByClass.js | Function = submit()');
        }
    };
       

    $scope.setDefault();
   

});