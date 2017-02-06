//simpleControl.js

(function () {
    "use strict";
    angular.module("simpleControl", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displyWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }

})()