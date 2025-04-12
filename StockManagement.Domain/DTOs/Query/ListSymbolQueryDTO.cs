namespace StockManagement.Domain.DTOs.Query
{
    public class ListSymbolQueryDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Symbol { get; set; }
    }
}
