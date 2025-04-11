using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.DTOs.Query;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Shared.Enum;

namespace StockManagement.Application.CQRS.Query
{
    public class ListCandleQuery : IRequest<List<OhlcDTO>>
    {
        public long? id_month { get; set; }
        public long? id_week { get; set; }
        public long? id_day { get; set; }
        public long? id_h12 { get; set; }
        public long? id_h4 { get; set; }
        public long? id_h1 { get; set; }
        public long? id_m30 { get; set; }
        public long? id_m15 { get; set; }
        public long? id_m5 { get; set; }
        public int TimeFrame { get; set; }
        public string Symbol { get; set; }
        public string Broker { get; set; }
        public DateTime? From { get; set; } = null;
        public DateTime? To { get; set; } = null;
    }

    public class ListCandleQueryHandler : IRequestHandler<ListCandleQuery, List<OhlcDTO>>
    {
        private readonly ILogger<ListCandleQueryHandler> _logger;
        private readonly ISender _sender;
        private readonly IOhlcRepository _ohlcRepo;
        private readonly IMapper _mapper;

        public ListCandleQueryHandler(
            IOhlcRepository ohlcRepo,
            IMapper mapper,
            ILogger<ListCandleQueryHandler> logger,
            ISender sender)
        {
            _logger = logger;
            _sender = sender;
            _ohlcRepo = ohlcRepo;
            _mapper = mapper;
        }

        public async Task<List<OhlcDTO>> Handle(ListCandleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reqMap = _mapper.Map<ListCandleQueryDTO>(request);
                List<OhlcDTO> result;

                switch (request.TimeFrame)
                {
                    case (int)TimeFrame.M1:
                        result = await _ohlcRepo.ListCandleM1(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.M5:
                        result = await _ohlcRepo.ListCandleM5(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.M15:
                        result = await _ohlcRepo.ListCandleM15(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.M30:
                        result = await _ohlcRepo.ListCandleM30(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.H1:
                        result = await _ohlcRepo.ListCandleH1(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.H4:
                        result = await _ohlcRepo.ListCandleH4(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.H12:
                        result = await _ohlcRepo.ListCandleH12(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.DAY:
                        result = await _ohlcRepo.ListCandleDay(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.WEEK:
                        result = await _ohlcRepo.ListCandleWeek(reqMap, cancellationToken);
                        break;
                    case (int)TimeFrame.MONTH:
                        result = await _ohlcRepo.ListCandleMonth(reqMap, cancellationToken);
                        break;
                    default:
                        throw new ArgumentException($"Invalid timeframe: {request.TimeFrame}");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling ListCandleQuery");
                throw;
            }
        }
    }
}
