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

    $scope.NewProductType = function () {
        $scope.currentProduct = {
            Name: "New Product Type",
            RequiredProducts: []
        };
    }

    $scope.setProductType = function (prod) {
        $scope.currentProduct = prod;
    }
    $scope.onNewRequiredProduct = function (currentProduct) {
        currentProduct.RequiredProducts.push({});
    }
    $scope.Goto = function (where) {
        $window.location.href = where;
    }
    $scope.SaveProductType = function () {
        console.log($scope.currentProduct);
        var requiredProducts = [];
        $.each($scope.currentProduct.RequiredProducts, function (index, obj) {
            requiredProducts.push({
                ProductTypeId: obj.ProductTypeId,
                Quantity: obj.Quantity
            });
        });
        $.post("http://localhost:59892/api/productType",
                {
                    Id: $scope.currentProduct.Id,
                    SalePriceInDollars: $scope.currentProduct.SalePriceInDollars,
                    Name: $scope.currentProduct.Name,
                    TimeToManufacture: $scope.currentProduct.TimeToManufacture,
                    RequiredProducts: requiredProducts,
                    ManufacturerTypeId: $scope.currentProduct.ManufacturerTypeWrapper.Id
                }
            )
            .then(function (data) {
                alert("Save Complete");
            });
    }
    $scope.RemoveRequiredProduct = function (curr, index) {
        curr.RequiredProducts.splice(index, 1);
    }
});