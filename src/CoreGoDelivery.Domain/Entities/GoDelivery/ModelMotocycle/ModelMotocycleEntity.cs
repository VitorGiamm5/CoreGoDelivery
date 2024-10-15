using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle
{
    public sealed class ModelMotocycleEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
