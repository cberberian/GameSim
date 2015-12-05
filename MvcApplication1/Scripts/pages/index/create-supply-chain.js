
function CreateSupplyChainStrategy() {
    
    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs();

    if (propertyUpgrades.length === 1) {
        alert("no upgrades to process");
    }
    var supplyChainRequest = {
        BuildingUpgrades: propertyUpgrades
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

function PopulateSupplyChainResults(data) {
    PopulateRequiredProductsFromSupplyChain(data.RequiredProducts);
    PopulateTotalProductsFromSupplyChain(data.TotalProducts);
    PropulateAvaialbeCityStorageFromSupplyChain(data.AvailableStorage);
    PropulateRequiredInInventory(data.RequiredProductsInCityStorage);
    PropulateUpgradeDurations(data.BuildingUpgrades);
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion("option", "active", 0);
}

function PropulateRequiredInInventory(itemList) {
    var div = $(".required-in-storage-div");
    var ul = $(".required-in-storage-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append($("<span/>").text("required: " + value.Quantity));

    });
    ul.menu();
    return div;
}

function PropulateUpgradeDurations(upgrades) {
    $.each(upgrades, function(index, data) {
        var nameSpan = $("span").filter(function () { return ($(this).text() === data.Name) });
        var parent = nameSpan.parent();
        var timeRemainingString = MinutesToHours(data.RemainingUpgradeTime) + " of " + MinutesToHours(data.TotalUpgradeTime) + " remaining";
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
        listItem.append($("<input\>", { "type": "checkbox", "onclick": "UpdateCityStorage(this, " + value.ProductTypeId + ")" }).appendTo(ul));
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append(GetInventoryItemTypeIdSpan(value));
        listItem.append(GetGetCombinedItemListQuantitySpan(value));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));

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
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));

    });
    ul.menu();
    return div;
}

function PropulateAvaialbeCityStorageFromSupplyChain(itemList) {
    var div = $(".available-storage-div");
    var ul = $(".available-storage-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append($("<span/>").text("available: " + value.Quantity));

    });
    ul.menu();
    return div;
}
