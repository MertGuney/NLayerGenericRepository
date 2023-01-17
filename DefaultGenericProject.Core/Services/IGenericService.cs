using DefaultGenericProject.Core.DTOs.Paging;
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
        /// Senkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye Entity Döner.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Response<TEntity> GetById(Guid id);
        /// <summary>
        /// Senkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye TypeParam(class olmalıdır.) Olarak Tanımlanan DTO Nesnesini Döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="id"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Response<TDTO> GetById<TDTO>(Guid id) where TDTO : class;
        /// <summary>
        /// ASenkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye Entity döner.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Task<Response<TEntity>> GetByIdAsync(Guid id);
        /// <summary>
        /// ASenkron 'ID' alanına göre tekil veri getirme işlemidir. Geriye TypeParam(class olmalıdır.) Olarak Tanımlanan DTO Nesnesini Döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="id"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Task<Response<TDTO>> GetByIdAsync<TDTO>(Guid id) where TDTO : class;
        /// <summary>
        /// Senkron tüm verileri getirme işlemidir. IQueryable(Sorgulanabilir) Entity Döner.
        /// </summary>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Response<IQueryable<TEntity>> GetAll();
        /// <summary>
        /// Senkron tüm verileri getirme işlemidir.Geriye TypeParam(class olmalıdır.) IQueryable(Sorgulanabilir) DTO Nesnesini Döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Response<IQueryable<TDTO>> GetAll<TDTO>();
        /// <summary>
        /// Sayfalama ile tüm verileri getirme işlemi. Aramak istediğiniz sorguyu parametre olarak linq ile sorgulamanız gerekmektedir.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="pagingParamaterDTO"></param>
        /// <param name="predicate"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Response<PagingResponseDTO<TDTO>> GetAll<TDTO>(PagingParamaterDTO pagingParamaterDTO, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// ASenkron tüm verileri getirme işlemidir. Geriye Entity Listesi Döner.
        /// </summary>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> GetAllAsync();
        /// <summary>
        /// ASenkron tüm verileri getirme işlemidir. Geriye TypeParam(class olmalıdır.) tipinde DTO listesi döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TDTO>>> GetAllAsync<TDTO>();
        /// <summary>
        /// Sorgulama işlemidir. Func Delege alır. Geriye IEnumerable Entity döner.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Sorgulama işlemidir. Func Delege alır. Geriye TypeParam(class olmalıdır.) tipinde IEnumerable TDTO döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TDTO>>> Where<TDTO>(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Entity dahil etme işlemidir. Func Delege alır. Geriye IEnumerable Entity döner.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> Include(Expression<Func<TEntity, object>> expression);
        /// <summary>
        /// Entity dahil etme işlemidir. Func Delege alır. Geriye TypeParam(class olmalıdır.) tipinde IEnumerable TDTO döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TDTO>>> Include<TDTO>(Expression<Func<TEntity, object>> expression);
        /// <summary>
        /// Veri olup olmadığını kontrol eder. Func delege alır. Geriye Bool döner.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Veri ekleme işlemidir. Entity nesnesi alır geriye Entity nesnesi döner.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Response<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// Veri ekleme işlemidir. Entity nesnesi alır geriye TypeParam(class olmalıdır.) tipinde DTO nesnesi döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Response<TDTO>> AddAsync<TDTO>(TEntity entity) where TDTO : class;
        /// <summary>
        /// Liste halinde veri ekleme işlemidir. Liste olarak Entity nesnesi alır geriye Liste olarak Entity nesnesi döner.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Liste halinde veri ekleme işlemidir. Liste olarak Entity nesnesi alır geriye Liste olarak TypeParam(class olmalıdır.) tipinde DTO nesnesi döner.
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<TDTO>>> AddRangeAsync<TDTO>(IEnumerable<TEntity> entities);
        /// <summary>
        /// Veri güncelleme işlemidir. Entity alır. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data  olmayacak.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> Update(TEntity entity);
        /// <summary>
        /// Veri güncelleme işlemidir.Entity alır. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data  olmayacak.
        /// Entry State => modified
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> UpdateEntryState(TEntity entity);
        /// <summary>
        /// Verilen ID verisinden entity nesnesini bulup status durumunu inactive yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> SetInactiveById(Guid id);
        /// <summary>
        /// Veri silme işlemidir. Geriye data dönmez.
        /// 204 durum kodu =>  No Content  => Response body'sinde hiç bir data olmayacak.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> Remove(Guid id);
        /// <summary>
        /// Çoklu veri silme işlemidir. Liste olarak Entity alır. Geriye data dönmez.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}