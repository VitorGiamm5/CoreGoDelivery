namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IModelMotorcycleRepository
{
    Task<string> GetIdByModelName(string model);
}
