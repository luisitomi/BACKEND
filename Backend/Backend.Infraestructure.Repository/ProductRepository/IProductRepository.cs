using Backend.Domain.Entities.Entities.Product.Command;
using Backend.Domain.Entities.Entities.Product.Queries;
using Backend.Domain.Entities.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> List(string Name);
        Task<Return> Create(ProductCommand request, int UserId);
        Task<Return> Update(ProductUpdateCommand request, int UserId);
        Task<Return> Delete(Entity request, int UserId);
    }
}
