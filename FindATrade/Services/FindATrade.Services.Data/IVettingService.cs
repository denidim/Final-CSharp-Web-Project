namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    public interface IVettingService
    {
        Task<T> GetByServiceIdAsync<T>(int id);
    }
}
