using AutoMapper;
using IndustryFour.Server.Dtos.Category;
using IndustryFour.Server.Dtos.Document;
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
