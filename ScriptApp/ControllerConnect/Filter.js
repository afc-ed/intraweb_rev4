
app.controller('Connect/Filter', function ($scope, appFactory, $modalInstance, $modal)
{
    window.scope = $scope;    
    $scope.input = { Title: "", Active: "yes", PageContent: "", Id: 0 };
    Spinner($scope, 'off');
    $scope.header = header;
    $scope.filter = {};
    //load defaults on page load.
    $scope.setDefault = function () {
        try {
            $scope.filter =
            {
                region: "",
                state: "",
                storegroup: "",
                regionid: "",
                stateid: "",
                storegroupid: ""
            };
        }
        catch (e) {
            ErrorMsg(e, 'File = ConnectFilter.js | Function = setDefault()');
        }
    };

    $scope.save = function ()
    {




        appFactoryExt.postRequest(ajaxUrl, 'saveFilter', $scope.filter, $scope.header)
            .success(function (data) {
                if (!appFactoryExt.checkForError(data)) {
                    $modalInstance.close([$scope.filter.region, $scope.filter.storegroup, $scope.filter.state]);
                }
            })
            .error(function (error) {
                alert("Error on saving data., Function = setDefault(), Description: " + error.message);
            });
    };


    //open modal
    $scope.openModal = function (url, controller, size) {
        var modalInstance = $modal.open(
            {
                templateUrl: templateDir + url,
                controller: controller,
                backdrop: 'static',
                windowClass: size,
                //pass in header scope to modal.
                resolve:
                {
                    header: function () {
                        return $scope.filter;
                    }
                }
            });
        //when modal is closed calling page is updated.
        modalInstance.result.then(function (item) {
            var id = item[0], name = item[1];

            switch (url.split('.')[0]) {
                case 'Region':
                    $scope.filter.region = name;
                    $scope.filter.regionid = id;
                    break;
                case 'State':
                    $scope.filter.state = name;
                    $scope.filter.stateid = id;
                    break;
                case 'StoreGroup':
                    $scope.filter.storegroup = name;
                    $scope.filter.storegroupid = id;
                    break;
            }
        });
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    //load page defaults.
    $scope.setDefault(); 
   

   

});