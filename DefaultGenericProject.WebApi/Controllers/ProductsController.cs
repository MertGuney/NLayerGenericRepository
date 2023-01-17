using DefaultGenericProject.Core.Constants;
using DefaultGenericProject.Core.DTOs;
using DefaultGenericProject.Core.DTOs.Paging;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Services;
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
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IGenericService<Product> genericService, IUnitOfWork unitOfWork)
        {
            _genericService = genericService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }

        [HttpGet("UoW")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _unitOfWork.ProductRepository.GetAllAsync();
            return Ok();
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
        public async Task<IActionResult> Post(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.AddAsync(ObjectMapper.Mapper.Map<Product>(productDTO)));
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductDTO productDTO)
        {
            return ActionResultInstance(await _genericService.Update(ObjectMapper.Mapper.Map<Product>(productDTO)));
        }
    }
}
