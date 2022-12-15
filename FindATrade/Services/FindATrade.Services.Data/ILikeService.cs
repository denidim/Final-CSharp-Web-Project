namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    public interface ILikeService
    {
        Task SetLike(int companyId, string userId);

        Task<int> GetLikeCount(int companyId);
    }
}
