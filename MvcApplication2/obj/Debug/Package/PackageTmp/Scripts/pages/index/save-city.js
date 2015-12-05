
function SaveCity() {
    var city = GetCityForSave();

    $.post("http://localhost:59892/api/city", city)
       .then(function (data) {
            alert("save completed successfully");
        });
}

function GetCityForSave() {

    var propertyUpgrades = GetPropertyUpgradesFromUpgradDivs();
    return { "BuildingUpgrades": propertyUpgrades }
}