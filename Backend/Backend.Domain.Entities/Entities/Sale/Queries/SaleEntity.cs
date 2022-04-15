using System;

namespace Backend.Domain.Entities.Entities.Sale.Queries
{
    public class SaleEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Register { get; set; }
        public string Status { get; set; }
        public int DesatilSaleId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
