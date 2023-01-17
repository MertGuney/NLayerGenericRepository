using DefaultGenericProject.Core.DTOs;
using DefaultGenericProject.Core.DTOs.Paging;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.StringInfos;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Service.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product> _genericService;

        public ProductsController(IGenericService<Product> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }

        [HttpGet("Search")]
        public IActionResult GetAll([FromQuery] PagingParamaterDTO pagingParamaterDTO)
        {
            return ActionResultInstance(_genericService.GetAll<ProductDTO>(pagingParamaterDTO, x => x.Name.Contains(pagingParamaterDTO.Search)));
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
    }
}
