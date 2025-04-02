using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JOIEnergy.Domain
{
    public class MeterReadings
    {
        [Required(ErrorMessage = "SmartMeterId is required")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerSchema(Description = "The ID of the smart meter")]
        public string SmartMeterId { get; set; }

        [Required(ErrorMessage = "At least one electricity reading is required")]
        [MinLength(1, ErrorMessage = "At least one electricity reading is required")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerSchema(Description = "List of electricity readings")]
        public List<ElectricityReading> ElectricityReadings { get; set; }
    }
}
