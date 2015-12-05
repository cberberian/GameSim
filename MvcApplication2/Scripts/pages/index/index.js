/// <reference path="~/Scripts/pages/index/initialize-manufacturers.js" />
/// <reference path="~/Scripts/pages/index/new-building-upgrade.js" />
/// <reference path="~/Scripts/pages/index/create-supply-chain.js" />
/// <reference path="~/Scripts/common/common.js" />

function InitializePage() {
//    $("#tabs").tabs();
    $(".inventory-list").menu();
    $(".inventory-item-count-spinner").spinner();
    $("#factoryItemsAccordian")
        .accordion({ "collapsible": "true", "heightStyle": "content" });
    $("#upgradeListAccordian")
        .filter(":has(.accordian-definition)")
        .accordion({ "collapsible": "true", "heightStyle": "content" });
    $("#btnCreateSupplyChainStrategy").button();

    $("#divTrash").droppable({
        tolerance: "touch",
        drop: onDropTrash
    });
    PopulateFacilities(FinishInitialize, false);
    
}

function FinishInitialize() {
    UpdateStorageQuantities();
    GetBuildingUpgrades();
//    NewBuildingUpgrade();
}

function GetBuildingUpgrades() {
    $.ajax({
        url: "http://localhost:59892/api/BuildingUpgrade?$filter=Completed eq false",
        error:
            function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
    }).then(function (buildingUpgrades) {
        var count = buildingUpgrades.length;
        $.each(buildingUpgrades, function (index, upgrade) {
            AddBuildingUpgradeFromServer(index, count, upgrade);
        });
        SetupInventoryListDragDropBehavior();
//        CreateSupplyChainStrategy();
    });
}

function AddBuildingUpgradeFromServer(index, count, upgrade) {
    
    //grab upgrade elements from the template. 
    var newUpgrade = $("#upgradeListTemplate").clone();
    var newUpgradeHeader = newUpgrade.find("h3")
        .clone();
    newUpgradeHeader.draggable({
        revert: true
    });
    //.draggable({ revert: true });//heading.
    var newUpgradeDiv = newUpgrade.find("div").clone();//ul list of inventory.
    var newUl = newUpgradeDiv.find("ul");
    

    if (index === 0)
        newUpgradeHeader.find(".btn-move-up").hide();
    if (index === (count - 1))
        newUpgradeHeader.find(".btn-move-down").hide();

    //set proper property upgrade name text
    newUpgradeHeader.find("span.property-upgrade-name").text(upgrade.Name);
    newUpgradeDiv.find("span.property-upgrade-name").text(upgrade.Name);

    //set the id property to a unique id using index
    $("#upgradeListAccordian").append(newUpgradeHeader);
    $("#upgradeListAccordian").append(newUpgradeDiv);

    $.each(upgrade.Products, function (index, product) {
        AddUpgradeProduct(product, newUl);
    });
    
    newUl.menu(); //make list a menu

    //refresh the accordion to resize it. 
    $("#upgradeListAccordian").accordion("refresh");
    UpdateHeaders();
}

function AddUpgradeProduct(product, ul) {
    //we want a clone of the original inventory item. 
    var listItem = $("<li\>").appendTo(ul);
    listItem.draggable(
        {
            helper: "clone",
            appendTo: "body"
        }
    );
    //add name text to the inventory item.
    listItem.append(GetInventoryItemNameSpan(product.Name, product.RequiredProductsToolTip));
    listItem.append(GetInventoryItemTypeIdSpan(product.ProductTypeId));
    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner" })
        .appendTo(listItem)
        .spinner()
        .val(product.Quantity);
    //refresh the accordian parent to resize it to fit new menu item. 
}

function onUpdateCityStorage(qty, productTypeId) {
    //get spinner to update
    var ctyStorageSpinner = $("#storageListProductAmount" + productTypeId);
    var currentValue = GetInt(ctyStorageSpinner.val());

    var newCtyStorageVal = currentValue + qty;
    if (newCtyStorageVal < 0)
        newCtyStorageVal = 0;
    //get checked or unchecked value 
    ctyStorageSpinner.val(newCtyStorageVal);
}

function GetInventoryItemTypeSpan(itemType) {
    var span = $("<span\>", { "class": "inventory-item-type" });
    span.text(itemType);
    return span;
}

/**
    Functions to get identifier spans for update list products
 */
function GetInventoryItemNameSpan(value, requiredToolTip) {

    var span = $("<span\>", { "class": "inventory-item-name", "title": "" })
        .tooltip({
            content: requiredToolTip,
            position: { my: "left+100", at: "left" }
        })
        .text(value);
    return span;
}

function GetInventoryItemTypeIdSpan(value) {

    var span = $("<span\>", { "class": "inventory-item-type inventory-item-type-id" });
    span.text(value);
    return span;
}

function GetGetCombinedItemListQuantitySpan(value, productTypeId) {

    var span = $("<span\>", { "class": "inventory-item-quantity", "id": "combined_List_Quantity" + productTypeId });
    span.text(value);
    return span;
}

function GetInventoryItemQuantityDisplayText(quantity, duration) {
    var minutesToHours = MinutesToHours(duration);
    var jQuery = $("<span/>", { "class": "inventory-item-quantity-display-text" }).text("Qty: " + quantity + " Duration: " + minutesToHours + + "   ");
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


function GetInventoryItemsFromUlList(propertyUpgradeDiv) {
    var ret = [];
    var propertyUpgradeListItems = propertyUpgradeDiv.find("li");
    $.each(propertyUpgradeListItems, function (liIndex, liValue) {
        var inventoryItem = $(liValue);
        var itemName = inventoryItem.find("span.inventory-item-name").text();
        if (itemName === "") //if no item name then skip
            return;
        var itemType = inventoryItem.find("span.inventory-item-type-id").text();
        var itemAmount = GetInt(inventoryItem.find("input.inventory-item-count-spinner").spinner("value"));

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
            UpdateHeaders();
        });

}

function UpdateHeaders() {
    var names = $(".property-upgrade-h3");
    var count = names.length;
    names.each(function (index, header) {
        var upgrade = index;
        var jqHeader = $(header);
        var jqDiv = jqHeader.next();
        var hdrName = jqHeader.find("span.property-upgrade-name");
        if (hdrName.text() === "")
            return;
        hdrName.text("Property Upgrade " + upgrade);
        jqDiv.find("span.property-upgrade-name").text("Property Upgrade " + upgrade);
        if (upgrade === 1) {
            jqHeader.find(".btn-move-up").hide();
        }
        else {
            jqHeader.find(".btn-move-up").show();
        }

        var siblingCount = count - 1;
        if (index === siblingCount) {
            jqHeader.find(".btn-move-down").hide();
        }
        else
        {
            jqHeader.find(".btn-move-down").show();
        }

    });
}

function onDropTrash(event, ui) {
    var src = $(ui.draggable);
    var upgradeAccordian = $("#upgradeListAccordian");
    if (src.hasClass("property-upgrade-h3")) {

        if (confirm("remove upgrade?")) {
//            var lastActive = upgradeAccordian.accordion("option", "active");
//            //                            RemoveUpgrade(src);
            var div = src.next();
//
//            div.fadeOut("slow",
//                function () {
//                    div.remove();
//                    src.remove();
//                    UpdateHeaders();
//                    upgradeAccordian.accordion("option", "active", lastActive - 1);
//                });
            DeleteUpgrade(div, src);

        }
    } else {
        if (confirm("remove item from list?")) {
            src.remove();
            upgradeAccordian.accordion("refresh");
        }
    }

}

function DeleteUpgrade(div, header) {
    var upgradeAccordian = $("#upgradeListAccordian");
    var lastActive = upgradeAccordian.accordion("option", "active");
    div.fadeOut("slow",
                function () {
                    div.remove();
                    header.remove();
                    UpdateHeaders();
                    upgradeAccordian.accordion("option", "active", lastActive - 1);
                });
}

function PopulateCityStorage() {
    $.ajax({
        url: "http://localhost:59892/api/ProductType",
        error:
            function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
    }).then(AddProductTypesToCityStorageWithUpdate);
    
}

function UpdateStorageQuantities() {
    $.ajax({
        url: "http://localhost:59892/api/Product?$filter=IsCityStorage eq true",
        error:
            function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
    }).then(function(products) {
        $.each(products, function (index, product) {
            $("#storageListProductAmount" + product.ProductTypeId).spinner("value", product.Quantity);
        });
    });
}

/**
 * Adds Product types to city storage and updates the quantities. 
 * @param {} prdTypeLst 
 * @returns {} 
 */
function AddProductTypesToCityStorageWithUpdate(prdTypeLst) {
    AddProductTypesToCityStorage(prdTypeLst);
    UpdateStorageQuantities();
}

/**
 * Adds the specified product type list to the City Storage list
 *      This was used when we were not populating from the database.
 *      note: quanity defaults to zero 
 *      
 * @param {} prdTypeLst 
 * @returns {} 
 */
function AddProductTypesToCityStorage(prdTypeLst) {
    var ul = $(".storage-list-ul");
    $.each(prdTypeLst, function (index, prdType) {
        AppendListItemToStorageList(ul, prdType.Name, prdType.Id, 0, prdType.RequiredProductsToolTip);
    });
}

/**
 * Adds a new Product row to the City storage List
 * @param {} ul 
 * @param {} propertyUpgradeName 
 * @param {} productTypeId 
 * @param {} quantity 
 * @returns {} 
 */
function AppendListItemToStorageList(ul, propertyUpgradeName, productTypeId, quantity, requiredProductsToolTip) {

    var listItem = $("<li\>").appendTo(ul);
    //add name text to the inventory item.

    listItem.append(GetInventoryItemNameSpan(propertyUpgradeName, requiredProductsToolTip));
    listItem.append(GetInventoryItemTypeIdSpan(productTypeId));
    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner", "id": "storageListProductAmount" + productTypeId })
        .appendTo(listItem)
        .spinner()
        .val(quantity);
    //.val(value.Quantity);
    $("<input/>", { "type": "checkbox", "class": "inventory-item-queued-to-manufacture" }).appendTo(listItem);
    $("<span/>", { "id": "span-city-storage-queued-time-" + productTypeId, "class": "inventory-item-queued-to-manufacture" }).appendTo(listItem);
}

function onBtnCompleteUpgradeClick(context) {

    if (!confirm("Complete Upgrade?"))
        return;
    var header = $(context).closest("h3");
    var div = header.next();
    var inventoryItems = GetInventoryItemsFromUlList(div);
    $.each(inventoryItems, function (index, product) {
        var storageListSpinner = $("#storageListProductAmount" + product.ProductTypeId);
        var currentInventory = GetInt(storageListSpinner.spinner("value"));
        var quantityToDecrease = product.Quantity;
        var newStorage = currentInventory - quantityToDecrease;
        storageListSpinner.spinner("value", newStorage < 0 ? 0 : newStorage);
    });
    DeleteUpgrade(div, header);

}

function onMoveUpClick(context) {
    var upgradeAccordian = $("#upgradeListAccordian");
    var header = $(context).closest("h3");
    var div = header.next();
    var newIndex = header.index() - 3;
    var insertAfter = header.siblings(":eq(" + newIndex + ")");
    header.insertAfter(insertAfter);
    div.insertAfter(header);
    upgradeAccordian.accordion("refresh");
    UpdateHeaders();
    
}


function onMoveDownClick(context) {
    var upgradeAccordian = $("#upgradeListAccordian");
    var header = $(context).closest("h3");
    var div = header.next();

    var newIndex = header.index() + 2;
    var insertAfter = header.siblings(":eq(" + newIndex + ")");
    header.insertAfter(insertAfter);
    div.insertAfter(header);
    upgradeAccordian.accordion("refresh");
    UpdateHeaders();
    
}

