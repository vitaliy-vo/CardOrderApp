using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.InputModels;
using CardOrderApp.Core.OutputModels;

namespace CardOrderApp.BLL
{
    public interface ICustomerManager
    {
        CustomerDto AddClient(CustomerInputModel customerInputModel);
        CustomerDocumentDto AddDocument(CustomerDocumentInputModel customerDocumentInputModel);
        List<CustomerOutModel> GetAllCustomer();
    }
}