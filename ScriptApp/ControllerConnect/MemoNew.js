
app.controller('Connect/MemoNew', function ($scope, appFactory, $modalInstance, $modal)
{
    window.scope = $scope;    
    $scope.input = { Title: "", Active: "1", PageContent: "", Id: 0 };
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
                        $scope.input.Id = response.data[0];
                        if ($scope.input.Id > 0)
                        {
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

    // to load detail page after create.
    $scope.openModal = function (url, controller, size)
    {
        $modal.open(
            {
                templateUrl: url,
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