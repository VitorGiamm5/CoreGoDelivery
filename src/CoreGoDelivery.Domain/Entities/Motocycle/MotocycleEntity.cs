using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.Motocycle
{
    public class MotocycleEntity
    {
        [Key]
        public string MotocycleId { get; set; }

        public int YearManufacture { get; set; }

        public string ModelName { get; set; }

        public string PlateIdNormalized { get; set; }
    }
}
