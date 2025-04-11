using StockManagement.Domain.DTOs;
using StockManagement.Domain.DTOs.Query;

namespace StockManagement.Domain.Interfaces.Repositories
{
    public interface IOhlcRepository
    {
        Task<List<OhlcDTO>> ListCandleM1(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleM5(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleM15(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleM30(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleH1(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleH4(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleH12(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleDay(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleWeek(ListCandleQueryDTO query, CancellationToken cancellationToken);
        Task<List<OhlcDTO>> ListCandleMonth(ListCandleQueryDTO query, CancellationToken cancellationToken);
    }
}
