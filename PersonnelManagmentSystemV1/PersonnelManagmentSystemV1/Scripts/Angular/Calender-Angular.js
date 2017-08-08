(function () {
    var app = angular.module("calenderApp", []);

    app.controller("MainController", ["$scope", "$http", function ($scope, $http) {

        $scope.getData = getData;
        $scope.Events;

        function getData() {
            $http.get('/Calender/EventsJSON')
                .then(onRepos)
        };

        var onRepos = function (response) {
            $scope.Events = response.data;
        };

        $scope.reverse = true;
        $scope.orderByMe = function (CalenderSort) {
            $scope.reverse = ($scope.CalenderSort === CalenderSort) ? !$scope.reverse : false;
            $scope.CalenderSort = CalenderSort;
        };

    }]);


}());