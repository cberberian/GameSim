/// <reference path="initialize-manufacturers.js" />
function GetInt(a) {
    return +a || 0;
}

function UpdateCityStorage(source, productTypeId) {
    //get spinner to update
    var storageSpinner = $("#storageListProductAmount" + productTypeId);
    var quantitySpan = $("#combined_List_Quantity" + productTypeId);
    if (!quantitySpan)
        return;
    var quanityToUpdate = GetInt(quantitySpan.text());
    var currentValue = GetInt(storageSpinner.val());
    if (quanityToUpdate < 1)
        return;
    var updatedValue = currentValue + quanityToUpdate;
    if (updatedValue < 0)
        updatedValue = 0;
    //get checked or unchecked value 
    if ($(source).is(":checked"))
        storageSpinner.val(updatedValue);
    else 
        storageSpinner.val(updatedValue);

}

function GetInventoryItemTypeSpan(itemType) {
    var span = $("<span\>", { "class": "inventory-item-type" });
    span.text(itemType);
    return span;
}

/**
    Functions to get identifier spans for update list products
 */
function GetInventoryItemNameSpan(value) {

    var span = $("<span\>", { "class": "inventory-item-name", "title" : "" })
        .tooltip({
            content: value.RequiredProductsToolTip,
            position: { my: "left+100", at: "left" }
        })
        .text(value.Name);
    return span;
}

function GetInventoryItemTypeIdSpan(value) {

    var span = $("<span\>", { "class": "inventory-item-type inventory-item-type-id" });
    span.text(value.ProductTypeId);
    return span;
}

function GetGetCombinedItemListQuantitySpan(value) {

    var span = $("<span\>", { "class": "inventory-item-quantity", "id": "combined_List_Quantity" + value.ProductTypeId });
    span.text(value.Quantity);
    return span;
}

function GetInventoryItemQuantityDisplayText(quantity, duration) {
    var minutesToHours = MinutesToHours(duration);
    var jQuery = $("<span/>").text("quantity: " + quantity + ", " + minutesToHours);
    return jQuery;
}

function GetPropertyUpgradesFromUpgradDivs() {
    var upgradeDivs = $(".upgrade-list-div"); //
    var ret = [];
    $.each(upgradeDivs, function (index, value) {
        var propertyUpgradeDiv = $(value);
        var propertyUpgradeName = propertyUpgradeDiv.find("span.property-upgrade-name").first().text();
        if (propertyUpgradeName === "")
            return;
        var propertyUpgradeListItems = propertyUpgradeDiv.find("li");
        var requiredProducts = GetInventoryItemsFromUlList(propertyUpgradeListItems);
        ret.push(
            {
                Name: propertyUpgradeName,
                Products: requiredProducts
            }
        );
    });
    return ret;
}

function GetInventoryItemsFromUlList(propertyUpgradeListItems) {
    var ret = [];
    $.each(propertyUpgradeListItems, function (liIndex, liValue) {
        var inventoryItem = $(liValue);
        var itemName = inventoryItem.find("span.inventory-item-name").text();
        if (itemName === "") //if no item name then skip
            return;
        var itemType = inventoryItem.find("span.inventory-item-type").text();
        var itemAmount = inventoryItem.find("input.inventory-item-count-spinner").spinner("value");
        ret.push({
            ProductTypeId: itemType,
            Name: itemName,
            Quantity: itemAmount
        });
    });
    return ret; 
}

function RemoveUpgrade(head) {
    //    head.draggable("destroy");
//    var parent = $(this).closest("div");
//    parent.add(head).fadeOut("slow", function () { $(this).remove(); });
    var id = head.attr("id");
    var div = $("#upgrade" + id.substr(6));
    
    div.fadeOut("slow",
        function () {
            div.remove();
            head.remove();
            RenameHeaders();
        });
    
}

function RenameHeaders() {
    var names = $(".property-upgrade-h3");
    names.each(function (index, value) {
        var upgrade = index + 1;
        $(value).find("span.property-upgrade-name").text("Property Upgrade " + upgrade);
    });
}

function onDropTrash(event, ui) {
    var src = $(ui.draggable);
    var upgradeAccordian = $("#upgradeListAccordian");
    if (src.hasClass("property-upgrade-h3")) {
                        
        if (confirm("remove upgrade?")) {
            var lastActive = upgradeAccordian.accordion("option", "active");
            //                            RemoveUpgrade(src);
                            
            var div = src.next();

            div.fadeOut("slow",
                function () {
                    div.remove();
                    src.remove();
                    RenameHeaders();
                    upgradeAccordian.accordion("option", "active", lastActive - 1);
                });

        }
    } else {
        if (confirm("remove item from list?")) {
            src.remove();
            upgradeAccordian.accordion("refresh");
        }
    }
                    
}
