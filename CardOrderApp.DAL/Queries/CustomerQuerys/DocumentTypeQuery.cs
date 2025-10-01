using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.CustomerQuerys
{
    public class DocumentTypeQuery
    {
        public const string GetTypeDocuments =
            """
            SELECT document_type_id, "name", is_primary
            FROM customer.document_type;
            """;
    }
}
