namespace School_Management_System.Application.Common
{
    /// <summary>
    /// Wraps a paged list result with metadata for the client.
    /// </summary>
    public class PagedResult<T>
    {
        public IEnumerable<T> Items      { get; }
        public int            TotalCount { get; }
        public int            Page       { get; }
        public int            PageSize   { get; }
        public int            TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool           HasNext    => Page < TotalPages;
        public bool           HasPrev    => Page > 1;

        public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Items      = items;
            TotalCount = totalCount;
            Page       = page;
            PageSize   = pageSize;
        }
    }
}
