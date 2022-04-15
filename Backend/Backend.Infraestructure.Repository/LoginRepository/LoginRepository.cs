using Backend.CrossCuting.Helpers;
using Backend.Domain.Entities.Entities.User.Command;
using Backend.Domain.Entities.Entities.User.Queries;
using Backend.Domain.Entities.Util;
using Backend.Infraestructure.Repository.Repository;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.LoginRepository
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        private readonly AppConfiguration _configuration;
        public LoginRepository(IDbTransaction transaction) : base(transaction)
        {
            _configuration = new AppConfiguration();
        }

        public async Task<Return> Login(Login request)
        {
            IDbConnection connection = Connection;
            DynamicParameters param = new();
            param.Add("@UserName", request.UserName, DbType.String);
            param.Add("@Password", Sha256(request.Password), DbType.String);
            List<UserInformation> result = (List<UserInformation>)await connection.QueryAsync<UserInformation>(@"[test].[SP_GET_INFORMATION_USER]", param, transaction: Transaction, commandType: CommandType.StoredProcedure);

            if (!result.Any())
            {
                return new()
                {
                    Message = "Credenciales Inválidas",
                    Valid = false,
                };
            }

            UserInformation oUser = new()
            {
                Id = result[0].Id,
                Name = result[0].Name,
                UserName = result[0].UserName,
            };

            string jwtToken = GenerateJwtToken(oUser);

            return new()
            {
                Message = jwtToken,
                Valid = true,
            };
        }

        private string GenerateJwtToken(UserInformation tokens)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_configuration._TokenKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", tokens.Id.ToString()),
                    new Claim("UserName", tokens.UserName),
                    new Claim("Name", tokens.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration._AppName,
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
