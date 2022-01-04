
// instantiate object and set dependencies.
var app = angular.module("app", ['ui.bootstrap', 'ckeditor']);
 
// ---------------- VIEW CONTROLLER ------------------------
app.controller( 'ViewController', function($scope, appFactory)
{
    window.scope = $scope;
   

    $scope.open = function(item)
    {  
        appFactory.openPage(item);
    };
    
   
    
    
    
});


