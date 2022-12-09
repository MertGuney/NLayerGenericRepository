namespace DefaultGenericProject.Core.DTOs.Paging
{
    public class PagingParamaterDTO
    {
        private const int maxPageSize = 100;
        private int _pageSize = 50;

        public string Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
