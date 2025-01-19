namespace CA.And.DDD.Template.Application.Shared
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        private static readonly string[] ValidSortOrders = { "asc", "desc" };

        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SortBy { get; }
        public string SortOrder { get; }
        public string OrderColumn { get; }

        public PaginationParameters(int pageNumber = 1, int pageSize = 10, string? sortBy = null, string? sortOrder = "asc", string orderColumn = null)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > MaxPageSize ? MaxPageSize : (pageSize < 1 ? 1 : pageSize);
            SortBy = sortBy;
            SortOrder = !string.IsNullOrEmpty(sortOrder) && ValidSortOrders.Contains(sortOrder.ToLower())
                ? sortOrder.ToLower()
                : "asc";
            OrderColumn = orderColumn;
        }
        public string GetOrdering => !string.IsNullOrWhiteSpace(OrderColumn) ? string.Join(',', OrderColumn.Split(',').Select(x => $"{x} {SortOrder}")) : null;
    }
}
