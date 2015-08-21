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

cityManagerControllers.controller("ProductTypeCtrl", function ($scope, $http, $timeout, $window) {
    $scope.manufacturers = [];
    $scope.productTypes = [];
    $scope.gridOptions = {
        columnDefs: [
          { name: "Name", field: "Name" },
          { name: "Quantity", field: "Quantity" }
        ],
        data: []
    };
    $scope.currentProduct = {
        RequiredProducts: []
    }
    $http.get("http://localhost:59892/api/ProductType").success(function (data) {
        $scope.productTypes = data;
        if (data.length > 0) {
            $scope.currentProduct = data[0];
            $scope.gridOptions = {
                columnDefs: [
                  { name: "Name", field: "Name" },
                  { name: "Quantity", field: "Quantity" }
                ],
                data: $scope.currentProduct.RequiredProducts
            };
        }
    });
    $http.get("http://localhost:59892/api/ManufacturerType").success(function (data) {
        $scope.manufacturers = data;
    });

    

    $scope.setProductType = function (prod) {
        $scope.currentProduct = prod;
    }
    $scope.newRequiredProduct = function () {
        $scope.currentProduct.RequiredProducts.push({});
    }
    $scope.Goto = function (where) {
        $window.location.href = where;
    }
});
cityManagerControllers.controller("TabsCtrl", function ($scope, $http, $timeout, $location) {

    $scope.Goto = function (where) {
        $location.path(where);
    }
});
cityManagerControllers.controller("CityManagerCtrl", function ($scope, $http, $timeout, $window) {
    $scope.dataloading = true;
    $scope.datafinished = false;
    $scope.manufacturers = [];
    $scope.city = {
        CurrentCityStorage: {
            CurrentInventory: []
        },
        BuildingUpgrades: [],
        RequiredProducts: []
    };
    $http.get("http://localhost:59892/api/ManufacturerType").success(function(data) {
        $scope.manufacturers = data;
        $http.get("http://localhost:59892/api/City").success(function (data) {
            $scope.city = data;
        });
    });
    $scope.Goto = function(where) {
        $window.location.href = where;
    }
    $scope.UpdateSupplyChain = function (postUpdateCallback) {

        var requiredProductUpdates = [];
        $.each($scope.city.RequiredProducts, function(idx, entity) {
            if (entity.AdjustedQuantity > 0) {

                requiredProductUpdates.push({
                    Quantity: entity.AdjustedQuantity,
                    ProductTypeId: entity.ProductTypeId
                });
            }
        });
        var buildingUpgrades = GetBuildingUpgradesForPost($scope.city.BuildingUpgrades);//$scope.city.BuildingUpgrades;
        var currentCityStorage = GetCurrentCityStorageForPost($scope.city.CurrentCityStorage);
        var supplyChainRequest = {
            BuildingUpgrades: buildingUpgrades,
            CurrentCityStorage: currentCityStorage,
            RequiredProductUpdates: requiredProductUpdates
        }

        $.ajax({
                url: "http://localhost:59892/api/SupplyChain",
                type: "POST",
                data: supplyChainRequest,
                dataType: "json",
                error:
                    function(xhr, ajaxOptions, thrownError) {
                        alert(thrownError);
                    }
            })
            .success(function(data) {
                $scope.city = data;

                if (postUpdateCallback)
                    postUpdateCallback();
                else
                    $scope.$apply();
            });
    }

    $scope.RequiredProductQuantityChange = function (product) {
        if (!product.OriginalQuanity)
            product.OriginalQuanity = product.Quantity;
        product.Quantity = product.OriginalQuanity - product.AdjustedQuantity;
    }

    $scope.AvailableProductQuantityChange = function (product) {
        if (!product.OriginalQuanity)
            product.OriginalQuanity = product.Quantity;
        product.Quantity = product.OriginalQuanity - product.AdjustedQuantity;
    }

    $scope.NewBuildingUpgrade = function() {
        $scope.city.BuildingUpgrades.push(
            {
                Name: "Property Upgrade " + $scope.city.BuildingUpgrades.length,
                TotalUpgradeTime: 0,
                RemainingUpgradeTime: 0,
                Products: [],
                ProductsInStorage: []
            }
        );
    }

    $scope.CompleteUpgradeClick = function (index) {
        if (!confirm("Complete Upgrade?"))
            return;

        $.each($scope.city.BuildingUpgrades[index].Products, function (index, product) {
            var inventoryProducts = getObjects($scope.city.CurrentCityStorage.CurrentInventory, "ProductTypeId", product.ProductTypeId);
            if (inventoryProducts.length > 0) {
                inventoryProducts[0].Quantity = inventoryProducts[0].Quantity - product.Quantity;
                if (inventoryProducts[0].Quantity < 0)
                    inventoryProducts[0].Quantity = 0;
            }
        });
        $scope.city.BuildingUpgrades.splice(index, 1);
    }

    $scope.SaveCity = function () {
        $scope.UpdateStorageFromOverstock();
        $scope.UpdateSupplyChain(function() {
            $.post("http://localhost:59892/api/city", {
                "BuildingUpgrades": GetBuildingUpgradesForPost($scope.city.BuildingUpgrades),
                "CurrentCityStorage": GetCurrentCityStorageForPost($scope.city.CurrentCityStorage)
            })
            .then(function (data) {
                    $scope.city = data;
                    $scope.$apply();
                    alert("Save Complete");
                });
        });
    }

    $scope.DropBuildingUpgradProduct = function(evt, ui, buildingUpgrade) {
        var productType = angular.element(ui.draggable).scope().productType;
        var newProduct = {
            BuildingUpgradeId: buildingUpgrade.Id,
            Name: productType.Name,
            Quantity: 0,
            ProductTypeId: productType.Id,
            RequiredProductsToolTip: productType.RequiredProductsToolTip,
            RequiredProducts: productType.Products
        };
        buildingUpgrade.Products.push(newProduct);
        console.log(newProduct);
    }

    $scope.DropDelete = function(evt, ui) {
        var draggableScope = angular.element(ui.draggable).scope();
        if (draggableScope.buildingUpgrade) {
            if (draggableScope.product) {
                if (confirm("remove product '" + draggableScope.product.Name + "' from upgrade '" + draggableScope.buildingUpgrade.Name + "'?"))
                    draggableScope.buildingUpgrade.Products.splice(draggableScope.$index, 1);
            } else {
                if (confirm("remove upgrade '" + draggableScope.buildingUpgrade.Name + "'?"))
                    $scope.city.BuildingUpgrades.splice(draggableScope.$index, 1);
            }
        }
    }

    $scope.onBuildingUpgradeProductChange = function() {
        console.log("build upgrade product change");
    }
    $scope.onUpdateStorage = function() {
        $scope.UpdateStorageFromOverstock();
        alert("Product Adjustments Complete");
    }
    $scope.UpdateStorageFromOverstock = function () {
        var removes = [];
        $.each($scope.city.AvailableStorage, function(index, product) {
            if (product.AdjustedQuantity > 0) {
                var productMatches = getObjects($scope.city.CurrentCityStorage.CurrentInventory, "ProductTypeId", product.ProductTypeId);
                if (productMatches.length) {
                    productMatches[0].Quantity = productMatches[0].Quantity - product.AdjustedQuantity;
                    product.AdjustedQuantity = 0;
                    if (productMatches[0].Quantity < 0)
                        productMatches[0].Quantity = 0;
                    console.log(product);
                    if (product.Quantity < 1)
                        removes.push(index);
                }
            }
        });
        $.each(removes, function(index, val) {
            $scope.city.AvailableStorage.splice(index, 1);
        });
        
    }

});
