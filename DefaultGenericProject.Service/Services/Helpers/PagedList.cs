using DefaultGenericProject.Core.DTOs.Paging;
using DefaultGenericProject.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultGenericProject.Service.Services.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public List<T> Data { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = items;
        }

        public static PagingResponseDTO<TDTO> GetValues<TDTO>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = ObjectMapper.Mapper.Map<List<TDTO>>(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            return new PagingResponseDTO<TDTO>
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Values = items
            };
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}