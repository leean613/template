namespace DTOs.Share
{
    public class PagedResultRequestDto
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public PagedResultRequestDto(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public PagedResultRequestDto()
        {
            Page = 0;
            PageSize = MaxPageSize;
        }

        public PagedResultRequestDto(int page)
        {
            Page = page;
            PageSize = MaxPageSize;
        }
    }
}
