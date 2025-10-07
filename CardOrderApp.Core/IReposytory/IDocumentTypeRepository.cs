using CardOrderApp.Core.Dtos.Customer;

namespace CardOrderApp.DAL
{
    public interface IDocumentTypeRepository
    {
        Dictionary<string, DocumetTypeDto> GetAllDocumentTypes();
    }
}