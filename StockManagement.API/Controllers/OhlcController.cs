using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.CQRS.Query;
using StockManagement.Domain.Interfaces.Repositories;

namespace StockManagement.API.Controllers
{
    [Route("api/ohlc")]
    [ApiController]
    public class OhlcController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OhlcController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenRepo;

        public OhlcController(
            ILogger<OhlcController> logger,
            IConfiguration configuration,
            IAuthenticationRepository authenRepo,
            IMediator mediator,
            IMapper mapper)
        {
            _configuration = configuration;
            _authenRepo = authenRepo;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        #region ListCandle
        [HttpPost("list-candles")]
        //[Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> ListCandle([FromBody] ListCandleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"OhlcController - ListCandle");
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new { Message = errorMessage });
            }
            try
            {
                var candles = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Candle(s) was fetched successfully!",
                    Data = candles
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Conflict occurred: {Message}", ex.Message);
                return Conflict(new { ex.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new
                {
                    Message = $"Internal error while fetching candle(s): {e.Message}"
                });
            }
        }
        #endregion

        #region ListCandle
        [HttpPost("list-symbol")]
        //[Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> ListSymbol([FromBody] ListSymbolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"OhlcController - ListCandle");
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new { Message = errorMessage });
            }
            try
            {
                var res = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Data(s) was fetched successfully!",
                    Data = res
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Conflict occurred: {Message}", ex.Message);
                return Conflict(new { ex.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new
                {
                    Message = $"Internal error while fetching data(s): {e.Message}"
                });
            }
        }
        #endregion


    }
}