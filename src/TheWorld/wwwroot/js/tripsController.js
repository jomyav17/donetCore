﻿//tripsController.js

(function () {
    "use strict";

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.trips = [];

        vm.errorMessage = "";

        vm.isBusy = true;
        $http.get('/api/trips')
            .then(function (response) {
                //success
                angular.copy(response.data, vm.trips);
            },
            function (error) {
                //error
                vm.errorMessage = "Failed to load data. " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.newTrip = {};

        vm.AddTrip = function () {

            vm.errorMessage = "";
            vm.isBusy = true;

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                    vm.newTrip = {};

                }, function (error) {
                    vm.errorMessage = "Failed to save new trip. " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };


    }

})();