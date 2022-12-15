namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    public interface ISubscriptionService
    {
        Task<T> GetPaidOrderAsync<T>(int id);

        Task AddSubscriptionAsync(int serviceId);

        Task RemoveExpiredSubscriptionsAsync(int serviceId);
    }
}
