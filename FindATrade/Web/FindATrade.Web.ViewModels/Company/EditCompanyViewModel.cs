using FindATrade.Services.Mapping;

namespace FindATrade.Web.ViewModels.Company
{
    public class EditCompanyViewModel : CreateCompanyInputModel, IMapFrom<Data.Models.Company>
    {
        public int Id { get; set; }
    }
}
