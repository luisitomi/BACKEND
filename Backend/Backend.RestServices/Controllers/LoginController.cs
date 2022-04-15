using Backend.Domain.Entities.Entities.User.Command;
using Backend.Domain.Entities.Util;
using Backend.Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendCMS.RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Access")]
        public async Task<ActionResult<Return>> Access(Login request)
        {
            Return message = await _unitOfWork.LoginRepository.Login(request);
            return message == null ? StatusCode(StatusCodes.Status412PreconditionFailed, "Error en la solicitud") : message.Valid ? Ok(message) : StatusCode(StatusCodes.Status412PreconditionFailed, message.Message);
        }
    }
}
