function GetCityStorageQuantitySpinner(product) {
    var ret = $("<input/>", { "type": "text", "class": "inventory-item-count-spinner", "id": "storageListProductAmount" + product.ProductTypeId });
    $(ret).val(product.Quantity);
    return ret;
}

function GetProductListItem(productType) {
    var li = $("<li/>");
    li.append(GetProductTypeNameSpan(productType.Name, productType.RequiredProductsToolTip));
    li.append(GetProductTypeIdSpan(productType.Id));
    return li;
}

function GetProductTypeNameSpan(productTypeName, tooltip) {
    var nameSpan = $("<li/>", { "class": "inventory-item-name", "title": "" })
        .tooltip({
            content: tooltip,
            position: { my: "left+100", at: "left" }
        })
        .text(productTypeName);
    return nameSpan;
}
function GetProductTypeIdSpan(productTypeId) {//inventory-item-upgrade-id property-upgrade-hidden
    var typeSpan = $("<li/>", { "class": "inventory-item-type inventory-item-type-id" });
    typeSpan.text(productTypeId);
    return typeSpan;
}
function GetProductIdSpan(productId) {
    var typeSpan = $("<li/>", { "class": "inventory-item-id property-upgrade-hidden" });
    typeSpan.text(productId);
    return typeSpan;
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

function GetUpgradeListHeader(headerText) {
    var ret = $("<h3/>", { "class": "accordian-definition" });
    ret.text(headerText);
    return ret;
}
//
function GetUpgradeListDiv() {
    var ret = $("<div/>", { "class": "accordian-definition upgrade-list-div" });
    return ret;
}

function GetBuildingUpgradeDiv(buildingUpgrade) {
    var ret = GetUpgradeListDiv();
    ret.append(GetHiddenUpgradeName(buildingUpgrade));
    ret.append(GetBuildingUpgradeProductList(buildingUpgrade));
    return ret;

}

function GetHiddenUpgradeName(buildingUpgrade) {
    //    <span class="property-upgrade-name property-upgrade-hidden">@upgrade.Name</span>
    return $("<span/>", { "class": "property-upgrade-name property-upgrade-hidden" }).text(buildingUpgrade.Name);
}

function GetBuildingUpgradeProductList(buildingUpgrade) {
    //    <ul class="inventory-list inventory-source">
    var ret = $("<ul/>", { "class": "inventory-list inventory-source" });
    $.each(buildingUpgrade.Products, function (index, prod) {
        ret.append(GetBuildingUpgradeProductLine(prod));
    });
    ret.menu();
    return ret;

}

function GetBuildingUpgradeProductLine(prod) {
    var ret = $("<li/>");
    ret.append(GetProductTypeNameSpan(prod.Name, ""));
    ret.append(GetProductTypeIdSpan(prod.ProductTypeId));
    ret.append(GetProductIdSpan(prod.Id));
    ret.append(GetProductQuantityInput(prod.Quantity));
    return ret;
}

function GetProductQuantityInput(quantity) {
    var ret = $("<input/>", { "class": "inventory-item-count-spinner" });
    ret.val(quantity);
    return ret;
}

//Sets up drag/drop from inventory to property upgrade and
//  assigns event handlers 
function SetupInventoryListDragDropBehavior() {
    //note: all lists have this class (source inventory and property upgrade). 
    //  We should only be able to drag drop 
    //  between source inventory list and a property upgrade but
    //  this allows drop between source inventory lists. This is ok
    //  because only one list is visible at a time. If more than 
    //  one source list is visible at a time this should be changed
    //  to only allow from inventory to prop upgrade.
    $(".inventory-list").sortable({
        connectWith: ".inventory-list",
        stop: onUpgradeDragStart,
        receive: onUpgradeListReceive,
        remove: onInventoryListRemove
    }).disableSelection();
}

//event handler for removal of the draggable item from source list. 
function onInventoryListRemove(event, ui) {
    //We don't the item removed from the inventory list so we cancel
    $(this).sortable("cancel");
}

function onUpgradeDragStart(event, ui) {
    $(".inventory-item-name").tooltip("close");
}

//event handler for receive droppable item on the target list
function onUpgradeListReceive(event, ui) {
    //we want a clone of the original inventory item. 
    var source = $(ui.item);
    if (source.prop("tagName") !== "LI") //.prop("tagName")
        return;
    var clone = source.clone();
    clone.draggable(
    {
        helper: "clone",
        appendTo: "body"
    });
    var toolTipValue = source.find("span.inventory-item-name")
                             .tooltip("option", "content");

    clone.find("span.inventory-item-name")
        .tooltip({ content: toolTipValue });
    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner" })
        .appendTo(clone)
        .spinner();


    $(this).append(clone);
    $(this).menu(); //need refresh after append

    //refresh the accordian parent to resize it to fit new menu item. 
    $("#upgradeListAccordian").accordion("refresh");
}

//Sets up drag/drop from inventory to property upgrade and
//  assigns event handlers 
function SetupInventoryListDragDropBehavior() {
    //note: all lists have this class (source inventory and property upgrade). 
    //  We should only be able to drag drop 
    //  between source inventory list and a property upgrade but
    //  this allows drop between source inventory lists. This is ok
    //  because only one list is visible at a time. If more than 
    //  one source list is visible at a time this should be changed
    //  to only allow from inventory to prop upgrade.
    $(".inventory-list").sortable({
        connectWith: ".inventory-list",
        stop: onUpgradeDragStart,
        receive: onUpgradeListReceive,
        remove: onInventoryListRemove
    }).disableSelection();
}

//event handler for removal of the draggable item from source list. 
function onInventoryListRemove(event, ui) {
    //We don't the item removed from the inventory list so we cancel
    $(this).sortable("cancel");
}

function onUpgradeDragStart(event, ui) {
    $(".inventory-item-name").tooltip("close");
}

//event handler for receive droppable item on the target list
function onUpgradeListReceive(event, ui) {
    //we want a clone of the original inventory item. 
    var source = $(ui.item);
    if (source.prop("tagName") !== "LI") //.prop("tagName")
        return;
    var clone = source.clone();
    clone.draggable(
    {
        helper: "clone",
        appendTo: "body"
    });
    var toolTipValue = source.find("span.inventory-item-name")
                             .tooltip("option", "content");

    clone.find("span.inventory-item-name")
        .tooltip({ content: toolTipValue });
    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner" })
        .appendTo(clone)
        .spinner();


    $(this).append(clone);
    $(this).menu(); //need refresh after append

    //refresh the accordian parent to resize it to fit new menu item. 
    $("#upgradeListAccordian").accordion("refresh");
}

function onDropTrash(event, ui) {
    var src = $(ui.draggable);
    var upgradeAccordian = $("#upgradeListAccordian");
    if (src.hasClass("property-upgrade-h3")) {
        if (confirm("remove upgrade?")) {
            var div = src.next();
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

function UpdateSupplyChain() {
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
    }).then(function(data) {
        
    });
}
//div class="upgrade-list-div combined-list-div accordian-definition">combined-list-ul
function GetRequiredProductsFromCity(itemList) {
    var div = $("<div/>", { "class": "upgrade-list-div combined-list-div accordian-definition" });
    var ul = $("<ul/>", { "class": "combined-list-ul" }).appendTo(div);
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append(GetProductTypeNameSpan(value.Name, value.RequiredProductsToolTip));
        listItem.append(GetProductTypeIdSpan(value.ProductTypeId));
        listItem.append(GetGetCombinedItemListQuantitySpan(value.Quantity, value.ProductTypeId));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));
        listItem.append("&nbsp;&nbsp;");
        $("<input/>", { "class": "inventory-item-count-spinner required-product-count-spinner", "id": "combinedListProductSpinner" + value.ProductTypeId })
            .appendTo(listItem)
            .spinner();
    });
    ul.menu();
    return div;
}
