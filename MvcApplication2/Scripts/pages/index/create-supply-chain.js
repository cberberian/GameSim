
function CreateSupplyChainStrategy() {
//        UpdateStorageFromRequiredProducts();
    var requiredProductUpdates = GetRequiredProductUpdates();
    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs();

    if (propertyUpgrades.length === 1) {
        alert("no upgrades to process");
    }
    var supplyChainRequest = {
        BuildingUpgrades: propertyUpgrades,
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
    }).then(PopulateSupplyChainResults);
}

function GetRequiredProductUpdates() {
    var requiredSpinners = $(".required-product-count-spinner");
    var ret = [];
    $.each(requiredSpinners, function (index, spinner) {
        var rqPrdQtySpinner = $(spinner);
        var chngStrgeQty = GetInt(rqPrdQtySpinner.spinner("value"));
        var prdTypId = rqPrdQtySpinner.attr("id").substr("combinedListProductSpinner".length);
        
        ret.push({
            ProductTypeId: prdTypId,
            Quantity: chngStrgeQty
        });
    });
    return ret;
}

function UpdateStorageFromRequiredProducts() {
    var requiredSpinners = $(".required-product-count-spinner");
    $.each(requiredSpinners, function(index, spinner) {
        var rqPrdQtySpinner = $(spinner);
        var chngStrgeQty = GetInt(rqPrdQtySpinner.spinner("value"));
        var prdTypId = rqPrdQtySpinner.attr("id").substr("combinedListProductSpinner".length);
        var storegLstQtySpinner = $("#storageListProductAmount" + prdTypId);
        var curStrgeQty = GetInt(storegLstQtySpinner.spinner("value"));

        var newQty = curStrgeQty + chngStrgeQty;
        if (newQty < 0)
            newQty = 0;
        storegLstQtySpinner.spinner("value", newQty);
    });
}

function PopulateSupplyChainResults(data) {
    PopulateRequiredProductsFromSupplyChain(data.RequiredProducts);
    PopulateTotalProductsFromSupplyChain(data.TotalProducts);
    PopulateAvaialbeCityStorageFromSupplyChain(data.AvailableStorage);
    PopulateRequiredInInventory(data.RequiredProductsInCityStorage);
    PopulateUpgradeDurations(data.BuildingUpgrades);
    $.each(data.CurrentCityStorage.CurrentInventory, function (index, product) {
        $("#storageListProductAmount" + product.ProductTypeId).spinner("value", product.Quantity);
    });
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion("option", "active", 0);
}

function PopulateRequiredInInventory(itemList) {
    var div = $(".required-in-storage-div");
    var ul = $(".required-in-storage-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append(GetInventoryItemNameSpan(value.Name, value.RequiredProductsToolTip));
        listItem.append($("<span/>").text("required: " + value.Quantity));

    });
    ul.menu();
    return div;
}

function PopulateUpgradeDurations(upgrades) {
    $.each(upgrades, function(index, data) {
        var nameSpan = $("span").filter(function () { return ($(this).text() === data.Name) });
        var parent = nameSpan.parent();
        var timeRemainingString = MinutesToHours(data.RemainingUpgradeTime) + " of " + MinutesToHours(data.TotalUpgradeTime) + " remaining<br/>";
        var requiredProducts = data.Products;
        if (requiredProducts) {
            for (var i = 0; i < requiredProducts.length; i++) {
                var prdInStorage = getObjects(data.ProductsInStorage, "ProductTypeId", requiredProducts[i].ProductTypeId);
                var inStorage = prdInStorage[0];
                if (prdInStorage.length > 0)
                //                    timeRemainingString += prdInStorage.Quantity + " " + data.ProductsInStorage[i].Name + " of " + + "already in storage<br/>";
                    timeRemainingString = timeRemainingString.concat(inStorage.Quantity, " ", requiredProducts[i].Name, " of ", requiredProducts[i].Quantity, " required");
                else
                    timeRemainingString = timeRemainingString.concat(requiredProducts[i].Quantity, " of ", requiredProducts[i].NAME, " required");
            }
        }
        
        parent.find(".progress-bar-container")
            .progressbar({ value: data.TotalUpgradeTime - data.RemainingUpgradeTime, max: data.TotalUpgradeTime });
        parent.find(".progress-bar-tooltip").tooltip({ content: timeRemainingString, position: { my: "right", at: "right" } });


    });
}

function PopulateRequiredProductsFromSupplyChain(itemList) {
    var div = $(".combined-list-div");
    var ul = $(".combined-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        var input = $("<input\>");

        listItem.append(GetInventoryItemNameSpan(value.Name, value.RequiredProductsToolTip));
        listItem.append(GetInventoryItemTypeIdSpan(value.ProductTypeId));
        listItem.append(GetGetCombinedItemListQuantitySpan(value.Quantity, value.ProductTypeId));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));
        listItem.append("&nbsp;&nbsp;");
        $("<input/>", { "class": "inventory-item-count-spinner required-product-count-spinner", "id": "combinedListProductSpinner" + value.ProductTypeId })
            .appendTo(listItem)
            .spinner(
                {
                    change: function(event, ui) {   
//                        onUpdateCityStorage($(this).spinner("value"), value.ProductTypeId);
                    }
                }
            );
    });
    ul.menu();
    return div;
}

function PopulateTotalProductsFromSupplyChain(itemList) {
    var div = $(".total-combined-list-div");
    var ul = $(".total-combined-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append($("<input\>", { "type": "checkbox" }).appendTo(ul));
        listItem.append(GetInventoryItemNameSpan(value.Name, value.RequiredProductsToolTip));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));

    });
    ul.menu();
    return div;
}

function PopulateAvaialbeCityStorageFromSupplyChain(itemList) {
    var div = $(".available-storage-div");
    var ul = $(".available-storage-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append(GetInventoryItemNameSpan(value.Name, value.RequiredProductsToolTip));
        listItem.append($("<span/>").text("available: " + value.Quantity));

    });
    ul.menu();
    return div;
}

function GetPropertyUpgradesFromUpgradDivs() {
    var upgradeDivs = $(".upgrade-list-div"); //
    var ret = [];
    $.each(upgradeDivs, function (index, value) {
        var propertyUpgradeDiv = $(value);
        var propertyUpgradeName = propertyUpgradeDiv.find("span.property-upgrade-name").first().text();
        if (propertyUpgradeName === "")
            return;
        var propertyUpgradeId = propertyUpgradeDiv.find("span.property-upgrade-id").first().text();
        var requiredProducts = GetInventoryItemsFromUlList(propertyUpgradeDiv);
        ret.push(
            {
                Id: propertyUpgradeId,
                Name: propertyUpgradeName,
                Products: requiredProducts
            }
        );
    });
    return ret;
}


