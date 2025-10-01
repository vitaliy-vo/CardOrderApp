using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.CardTypeOuery
{
    public class CardTypeQuery
    {
        public const string GetAllCardType =
            """
            SELECT card_type_id, "name" as cardName, payment_system, "type"
            FROM product.card_type;
            """;
    }
}
