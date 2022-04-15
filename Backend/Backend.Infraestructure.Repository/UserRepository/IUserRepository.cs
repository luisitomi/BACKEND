using Backend.Domain.Entities.Entities.User.Command;
using Backend.Domain.Entities.Entities.User.Queries;
using Backend.Domain.Entities.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> List(string Name);
        Task<Return> Create(UserCommand request, int UserId);
        Task<Return> Update(UserUpdateCommand request, int UserId);
        Task<Return> Delete(Entity request, int UserId);
    }
}
