var app = angular.module('app', []);
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

    $scope.setCapForVendors = function (cap) {
        return $http({
            url: '/Home/SetAllVendorsCap?cap=' + cap,
            method: "GET"
        });
    };

});