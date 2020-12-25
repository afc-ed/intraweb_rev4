
app.controller('RnD/ConnectUpdatePasswordController', function ($scope, appFactory, $modalInstance)
{
    window.scope = $scope;
    Spinner($scope, 'off');
    
    $scope.save = function ()
    {
        try
        {
            // upload and save the file.
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
                    //Form Data object
                    var formdata = new FormData();
                    var fileInput = document.getElementById('fileInput');
                    // add key/value pair to form object.
                    formdata.append("loadfile", fileInput.files[0]);
                    //Creating an XMLHttpRequest and sending
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', '/RnD/ConnectPasswordUpdate');
                    xhr.send(formdata);                    
                    xhr.onreadystatechange = function ()
                    {  
                        if (xhr.readyState == 4 && xhr.status == 200)
                        {
                            alert(JSON.parse(xhr.responseText));
                            $modalInstance.close();
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
            ErrorMsg(e, 'File = ConnectUpdatePassword.js | Function = submit()');
        }
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

  
   

});