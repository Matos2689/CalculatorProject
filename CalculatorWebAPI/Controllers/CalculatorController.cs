using CalculatorProject.BusinessLogic;
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
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
                return BadRequest("Expression cannot be empty.");

            try
            {
                _calculator.Calculate(request.Expression);
                var result = _calculator.Memory.Last();
                return Ok(new CalculationResult(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error calculating expression: {ex.Message}");
            }
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            var history = _calculator.Memory.Select(log => new CalculationResult(log));
            return Ok(history);
        }
    }
}
