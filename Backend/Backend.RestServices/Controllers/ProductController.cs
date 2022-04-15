using Backend.Domain.Entities.Entities.Product.Command;
using Backend.Domain.Entities.Entities.Product.Queries;
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
    
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
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
            Return response = await u.ProductRepository.Delete(request, UserId);
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
        public async Task<ActionResult<IEnumerable<ProductModel>>> List(string Name)
        {
            return Ok(await _unitOfWork.ProductRepository.List(Name));
        }

        [HttpPost]
        public async Task<ActionResult<Return>> Create(ProductCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentProduct = User;
            int UserId = int.Parse(currentProduct.FindFirst("Id").Value);
            Return response = await u.ProductRepository.Create(request, UserId);
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
        public async Task<ActionResult<Return>> Update(ProductUpdateCommand request)
        {
            using IUnitOfWork u = _unitOfWork;
            ClaimsPrincipal currentProduct = User;
            int UserId = int.Parse(currentProduct.FindFirst("Id").Value);
            Return response = await u.ProductRepository.Update(request, UserId);
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
