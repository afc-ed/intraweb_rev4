
app.controller('Connect/MemoNew', function ($scope, appFactory, $modalInstance, $modal)
{
    window.scope = $scope;    
    $scope.input = { Title: "", Active: "yes", PageContent: "", Id: 0 };
    Spinner($scope, 'off');
    // Editor options.
    $scope.options =
    {   
        language: 'en',
        allowedContent: true,
        entities: false,
        height: '200'
    };

    $scope.save = function ()
    {
        if (RequiredFields($scope.input.Title))
        {
            Spinner($scope, 'on'); 
            appFactory.postRequest('/Connect/MemoCreate', $scope.input)
                .then(function (response)
                {
                    if (!appFactory.errorCheck(response))
                    {
                        if (response.data[0] > 0)
                        {
                            $scope.input.Id = response.data[0];
                            appFactory.SetPageScope = $scope.input;
                            $modalInstance.close();
                            $scope.openModal("/Connect/MemoDetail", "Connect/MemoDetail", "large");
                        }
                        else
                        {
                            alert("Error. Cannot create Memo.");
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
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };

    $scope.openModal = function (url, controller, size)
    {
        $modal.open(
            {
                templateUrl: templateDir + url,
                controller: controller,
                backdrop: 'static',
                windowClass: size,
                resolve:
                {
                    header: function ()
                    {
                        return $scope.input;
                    }
                }
            });
    };    
   

   

});