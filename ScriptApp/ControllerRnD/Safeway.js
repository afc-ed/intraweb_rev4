
app.controller('RnD/SafewayController', function ($scope, appFactory)
{
    window.scope = $scope;
    $scope.input = { type: "load_data", startdate: "", enddate: "" };

    Spinner($scope, 'off');

    $scope.setControls = function ()
    {
        switch ($scope.input.type)
        {
            case "load_data": case "item_recode":
                $scope.isFile = false;
                $scope.isDate = true;
                break;
            case "purge_by_date":
                $scope.isDate = false;
                $scope.isFile = true;
                break;
        }
    };

    $scope.checkRequiredFieldsBasedOnType = function ()
    {
        var isFound = false;
        switch ($scope.input.type)
        {
            case "load_data": case "item_recode":
                {
                    var filename = UploadFilename('fileInput');
                    if (IsEmpty(filename))
                    {
                        alert('Please select a file.');
                    }
                    else if (/xlsx|xls/gi.test(FileExtension(filename)))
                    {
                        isFound = true;
                    }
                    else
                    {
                        alert('Only excel files are accepted.');
                    }
                }                
                break;
            case "purge_by_date":
                if (RequiredFields($scope.input.startdate, $scope.input.enddate))
                {
                    isFound = true;
                }
                break;
        }
        return isFound;
    };

    $scope.submit = function ()
    {
        try
        {
            // based on type check for required fields.
            if ($scope.checkRequiredFieldsBasedOnType())
            {
                Spinner($scope, 'on');
                //FormData object
                var input = $scope.input;
                var formdata = new FormData();
                var fileInput = document.getElementById('fileInput');
                // add key/value pair to form object.
                formdata.append("type", input.type);
                formdata.append("startdate", input.startdate);
                formdata.append("enddate", input.enddate);
                formdata.append("loadfile", fileInput.files[0]);
                //Creating an XMLHttpRequest and sending
                var xhr = new XMLHttpRequest();
                xhr.open('POST', '/RnD/SafewayData');
                xhr.send(formdata);
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        alert(JSON.parse(xhr.responseText));
                        window.location.href = '/RnD';
                    }
                }
            }           
        }
        catch (e)
        {
            ErrorMsg(e, 'File = Safeway.js | Function = submit()');
        }
    };

    $scope.setControls();
   

});