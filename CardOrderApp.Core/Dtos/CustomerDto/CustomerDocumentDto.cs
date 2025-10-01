using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.Dtos.Customer
{
    /*SELECT document_id, customer_id, document_type_id, series, "number", issue_date, department_code
FROM customer.customer_document
where document_id=1;*/

    public class CustomerDocumentDto
    {
        public int DocumentId { get; set; }
        public int СustomerId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Series { get; set; }
        public string? Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string? DepartmentCode { get; set; }

        public override string? ToString()
        {
            return $"DocumentDto:={DocumentTypeId}: Series={Series}-Number={Number} IssueDate=({IssueDate:yyyy-MM-dd})";
        }
    }
}
