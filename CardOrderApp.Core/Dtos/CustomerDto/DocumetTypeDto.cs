using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.Dtos.Customer
{
    public class DocumetTypeDto
    {
        public int DocumentTypeId { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }

    

    public override string ToString()
        {
            return $"DocumentTypeId: {DocumentTypeId}, Name: {Name}, IsPrimary: {IsPrimary}";
        }
    }
}
