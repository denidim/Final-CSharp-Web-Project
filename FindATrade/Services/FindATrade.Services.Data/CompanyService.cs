namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.EntityFrameworkCore;

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


            var compnay = new Company()
            {
                Name = input.Name,
                AddedByUserId = currentUser.Id,
                Website = input.Website,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Description = input.Description,
                Address = address,
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

        public async Task UpdateAsync(int id, EditCompanyViewModel input)
        {
            var company = this.companyRepo.All().FirstOrDefault(x => x.Id == id);

            company.Name = input.Name;
            company.Description = input.Description;
            company.Website = input.Website;
            company.Email = input.Email;
            company.PhoneNumber = input.PhoneNumber;
            company.Address = new Address
            {
                City = input.Address.City,
                PostalCode = input.Address.PostalCode,
                Country = input.Address.Country,
                HouseNumberAddition = input.Address.HouseNumberAddition,
                HouseNumber = input.Address.HouseNumber,
                Street = input.Address.Street,
            };
            company.Skills = new List<Skill>();
            foreach (var item in input.Skills)
            {
                company.Skills.Add(new Skill { Name = item.Name, });
            }

            await this.companyRepo.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }
    }
}
