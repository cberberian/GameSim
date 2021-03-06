﻿cityManagerControllers.controller("CityManagerCtrl", function ($scope, $http, $timeout, $window) {
    $scope.dataloading = true;
    $scope.datafinished = false;
    $scope.manufacturers = [];

    $scope.city = {
        CurrentCityStorage: {
            CurrentInventory: []
        },
        BuildingUpgrades: [],
        RequiredProducts: [],
        NewManufacturingQueueSlots: []
    };
    $http.get("http://localhost:59892/api/ManufacturerType").success(function (data) {
        $scope.manufacturers = data;
        $http.get("http://localhost:59892/api/City").success(function (data) {
            $scope.city = data;
        });
    });
    $scope.Goto = function (where) {
        $window.location.href = where;
    }
    $scope.CalculateView = function () {
        $scope.UpdateStorageFromOverstock();
        $scope.UpdateSupplyChain();
    }
    $scope.UpdateSupplyChain = function (postUpdateCallback) {

        var requiredProductUpdates = [];
        $.each($scope.city.RequiredProducts, function (idx, entity) {
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
                function (xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }
        })
            .success(function (data) {
                $scope.city = data;

                if (postUpdateCallback)
                    postUpdateCallback();
                else
                    $scope.$apply();
            });
    }

    $scope.RequiredProductQuantityChange = function (product) {
        if (typeof product.OriginalQuantity === "undefined")
            product.OriginalQuantity = product.Quantity;
        var newQuantity = product.OriginalQuantity - product.AdjustedQuantity;
        $scope.RequiredProductQuantityDirty = newQuantity !== product.OriginalQuantity;
        product.Quantity = newQuantity;
        for (var i = 0; i < product.AdjustedQuantity; i++) {
            $scope.city.NewManufacturingQueueSlots.push({
                ProductId: product.ProductTypeId,
                ProductName: product.Name
            });
        }
    }

    $scope.CityStorageQuantityChanged = function (product, originalQuantity) {
        if (typeof product.OriginalQuantity === "undefined")
            product.OriginalQuantity = GetInt(originalQuantity);
        console.log(product.OriginalQuantity);
        $scope.CityStorageQuantityDirty = product.OriginalQuantity !== product.Quantity;
    }

    $scope.AvailableProductQuantityChange = function (product) {
        if (typeof product.OriginalQuantity === "undefined")
            product.OriginalQuantity = product.Quantity;
        var newQuantity = product.OriginalQuantity - product.AdjustedQuantity;
        $scope.AvailableProductQuantityDirty = newQuantity !== product.OriginalQuantity;
        product.Quantity = newQuantity;
    }

    $scope.NewBuildingUpgrade = function () {
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
        $scope.UpdateSupplyChain(function () {
            $.post("http://localhost:59892/api/city", {
                "BuildingUpgrades": GetBuildingUpgradesForPost($scope.city.BuildingUpgrades),
                "CurrentCityStorage": GetCurrentCityStorageForPost($scope.city.CurrentCityStorage)
            })
            .then(function (data) {
                $scope.city = data;
                $scope.$apply();
                $scope.CityStorageQuantityDirty = false;
                $scope.AvailableProductQuantityDirty = false;
                $scope.RequiredProductQuantityDirty = false;
                alert("Save Complete");
            });
        });
    }

    $scope.DropBuildingUpgradProduct = function (evt, ui, buildingUpgrade) {
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

    $scope.DropDelete = function (evt, ui) {
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

    $scope.onBuildingUpgradeProductChange = function () {
        console.log("build upgrade product change");
    }
    $scope.onUpdateStorage = function () {
        $scope.UpdateStorageFromOverstock();
        alert("Product Adjustments Complete");
    }
    $scope.UpdateStorageFromOverstock = function () {
        var removes = [];
        $.each($scope.city.AvailableStorage, function (index, product) {
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
        $.each(removes, function (index, val) {
            $scope.city.AvailableStorage.splice(index, 1);
        });

    }

});