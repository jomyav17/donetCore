//tripsEditorController

(function () {
    "use strict";

    angular.module("app-trips").controller("tripsEditorController", tripsEditorController);

    function tripsEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.ErrorMessage = "";
        vm.isBusy = true;

        vm.newStop = {};
        var url = "/api/trips/" + vm.tripName + "/stops";
        $http.get(url)
            .then(function (response) {
                angular.copy(response.data, vm.stops);
                vm.ErrorMessage = "";
                _showMap(vm.stops);
            }, function (error) {
                vm.ErrorMessage = "Error occured while retrieving stops. " + error.toString();
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.AddStop = function () {
            vm.isBusy = true;

            $http.post(url, vm.newStop)
                .then(function (response) {
                    vm.stops.push(response.data);
                    vm.ErrorMessage = "";
                    vm.newStop = {};
                    _showMap(vm.stops);
                }, function (error) {
                    vm.ErrorMessage = "Error occured while adding stop. " + error.toString();
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };
    }

    function _showMap(stops) {
        if (stops && stops.length > 0) {
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });
        }
    }
})();