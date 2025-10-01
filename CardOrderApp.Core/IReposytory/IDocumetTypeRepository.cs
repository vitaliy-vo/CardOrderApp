using CardOrderApp.Core.Dtos.Customer;

namespace CardOrderApp.DAL
{
    public interface IDocumetTypeRepository
    {
        Dictionary<int, DocumetTypeDto> GetTypeDocuments();
    }
}