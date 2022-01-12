﻿
app.controller('Connect/Region', function ($scope, appFactory, $modalInstance, $modal, header)
{
    window.scope = $scope;    
    Spinner($scope, 'off');
    $scope.filter = header;
    $scope.filter.Type = "region";
    $scope.region = { filtertext: "" };
    var itemId = '', itemName = '';
    //for grid selection.
    $scope.rowSelected = [];
    //grid.
    try {
        $scope.regionGrid =
        {
            data: "gridoutput",
            columnDefs: [
                {
                    field: 'name', displayName: 'Region', cellTemplate: '<div style="padding:0.2em">' +
                        '{{ row.entity.name }}</div>', cellClass: 'text-left', width: '250px'
                },
                {
                    field: 'code', displayName: 'Code', cellTemplate: '<div style="padding:0.2em">' +
                        '{{ row.entity.code }}</span></div>', cellClass: 'text-left', width: '60px'
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
        ErrorMsg(e, 'Error on grid setup. | Function = regionGrid');
    }

    //update grid filter when search options are changed.
    $scope.gridFilter = function () {
        try {
            $scope.regionGrid.filterOptions.filterText = appFactory.getGridFilter($scope.region.filtertext);
        }
        catch (e) {
            ErrorMsg(e, 'File = Region.js | Function= gridFilter()');
        }
    };

    //load defaults.
    $scope.setDefault = function () {
        Spinner($scope, 'on');
        appFactory.postRequest('/Connect/FilterGrid', $scope.filter)
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
            itemId += item.id;
            if (itemName !== '') {
                itemName += ' | ';
            }
            itemName += item.code;
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