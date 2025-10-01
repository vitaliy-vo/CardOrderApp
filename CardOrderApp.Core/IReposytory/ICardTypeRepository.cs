using CardOrderApp.Core.Dtos.ProductDto;

namespace CardOrderApp.DAL
{
    public interface ICardTypeRepository
    {
        List<CardTypeDto> GetAllTypeCard();
    }
}