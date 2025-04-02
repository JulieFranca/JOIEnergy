using System;
using JOIEnergy.Services.Price;
using JOIEnergy.Services.MeterReading;
using Xunit;
using Moq;
using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Tests
{
    public class PricePlanServiceTest
    {
        private readonly IPricePlanService _pricePlanService;
        private readonly Mock<IMeterReadingService> _mockMeterReadingService;
        private List<PricePlan> _pricePlans;

        public PricePlanServiceTest()
        {
            _mockMeterReadingService = new Mock<IMeterReadingService>();
            _pricePlans = new List<PricePlan>();
            _pricePlanService = new PricePlanService(_pricePlans, _mockMeterReadingService.Object);

            _mockMeterReadingService.Setup(service => service.GetReadings(It.IsAny<string>())).Returns(new List<ElectricityReading>(){new ElectricityReading(),
                new ElectricityReading()});
        }
    }
}
