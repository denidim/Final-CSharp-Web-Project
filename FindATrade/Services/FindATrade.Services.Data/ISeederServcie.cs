namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    public interface ISeederServcie
    {
        Task SeedAsync(int count);
    }
}
