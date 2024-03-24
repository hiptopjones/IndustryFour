using AutoMapper;
using IndustryFour.Shared.Dtos.Category;
using IndustryFour.Shared.Dtos.Document;
using IndustryFour.Server.Models;
using IndustryFour.Shared.Dtos.Chunk;

namespace IndustryFour.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryEditDto, Category>();
            CreateMap<Category, CategoryResultDto>();

            CreateMap<DocumentAddDto, Document>();
            CreateMap<DocumentEditDto, Document>();
			CreateMap<Document, DocumentResultDto>();
			
            CreateMap<Chunk, ChunkResultDto>();
		}
	}
}
