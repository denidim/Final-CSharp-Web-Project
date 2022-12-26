namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Skill> skillRepo;
        private readonly ICloudStorageService cloudStorageService;
        private readonly IDeletableEntityRepository<Image> imageRepo;
        private readonly IDeletableEntityRepository<Address> addressRepo;
        private readonly IDeletableEntityRepository<Rating> ratingRepo;
        private readonly IDeletableEntityRepository<Like> likeRepo;

        public CompanyService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Skill> skillRepo,
            ICloudStorageService cloudStorageService,
            IDeletableEntityRepository<Image> imageRepo,
            IDeletableEntityRepository<Address> addressRepo,
            IDeletableEntityRepository<Rating> ratingRepo,
            IDeletableEntityRepository<Like> likeRepo)
        {
            this.companyRepo = companyRepo;
            this.skillRepo = skillRepo;
            this.cloudStorageService = cloudStorageService;
            this.imageRepo = imageRepo;
            this.addressRepo = addressRepo;
            this.ratingRepo = ratingRepo;
            this.likeRepo = likeRepo;
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
                image.ImageStorageName = ImageNameGenerator.GenerateFileName(input.Image.FileName);
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

                for (int i = 0; i < skills.Count; i++)
                {
                    skills[i].Name = input.Skills[i].Name;
                }

                await this.skillRepo.SaveChangesAsync();
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

                company.Image.ImageStorageName = ImageNameGenerator.GenerateFileName(input.Image.FileName);
                company.Image.ImageUrl = await this.cloudStorageService
                    .UploadFileAsync(input.Image, company.Image.ImageStorageName);
            }

            await this.companyRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = this.companyRepo.All()
                .Include(x => x.Services)
                .Include(x => x.Skills)
                .Include(x => x.Ratings)
                .Include(x => x.Likes)
                .Include(x => x.Image)
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Id == id);

            if (company.Services.Any())
            {
                throw new ArgumentException("Cannot delete company with active services delete all services first");
            }

            if (company.Image != null)
            {
                this.imageRepo.HardDelete(company.Image);
            }

            if (company.Address != null)
            {
                this.addressRepo.HardDelete(company.Address);
            }

            if (company.Skills != null)
            {
                foreach (var skill in company.Skills)
                {
                    this.skillRepo.HardDelete(skill);
                }
            }

            if (company.Ratings != null)
            {
                foreach (var rating in company.Ratings)
                {
                    this.ratingRepo.HardDelete(rating);
                }
            }

            if (company.Likes != null)
            {
                foreach (var like in company.Likes)
                {
                    this.likeRepo.HardDelete(like);
                }
            }

            this.companyRepo.HardDelete(company);

            await this.companyRepo.SaveChangesAsync();
        }

        public bool IsUsersCompany(string userId, int companyId)
        {
            return this.companyRepo.All()
                .Where(x => x.Id == companyId)
                .Any(x => x.AddedByUserId == userId);
        }

        public async Task<T> GetCompanyByIdAsync<T>(int id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.Id == id && x.Services.Any(x => x.Vetting.Passed == true))
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<T> GetForEditCompanyByIdAsync<T>(int id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<int?> GetCompanyByServiceId(int serviceId)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.Services.Any(x => x.Id == serviceId))
                .FirstOrDefaultAsync();

            return company.Id;
        }

        public async Task<T> GetCompanyByUserIdAsync<T>(string id)
        {
            var company = await this.companyRepo.All()
                .Where(x => x.AddedByUserId == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<IEnumerable<IndexPageOutputViewModel>> GetAll(int pageNumber, int itemsPerpage = 12)
        {
            var output = new List<IndexPageOutputViewModel>();

            var company = await this.companyRepo.All()
                .Where(x => x.Services.Any(x => x.Vetting.Passed == true))
                .Include(x => x.Image)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerpage)
                .Take(itemsPerpage)
                .ToListAsync();

            foreach (var item in company)
            {
                var page = new IndexPageOutputViewModel
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
                    page.OutputImageUrl = ImageConstants.DefaultImage;
                }

                output.Add(page);
            }

            return output;
        }

        public async Task<IEnumerable<IndexPageOutputViewModel>> GetSubscribed()
        {
            var output = new List<IndexPageOutputViewModel>();

            var company = await this.companyRepo.All()
                .Where(x => x.Services.Any(x => x.Vetting.Passed == true))
                .Include(x => x.Image)
                .OrderByDescending(x => x.Services.Any(x => x.PaidOrder.EndDate > DateTime.UtcNow))
                .ThenByDescending(x => x.Id)
                .Take(12)
                .ToListAsync();

            foreach (var item in company)
            {
                var page = new IndexPageOutputViewModel
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
                    page.OutputImageUrl = ImageConstants.DefaultImage;
                }

                output.Add(page);
            }

            return output;
        }

        public int GetCount()
        {
            return this.companyRepo.All().Count();
        }
    }
}
