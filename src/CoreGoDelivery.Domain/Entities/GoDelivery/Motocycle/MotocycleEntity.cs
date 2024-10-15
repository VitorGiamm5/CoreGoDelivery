using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle
{
    public sealed class MotocycleEntity
    {
        [Key]
        public string Id { get; set; }

        public int YearManufacture { get; set; }

        public string PlateIdNormalized { get; set; }

        #region Relationship

        [ForeignKey(nameof(ModelMotocycle))]
        public string ModelMotocycleId { get; set; }
        public ModelMotocycleEntity ModelMotocycle { get; set; }

        #endregion
    }
}
