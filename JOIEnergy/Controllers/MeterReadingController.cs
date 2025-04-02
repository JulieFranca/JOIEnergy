using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JOIEnergy.Domain;
using JOIEnergy.Services.MeterReading;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using JOIEnergy.Enums;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [ApiController]
    [Route("readings")]
    [Authorize]
    public class MeterReadingController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly Dictionary<string, Supplier> _smartMeterToPricePlanAccounts;

        public MeterReadingController(IMeterReadingService meterReadingService, Dictionary<string, Supplier> smartMeterToPricePlanAccounts)
        {
            _meterReadingService = meterReadingService;
            _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts;
        }

        [HttpPost("store")]
        [SwaggerOperation(
            Summary = "Store meter readings",
            Description = "Stores electricity readings for a specific smart meter",
            OperationId = "StoreReadings",
            Tags = new[] { "Readings" }
        )]
        [SwaggerResponse(200, "Readings stored successfully")]
        [SwaggerResponse(400, "Invalid input")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Smart meter not found")]
        public IActionResult StoreReadings([FromBody] MeterReadings meterReadings)
        {
            if (!IsMeterReadingsValid(meterReadings))
            {
                return BadRequest(new { message = "Invalid meter readings data" });
            }

            if (!_smartMeterToPricePlanAccounts.ContainsKey(meterReadings.SmartMeterId))
            {
                return NotFound(new { message = $"Smart meter ID '{meterReadings.SmartMeterId}' not found" });
            }

            _meterReadingService.StoreReadings(meterReadings.SmartMeterId, meterReadings.ElectricityReadings);
            return Ok(new { message = "Readings stored successfully" });
        }

        private bool IsMeterReadingsValid(MeterReadings meterReadings)
        {
            return meterReadings != null 
                && !string.IsNullOrEmpty(meterReadings.SmartMeterId)
                && meterReadings.ElectricityReadings != null 
                && meterReadings.ElectricityReadings.Any();
        }

        [HttpGet("read/{smartMeterId}")]
        [SwaggerOperation(
            Summary = "Get meter readings",
            Description = "Retrieves all readings for a specific smart meter",
            OperationId = "GetReadings",
            Tags = new[] { "Readings" }
        )]
        [SwaggerResponse(200, "Readings retrieved successfully")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Smart meter not found")]
        public IActionResult GetReading(string smartMeterId)
        {
            if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId))
            {
                return NotFound(new { message = $"Smart meter ID '{smartMeterId}' not found" });
            }

            var readings = _meterReadingService.GetReadings(smartMeterId);
            if (!readings.Any())
            {
                return NotFound(new { message = "No readings found for this smart meter" });
            }
            return Ok(readings);
        }
    }
}
