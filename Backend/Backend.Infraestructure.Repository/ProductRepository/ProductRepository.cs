﻿using Backend.CrossCuting.Helpers.Enum;
using Backend.Domain.Entities.Entities.Product.Command;
using Backend.Domain.Entities.Entities.Product.Queries;
using Backend.Domain.Entities.Util;
using Backend.Infraestructure.Repository.Repository;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.ProductRepository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Return> Create(ProductCommand request, int UserId)
        {
            IDbConnection connection = Connection;
            {
                DynamicParameters parameters = new();
                parameters.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Price", request.Price, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 80000);
                _ = await connection.ExecuteAsync(@"[test].[SP_POST_INSERT_PRODUCT]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

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
                _ = await connection.ExecuteAsync(@"[test].[SP_DELETE_DELETE_PRODUCT]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<ProductModel>> List(string Name)
        {
            using IDbConnection con = Connection;
            DynamicParameters parameters = new();
            parameters.Add("@Name", Name, DbType.String, ParameterDirection.Input);
            return await con.QueryAsync<ProductModel>("[test].[SP_GET_PRODUCT]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<Return> Update(ProductUpdateCommand request, int UserId)
        {
            IDbConnection connection = Connection;
            {
                DynamicParameters parameters = new();
                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Price", request.Price, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 80000);
                _ = await connection.ExecuteAsync(@"[test].[SP_PUT_UPDATE_PRODUCT]", parameters, transaction: Transaction, commandType: CommandType.StoredProcedure);

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
