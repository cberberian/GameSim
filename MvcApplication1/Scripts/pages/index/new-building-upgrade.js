
function NewBuildingUpgrade() {

    AddNewBuildingUpgradeToAccordian();
    SetupInventoryListDragDropBehavior();
    $("#factoryItemsAccordian").accordion({ "active": "false" });

}

//Adds user controls to list and refreshes the accordion. 
function AddNewBuildingUpgradeToAccordian() {
    //next index
    var nextIndex = $(".property-upgrade-h3").length - 1;

    //grab upgrade elements from the template. 
    var newUpgrade = $("#upgradeListTemplate").clone();
    var newUpgradeHeader = newUpgrade.find("h3")
        .clone();
//    newUpgradeHeader.draggable({
//        revert: true
//    });
    //.draggable({ revert: true });//heading.
    var newUpgradeDiv = newUpgrade.find("div").clone();//ul list of inventory.
    newUpgradeDiv.find("ul").menu(); //make list a menu

    //set proper property upgrade name text
    newUpgradeHeader.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);
    newUpgradeDiv.find("span.property-upgrade-name").text("Property Upgrade " + nextIndex);

    //set the id property to a unique id using index
    $("#upgradeListAccordian").append(newUpgradeHeader);
    $("#upgradeListAccordian").append(newUpgradeDiv);

    //refresh the accordion to resize it. 
    $("#upgradeListAccordian").accordion("refresh");
    $("#upgradeListAccordian").accordion({
        active: nextIndex + 5
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