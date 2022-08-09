using DefaultGenericProject.Core.DTOs;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.StringInfos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleInfo.ProductList)]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product, ProductDTO> _genericService;

        public ProductsController(IGenericService<Product, ProductDTO> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return ActionResultInstance(await _genericService.GetByIdAsync(id));
        }
        [HttpPost]
        [Authorize(Roles = RoleInfo.ProductAdd)]
        public async Task<IActionResult> Post(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.AddAsync(productDTO));
        }
        [HttpPut]
        [Authorize(Roles = RoleInfo.ProductUpdate)]
        public async Task<IActionResult> Put(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.Update(productDTO, productDTO.Id));
        }
        [HttpDelete]
        [Authorize(Roles = RoleInfo.ProductRemove)]
        public async Task<IActionResult> Remove(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.SetInactive(productDTO, productDTO.Id));
        }
    }
}
