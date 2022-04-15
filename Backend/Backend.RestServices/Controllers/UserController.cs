using Backend.Domain.Entities.Entities.User.Command;
using Backend.Domain.Entities.Entities.User.Queries;
using Backend.Domain.Entities.Util;
using Backend.Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendCMS.RestServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public async Task<ActionResult<Return>> Delete(int Id)
        {
            Entity request = new()
            {
                Id = Id,
            };
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentUser = User;
            int UserId = int.Parse(currentUser.FindFirst("Id").Value);
            Return response = await u.UserRepository.Delete(request, UserId);
            u.Commit();
            if (!response.Valid)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, response.Message);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> List(string Name)
        {
            return Ok(await _unitOfWork.UserRepository.List(Name));
        }

        [HttpPost]
        public async Task<ActionResult<Return>> Create(UserCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentUser = User;
            int UserId = int.Parse(currentUser.FindFirst("Id").Value);
            Return response = await u.UserRepository.Create(request, UserId);
            u.Commit();
            if (!response.Valid)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, response.Message);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Return>> Update(UserUpdateCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentUser = User;
            int UserId = int.Parse(currentUser.FindFirst("Id").Value);
            Return response = await u.UserRepository.Update(request, UserId);
            u.Commit();
            if (!response.Valid)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, response.Message);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
