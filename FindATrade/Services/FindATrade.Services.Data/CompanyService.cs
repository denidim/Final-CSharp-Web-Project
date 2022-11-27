namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Address> addressRepo;

        public CompanyService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Address> addressRepo)
        {
            this.companyRepo = companyRepo;
            this.addressRepo = addressRepo;
        }

        public async Task CreateAsync(CreateCompanyInputModel input, ApplicationUser currentUser)
        {
            var address = new Address()
            {
                Street = input.Address.Street,
                HouseNumber = input.Address.HouseNumber,
                HouseNumberAddition = input.Address.HouseNumberAddition,
                City = input.Address.City,
                Country = input.Address.Country,
                PostalCode = input.Address.PostalCode,
            };

            await this.addressRepo.AddAsync(address);

            var compnay = new Company()
            {
                Name = input.Name,
                AddedByUserId = currentUser.Id,
                Website = input.Website,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Description = input.Description,
                AddressId = address.Id,
            };

            foreach (var item in input.Skills)
            {
                var skill = new Skill
                {
                    Name = item.Name,
                };

                compnay.Skills.Add(skill);
            }

            await this.companyRepo.AddAsync(compnay);
            await this.companyRepo.SaveChangesAsync();
        }

        //public T GetById<T>(int id)
        //{
        //    return;
        //}
    }
}
