using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly Calculator _calculator;

        public CalculatorController(Calculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("calculate")]
        public ActionResult<CalculationResponse> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                _calculator.Calculate(request.Expression!);
                var last = _calculator.Memory.Last();

                if (last.Type == MathLogTypes.NumericBased)
                {
                    return Ok(new CalculationResponse
                    {
                        Expression = last.Expression,
                        NumericResult = last.NumericResult
                    });
                }
                else
                {
                    return Ok(new CalculationResponse
                    {
                        Expression = last.Expression,
                        NumericResult = (double?)last.QuantityResult.Value,
                        UnitResult = last.QuantityResult.Unit.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResponse
                {
                    Expression = request.Expression,
                    //Error = ex.Message
                });
            }
        }

        [HttpGet("history")]
        public ActionResult<IEnumerable<CalculationHistoryItem>> GetHistory()
        {
            var history = _calculator.Memory.Select(item => new CalculationHistoryItem
            {
                Expression = item.Expression,
                NumericResult = item.Type == MathLogTypes.NumericBased ? item.NumericResult : (double?)item.QuantityResult?.Value,
                UnitResult = item.Type == MathLogTypes.UnitBased ? item.QuantityResult?.Unit.ToString() : null,
                Type = item.Type?.ToString() ?? "Unknown"
            }).ToList();

            return Ok(history);
        }
    }
}
