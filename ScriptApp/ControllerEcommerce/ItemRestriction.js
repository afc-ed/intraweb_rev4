
app.controller('Ecommerce/ItemRestrictionController', function ($scope, appFactory ) {
    window.scope = $scope;
    Spinner($scope, 'off');
    
    $scope.submit = function ()
    {
        try
        {
            var filename = UploadFilename('fileInput');
            if (IsEmpty(filename))
            {
                alert('Please select a file.');
            }
            else
            {
                // check xlsx or xls file extension.
                if (/xlsx|xls/gi.test(FileExtension(filename)))
                {   
                    Spinner($scope, 'on');
                    //FormData object
                    var formdata = new FormData(); 
                    var fileInput = document.getElementById('fileInput');
                    //Iterating through each files selected in fileInput
                    for (i = 0; i < fileInput.files.length; i++)
                    {
                        //Appending each file to FormData object
                        formdata.append(fileInput.files[i].name, fileInput.files[i]);
                    }
                    //Creating an XMLHttpRequest and sending
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', '/Ecommerce/ItemRestrictionRun');
                    xhr.send(formdata);
                    xhr.onreadystatechange = function ()
                    {
                        if (xhr.readyState == 4 && xhr.status == 200)
                        {
                            alert(JSON.parse(xhr.responseText));
                            window.location.href = '/Ecommerce';
                        }
                    }
                }
                else
                {
                    alert('Only excel files are accepted.');
                }
            }  
        }
        catch (e)
        {
            ErrorMsg(e, 'File = ItemRestriction.js | Function = submit()');
        }
    };

    
   

});