using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.InputModels
{
    public class CustomerDocumentInputModel
    {
       
        public int СustomerId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Series { get; set; }
        public string? Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string? DepartmentCode { get; set; }
    }
}
