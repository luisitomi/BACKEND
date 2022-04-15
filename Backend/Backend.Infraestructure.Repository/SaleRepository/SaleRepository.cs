using Backend.CrossCuting.Helpers.Enum;
using Backend.Domain.Entities.Entities.Sale.Command;
using Backend.Domain.Entities.Entities.Sale.Queries;
using Backend.Domain.Entities.Util;
using Backend.Infraestructure.Repository.Repository;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.SaleRepository
{
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        public SaleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<List<SaleModel>> ById(int Id)
        {
            using IDbConnection con = Connection;
            DynamicParameters parameters = new();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            List<SaleEntity> result = (List<SaleEntity>)await con.QueryAsync<SaleEntity>("[test].[SP_GET_SALE_BYID]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

            if (!result.Any())
            {
                return null;
            }

            List<SaleModel> data = new();
            foreach (IGrouping<string, SaleEntity> item in result.GroupBy(p => p.Id))
            {
                List<DetailSaleModel> _detail = new();
                foreach (SaleEntity subitem in result.Where(p => p.Id == item.FirstOrDefault().Id))
                {
                    _detail.Add(new DetailSaleModel()
                    {
                        Count = subitem.Count,
                        Id = subitem.DesatilSaleId,
                        Price = subitem.Price,
                        Product = subitem.ProductName,
                    });
                }

                data.Add(new SaleModel()
                {
                    Client = item.FirstOrDefault().Name,
                    Id = item.FirstOrDefault().Id,
                    Register = item.FirstOrDefault().Register,
                    Status = item.FirstOrDefault().Status,
                    Detail = _detail.GroupBy(p => p.Id).Select(g => g.First()).ToList(),
                });
            }

            return data.GroupBy(p => p.Id).Select(g => g.Last()).ToList();
        }

        public async Task<Return> Create(SaleCommand request, int UserId)
        {
            string listItems = request.DetailSale.Count > 0 ? XmlFunction.Serialize(request.DetailSale) : "";
            IDbConnection connection = Connection;
            {
                DynamicParameters parameters = new();
                parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ListItemsXml", listItems, DbType.String, ParameterDirection.Input);
                parameters.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 80000);
                _ = await connection.ExecuteAsync(@"[test].[SP_POST_INSERT_SALE]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Success") switch
                {
                    1 => new()
                    {
                        Message = ReturnMessage.Post,
                        Valid = true
                    },
                    2 => new()
                    {
                        Message = parameters.Get<string>("@Message"),
                        Valid = false
                    },
                    _ => new()
                    {
                        Message = ReturnMessage.PostError,
                        Valid = false
                    },
                };
            }
        }

        public async Task<Return> Delete(Entity request, int UserId)
        {
            IDbConnection connection = Connection;
            {
                DynamicParameters parameters = new();
                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 80000);
                _ = await connection.ExecuteAsync(@"[test].[SP_DELETE_DELETE_SALE]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Success") switch
                {
                    1 => new()
                    {
                        Message = ReturnMessage.Delete,
                        Valid = true
                    },
                    2 => new()
                    {
                        Message = parameters.Get<string>("@Message"),
                        Valid = false
                    },
                    _ => new()
                    {
                        Message = ReturnMessage.DeleteError,
                        Valid = false
                    },
                };
            }
        }

        public async Task<SaleModel> List(int Id)
        {
            using IDbConnection con = Connection;
            DynamicParameters parameters = new();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            List<SaleEntity> result = (List<SaleEntity>)await con.QueryAsync<SaleEntity>("[test].[SP_GET_SALE]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

            if (!result.Any())
            {
                return null;
            }

            SaleModel data = new();
            foreach (IGrouping<string, SaleEntity> item in result.GroupBy(p => p.Id))
            {
                List<DetailSaleModel> _detail = new();
                foreach (SaleEntity subitem in result.Where(p => p.Id == item.FirstOrDefault().Id))
                {
                    _detail.Add(new DetailSaleModel()
                    {
                        Count = subitem.Count,
                        Id = subitem.DesatilSaleId,
                        Price = subitem.Price,
                        Product = subitem.ProductName,
                    });

                    data.Detail = _detail.GroupBy(p => p.Id).Select(g => g.First()).ToList();
                }

                data.Client = item.FirstOrDefault().Name;
                data.Id = item.FirstOrDefault().Id;
                data.Register = item.FirstOrDefault().Register;
                data.Status = item.FirstOrDefault().Status;
            }

            return data;
        }

        public async Task<Return> Update(SaleUpdateCommand request, int UserId)
        {
            string listItems = request.DetailSale.Count > 0 ? XmlFunction.Serialize(request.DetailSale) : "";
            IDbConnection connection = Connection;
            {
                DynamicParameters parameters = new();
                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ListItemsXml", listItems, DbType.String, ParameterDirection.Input);
                parameters.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 80000);
                _ = await connection.ExecuteAsync(@"[test].[SP_PUT_UPDATE_SALE]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Success") switch
                {
                    1 => new()
                    {
                        Message = ReturnMessage.Put,
                        Valid = true
                    },
                    2 => new()
                    {
                        Message = parameters.Get<string>("@Message"),
                        Valid = false
                    },
                    _ => new()
                    {
                        Message = ReturnMessage.PutError,
                        Valid = false
                    },
                };
            }
        }
    }
}
