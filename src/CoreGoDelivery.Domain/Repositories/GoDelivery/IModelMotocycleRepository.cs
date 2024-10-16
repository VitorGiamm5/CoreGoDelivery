namespace CoreGoDelivery.Domain.Repositories.GoDelivery
{
    public interface IModelMotocycleRepository
    {
        Task<string> GetIdByModelName(string model);
    }
}
