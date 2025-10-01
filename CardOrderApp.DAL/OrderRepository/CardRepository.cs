using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.OrderDto;
using CardOrderApp.DAL.Queries.OrderQuery;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.OrderRepository
{
    public class CardRepository : ICardRepository
    {
        public CardDto GetById(int cardId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var parameters = new { CardId = cardId };
                CardDto result = connection.QuerySingle<CardDto>(CardQuery.GetCardById, parameters);
                return result;
            }
        }

        public CardDto GetByIdOrder(int idOrder)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var parameters = new { OrderId = idOrder };
                CardDto result = connection.QuerySingle<CardDto>(CardQuery.GetCardByOrderId, parameters);
                return result;
            }
        }
    }
}
