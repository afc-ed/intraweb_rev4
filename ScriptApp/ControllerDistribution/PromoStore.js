
app.controller('Distribution/PromoStoreController', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;
    $scope.input = { batch: "", freight: "0", comment: "", location: "1" };
    $scope.header = header;
    Spinner($scope, 'off');

    $scope.save = function ()
    {
        try
        {
            if (RequiredFields($scope.input.batch))
            {
                // check freight for number.
                if (!CheckNumbers($scope.input.freight))
                {
                    return false;
                }
                // upload and save the file.
                var filename = UploadFilename('fileInputStorePage');
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
                        var fileInput = document.getElementById('fileInputStorePage');
                        // add key/value pair to form object.
                        formdata.append("id", header.Id);
                        formdata.append("batch", $scope.input.batch);
                        formdata.append("freight", $scope.input.freight);
                        formdata.append("comment", $scope.input.comment);
                        formdata.append("location", $scope.input.location);
                        formdata.append("loadfile", fileInput.files[0]);
                        //Creating an XMLHttpRequest and sending
                        var xhr = new XMLHttpRequest();
                        xhr.open('POST', '/Distribution/PromoStoreUpdate');
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
        }
        catch (e)
        {
            ErrorMsg(e, 'File = PromoStore.js | Function = submit()');
        }
    };

    $scope.cancel = function ()
    {
        $modalInstance.dismiss();
    };  

   
   

});