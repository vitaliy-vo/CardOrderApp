using CardOrderApp.Core.Dtos.OrderDto;

namespace CardOrderApp.DAL.OrderRepository
{
    public interface ICardRepository
    {
        CardDto GetById(int cardId);
        CardDto GetByIdOrder(int idOrder);
    }
}