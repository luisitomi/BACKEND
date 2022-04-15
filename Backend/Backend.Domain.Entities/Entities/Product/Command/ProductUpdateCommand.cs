using Backend.Domain.Entities.Util;

namespace Backend.Domain.Entities.Entities.Product.Command
{
    public class ProductUpdateCommand : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
