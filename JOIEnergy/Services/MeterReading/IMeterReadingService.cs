using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services.MeterReading
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}