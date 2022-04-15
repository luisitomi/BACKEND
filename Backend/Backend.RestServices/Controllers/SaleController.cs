using Backend.Domain.Entities.Entities.Sale.Command;
using Backend.Domain.Entities.Entities.Sale.Queries;
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
    
    public class SaleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleController(IUnitOfWork unitOfWork)
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
            ClaimsPrincipal currentProduct = User;
            int UserId = int.Parse(currentProduct.FindFirst("Id").Value);
            Return response = await u.SaleRepository.Delete(request, UserId);
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
        [Route("ById")]
        public async Task<ActionResult<List<SaleModel>>> ById()
        {
            ClaimsPrincipal currentSale = User;
            int UserId = int.Parse(currentSale.FindFirst("Id").Value);
            return Ok(await _unitOfWork.SaleRepository.ById(UserId));
        }

        [HttpGet]
        public async Task<ActionResult<SaleModel>> List(int Id)
        {
            return Ok(await _unitOfWork.SaleRepository.List(Id));
        }

        [HttpPost]
        public async Task<ActionResult<Return>> Create(SaleCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentSale = User;
            int UserId = int.Parse(currentSale.FindFirst("Id").Value);
            Return response = await u.SaleRepository.Create(request, UserId);
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
        public async Task<ActionResult<Return>> Update(SaleUpdateCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentSale = User;
            int UserId = int.Parse(currentSale.FindFirst("Id").Value);
            Return response = await u.SaleRepository.Update(request, UserId);
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
