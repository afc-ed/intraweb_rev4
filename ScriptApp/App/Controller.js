
// instantiate object and set dependencies.
var app = angular.module("app", ['ui.bootstrap', 'ckeditor', 'ngGrid']);

// to display image or video, otherwise security errors will occur.
app.config(function ($sceDelegateProvider) {
    $sceDelegateProvider.resourceUrlWhitelist([
        'self',
        '*://intraweb.hq.afcsushi.net/**'
    ]);
});
 
// ---------------- VIEW CONTROLLER ------------------------
app.controller( 'ViewController', function($scope, appFactory)
{
    window.scope = $scope;
   

    $scope.open = function(item)
    {  
        appFactory.openPage(item);
    };
    
   
    
    
    
});


