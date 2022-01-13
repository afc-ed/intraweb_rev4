
app.controller('Connect/Storegroup', function ($scope, appFactory, $modalInstance, header)
{
    window.scope = $scope;    
    Spinner($scope, 'off');
    $scope.filter = header;
    $scope.filter.Type = "storegroup";
    $scope.storegroup = { filtertext: "" };
    var itemId = '', itemName = '';
    //for grid selection.
    $scope.rowSelected = [];
    //grid.
    try {
        $scope.storeGroupGrid =
        {
            data: "gridoutput",
            columnDefs: [
                {
                    field: 'Name', displayName: 'Name', cellTemplate: '<div style="padding:0.2em">' +
                        '{{ row.entity.Name }}</div>', cellClass: 'text-left', width: '250px'
                }
            ],
            rowHeight: 40,
            multiSelect: true,
            enableSorting: true,
            enableColumnResize: false,
            maintainColumnRatios: false,
            showFilter: false,
            showGroupPanel: false,
            showFooter: true,
            filterOptions: { filterText: "", useExternalFilter: false },
            selectedItems: $scope.rowSelected
        };
    }
    catch (e) {
        ErrorMsg(e, 'Error on grid setup. | Function = storeGroupGrid');
    }   

    //update grid filter when search options are changed.
    $scope.gridFilter = function () {
        try {
            $scope.storeGroupGrid.filterOptions.filterText = appFactory.getGridFilter($scope.storegroup.filtertext);
        }
        catch (e) {
            ErrorMsg(e, 'File = Storegroup.js | Function= gridFilter()');
        }
    };

    //load defaults.
    $scope.setDefault = function () {
        Spinner($scope, 'on');
        appFactory.postRequest('/Connect/FilterOptions', $scope.filter)
            .then(function (response) {
                if (!appFactory.errorCheck(response)) {
                    $scope.gridoutput = response.data[0];
                }
            })
            .catch(function (reason) {
                alert('Error: ' + reason.status + ' - ' + reason.statusText);
            })
            .finally(function () {
                Spinner($scope, 'off');
            });
    };

    //save button returns array of items from grid that was selected by user.
    $scope.save = function () {
        angular.forEach($scope.rowSelected, function (item) {
            if (itemId !== '') {
                itemId += ',';
            }
            itemId += item.Id;
            if (itemName !== '') {
                itemName += ' | ';
            }
            itemName += item.Name;
        });
        $modalInstance.close([itemId, itemName]);
    };

    //cancel button.
    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    //load defaults.
    $scope.setDefault();
   

   

});