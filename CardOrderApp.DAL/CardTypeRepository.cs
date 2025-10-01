using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.ProductDto;
using CardOrderApp.DAL.Queries.CardTypeOuery;
using CardOrderApp.DAL.Queries.CustomerQuerys;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL
{
    public class CardTypeRepository : ICardTypeRepository
    {
        public List<CardTypeDto> GetAllTypeCard()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(CardTypeQuery.GetAllCardType, connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                List<CardTypeDto> cardTypeDtos = new List<CardTypeDto>();

                while (reader.Read())


                {
                    cardTypeDtos.Add(new CardTypeDto()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        PaymentSystem = reader.GetString(2),
                        TypeCard = reader.GetString(3)

                    });

                }

                return cardTypeDtos;
            }
        }
    }
}
