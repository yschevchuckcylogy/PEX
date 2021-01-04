var app = angular.module('app', []);

app.directive('ngConfirmClick', [
    function () {
        return {
            link: function (scope, element, attr) {
                var msg = attr.ngConfirmClick || "Are you sure?";
                var clickAction = attr.confirmedClick;
                element.bind('click', function (event) {
                    if (window.confirm(msg)) {
                        scope.$eval(clickAction);
                    }
                });
            }
        };
    }]);

app.controller("EditVendors", function ($http, $scope) {


    $scope.vendors = window.vendors;
    $scope.cap = 100500;

    $scope.save = function (item) {
        return $http({
            url: '/Home/EditVendor',
            method: "POST",
            data: item,
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };

    $scope.saveAll = function (vendors) {
        return $http({
            url: '/Home/EditVendors',
            method: "POST",
            data: vendors,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function successCallback(response) {
            location.reload();
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

    $scope.setCapForVendors = function (cap) {
        return $http({
            url: '/Home/SetAllVendorsCap?cap=' + cap,
            method: "GET"
        }).then(function successCallback(response) {
            location.reload();
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

});