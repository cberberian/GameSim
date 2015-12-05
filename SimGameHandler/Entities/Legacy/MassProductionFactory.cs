namespace SimGame.Handler.Entities.Legacy
{
    public class MassProductionFactory : BuildingFacility
    {
        public override bool ParallelProcessing
        {
            get { return true; }
            set {  }
        }

        public override int QueueSize
        {
            get { return 4; }
            set {  }
        }
    }
}