using System.Collections.Generic;

namespace DefaultGenericProject.Core.DTOs.Paging
{
    public class PagingResponseDTO<T>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Values { get; set; } = new List<T>();
    }
}
