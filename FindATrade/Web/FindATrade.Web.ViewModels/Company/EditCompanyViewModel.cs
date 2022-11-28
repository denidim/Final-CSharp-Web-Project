﻿using AutoMapper;
using FindATrade.Services.Mapping;

namespace FindATrade.Web.ViewModels.Company
{
    public class EditCompanyViewModel : CreateCompanyInputModel, IMapFrom<Data.Models.Company>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {

            configuration.CreateMap<Data.Models.Company, EditCompanyViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
    }
}
