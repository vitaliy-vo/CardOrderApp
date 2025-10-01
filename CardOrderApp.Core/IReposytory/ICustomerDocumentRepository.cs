using CardOrderApp.Core.Dtos.Customer;

namespace CardOrderApp.Core.Interfaces
{
    public interface ICustomerDocumentRepository
    {
        void UpdateDocumetById(CustomerDocumentDto document);
        CustomerDocumentDto GetPassportByIdCustomer(int id);
        int CreateDocument(CustomerDocumentDto customerDocumentDto);

    }
}