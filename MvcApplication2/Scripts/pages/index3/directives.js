cityManagerApp.directive("manufacturerList", function ($parse) {
    return {
        restrict: "E",
        replace: true,
        templateUrl: "/scripts/pages/index3/templates/manufacturerList.html",
        link: function (scope, element, attrs, controller) {
            if (scope.$last) {
                var parent = element.parent();
                parent.accordion("refresh");
                parent.accordion("option", "active", "false");
            }
        }
    };
});

cityManagerApp.directive("viewFadeIn", function ($timeout) {
    return {
        restrict: "A",
        link: function (scope, element, attrs, controller) {
            $timeout(scope.DataFinishedLoading(element), 0);
        }
    };
});

cityManagerApp.directive("updateSupplyChainLink", function() {
    return {
        restrict: "E",
        replace: true,
        link: function(scope, element, attrs, controller) {
            var ret = $("<a/>").text("Update Supply Chain");
            ret.button().click(scope.UpdateSupplyChain);
            $(element).replaceWith(ret);
        }
    };
});

cityManagerApp.directive("buildingUpgradeProgressBar", function () {
    return {
        restrict: "A",
        replace: false,
        link: function(scope, element, attrs, controller) {
            element.progressbar({ value: scope.buildingUpgrade.TotalUpgradeTime - scope.buildingUpgrade.RemainingUpgradeTime, max: scope.buildingUpgrade.TotalUpgradeTime });
        }
    };
});

cityManagerApp.directive("buildingUpgradeTooltip", function () {
    return {
        restrict: "A",
        replace: false,
        link: function(scope, element, attrs, controller) {
            element.tooltip({ content: scope.buildingUpgrade.Tooltip, position: { my: "right", at: "right" } });
        }
    };
});

cityManagerApp.directive("buildingUpgradeDraggable", function () {
    return {
        restrict: "EA",
        replace: true,
        link: function(scope, element, attrs, controller) {
            element.draggable({
                revert: true
            });
        }
    };
});

cityManagerApp.directive("productDraggable", function () {
    return {
        restrict: "EA",
        replace: true,
        link: function(scope, element, attrs, controller) {
            element.draggable({
                revert: true,
                helper: "clone",
                appendTo: "body"
            }).disableSelection();
        }
    };
});

cityManagerApp.directive("trashDroppable", function () {
    return {
        restrict: "EA",
        replace: true,
        link: function (scope, element, attrs, controller) {
            element.droppable({
                tolerance: "touch",
                drop: function(event, ui) {
                    if (!confirm("Delete the selected item?"))
                        return;
                    var text = ui.draggable.find("span.building-upgrade-index").text();
                    if (text === "")
                        return;
                    var index = GetInt(text);
                    var bu = scope.city.BuildingUpgrades.splice(index, 1);
                    scope.$apply();
                }
            });
        }
    };
});

cityManagerApp.directive("mainAccordion", function () {
    return {
        restrict: "AE",
        replace: false,
        link: function (scope, element, attrs, controller) {
            element.accordion({
                "header": "h3",
                "collapsible": "true",
                "heightStyle": "content",
                "active": "false"
            });
        }
    }
});

cityManagerApp.directive("buildingUpgrade", function () {
    return {
        restrict: "E",
        replace: true,
        priority: 3,
        templateUrl: "/scripts/pages/index3/templates/buildingUpgrade.html",
        link: function (scope, element, attrs, controller) {
            if (scope.$last) {
                var parent = element.parent();
                parent.accordion("refresh");
                parent.accordion("option", "active", "false");
            }
        }
    }
});

cityManagerApp.directive("productMenu", function () {
    return {
        restrict: "A",
        link: function (scope, element, attrs, controller) {
            angular.element(element).menu();
            
//            $(element).menu()
//                    .sortable({
//                    connectWith: ".inventory-list",
//                    stop: onUpgradeDragStart,
//                    receive: onUpgradeListReceive,
//                    remove: onInventoryListRemove
//                }).disableSelection();
        }
    }
});

cityManagerApp.directive("productTooltip", function () {
    return {
        restrict: "AE",
        replace: true,
        template: "<span title='' class='inventory-item-name'>{{product.Name}}</span>",
        link: function (scope, element, attrs, controller) {
            element.tooltip(
                {
                    content: scope.product.RequiredProductsToolTip,
                    position: { my: "left+100", at: "left" }
                }
            );   
        }
    }
});

cityManagerApp.directive("productQuantityWarning", function () {
    return {
        restrict: "AE",
        link: function (scope, element, attrs, controller) {
            if (scope.product.Quantity > scope.product.StorageQuantity)
                    element.addClass("product-warning");
            }
    }
});
