namespace SimGame.Handler.Entities.Legacy
{
    public class BasicFactory : BuildingFacility
    {
        public override int FacilityType
        {
            get { return Factory; }
        }

        public override int QueueSize
        {
            get { return 3; }
            set {  }
        }
    }
}