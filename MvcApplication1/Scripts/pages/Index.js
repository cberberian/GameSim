function GetInt(a) {
    return +a || 0;
}


function NewBuildingUpgrade() {

    AddNewBuildingUpgradeToAccordian();
    SetupInventoryListDragDropBehavior();
    $("#factoryItemsAccordian").accordion({ "active": "false" });

}

//Adds user controls to list and refreshes the accordion. 
function AddNewBuildingUpgradeToAccordian() {
    //next index
    var nextIndex = $(".property-upgrade-h3").length;

    //grab upgrade elements from the template. 
    var newUpgrade = $("#upgradeListTemplate").clone();
    var newUpgradeHeader = newUpgrade.find("h3").clone()
        .addClass("property-upgrade-h3")
        .draggable({ revert: true });//heading.
    var newUpgradeDiv = newUpgrade.find("div").clone();//ul list of inventory.
    newUpgradeDiv.find("ul").menu(); //make list a menu

    //set proper property upgrade name text
    newUpgradeHeader.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);
    newUpgradeDiv.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);

    //set the id property to a unique id using index
    var newId = "upgrade" + nextIndex;
    newUpgradeDiv.attr("id", newId);
    $("#upgradeListAccordian").append(newUpgradeHeader);
    $("#upgradeListAccordian").append(newUpgradeDiv);

    //refresh the accordion to resize it. 
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion({
        active: nextIndex +3
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
        receive: onUpgradeListReceive,
        remove: onInventoryListRemove
    }).disableSelection();
}

//event handler for receive droppable item on the target list
function onUpgradeListReceive (event, ui) { 
    //we want a clone of the original inventory item. 
    var clone = ui.item.clone();
    //add the spinner to allow quantity input of item
    $("<input/>", { "class": "inventory-item-count-spinner" }).appendTo(clone).spinner();

    $(this).append(clone);
    $(this).menu(); //need refresh after append

    //refresh the accordian parent to resize it to fit new menu item. 
    $("#upgradeListAccordian").accordion("refresh");
}

//event handler for removal of the draggable item from source list. 
function onInventoryListRemove(event, ui) {
    //We don't the item removed from the inventory list so we cancel
    $(this).sortable("cancel");
}

//Fetches details from server for inventory item. 
function ShowDetails() {
    $.ajax({
        url: "http://localhost:59892/api/LegacyProduct"
    }).then(function (data) {
        $("#itemName").text(data[0].Name);
        var pre = "";
        $.each(data[0].Prerequisites, function (index, value) {
            pre = pre + value.Name;
        });
        $("#prerequisites").text(pre);
        $("#inventoryItemDialogue").dialog();
    });
};

//populates the source inventory lists from server
function PopulateFacilities(afterPopulateMethod) {
    $.ajax(
        {
            url: "http://localhost:59892/api/ManufacturerType",
            error:
                function(xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }
        })
    .then(
        function (data) {
            var facilityDiv = $("#factoryItemsAccordian");
            //iterate the facility types and add
            //  a header and div for each type
            $.each(data, function (index, value) {
                facilityDiv.append(GetFacilityTypeHeader(value.Name)); //adds h3 
                facilityDiv.append(GetFacilityItemList(value.ProductTypes)); //adds div with ul
                AddProductTypesToCityStorage(value.ProductTypes);
            });
            $("#factoryItemsAccordian").accordion("refresh");
            if (afterPopulateMethod) //if specified call afterPopulate callback. 
                afterPopulateMethod();
        }
    );
}

function AddProductTypesToCityStorage(itemList) {
    var ul = $(".storage-list-ul");
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item.
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append(GetInventoryItemTypeIdSpan(value));
        listItem.append(GetInventoryItemTypeSpan(value.Id));
        //add the spinner to allow quantity input of item
        $("<input/>", { "class": "inventory-item-count-spinner", "id": "storageListProductAmount" + value.Id })
            .appendTo(listItem)
            .spinner();
            //.val(value.Quantity);
        $("<input/>", { "type": "checkbox", "class": "inventory-item-queued-to-manufacture" }).appendTo(listItem);
        $("<span/>", { "id": "span-city-storage-queued-time-" + value.ProductTypeId, "class": "inventory-item-queued-to-manufacture" }).appendTo(listItem);
    });
}

function GetFacilityTypeHeader(name) {
    var header = $("<h3\>");
    header.text(name);
    return header;
}

function GetCombinedItemList(itemList) {
    var div = $(".combined-list-div");
    var ul = $(".combined-list-ul");
    ul.empty();
    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item
        listItem.append($("<input\>", { "type" : "checkbox", "onclick" : "UpdateCityStorage(this, " + value.ProductTypeId + ")" }).appendTo(ul));
        listItem.append(GetInventoryItemNameSpan(value));
        listItem.append(GetInventoryItemTypeIdSpan(value));
        listItem.append(GetGetCombinedItemListQuantitySpan(value));
        listItem.append(GetInventoryItemQuantityDisplayText(value.Quantity, value.TotalDuration));
        
    });
    ul.menu();
    return div;
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

function GetTotalCombinedItemList(itemList) {
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

function GetAvaialbeList(itemList) {
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

function GetFacilityItemList(itemList) {
    var div = $("<div\>");
    var ul = $("<ul\>", { "class": "inventory-list inventory-source" }).appendTo(div);

    //iterate and append each inventory list item (<li/>)
    $.each(itemList, function (index, value) {
        var listItem = $("<li\>").appendTo(ul);
        //add name text to the inventory item.
        listItem.append(GetInventoryItemNameSpan(value)); 
        listItem.append(GetInventoryItemTypeSpan(value.Id));
    });

    ul.menu();
    return div;
}


function CreateSupplyChainStrategy() {
    var upgradeDivs = $(".upgrade-list-div"); //
    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs(upgradeDivs);
    
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

/**
    Functions to get identifier spans for update list products
 */
function GetInventoryItemNameSpan(value) {

    var span = $("<span\>", { "class": "inventory-item-name" });
    span.text(value.Name);
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

function GetInventoryItemTypeSpan(itemType) {
    var span = $("<span\>", { "class": "inventory-item-type" });
    span.text(itemType);
    return span;
}

function GetInventoryItemQuantityDisplayText(quantity, duration) {
    var jQuery = $("<span/>").text("quantity: " + quantity + ", " + duration + " min");
    return jQuery;
}

function GetPropertyUpgradesFromUpgradDivs(upgradeDivs) {
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

function PopulateSupplyChainResults(data) {
    GetCombinedItemList(data.RequiredProducts);
    GetTotalCombinedItemList(data.TotalProducts);
    GetAvaialbeList(data.AvailableStorage);
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion("option", "active", 0);
}

function RemoveUpgrade(head) {
    head.draggable("destroy");
    var div = head.next();
    div.add(head).fadeOut("slow",
        function() {
            head.remove();
            div.remove();//
            var names = $(".property-upgrade-h3");
            names.each(function (index, value) {
               var upgrade = index + 1;
               $(value).find("span.property-upgrade-name").text("Property Upgrade " + upgrade);
            });
        });
    
}