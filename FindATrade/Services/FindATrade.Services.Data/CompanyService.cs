namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Home;
    using Google.Api.Gax;
    using Microsoft.EntityFrameworkCore;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Skill> skillRepo;
        private readonly ICloudStorageService cloudStorageService;
        private readonly IDeletableEntityRepository<Image> imageRepo;

        public CompanyService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Skill> skillRepo,
            ICloudStorageService cloudStorageService,
            IDeletableEntityRepository<Image> imageRepo)
        {
            this.companyRepo = companyRepo;
            this.skillRepo = skillRepo;
            this.cloudStorageService = cloudStorageService;
            this.imageRepo = imageRepo;
        }

        public async Task CreateAsync(CreateCompanyInputModel input, ApplicationUser currentUser)
        {
            var compnay = new Company()
            {
                Name = input.Name,
                AddedByUserId = currentUser.Id,
                Website = input.Website,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Description = input.Description,
            };

            if (input.Image != null)
            {
                var image = new Image();
                image.ImageStorageName = this.GenerateFileName(input.Image.FileName);
                image.ImageUrl = await this.cloudStorageService
                    .UploadFileAsync(input.Image, image.ImageStorageName);
                compnay.Image = image;
            }

            var address = new Address()
            {
                Street = input.Address.Street,
                HouseNumber = input.Address.HouseNumber,
                HouseNumberAddition = input.Address.HouseNumberAddition,
                City = input.Address.City,
                Country = input.Address.Country,
                PostalCode = input.Address.PostalCode,
            };

            compnay.Address = address;

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
            var company = this.companyRepo.All()
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .Include(x => x.Image)
                .FirstOrDefault(x => x.Id == id);

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

            if (company.Skills.Any())
            {
                var skills = await this.skillRepo
                .All()
                .Where(x => x.CompanyId == company.Id)
                .ToListAsync();

                foreach (var item in skills)
                {
                    this.skillRepo.HardDelete(item);
                }

                await this.skillRepo.SaveChangesAsync();
            }

            if (input.Skills != null)
            {
                foreach (var item in input.Skills)
                {
                    company.Skills.Add(new Skill { Name = item.Name, });
                }
            }

            if (input.Image != null)
            {
                if (company.Image == null)
                {
                    company.Image = new Image();
                }

                if (!string.IsNullOrEmpty(company.Image.ImageStorageName))
                {
                    await this.cloudStorageService.DeleteFileAsync(company.Image.ImageStorageName);
                }

                company.Image.ImageStorageName = this.GenerateFileName(input.Image.FileName);
                company.Image.ImageUrl = await this.cloudStorageService
                    .UploadFileAsync(input.Image, company.Image.ImageStorageName);
            }

            await this.companyRepo.SaveChangesAsync();
        }

        public async Task<string> GenerateImageUrl(int companyId)
        {
            // Get the sorage name from company image
            var savedImageName = await this.imageRepo.All()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.ImageStorageName)
                .SingleOrDefaultAsync();

            // creates new Url with exparation to show to the outside world
            return await this.cloudStorageService.GetSignedUrlAsync(savedImageName);
        }

        public async Task<T> GetCompanyByIdAsync<T>(int id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<T> GetCompanyByUserIdAsync<T>(string id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.AddedByUserId == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<IEnumerable<IndexPageViewModel>> GetPopular()
        {
            var output = new List<IndexPageViewModel>();

            var company = this.companyRepo.All()
                .Include(x => x.Image)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();

            foreach (var item in company)
            {
                var page = new IndexPageViewModel
                {
                    Id = item.Id,
                    Description = item.Description.Length >= 60 ? item.Description.Substring(0, 60) : item.Description,
                    Name = item.Name,
                };

                if (item.Image != null)
                {
                    page.OutputImageUrl = await this.cloudStorageService
                        .GetSignedUrlAsync(item.Image.ImageStorageName);
                }
                else
                {
                    page.OutputImageUrl = "https://images.pexels.com/photos/617278/pexels-photo-617278.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";
                }

                output.Add(page);
            }

            return output;
        }

        private string GenerateFileName(string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            return $"{name}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
