using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Models;
using SampleApp.Application.Common.Exceptions;
using SampleApp.Application.UseCases.Covert;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController(IMediator mediator) : ControllerBase
    {
        [HttpPost(Name = "ExchangeRate")]
        public async Task<IActionResult> ExchangeRate(ExchangeRateRequest request)
        {
            try
            {
                var response = await mediator.Send(new Request()
                {
                    Amount = request.Amount,
                    InputCurrency = request.InputCurrency,
                    OutputCurrency = request.OutputCurrency
                });

                return Ok(new ExchangeRateResponse()
                {
                    Amount = request.Amount,
                    InputCurrency = request.InputCurrency,
                    OutputCurrency = request.OutputCurrency,
                    Value = response.Value
                });
            }
            catch (ValidationFailedException ex)
            {
                var validationErrors = ex.Failures.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Errors = validationErrors });
            }
            catch (ExternalServiceException ex)
            {
                var errors = ex.Error.Select(x => x).ToList();
                return StatusCode(500, new { Errors = errors });
            }
        }
    }
}
