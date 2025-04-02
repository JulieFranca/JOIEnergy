using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Enums;
using JOIEnergy.Services.Account;
using JOIEnergy.Services.Price;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("price-plans")]
    [Authorize]
    public class PricePlanComparatorController : Controller
    {
        private readonly IPricePlanService _pricePlanService;
        private readonly IAccountService _accountService;

        public PricePlanComparatorController(IPricePlanService pricePlanService, IAccountService accountService)
        {
            this._pricePlanService = pricePlanService;
            this._accountService = accountService;
        }

        [HttpGet("compare-all/{smartMeterId}")]
        [SwaggerOperation(
            Summary = "Compare all price plans",
            Description = "Calculates cost for each price plan for a specific smart meter",
            OperationId = "CompareAllPricePlans",
            Tags = new[] { "Price Plans" }
        )]
        [SwaggerResponse(200, "Price plans compared successfully")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Smart meter not found")]
        public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
        {
            Supplier pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            Dictionary<string, decimal> costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);
            if (!costPerPricePlan.Any())
            {
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }

            return
                costPerPricePlan.Any() ? 
                new ObjectResult(costPerPricePlan) : 
                new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
        }

        [HttpGet("recommend/{smartMeterId}")]
        [SwaggerOperation(
            Summary = "Recommend cheapest price plans",
            Description = "Recommends the cheapest price plans for a specific smart meter",
            OperationId = "RecommendCheapestPricePlans",
            Tags = new[] { "Price Plans" }
        )]
        [SwaggerResponse(200, "Recommendations retrieved successfully")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Smart meter not found")]
        public ObjectResult RecommendCheapestPricePlans(string smartMeterId, int? limit = null) {
            var consumptionForPricePlans = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

            if (!consumptionForPricePlans.Any()) {
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }

            var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

            if (limit.HasValue && limit.Value < recommendations.Count())
            {
                return new ObjectResult(recommendations.Take(limit.Value));
            }

            return new ObjectResult(recommendations);
        }
    }
}
