using IndustryFour.Shared.Dtos.Document;

namespace IndustryFour.Client.HttpRepository
{
    public interface IDocumentHttpRepository
    {
        Task<List<DocumentResultDto>> GetAll();
        Task<DocumentResultDto> GetById(int id);
        Task Create(DocumentAddDto document);
        Task<string> UploadFile(MultipartFormDataContent content);
        Task Update(DocumentEditDto document);
        Task Delete(int id);
	}
}
