namespace CA.And.DDD.Template.Application.Shared
{
    public class PaginationParameters(int pageNumber = 1, int pageSize = 10, string? sortBy = null, string? sortOrder = "asc", string? orderColumn = null)
    {
        private const int _maxPageSize = 50;
        private static readonly string[] _validSortOrders = { "asc", "desc" };

        public int PageNumber { get; } = pageNumber < 1 ? 1 : pageNumber;
        public int PageSize { get; } = pageSize > _maxPageSize ? _maxPageSize : (pageSize < 1 ? 1 : pageSize);
        public string? SortBy { get; } = sortBy;
        public string SortOrder { get; } = !string.IsNullOrEmpty(sortOrder) && _validSortOrders.Contains(sortOrder.ToLower())
                ? sortOrder.ToLower()
                : "asc";
        public string? OrderColumn { get; } = orderColumn;

        public string? GetOrdering => !string.IsNullOrWhiteSpace(OrderColumn) ? string.Join(',', OrderColumn.Split(',').Select(x => $"{x} {SortOrder}")) : null;
    }
}
