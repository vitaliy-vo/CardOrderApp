using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.DAL.Queries.CustomerQuerys;
using Npgsql;

namespace CardOrderApp.DAL.CustomerRepository
{
    public class DocumetTypeRepository : IDocumetTypeRepository
    {
        public Dictionary<int, DocumetTypeDto> GetTypeDocuments()
        {
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand(DocumentTypeQuery.GetTypeDocuments, connection);


                    NpgsqlDataReader reader = command.ExecuteReader();


                    var typeDocuments = new Dictionary<int, DocumetTypeDto>();
                    while (reader.Read())
                    {
                        typeDocuments.Add(reader.GetInt32(0), new DocumetTypeDto
                        {
                            DocumentTypeId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsPrimary = reader.GetBoolean(2)
                        });
                    }
                    return typeDocuments;




                }
            }
        }
    }
}
