using Backend.Domain.Entities.Entities.User.Command;
using Backend.Domain.Entities.Util;
using System.Threading.Tasks;

namespace Backend.Infraestructure.Repository.LoginRepository
{
    public interface ILoginRepository
    {
        Task<Return> Login(Login request);
    }
}
