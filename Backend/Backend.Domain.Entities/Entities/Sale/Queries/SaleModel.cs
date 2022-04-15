using System;
using System.Collections.Generic;

namespace Backend.Domain.Entities.Entities.Sale.Queries
{
    public class SaleModel
    {
        public string Id { get; set; }
        public string Client { get; set; }
        public DateTime Register { get; set; }
        public string Status { get; set; }
        public List<DetailSaleModel> Detail { get; set; }
    }
}
