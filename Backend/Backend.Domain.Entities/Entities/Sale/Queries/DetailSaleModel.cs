namespace Backend.Domain.Entities.Entities.Sale.Queries
{
    public class DetailSaleModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
    }
}
