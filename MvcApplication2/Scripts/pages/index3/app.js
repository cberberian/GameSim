"use strict";

/* Controllers */
var cityManagerApp = angular.module("cityManagerApp", ["ngRoute", "cityManagerControllers", "ui.bootstrap", "ngDragDrop"]);
cityManagerApp.config(["$routeProvider",
  function ($routeProvider) {
      $routeProvider.
        when("/buildingManager", {
            templateUrl: "/scripts/pages/index3/partials/buildingManager.html",
            controller: "CityManagerCtrl"
        }).
        when("/productTypes", {
            templateUrl: "/scripts/pages/index3/partials/productTypes.html",
            controller: "ProductTypeCtrl"
        }).
        when("/manufacturingQueue", {
            templateUrl: "/scripts/pages/index3/partials/manufacturingQueue.html",
            controller: "ManufacturingQueueCtrl"
        }).
        otherwise({
            redirectTo: "/buildingManager"
        });
  }]);

var cityManagerControllers = angular.module("cityManagerControllers", []);

cityManagerControllers.controller("ManufacturingQueueCtrl", function ($scope, $http, $timeout, $window) {
    $http.get("http://localhost:59892/api/City").success(function (data) {
        $scope.city = data;
    });
});


cityManagerControllers.controller("TabsCtrl", function ($scope, $http, $timeout, $location) {

    $scope.Goto = function (where) {
        $location.path(where);
    }
});

