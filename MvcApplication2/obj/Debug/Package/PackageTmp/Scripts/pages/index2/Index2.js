function InitializePage() {
    //    $("#tabs").tabs();
    $(".inventory-list").menu();
    $(".inventory-item-count-spinner").spinner();
    $(".inventory-item-name").tooltip();
    $(".property-upgrade-h3").draggable({ revert: true });

    $("#factoryItemsAccordian")
        .accordion({
            "collapsible": "true",
            "heightStyle": "content",
            "active": "false"
        });
    $("#upgradeListAccordian")
        .filter(":has(.accordian-definition)")
        .accordion({
            "collapsible": "true",
            "heightStyle": "content",
            "active": "false"
        });
    $("#btnUpdateSupplyChain").button();

    $("#divTrash").droppable({
        tolerance: "touch",
        drop: onDropTrash
    });

    SetupInventoryListDragDropBehavior();
    UpdateHeaders();
    UpdateSupplyChain(PopulateSupplyChainResults);
//    UpdateStorageQuantities();
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

function NewBuildingUpgrade() {

    AddEmptyBuildingUpgradeToAccordian();
    SetupInventoryListDragDropBehavior();
    $("#factoryItemsAccordian").accordion({ "active": "false" });

}

//Adds user controls to list and refreshes the accordion. 
function AddEmptyBuildingUpgradeToAccordian() {
    //next index
    var nextIndex = $(".property-upgrade-h3").length;

    //grab upgrade elements from the template. 
    var newUpgrade = $("#upgradeListTemplate").clone();
    var newUpgradeHeader = newUpgrade.find("h3")
        .clone();
    newUpgradeHeader.draggable({
        revert: true
    });
    //.draggable({ revert: true });//heading.
    var newUpgradeDiv = newUpgrade.find("div").clone();//ul list of inventory.
    newUpgradeDiv.find("ul").menu(); //make list a menu

    if (nextIndex === 1)
        newUpgradeHeader.find(".btn-move-up").hide();

    newUpgradeHeader.find(".btn-move-down").hide();

    //set proper property upgrade name text
    newUpgradeHeader.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);
    newUpgradeHeader.find("input.property-upgrade-name").val("Property Upgrade " + nextIndex);
    newUpgradeDiv.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);

    //set the id property to a unique id using index
    $("#upgradeListAccordian").append(newUpgradeHeader);
    $("#upgradeListAccordian").append(newUpgradeDiv);

    //refresh the accordion to resize it. 
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion({
        active: nextIndex + 5
    });
    UpdateHeaders();

}

function UpdateHeaders() {
    var names = $(".property-upgrade-h3");
    var count = names.length;
    names.each(function (index, header) {
        var upgrade = index;
        var jqHeader = $(header);
        var jqDiv = jqHeader.next();
        var hdrName = jqHeader.find("input.property-upgrade-name").val();
        if (hdrName === "") {
            hdrName = jqHeader.find("span.property-upgrade-name").text();
            if (hdrName === "")
                return;
        }
//        hdrName.text("Property Upgrade " + upgrade);
        jqDiv.find("span.property-upgrade-name").text(hdrName);
        if (upgrade === 0) {
            jqHeader.find(".btn-move-up").hide();
        }
        else {
            jqHeader.find(".btn-move-up").show();
        }

        var siblingCount = count - 1;
        if (index === siblingCount) {
            jqHeader.find(".btn-move-down").hide();
        }
        else {
            jqHeader.find(".btn-move-down").show();
        }

    });
}

/**
 * Supply Chain Functionality updates quantities
 * @returns {} 
 */
function UpdateSupplyChain(callback) {
    //        UpdateStorageFromRequiredProducts();
    var requiredProductUpdates = GetRequiredProductUpdates();
    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs();
    if (!callback)
        callback = PopulateSupplyChainResults;
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
    }).then(callback);
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

function GetPropertyUpgradesFromUpgradDivs() {
    var upgradeDivs = $(".upgrade-list-div"); //
    var ret = [];
    $.each(upgradeDivs, function (index, value) {
        var propertyUpgradeDiv = $(value);
        var first = propertyUpgradeDiv.prev().find("input.property-upgrade-name").first();
        var propertyUpgradeName = first.val();
        if (!propertyUpgradeName)
            propertyUpgradeName = propertyUpgradeDiv.find("span.property-upgrade-name").first().text();
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

function GetInventoryItemsFromUlList(propertyUpgradeDiv) {
    var ret = [];
    var propertyUpgradeListItems = propertyUpgradeDiv.find("li");
    $.each(propertyUpgradeListItems, function (liIndex, liValue) {
        var inventoryItem = $(liValue);
        var itemName = inventoryItem.find("span.inventory-item-name").text();
        if (itemName === "") //if no item name then skip
            return;
        var itemType = inventoryItem.find("span.inventory-item-type-id").text();
        var itemId = inventoryItem.find("span.inventory-item-id").text();
        var upgradeId = inventoryItem.find("span.inventory-item-upgrade-id").text();
        var itemAmount = GetInt(inventoryItem.find("input.inventory-item-count-spinner").spinner("value"));

        ret.push({
            BuildingUpgradeId: upgradeId,
            Id: itemId,
            ProductTypeId: itemType,
            Name: itemName,
            Quantity: itemAmount
        });
    });
    return ret;
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
            .spinner();
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
    $.each(upgrades, function (index, data) {
        var nameSpan = $("input").filter(function () { return ($(this).val() === data.Name) });
        var parent = nameSpan.parent();

        parent.find(".progress-bar-container")
            .progressbar({ value: data.TotalUpgradeTime - data.RemainingUpgradeTime, max: data.TotalUpgradeTime });
        parent.find(".progress-bar-tooltip").tooltip({ content: data.Tooltip, position: { my: "right", at: "right" } });
        var contentDiv = parent.next().find(".inventory-item-name");
        $.each(data.Products, function(index, prd) {
            var span = contentDiv.find("span:contains(" + prd.Name + ")");
            if (span) {
                var parent1 = span.parent();
                parent1.tooltip({ content : prd.RequiredProductsToolTip });
            }
        });
        

    });
}

function UpdateStorageQuantities() {
    $.ajax({
        url: "http://localhost:59892/api/Product?$filter=IsCityStorage eq true",
        error:
            function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
    }).then(function (products) {
        $.each(products, function (index, product) {
            $("#storageListProductAmount" + product.ProductTypeId).spinner("value", product.Quantity);
        });
    });
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

function GetInventoryItemIdSpan(value) {

    var span = $("<span\>", { "class": "inventory-item-id property-upgrade-hidden" });
    span.text(value);
    return span;
}

function GetInventoryItemUpgradeIdSpan(value) {
    var span = $("<span\>", { "class": "inventory-item-upgrade-id property-upgrade-hidden" });
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

function SaveCity() {
    UpdateSupplyChain(function (data) {
        PopulateSupplyChainResults(data);
        var city = GetCityForSave();
        $.post("http://localhost:59892/api/city", city)
           .then(function (data) {
               //RefreshBuildingUpgrades();
               document.location.reload();
                
            });
        }
    );
    
}

function GetCityForSave() {

    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs();
    return { "BuildingUpgrades": propertyUpgrades }
}

function RefreshBuildingUpgrades() {
    var header = $("input.property-upgrade-name").parent();
    header.next().remove();
    header.remove();
    GetBuildingUpgrades();
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
    listItem.append(GetInventoryItemIdSpan(product.Id));
    listItem.append(GetInventoryItemUpgradeIdSpan(product.BuildingUpgradeId));

    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner" })
        .appendTo(listItem)
        .spinner()
        .val(product.Quantity);
    //refresh the accordian parent to resize it to fit new menu item. 
}
