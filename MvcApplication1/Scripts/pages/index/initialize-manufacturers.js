//populates the source inventory lists from server
function PopulateFacilities(afterPopulateMethod) {
    $.ajax(
        {
            url: "http://localhost:59892/api/ManufacturerType",
            error:
                function (xhr, ajaxOptions, thrownError) {
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

function GetFacilityTypeHeader(name) {
    var header = $("<h3\>");
    header.text(name);
    return header;
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

