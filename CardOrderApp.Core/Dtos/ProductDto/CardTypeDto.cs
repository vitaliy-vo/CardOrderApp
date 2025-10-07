using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.Dtos.ProductDto
{
    public class CardTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PaymentSystem { get; set; }
        public string TypeCard { get; set; }
       

        public override string? ToString()
        {
            return $"Id={Id} Name={Name}  PaymentSystem={PaymentSystem} TypeCard={TypeCard}";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
