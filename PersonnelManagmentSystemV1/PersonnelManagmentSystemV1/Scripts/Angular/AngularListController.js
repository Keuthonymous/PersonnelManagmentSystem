(function () {
    var app = angular.module("PMS");

    app.controller("ListController", ["$scope",function ($scope) {
        $scope.data = null;
        $scope.onInitialise = function (jsonData) {
            $scope.data = jsonData;
        };

        $scope.reverse = true;
        $scope.orderByColumn = function (columnName) {
            $scope.reverse = ($scope.columnName === columnName) ? !$scope.reverse : false;
            $scope.columnName = columnName;
        };

    }]);


}());