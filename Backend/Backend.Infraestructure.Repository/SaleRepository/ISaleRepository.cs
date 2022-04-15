using Backend.Domain.Entities.Entities.Sale.Command;
using Backend.Domain.Entities.Entities.Sale.Queries;
using Backend.Domain.Entities.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.SaleRepository
{
    public interface ISaleRepository
    {
        Task<SaleModel> List(int Id);
        Task<List<SaleModel>> ById(int Id);
        Task<Return> Create(SaleCommand request, int UserId);
        Task<Return> Update(SaleUpdateCommand request, int UserId);
        Task<Return> Delete(Entity request, int UserId);
    }
}
