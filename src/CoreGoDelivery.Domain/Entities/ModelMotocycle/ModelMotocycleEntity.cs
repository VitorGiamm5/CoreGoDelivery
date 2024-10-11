using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.ModelMotocycle
{
    public sealed class ModelMotocycleEntity
    {
        [Key]
        public string ModelMotocycleId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
