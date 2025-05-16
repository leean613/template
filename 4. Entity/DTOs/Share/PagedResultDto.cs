namespace DTOs.Share
{
    public class PagedResultDto<T> : IPagedResultDto<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public T[] Items { get; set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public object ExtraData { get; set; }

        public object ExtraData2 { get; set; }

        public PagedResultDto() => Items = new T[0];
    }

    public static class EmptyPagedResult<T>
    {
        public static readonly IPagedResultDto<T> Instance = new PagedResultDto<T>();
    }
}
