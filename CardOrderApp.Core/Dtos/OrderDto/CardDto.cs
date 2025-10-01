namespace CardOrderApp.Core.Dtos.OrderDto
{
    public class CardDto
    {
        public int CardId { get; set; }
        public int OrderId { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string? Status { get; set; }



        public override string ToString()
        {
            return $"CardDto {{ " +
                   $"  CardId: {CardId}, " +
                   $"  OrderId: {OrderId}, " +
                   $"  CardNumber: {CardNumber}, " +
                   $"  ExpDate: {ExpDate}, " +
                   $"  Status: {Status ?? "null"} " +
                   "}}";
        }
    }
}
