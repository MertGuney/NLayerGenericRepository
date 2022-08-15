using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services
{
    public interface IGenericService<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Senkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye DTO döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Response<TEntity> GetById(string id);
        /// <summary>
        /// ASenkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye DTO döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<TEntity>> GetByIdAsync(string id);
        /// <summary>
        /// Senkron tüm verileri getirme işlemidir. IQueryable(Sorgulanabilir) DTO data döner. 
        /// </summary>
        /// <returns></returns>
        Response<IQueryable<TEntity>> GetAll();
        /// <summary>
        /// ASenkron tüm verileri getirme işlemidir. Geriye DTO data döner.
        /// </summary>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> GetAllAsync();
        /// <summary>
        /// Sorgulama işlemidir. Func Delege alır. Geriye IEnumerable DTO döner.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Veri olup olmadığını kontrol eder. Func delege alır. Geriye Bool döner.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Veri ekleme işlemidir. DTO nesnesi alır geriye DTO nesnesi döner.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Response<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// Liste halinde veri ekleme işlemidir. Liste olarak DTO nesnesi alır geriye Liste olarak DTO nesnesi döner.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Veri güncelleme işlemidir. DTO ve 'ID' alır. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data  olmayacak.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> Update(TEntity entity);
        /// <summary>
        /// Veri güncelleme işlemidir. DTO ve 'ID' alır. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data  olmayacak.
        /// Entry State => modified
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> UpdateEntryState(TEntity entity);
        /// <summary>
        /// Veri durumunu pasife çekerek silme işlemidir.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> SetInactive(TEntity entity);
        /// <summary>
        /// Veri silme işlemidir. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data  olmayacak.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> Remove(string id);
        /// <summary>
        /// Çoklu veri silme işlemidir. Liste olarak Entity alır. Geriye data dönmez.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}