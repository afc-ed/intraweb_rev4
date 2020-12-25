
app.controller('Distribution/DropshipImportController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    Spinner($scope, 'off');
    
    $scope.save = function ()
    {
        try
        {
            // upload and save the file.
            var filename = UploadFilename('fileInputPage');
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
                    // Form Data object
                    var formdata = new FormData();
                    var fileInput = document.getElementById('fileInputPage');
                    // add key/value pair to form object.
                    formdata.append("id", header.Id);
                    formdata.append("loadfile", fileInput.files[0]);
                    //Creating an XMLHttpRequest and sending
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', '/Distribution/DropshipImportData');
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
            ErrorMsg(e, 'File = DropshipImport.js | Function = save()');
        }
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

    

});