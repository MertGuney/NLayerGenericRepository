using DefaultGenericProject.Core.DTOs.Responses;
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
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TEntity>> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                return Response<TEntity>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommmitAsync();

            var newDTO = ObjectMapper.Mapper.Map<TEntity>(newEntity);

            return Response<TEntity>.Success(newDTO, 200);
        }

        public async Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return Response<IEnumerable<TEntity>>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntities = ObjectMapper.Mapper.Map<List<TEntity>>(entities);
            await _genericRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommmitAsync();
            var newDTOs = ObjectMapper.Mapper.Map<List<TEntity>>(newEntities);
            return Response<IEnumerable<TEntity>>.Success(newDTOs, 204);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _genericRepository.AnyAsync(expression);
        }

        public Response<IQueryable<TEntity>> GetAll()
        {
            var results = ObjectMapper.Mapper.Map<IQueryable<TEntity>>(_genericRepository.GetAll());
            if (results == null)
            {
                return Response<IQueryable<TEntity>>.Fail("Sonuç bulunamadı.", 400, true);
            }
            return Response<IQueryable<TEntity>>.Success(results, 200);
        }

        public async Task<Response<IEnumerable<TEntity>>> GetAllAsync()
        {
            var results = ObjectMapper.Mapper.Map<List<TEntity>>(await _genericRepository.GetAllAsync());
            if (results == null)
            {
                return Response<IEnumerable<TEntity>>.Fail("Sonuç bulunamadı.", 400, true);
            }
            return Response<IEnumerable<TEntity>>.Success(results, 200);
        }

        public Response<TEntity> GetById(string id)
        {
            var result = _genericRepository.GetById(id);
            if (result == null)
            {
                return Response<TEntity>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TEntity>.Success(ObjectMapper.Mapper.Map<TEntity>(result), 200);
        }

        public async Task<Response<TEntity>> GetByIdAsync(string id)
        {
            var result = await _genericRepository.GetByIdAsync(id);
            if (result == null)
            {
                return Response<TEntity>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TEntity>.Success(ObjectMapper.Mapper.Map<TEntity>(result), 200);
        }

        public async Task<Response<NoDataDTO>> Remove(string id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            _genericRepository.Remove(isExistEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return Response<NoDataDTO>.Fail("Model boş olamaz.", 400, true);
            }
            var newEntities = ObjectMapper.Mapper.Map<List<TEntity>>(entities);
            _genericRepository.RemoveRange(newEntities);
            await _unitOfWork.CommmitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> SetInactive(TEntity entity)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(entity.Id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.SetInactive(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> Update(TEntity entity)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(entity.Id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> UpdateEntryState(TEntity entity)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(entity.Id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.UpdateEntryState(updateEntity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var resultList = await _genericRepository.Where(predicate).ToListAsync();

            return Response<IEnumerable<TEntity>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TEntity>>(resultList), 200);
        }
    }
}