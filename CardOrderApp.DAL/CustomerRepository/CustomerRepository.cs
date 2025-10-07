using CardOrderApp.Core;
using CardOrderApp.Core.Dtos;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL.Queries.CustomerQuerys;
using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<CustomerDto> GetAllCustomers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.GetAllCustomers, connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                List<CustomerDto> customers = new List<CustomerDto>();

                while (reader.Read())


                {
                    CustomerDto customer = new CustomerDto()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        MiddleName = reader.GetString(3),
                        BirthDate = reader.GetDateTime(4),
                        PhoneNumber = reader.GetString(5),
                        Email = reader.GetString(6)
                    };

                    customers.Add(customer);
                }

                return customers;
            }
        }

        public CustomerDto GetCustomerById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.GetCustomerById, connection);
                command.Parameters.Add(new NpgsqlParameter("@id", id));

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    CustomerDto product = new CustomerDto()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        MiddleName = reader.GetString(3),
                        BirthDate = reader.GetDateTime(4),
                        PhoneNumber = reader.GetString(5),
                        Email = reader.GetString(6)
                    };

                    return product;
                }
                else
                {
                    throw new Exception($"Клиента с Id={id} не существует");
                }
            }
        }

        public void UpdateCustomer(CustomerDto customer)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();



                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.UpdateCustomerById, connection);
                command.Parameters.Add(new NpgsqlParameter("@FirstName", customer.FirstName));
                command.Parameters.Add(new NpgsqlParameter("@LastName", customer.LastName));
                command.Parameters.Add(new NpgsqlParameter("@MiddleName", customer.MiddleName));
                command.Parameters.Add(new NpgsqlParameter("@BirthDate", customer.BirthDate));
                command.Parameters.Add(new NpgsqlParameter("@PhoneNumber", customer.PhoneNumber));
                command.Parameters.Add(new NpgsqlParameter("@Email", customer.Email));
                command.Parameters.Add(new NpgsqlParameter("@Id", customer.Id));

                int n = command.ExecuteNonQuery();

                if (n == 0)
                {
                    throw new ArgumentOutOfRangeException($"Клиента с Id={customer.Id} не существует");
                }
            }
        }


        public void UnAnctiveCustomer(CustomerDto customer)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();



                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.UnActiveCustomerById, connection);
                ;
                command.Parameters.Add(new NpgsqlParameter("@Id", customer.Id));

                int n = command.ExecuteNonQuery();

                if (n == 0)
                {
                    throw new ArgumentOutOfRangeException($"Клиента с Id={customer.Id} не существует");
                }
            }
        }

        public CustomerDto GetCustomerWithAllDomementsById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.GetCustomerAndDocumensById, connection);
                command.Parameters.Add(new NpgsqlParameter("@id", id));

                NpgsqlDataReader reader = command.ExecuteReader();


                var customerDict = new Dictionary<int, CustomerDto>();
                while (reader.Read())
                {

                    var customerID = reader.GetInt32(0);
                    if (!customerDict.TryGetValue(customerID, out CustomerDto customer))
                    {

                        customer = new CustomerDto()
                        {

                            Id = customerID,
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            MiddleName = reader.GetString(3),
                            BirthDate = reader.GetDateTime(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6)
                        };
                        customerDict.Add(customerID, customer);
                    }
                    customer.Documents.Add(new CustomerDocumentDto()
                    {


                        DocumentId = reader.GetInt32(7),
                        СustomerId = reader.GetInt32(8),
                        DocumentTypeId = reader.GetInt32(9),
                        Series = reader.GetString(10),
                        Number = reader.GetString(11),
                        IssueDate = reader.GetDateTime(12),
                        DepartmentCode = reader.GetString(13)
                    });
                }
                return customerDict.Values.FirstOrDefault()
                ?? throw new KeyNotFoundException($"Клиента с Id={id} не существует");



            }
        }

        public int CreateCustomer(CustomerDto customer)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(CustomerQuery.CreateCustomer, connection);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@MiddleName", (object)customer.MiddleName ?? DBNull.Value);
                command.Parameters.AddWithValue("@BirthDate", customer.BirthDate);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@Email", (object)customer.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                

                try
                {
                    var result = command.ExecuteScalar();
                    if (result == null)
                        throw new Exception("Не удалось получить ID созданного клиента");

                    return (int)result;
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    throw new Exception("Ошибка при создании клиента", ex);
                }
            }
        }
        }
            


    }

