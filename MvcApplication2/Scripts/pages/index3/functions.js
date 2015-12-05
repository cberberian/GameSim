
function GetBuildingUpgradesForPost(originalUpgrades) {
    var buildingUpgrades = [];
    $.each(originalUpgrades, function (index, bu) {
        buildingUpgrades.push(
        {
            Id: bu.Id,
            Name: bu.Name,
            CalculateInBuildingUpgrades: bu.CalculateInBuildingUpgrades,
            Products: GetProductUpgradesForPost(bu.Products)
        });
    });
    return buildingUpgrades;
}

function GetProductUpgradesForPost(originalProducts) {
    var products = [];
    $.each(originalProducts, function (index, prod) {
        var productQuantity = prod.Quantity ? prod.Quantity : 0;
        products.push(
        {
            Id: prod.Id,
            BuildingUpgradeId: prod.BuildingUpgradeId,
            ProductTypeId: prod.ProductTypeId,
            Quantity: productQuantity,
            Name: prod.Name
        });
    });
    return products;
}

function GetCurrentCityStorageForPost(currentCityStorage) {
    return {
        CurrentInventory: GetProductUpgradesForPost(currentCityStorage.CurrentInventory)
    }
}