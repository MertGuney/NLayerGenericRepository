using DefaultGenericProject.Core.DTOs.Paging;
using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Enums;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Service.Mapping;
using DefaultGenericProject.Service.Services.Helpers;
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
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommmitAsync();
            return Response<TEntity>.Success(entity, 201);
        }

        public async Task<Response<TDTO>> AddAsync<TDTO>(TEntity entity) where TDTO : class
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommmitAsync();

            var newEntity = ObjectMapper.Mapper.Map<TDTO>(entity);
            return Response<TDTO>.Success(newEntity, 201);
        }

        public async Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommmitAsync();
            return Response<IEnumerable<TEntity>>.Success(entities, 201);
        }

        public async Task<Response<IEnumerable<TDTO>>> AddRangeAsync<TDTO>(IEnumerable<TEntity> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommmitAsync();
            var newEntities = ObjectMapper.Mapper.Map<List<TDTO>>(entities);
            return Response<IEnumerable<TDTO>>.Success(newEntities, 201);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _genericRepository.AnyAsync(expression);
        }

        public Response<IQueryable<TEntity>> GetAll(DataStatus? dataStatus = DataStatus.Active)
        {
            var results = _genericRepository.GetAll(dataStatus);
            if (results == null)
            {
                return Response<IQueryable<TEntity>>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<IQueryable<TEntity>>.Success(results, 200);
        }

        public Response<IQueryable<TDTO>> GetAll<TDTO>(DataStatus? dataStatus = DataStatus.Active)
        {
            var results = _genericRepository.GetAll(dataStatus);
            if (results == null)
            {
                return Response<IQueryable<TDTO>>.Fail("Sonuç bulunamadı.", 404, true);
            }
            var resultsMap = ObjectMapper.Mapper.Map<IQueryable<TDTO>>(results);
            return Response<IQueryable<TDTO>>.Success(resultsMap, 200);
        }

        public Response<PagingResponseDTO<TDTO>> GetAll<TDTO>(PagingParamaterDTO pagingParamaterDTO, Expression<Func<TEntity, bool>> predicate, DataStatus? dataStatus = DataStatus.Active)
        {
            var results = _genericRepository.GetAll(dataStatus);
            if (results == null)
            {
                return Response<PagingResponseDTO<TDTO>>.Fail("Sonuç bulunamadı.", 404, true);
            }
            PagingResponseDTO<TDTO> result;
            if (pagingParamaterDTO.Search != null)
            {
                result = PagedList<TEntity>.GetValues<TDTO>(results.Where(predicate).OrderBy(x => x.CreatedDate), pagingParamaterDTO.PageNumber, pagingParamaterDTO.PageSize);
            }
            else
            {
                result = PagedList<TEntity>.GetValues<TDTO>(results.OrderBy(x => x.CreatedDate), pagingParamaterDTO.PageNumber, pagingParamaterDTO.PageSize);
            }
            return Response<PagingResponseDTO<TDTO>>.Success(result, 200);
        }

        public async Task<Response<IEnumerable<TEntity>>> GetAllAsync(DataStatus? dataStatus = DataStatus.Active)
        {
            var results = await _genericRepository.GetAllAsync(dataStatus);
            if (results == null)
            {
                return Response<IEnumerable<TEntity>>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<IEnumerable<TEntity>>.Success(results, 200);
        }

        public async Task<Response<IEnumerable<TDTO>>> GetAllAsync<TDTO>(DataStatus? dataStatus = DataStatus.Active)
        {
            var results = await _genericRepository.GetAllAsync(dataStatus);
            if (results == null)
            {
                return Response<IEnumerable<TDTO>>.Fail("Sonuç bulunamadı.", 404, true);
            }
            var resultsMap = ObjectMapper.Mapper.Map<IEnumerable<TDTO>>(results);
            return Response<IEnumerable<TDTO>>.Success(resultsMap, 200);
        }

        public Response<TEntity> GetById(Guid id, DataStatus? dataStatus = DataStatus.Active)
        {
            var result = _genericRepository.GetById(id, dataStatus);
            if (result == null)
            {
                return Response<TEntity>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TEntity>.Success(result, 200);
        }

        public Response<TDTO> GetById<TDTO>(Guid id, DataStatus? dataStatus = DataStatus.Active) where TDTO : class
        {
            var result = _genericRepository.GetById(id, dataStatus);
            if (result == null)
            {
                return Response<TDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }
            var resultMap = ObjectMapper.Mapper.Map<TDTO>(result);
            return Response<TDTO>.Success(resultMap, 200);
        }

        public async Task<Response<TEntity>> GetByIdAsync(Guid id, DataStatus? dataStatus = DataStatus.Active)
        {
            var result = await _genericRepository.GetByIdAsync(id, dataStatus);
            if (result == null)
            {
                return Response<TEntity>.Fail("Sonuç bulunamadı.", 404, true);
            }
            return Response<TEntity>.Success(result, 200);
        }

        public async Task<Response<TDTO>> GetByIdAsync<TDTO>(Guid id, DataStatus? dataStatus = DataStatus.Active) where TDTO : class
        {
            var result = await _genericRepository.GetByIdAsync(id, dataStatus);
            if (result == null)
            {
                return Response<TDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }
            var resultMap = ObjectMapper.Mapper.Map<TDTO>(result);
            return Response<TDTO>.Success(resultMap, 200);
        }

        public async Task<Response<NoDataDTO>> Remove(Guid id)
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
            var newEntities = ObjectMapper.Mapper.Map<List<TEntity>>(entities);
            _genericRepository.RemoveRange(newEntities);
            await _unitOfWork.CommmitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> SetStatus(Guid id, DataStatus dataStatus)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            _genericRepository.SetStatus(isExistEntity, dataStatus);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> SetInactiveById(Guid id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Sonuç bulunamadı.", 404, true);
            }

            _genericRepository.SetInactive(isExistEntity);
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

            _genericRepository.Update(entity);
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

            _genericRepository.UpdateEntryState(entity);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var results = await _genericRepository.Where(predicate).ToListAsync();
            return Response<IEnumerable<TEntity>>.Success(results, 200);
        }

        public async Task<Response<IEnumerable<TDTO>>> Where<TDTO>(Expression<Func<TEntity, bool>> predicate)
        {
            var results = await _genericRepository.Where(predicate).ToListAsync();
            return Response<IEnumerable<TDTO>>.Success(ObjectMapper.Mapper.Map<List<TDTO>>(results), 200);
        }

        public async Task<Response<IEnumerable<TEntity>>> Include(Expression<Func<TEntity, object>> expression)
        {
            var results = await _genericRepository.Include(expression).ToListAsync();
            return Response<IEnumerable<TEntity>>.Success(results, 200);
        }

        public async Task<Response<IEnumerable<TDTO>>> Include<TDTO>(Expression<Func<TEntity, object>> expression)
        {
            var results = await _genericRepository.Include(expression).ToListAsync();
            return Response<IEnumerable<TDTO>>.Success(ObjectMapper.Mapper.Map<List<TDTO>>(results), 200);
        }
    }
}