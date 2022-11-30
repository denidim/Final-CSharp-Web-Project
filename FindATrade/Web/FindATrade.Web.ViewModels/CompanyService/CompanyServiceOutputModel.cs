namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;

    using AutoMapper;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CompanyServiceOutputModel : SingleServiceOutputModel
    {
        public PaidOrderOutputModel PaidOrder { get; set; }

    }
}