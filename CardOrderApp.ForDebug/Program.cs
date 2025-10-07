using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.Dtos.OrderDto;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL;
using CardOrderApp.DAL.CustomerRepository;
using CardOrderApp.DAL.OrderRepository;
using System.Collections.Generic;
using CardOrderApp.Core.Dtos.ProductDto;


namespace CardOrderApp.ForDebug
{
    internal class Program
    {
        

        static void Main(string[] args)
        {


            // var customerRepo = new CustomerRepository();
            // List<CustomerDto> customer = customerRepo.GetAllCustomers();
            // customer.ForEach(x => Console.WriteLine(x));
            /*CustomerDto customer2 = customerRepo.GetCustomerById(1);
            customer2.PhoneNumber = "+79816656352";
            customerRepo.UpdateCustomer(customer2);
            Console.WriteLine(customer2);
           // customerRepo.UnAciveCustomerbyId(customer2);*/
             

            var customerRepo = new CardTypeRepository();
            /*CustomerDto customer = customerRepo.GetCustomerById(1);
            customer.FirstName = "Андрей";
            customer.PhoneNumber = "+79816656355";
            customerRepo.CreateCustomer(customer);
*/
            // CustomerDocumentDto customerDocumentDto = customerRepo.GetPassportByIdCustomer(1);
            //orderDto.Comment = "bbbb";

            //Console.WriteLine(customerDocumentDto.Number);

            List<CardTypeDto> customerDto = customerRepo.GetAllTypeCard();

            foreach (var item in customerDto)
            {
                Console.WriteLine(item.Name);
              }

            ;

            /*var customerRepo = new CardTypeRepository();
            List<CardTypeDto> cardTypeDtos = customerRepo.GetAllTypeCard();

            foreach (var item in cardTypeDtos)
            {
                Console.WriteLine(item);
            }*/





        }
    }
}
