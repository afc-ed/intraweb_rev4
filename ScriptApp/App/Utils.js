//accept array of arguments for required fields validate.
var RequiredFields = function()
{
    try
    {
        var oCtl, bRet = true;
        for (var i=0; i < arguments.length; i++)
        {
            oCtl = arguments[i];
            if (oCtl === undefined || oCtl === '')
            {
                alert('A required field is missing.'); 
                bRet = false;
                break;
            }
        }
        return bRet;
    }
    catch(e)
    {
        ErrorMsg(e, 'File= Utils.js | Function= RequiredFields()');
    }
};

//opens new window.
var OpenWindow = function( url, title, wd, ht )
{
    try
    {
        var left = (screen.width/2)-(wd/2);
        var top = (screen.height/2)-(ht/2);
        var oWin = window.open( url, title, 'toolbar=0, location=0, status=0, menubar=0, scrollbars=1, resizable=1, directories=0, copyhistory=0,  width='+wd+', height='+ht+', top='+top+', left='+left );
    }
    catch(e)
    {
        ErrorMsg(e, 'File= utils.js | Function= OpenWindow()');
    }
};

//validate and return boolean.
var IsEmpty = function( val )
{
    val += "";
    return ( val === "null" || val === undefined || val === '' || val === null ) ? true : false;
};

//error handler.
var ErrorMsg = function( e, msg )
{
    alert(e + '\n' + msg);
};

//return date in mm/dd/yyyy format.
var InitializeDate = function( Type, DayNum )
{
    try
    {
        var CurrentDate = new Date();
        switch( Type )
        {
            case "add":
                CurrentDate.setDate(CurrentDate.getDate() + parseInt(DayNum));
                break;
            case "minus":
                CurrentDate.setDate(CurrentDate.getDate() - parseInt(DayNum));
                break;
        }
        var Day =  CurrentDate.getDate();
        var Month = CurrentDate.getMonth() + 1;
        var Year =  CurrentDate.getFullYear();
        return ( (Month <= 9 ? "0" + Month : Month) + "/" + (Day <= 9 ? "0" + Day : Day) + "/" + Year );
    }
    catch (e)
    {
        ErrorMsg(e, 'File= utils.js | Function= InitializeDate()'); 
    }
};

//check for number
var CheckNumbers = function()
{
    try
    {
        var oField, isValid = true;
        for( var i=0; i < arguments.length; i++ )
        {
            oField = arguments[i];
            //remove comma from currency.
            oField = oField.replace(/,/g, ''); 
            if( oField !== "" )
            {
                isValid = !jQuery.isArray( oField ) && (oField - parseFloat( oField ) + 1) >= 0;
                if( !isValid )
                {
                    alert('A number was expected.'); 
                    break;
                }
            }            
        }
        return isValid;        
    }
    catch(e)
    {
        ErrorMsg(e, 'File=  utils.js | Function= CheckNumbers()');
    }    
};

//add numbers
var AddNumbers = function()
{
    try
    {
        var oField, total = 0;
        for( var i=0; i < arguments.length; i++ )
        {
            oField = arguments[i];
            total += (!isNaN(oField) && !IsEmpty(oField) ? parseFloat(oField) : 0);                     
        }
        return parseFloat(total.toFixed(2)).toString();        
    }
    catch(e)
    {
        ErrorMsg(e, 'File=  utils.js | Function= AddNumbers()');
    }    
};

//confirm delete
var ConfirmDelete = function()
{
    return confirm("Continue with delete?");
};

//compare dates and show error message if start > end.
var ConfirmDates = function(StartDate, EndDate)
{
    var isValid = true;
    //reformat date to yyyymmdd for comparison.
    var d1 = StartDate.split("/");
    var d2 = EndDate.split("/"); 
    var dStart = d1[2] + d1[0] + d1[1];
    var dEnd = d2[2] + d2[0] + d2[1]; 
    if( dStart > dEnd )
    {
        isValid = false;
    }
    return isValid;
};

//checks for values in array passed in, returns true if found.
var CheckForValuesInArray = function(item)
{
    var isFound = false;
    angular.forEach(item, function(value, key)
    {
        if(!IsEmpty(value))
        {
            isFound = true;
        }
    });
    return isFound;
};

//reset element
var ResetElement = function(id)
{
    var elem = angular.element( document.querySelector( id ) );
    elem.empty();
};

//turn spinner on or off.
var Spinner = function(scope, item)
{
    switch( item )
    {
        case 'on':
            scope.loading = true;
            scope.dataload = false;
            break;
        case 'off':
            scope.loading = false;
            scope.dataload = true;
            break;
    }
};

// turn search and results on or off.
var ShowSearch = function (scope, item)
{
    switch (item) {
        case 'on':
            scope.isSearch = true;
            scope.isData = false;
            break;
        case 'off':
            scope.isSearch = false;
            scope.isData = true;
            break;
    }
};

var IsMobile = function()
{
    var ret = false;
    if( /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()) )
    {
        ret = true;
    }
    return ret;
};



