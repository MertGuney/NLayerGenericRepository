using DefaultGenericProject.Core.Dtos.Responses;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            if (entity == null)
            {
                return Response<TDto>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommmitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> entities)
        {
            if (entities == null)
            {
                return Response<IEnumerable<TDto>>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntities = ObjectMapper.Mapper.Map<List<TEntity>>(entities);
            await _genericRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommmitAsync();
            var newDtos = ObjectMapper.Mapper.Map<List<TDto>>(newEntities);
            return Response<IEnumerable<TDto>>.Success(newDtos, 204);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _genericRepository.AnyAsync(expression);
        }

        public Response<IQueryable<TDto>> GetAll()
        {
            var results = ObjectMapper.Mapper.Map<IQueryable<TDto>>(_genericRepository.GetAll());
            if (results == null)
            {
                return Response<IQueryable<TDto>>.Fail("Sonuç bulunamadı.", 400, true);
            }
            return Response<IQueryable<TDto>>.Success(results, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var results = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            if (results == null)
            {
                return Response<IEnumerable<TDto>>.Fail("Sonuç bulunamadı.", 400, true);
            }
            return Response<IEnumerable<TDto>>.Success(results, 200);
        }

        public Response<TDto> GetById(string id)
        {
            var result = _genericRepository.GetById(id);
            if (result == null)
            {
                return Response<TDto>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(result), 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(string id)
        {
            var result = await _genericRepository.GetByIdAsync(id);
            if (result == null)
            {
                return Response<TDto>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(result), 200);
        }

        public async Task<Response<NoDataDto>> Remove(string id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Sonuç bulunamadı.", 404, true);
            }

            _genericRepository.Remove(isExistEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> RemoveRangeAsync(IEnumerable<TDto> entities)
        {
            if (entities == null)
            {
                return Response<NoDataDto>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntities = ObjectMapper.Mapper.Map<List<TEntity>>(entities);
            _genericRepository.RemoveRange(newEntities);
            await _unitOfWork.CommmitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> SetInactive(TDto entity, string id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.SetInactive(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto entity, string id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> UpdateEntryState(TDto entity, string id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.UpdateEntryState(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var resultList = await _genericRepository.Where(predicate).ToListAsync();

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(resultList), 200);
        }
    }
}