using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.OrderQuery
{
    public class CardQuery
    {
        public const string GetCardById =
           """
            SELECT card_id as CardId , order_id as OrderId, card_number as CardNumber , exp_date as ExpDate, status
            FROM "order".card WHERE card_id = @CardId
            """;

        public const string GetCardByOrderId =
           """
            SELECT card_id as CardId , order_id as OrderId, card_number as CardNumber , exp_date as ExpDate, status
            FROM "order".card WHERE order_id = @OrderId
            """;
    }
}
