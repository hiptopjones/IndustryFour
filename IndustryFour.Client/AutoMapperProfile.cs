using AutoMapper;
using IndustryFour.Shared.Dtos.Document;

namespace IndustryFour.Client
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DocumentResultDto, DocumentEditDto>();
        }
    }
}
