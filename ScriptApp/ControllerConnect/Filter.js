
app.controller('Connect/Filter', function ($scope, appFactory, $modalInstance, $modal, header)
{
    window.scope = $scope;    
    Spinner($scope, 'off');
    //load defaults on page load, we set each one manually because we want to preserve the values passed in from parent.
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
            ErrorMsg(e, 'File = FilterController.js | Function = setDefault()');
        }
    };

    $scope.save = function ()
    {
        Spinner($scope, 'off');
        // set parent and id to scope, this is for filter settings update to record.
        $scope.filter.Id = header.Id;
        $scope.filter.Parent = header.Parent;
        appFactory.postRequest('/Connect/FilterUpdate', $scope.filter)
        .then(function (response) {
            if (!appFactory.errorCheck(response)) {
                $modalInstance.close([$scope.filter.region, $scope.filter.storegroup, $scope.filter.state]);
            }
        })
        .catch(function (reason) {
            alert('Error: ' + reason.status + ' - ' + reason.statusText);
        })
        .finally(function () {
            Spinner($scope, 'off');
        });
    };


    //open modal
    $scope.openModal = function (url, controller, size) {
        var modalInstance = $modal.open(
            {
                templateUrl: url,
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

            switch (url.split('/')[2].toLowerCase()) {
                case 'region':
                    $scope.filter.region = name;
                    $scope.filter.regionid = id;
                    break;
                case 'state':
                    $scope.filter.state = name;
                    $scope.filter.stateid = id;
                    break;
                case 'storegroup':
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