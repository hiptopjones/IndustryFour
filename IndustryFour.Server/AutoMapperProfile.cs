using AutoMapper;
using IndustryFour.Shared.Dtos.Category;
using IndustryFour.Shared.Dtos.Document;
using IndustryFour.Server.Models;

namespace IndustryFour.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryEditDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>().ReverseMap();
            CreateMap<Document, DocumentAddDto>().ReverseMap();
            CreateMap<Document, DocumentEditDto>().ReverseMap();
            CreateMap<Document, DocumentResultDto>().ReverseMap();
        }
    }
}
