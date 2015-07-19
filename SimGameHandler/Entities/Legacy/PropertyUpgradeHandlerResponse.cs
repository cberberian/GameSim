namespace SimGame.Handler.Entities.Legacy
{
    public class PropertyUpgradeHandlerResponse
    {
        /// <summary>
        /// Holds the Inventory Included in the order (Not Pending)
        /// </summary>
        public Product[] RequiredProductQueue { get; set; }
        /// <summary>
        /// Holds the balance of inventory, if any, that couldn't be 
        /// fit into the Manufacture order. 
        /// </summary>
        public Product[] PendingInventory { get; set; }
        /// <summary>
        /// Holds the work order that should be submitted. 
        /// </summary>
        public ManufacturingOrder ManufactureOrder { get; set; }
        /// <summary>
        /// Holds the list of Property Upgrades in the order they were priortized.
        /// </summary>
        public BuildingUpgrade[] OrderedUpgrades { get; set; }

        /// <summary>
        /// What is currently in the city storage based on the request. This is used to calculate the
        ///     total number required of each product.
        /// </summary>
        public CityStorage CityStorage { get; set; }

        public Product[] TotalProductQueue { get; set; }
        public Product[] AvailableStorage { get; set; }
    }
}