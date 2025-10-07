using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.DAL.Queries.CustomerQuerys;
using Npgsql;

namespace CardOrderApp.DAL.CustomerRepository
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        public Dictionary<string, DocumetTypeDto> GetAllDocumentTypes()
        {
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
                {
                   connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand(DocumentTypeQuery.GetTypeDocuments, connection);


                    NpgsqlDataReader reader = command.ExecuteReader();


                    var typeDocuments = new Dictionary<string, DocumetTypeDto>();
                    while ( reader.Read())
                    {
                        typeDocuments.Add(reader.GetString(1), new DocumetTypeDto
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
