
// --------------- FACTORY ------------------------

app.factory("appFactory", function($http, $window)
{
    var appFactory = {};
    var pageScope = {};

    appFactory.setPageScope = function (val)
    {
        pageScope = val;
    };

    appFactory.getPageScope = function ()
    {
        return pageScope;
    };
    
    appFactory.postRequest = function(url, item)
    {
        return $http({
            url: url,
            dataType: 'json',
            method: 'POST',
            data: item,
            headers: {
                "Content-Type": "application/json"
            }  
        });
    };   
    
    // check for error message.
    appFactory.errorCheck = function(item)
    {
        var isError = false; 
        if( /exception/gi.test(item.data) )
        {
            alert(item.data);
            isError = true;
        }
        return isError;
    };  
    
    appFactory.openPage = function(item)
    {
        $window.location.href = item;
    };

    appFactory.getGridFilter = function (text)
    {
        return !IsEmpty(text) ? text : '';
    };   
    
    return appFactory;
    
});

                                                        
