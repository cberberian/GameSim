namespace SimGame.Handler.Entities.Legacy
{
    public class BuildingUpgradeHandlerRequest
    {
        public City City { get; set; }
        public bool ReturnInventory { get; set; }
        public bool ReturnFaciltiyAssignment { get; set; }
        public bool CalculateBuidingUpgradeStatistics { get; set; }
        public Domain.ProductType[] ProductTypes { get; set; }
    }
}