using CardOrderApp.Core.Dtos.Customer;

namespace CardOrderApp.Core.Interfaces
{
    public interface ICustomerDocumentRepository
    {
        void UpdateDocumet(CustomerDocumentDto document);
        CustomerDocumentDto GetPassportByIdCustomer(int id);
        int CreateDocument(CustomerDocumentDto customerDocumentDto);
        List<CustomerDocumentDto> GetDocumentsByCustomerId(int customerId);

    }
}