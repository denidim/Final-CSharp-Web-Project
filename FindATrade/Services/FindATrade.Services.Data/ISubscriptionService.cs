namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    public interface ISubscriptionService
    {
        Task<T> GetPaidOrder<T>();
    }
}
