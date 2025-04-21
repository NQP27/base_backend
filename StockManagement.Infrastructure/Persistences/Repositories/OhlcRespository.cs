using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.DTOs.Query;
using StockManagement.Domain.Interfaces.Context;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Shared.Handler;

namespace StockManagement.Infrastructure.Persistences.Repositories
{
    public class OhlcRepository : IOhlcRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public OhlcRepository(
            IConfiguration configuration,
            IApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        #region ListSymbol
        public async Task<PaginatedData<AssetDTO>> ListSymbol(ListSymbolQueryDTO query, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var queryResult = from asset in _applicationDbContext.assets
                              select asset;
            // Lọc theo Symbol nếu được cung cấp
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                queryResult = queryResult.Where(x => x.symbol.ToLower().Contains(query.Symbol));
            }
            var cnt = queryResult.Count();
            var result = await queryResult.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
            return new PaginatedData<AssetDTO>
            {
                TotalCount = cnt,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                Data = _mapper.Map<List<AssetDTO>>(result)
            };
        }
        #endregion
        #region ListCandleM1
        public async Task<List<OhlcDTO>> ListCandleM1(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_m1
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            if (request.id_h4 != null)
            {
                query = query.Where(x => x.id_h4 == request.id_h4);
            }

            if (request.id_h1 != null)
            {
                query = query.Where(x => x.id_h1 == request.id_h1);
            }

            if (request.id_m30 != null)
            {
                query = query.Where(x => x.id_m30 == request.id_m30);
            }

            if (request.id_m15 != null)
            {
                query = query.Where(x => x.id_m15 == request.id_m15);
            }

            if (request.id_m5 != null)
            {
                query = query.Where(x => x.id_m5 == request.id_m5);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleM5
        public async Task<List<OhlcDTO>> ListCandleM5(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_m5
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            if (request.id_h4 != null)
            {
                query = query.Where(x => x.id_h4 == request.id_h4);
            }

            if (request.id_h1 != null)
            {
                query = query.Where(x => x.id_h1 == request.id_h1);
            }

            if (request.id_m30 != null)
            {
                query = query.Where(x => x.id_m30 == request.id_m30);
            }

            if (request.id_m15 != null)
            {
                query = query.Where(x => x.id_m15 == request.id_m15);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleM15
        public async Task<List<OhlcDTO>> ListCandleM15(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_m15
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            if (request.id_h4 != null)
            {
                query = query.Where(x => x.id_h4 == request.id_h4);
            }

            if (request.id_h1 != null)
            {
                query = query.Where(x => x.id_h1 == request.id_h1);
            }

            if (request.id_m30 != null)
            {
                query = query.Where(x => x.id_m30 == request.id_m30);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleM30
        public async Task<List<OhlcDTO>> ListCandleM30(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_m30
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            if (request.id_h4 != null)
            {
                query = query.Where(x => x.id_h4 == request.id_h4);
            }

            if (request.id_h1 != null)
            {
                query = query.Where(x => x.id_h1 == request.id_h1);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleH1
        public async Task<List<OhlcDTO>> ListCandleH1(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_h1
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            if (request.id_h4 != null)
            {
                query = query.Where(x => x.id_h4 == request.id_h4);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleH4
        public async Task<List<OhlcDTO>> ListCandleH4(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_h4
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            if (request.id_h12 != null)
            {
                query = query.Where(x => x.id_h12 == request.id_h12);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleH12
        public async Task<List<OhlcDTO>> ListCandleH12(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_h12
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            if (request.id_day != null)
            {
                query = query.Where(x => x.id_day == request.id_day);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleDay
        public async Task<List<OhlcDTO>> ListCandleDay(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_day
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            if (request.id_week != null)
            {
                query = query.Where(x => x.id_week == request.id_week);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleWeek
        public async Task<List<OhlcDTO>> ListCandleWeek(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_week
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            // Lọc theo các ID
            if (request.id_month != null)
            {
                query = query.Where(x => x.id_month == request.id_month);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion

        #region ListCandleMonth
        public async Task<List<OhlcDTO>> ListCandleMonth(ListCandleQueryDTO request, CancellationToken cancellationToken)
        {
            // Sử dụng cú pháp LINQ trực tiếp để tránh vấn đề với tên thuộc tính
            var query = from candle in _applicationDbContext.ohlc_month
                        where candle.asset_id == request.SymbolID
                        select candle;

            // Lọc theo Symbol nếu được cung cấp
            //if (!string.IsNullOrEmpty(request.Symbol))
            //{
            //    query = from candle in query
            //            join asset in _applicationDbContext.assets
            //            on candle.asset_id equals asset.id
            //            where asset.symbol == request.Symbol
            //            select candle;
            //}

            // Lọc theo khoảng thời gian
            if (request.From != null)
            {
                query = query.Where(x => x.datetime >= request.From.Value.AddHours(7));
            }

            if (request.To != null)
            {
                query = query.Where(x => x.datetime <= request.To.Value.AddHours(7));
            }

            // Lọc theo Broker
            if (!string.IsNullOrEmpty(request.Broker))
            {
                query = query.Where(x => x.broker == request.Broker);
            }

            var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.datetime).ToListAsync(cancellationToken);
            return _mapper.Map<List<OhlcDTO>>(result);
        }
        #endregion
    }
}
