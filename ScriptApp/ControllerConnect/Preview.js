
app.controller('Connect/Preview', function ($scope, $sce, $modalInstance, header)
{
    window.scope = $scope;       
    
    $scope.input =
    {
        Title: header.title,
        PageContent: $sce.trustAsHtml(header.PageContent)
    }; 

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };

   
   

   

});