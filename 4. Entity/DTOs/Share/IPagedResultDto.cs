namespace DTOs.Share
{
    public interface IPagedResultDto<T>
    {
        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        T[] Items { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        object ExtraData { get; set; }

        object ExtraData2 { get; set; }
    }
}
