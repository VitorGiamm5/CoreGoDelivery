using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle
{
    public class MotorcycleEntity
    {
        public string Id { get; set; }
        public int YearManufacture { get; set; }
        public string PlateNormalized { get; set; }

        #region relationships

        public string ModelMotorcycleId { get; set; }
        public ModelMotorcycleEntity ModelMotorcycle { get; set; }

        #endregion
    }
}
