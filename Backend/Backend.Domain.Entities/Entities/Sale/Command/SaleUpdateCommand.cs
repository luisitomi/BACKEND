using Backend.Domain.Entities.Entities.DetailSale.Command;
using Backend.Domain.Entities.Util;
using System.Collections.Generic;

namespace Backend.Domain.Entities.Entities.Sale.Command
{
    public class SaleUpdateCommand : Entity
    {
        public List<DetailSaleCommand> DetailSale { get; set; }
    }
}
