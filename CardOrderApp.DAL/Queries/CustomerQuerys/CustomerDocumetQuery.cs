using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.CustomerQuerys
{
    public class CustomerDocumetQuery
    {

       


        public const string UpdateDocumetById =
            """
            UPDATE customer.customer_document
            SET  document_type_id=@DocumetTypeId, series=@Series, "number"=@Number, issue_date=@IssueDate, department_code=@DepartmetCode
            WHERE document_id=@DocumentId;
            """;

        public const string GetDocumetsByCustomerId =
            """
            SELECT document_id, customer_id, document_type_id, series, 
                   number, issue_date, department_code 
            FROM customer.customer_document WHERE customer_id = @CustomerId
            """;


        public const string GetPassportByCustomerId =
            """
            SELECT document_id, customer_id, document_type_id, series, 
                   number, issue_date, department_code 
            FROM customer.customer_document WHERE customer_id = @CustomerId and document_type_id=1
            """;

        public const string AddDocument =
            """
            INSERT INTO customer.customer_document
            (customer_id, document_type_id, series, "number", issue_date, department_code)
            VALUES( @CustomerId,@DocumentTypeId, @Series, @Number, @IssueDate,@DepartmentCode)
            RETURNING document_id
            """;



    }
}
