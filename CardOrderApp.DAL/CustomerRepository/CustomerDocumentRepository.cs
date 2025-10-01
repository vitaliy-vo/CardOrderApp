using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.Dtos.OrderDto;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL.Queries.CustomerQuerys;
using CardOrderApp.DAL.Queries.OrderQuery;
using Dapper;
using Npgsql;
using System.Data;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CardOrderApp.DAL.CustomerRepository
{
    public class CustomerDocumentRepository : ICustomerDocumentRepository
    {
        public void UpdateDocumetById(CustomerDocumentDto document)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();



                NpgsqlCommand command = new NpgsqlCommand(CustomerDocumetQuery.UpdateDocumetById, connection);
                command.Parameters.Add(new NpgsqlParameter("@DocumetTypeId", document.DocumentTypeId));
                command.Parameters.Add(new NpgsqlParameter("@Series", document.Series));
                command.Parameters.Add(new NpgsqlParameter("@Number", document.Number));
                command.Parameters.Add(new NpgsqlParameter("@IssueDate", document.IssueDate));
                command.Parameters.Add(new NpgsqlParameter("@DepartmetCode", document.DepartmentCode));
                command.Parameters.Add(new NpgsqlParameter("@DocumentId", document.DocumentId));


                int n = command.ExecuteNonQuery();

                if (n == 0)
                {
                    throw new ArgumentOutOfRangeException($"Документ с Id={document.DocumentId} не существует");
                }
            }
        }

        public List<CustomerDocumentDto> GetDocumentsByCustomerId(int customerId)
        {
            var documents = new List<CustomerDocumentDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                

                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(CustomerDocumetQuery.GetDocumetsByCustomerId, connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                var reader=command.ExecuteReader();

                while (reader.Read())
                    documents.Add(new CustomerDocumentDto
                    {
                        DocumentId = reader.GetInt32("document_id"),
                        СustomerId = reader.GetInt32("customer_id"),
                        DocumentTypeId = reader.GetInt32("document_type_id"),
                        Series = reader.GetString("series"),
                        Number = reader.IsDBNull("number") ? null : reader.GetString("number"),
                        IssueDate = reader.GetDateTime("issue_date"),
                        DepartmentCode = reader.IsDBNull("department_code") ? null : reader.GetString("department_code")
                    });
                    }
           return documents;
        }

        public CustomerDocumentDto GetPassportByIdCustomer(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var parameters = new { CustomerId = id };
                CustomerDocumentDto result = connection.QuerySingle<CustomerDocumentDto>(CustomerDocumetQuery.GetPassportByCustomerId, parameters);
                return result;
            }
        }
        public int CreateDocument(CustomerDocumentDto customerDocumentDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
           
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(CustomerDocumetQuery.AddDocument, connection);
                command.Parameters.AddWithValue("@CustomerId", customerDocumentDto.СustomerId);
                command.Parameters.AddWithValue("@DocumentTypeId", customerDocumentDto.DocumentTypeId);
                command.Parameters.AddWithValue("@Series", customerDocumentDto.Series);
                command.Parameters.AddWithValue("@Number", customerDocumentDto.Number);
                command.Parameters.AddWithValue("@IssueDate", customerDocumentDto.IssueDate);
                command.Parameters.AddWithValue("@DepartmentCode", customerDocumentDto.DepartmentCode);
               



                return (int)command.ExecuteScalar();
            }
        }
    }

}
   