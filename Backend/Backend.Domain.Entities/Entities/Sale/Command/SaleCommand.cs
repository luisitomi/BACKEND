using Backend.Domain.Entities.Entities.DetailSale.Command;
using System.Collections.Generic;

namespace Backend.Domain.Entities.Entities.Sale.Command
{
    public class SaleCommand
    {
        public List<DetailSaleCommand> DetailSale { get; set; }
    }
}
