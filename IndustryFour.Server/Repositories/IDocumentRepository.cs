using IndustryFour.Server.Models;

namespace IndustryFour.Server.Repositories
{
    public interface IDocumentRepository : IRepository<Document>, IDisposable
    {
		// We use specific GetById and the GetAll methods for this class, because we want
		// to bring in the name of the category, and the generic class wouldn't do that
		// https://henriquesd.medium.com/creating-an-application-from-scratch-using-net-core-and-angular-part-3-e3c42cd9cc01
		new Task<List<Document>> GetAll();
        new Task<Document> GetById(int id);

        Task<IEnumerable<Document>> GetDocumentsByCategory(int categoryId);
        Task<IEnumerable<Document>> SearchDocumentWithCategory(string searchedValue);
    }
}
