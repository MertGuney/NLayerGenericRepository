using DefaultGenericProject.Core.DTOs;
using DefaultGenericProject.Core.Enums;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.StringInfos;
using DefaultGenericProject.Service.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleInfo.ProductList)]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product> _genericService;

        public ProductsController(IGenericService<Product> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return ActionResultInstance(await _genericService.GetByIdAsync(id));
        }
        [HttpPost]
        [Authorize(Roles = RoleInfo.ProductAdd)]
        public async Task<IActionResult> Post(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.AddAsync(ObjectMapper.Mapper.Map<Product>(productDTO)));
        }
        [HttpPut]
        [Authorize(Roles = RoleInfo.ProductUpdate)]
        public async Task<IActionResult> Put(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.Update(ObjectMapper.Mapper.Map<Product>(productDTO)));
        }
        [HttpDelete]
        [Authorize(Roles = RoleInfo.ProductRemove)]
        public async Task<IActionResult> Remove(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.SetStatus(ObjectMapper.Mapper.Map<Product>(productDTO), DataStatus.Inactive));
        }
    }
}
