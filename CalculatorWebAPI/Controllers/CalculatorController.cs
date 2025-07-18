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
        private readonly IRepository _repository;

        public CalculatorController(Calculator calculator, IRepository repository)
        {
            _calculator = calculator;
            _repository = repository;
        }

        [HttpPost("calculate")]
        public ActionResult<CalculationResponse> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                _calculator.Calculate(request.Expression!);                
                var result = _calculator.Memory.Last();
                _repository.Save(string.Empty);
                return Ok(new CalculationResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error calculating expression: {ex.Message}");
            }
        }

        [HttpGet("history")]
        public ActionResult<IEnumerable<CalculationHistoryItem>> GetHistory()
        {
            var history = _calculator.Memory.Select(item => new CalculationHistoryItem
            {
                Expression = item.Expression,
                NumericResult = item.Type == MathLogTypes.NumericBased ? item.NumericResult : (double?)item.QuantityResult?.Value,
                UnitResult = item.Type == MathLogTypes.UnitBased ? item.QuantityResult?.Unit.ToString() : null
            }).ToList();

            return Ok(history);
        }
    }
}
