using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.DTOs.Query;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Shared.Handler;

namespace StockManagement.Application.CQRS.Query
{
    public class ListSymbolQuery : IRequest<PaginatedData<AssetDTO>>
    {
        public int PageSize { get; set; } = 1000;
        public int PageNumber { get; set; } = 1;
        public string? Symbol { get; set; }
    }

    public class ListSymbolQueryHandler : IRequestHandler<ListSymbolQuery, PaginatedData<AssetDTO>>
    {
        private readonly ILogger<ListSymbolQueryHandler> _logger;
        private readonly ISender _sender;
        private readonly IOhlcRepository _ohlcRepo;
        private readonly IMapper _mapper;

        public ListSymbolQueryHandler(
            IOhlcRepository ohlcRepo,
            IMapper mapper,
            ILogger<ListSymbolQueryHandler> logger,
            ISender sender)
        {
            _logger = logger;
            _sender = sender;
            _ohlcRepo = ohlcRepo;
            _mapper = mapper;
        }

        public async Task<PaginatedData<AssetDTO>> Handle(ListSymbolQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reqMap = _mapper.Map<ListSymbolQueryDTO>(request);
                var result = await _ohlcRepo.ListSymbol(reqMap, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling ListSymbolQuery");
                throw;
            }
        }
    }
}
