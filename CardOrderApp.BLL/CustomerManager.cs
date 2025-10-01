using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.InputModels;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.Core.OutputModels;
using CardOrderApp.DAL.CustomerRepository;

namespace CardOrderApp.BLL
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerRepository _customerRepository;
        private ICustomerDocumentRepository _documentRepository;

        public CustomerManager()
        {
            _customerRepository = new CustomerRepository();
            _documentRepository = new CustomerDocumentRepository();
        }

        public List<CustomerOutModel> GetAllCustomer()
        {
            var p = _customerRepository.GetAllCustomers();
            var z = _documentRepository;
            List<CustomerOutModel> customerOutModels = new List<CustomerOutModel>();


            p.ForEach(customer =>
            {
                CustomerDocumentDto documentDto = z.GetPassportByIdCustomer(customer.Id);
                customerOutModels.Add(new CustomerOutModel(customer.Id,
                    customer.FirstName + " " + customer.LastName + " " + customer.MiddleName,
                   documentDto.Number,
                   documentDto.Series,
                    customer.PhoneNumber,
                    customer.Email));
            });



            return customerOutModels;
        }

        public CustomerDto AddClient(CustomerInputModel customerInputModel)
        {
            CustomerDto customerDto = new CustomerDto
            {


                FirstName = customerInputModel.FirstName,
                LastName = customerInputModel.LastName,
                MiddleName = customerInputModel.MiddleName,
                PhoneNumber = customerInputModel.PhoneNumber,
                Email = customerInputModel.Email,
                IsActive = customerInputModel.IsActive



            };






            return customerDto;
        }


        public CustomerDocumentDto AddDocument(CustomerDocumentInputModel customerDocumentInputModel)
        {
            CustomerDocumentDto customerDocumentDto = new ()
            {
                DocumentTypeId= customerDocumentInputModel.DocumentTypeId,
                Series= customerDocumentInputModel.Series,
                Number= customerDocumentInputModel.Number,
                IssueDate = customerDocumentInputModel.IssueDate,
                DepartmentCode = customerDocumentInputModel.DepartmentCode

            };






            return customerDocumentDto;
        }
    }
}
