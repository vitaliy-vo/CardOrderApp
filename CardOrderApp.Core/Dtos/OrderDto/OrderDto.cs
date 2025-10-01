using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.Dtos.OrderDto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CardTypeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }


        public override string ToString()
        {
            return $"OrderInfo {{ " +
                   $"  OrderId: {OrderId}, " +
                   $"  CustomerId: {CustomerId}, " +
                   $"  CardTypeId: {CardTypeId}, " +
                   $"  OrderDate: {OrderDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "null"}, " +
                   $"  Status: {Status ?? "null"}, " +
                   $"  Comment: {Comment ?? "null"} " +
                   "}";
        }
    }
}
