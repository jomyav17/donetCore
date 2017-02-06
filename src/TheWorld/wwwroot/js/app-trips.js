// app-trips.js

(function () {

    "use strict";

    angular.module("app-trips", ["simpleControl", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html"
            });
            $routeProvider.when("/editor/:tripName", {
                controller: "tripsEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripsEditorView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });
        });

})();