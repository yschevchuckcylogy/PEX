var app = angular.module('app', []);

app.directive('ngConfirmClick', [
    function () {
        return {
            link: function (scope, element, attr) {
                var clickAction = attr.confirmedClick;
                element.bind('click', function (event) {
                    var msg = "This will update all vendor caps to " + window.cap + ", are you sure?";
                    if (window.confirm(msg)) {
                        scope.$eval(clickAction);
                    }
                });
            }
        };
    }]);

app.controller("EditVendors", function ($http, $scope) {


    $scope.vendors = window.vendors;
    $scope.cap = window.cap;


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

    $scope.change = function () {
        window.cap = $scope.cap;
    };

});