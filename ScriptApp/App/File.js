

var UploadFilename = function(item)
{
    var filename = '';
    var fullPath = document.getElementById(item).value;
    if (fullPath)
    {
        var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
        var filename = fullPath.substring(startIndex);
        if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0)
        {
            filename = filename.substring(1);
        }
    }
    return filename;
};

var FileExtension = function(item)
{
    return item.slice((item.lastIndexOf(".") - 1 >>> 0) + 2).toLowerCase();
};


