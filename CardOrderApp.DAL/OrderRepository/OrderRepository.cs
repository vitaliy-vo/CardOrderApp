using CardOrderApp.Core;
using CardOrderApp.Core.Dtos.OrderDto;
using CardOrderApp.DAL.Queries.OrderQuery;
using Dapper;
using Npgsql;


namespace CardOrderApp.DAL.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {

        public OrderDto GetById(int orderId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var parameters = new { OrderId = orderId };
                OrderDto result = connection.QuerySingle<OrderDto>(OrderQuery.GetOrderById, parameters);
                return result;
            }
        }

        public List<OrderDto> GetOrderByCustomerId(int customerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();

                var parameters = new { CustomerId = customerId };
                var result = connection.Query<OrderDto>(OrderQuery.GetOrderByCustomerId, parameters).ToList(); ;
                return result;

            }
        }

        public int CreateOrder(OrderDto order)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();
                int createdOrderId = connection.Query<int>(OrderQuery.CreateOrder, order).Single();
                return createdOrderId;
            }
        }

        public bool UpdateOrder(OrderDto order)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Options.ConnectionString))
            {
                connection.Open();



                NpgsqlCommand command = new NpgsqlCommand(OrderQuery.UpdateOrder, connection);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@CardTypeId", order.CardTypeId);
                command.Parameters.AddWithValue("@OrderDate", (object)order.OrderDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", (object)order.Status ?? DBNull.Value);
                command.Parameters.AddWithValue("@Comment", (object)order.Comment ?? DBNull.Value);
                command.Parameters.AddWithValue("@OrderId", order.OrderId);

                return command.ExecuteNonQuery() > 0;


            }
        }

    }
}
