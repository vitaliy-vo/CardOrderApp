using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.OrderQuery
{
    public class OrderQuery
    {
        public const string GetOrderById =
           """
            SELECT order_id as OrderId, customer_id as CustomerId, card_type_id as CardTypeId , order_date as  OrderDate, status, "comment" 
            FROM "order".orders WHERE order_id= @OrderId
            """;
        public const string GetOrderByCustomerId =
           """
            SELECT order_id as OrderId, customer_id as CustomerId, card_type_id as CardTypeId, order_date as  OrderDate, status, comment 
            FROM "order".orders WHERE customer_id = @CustomerId
            """;

        public const string CreateOrder =
           """
            INSERT INTO "order".orders 
            (customer_id, card_type_id, order_date, status, comment) 
            VALUES (@CustomerId, @CardTypeId, @OrderDate, @Status, @Comment) 
            RETURNING order_id
            """;

        public const string UpdateOrder =
           """
            UPDATE "order".orders SET 
            customer_id = @CustomerId, card_type_id = @CardTypeId, 
            order_date = @OrderDate, status = @Status, comment = @Comment 
            WHERE order_id = @OrderId
            """;
    }
}
