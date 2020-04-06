using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringCore
{
    public static class SensorReadingFactory
    {
        public static ISensorReading GetSensorReading(RawSensorReading rawReading)
        {

            var mapperconfig = new MapperConfiguration(c =>
            {
                c.CreateMap<RawSensorReading, LightSensorReading>();
            });


            var mapper = mapperconfig.CreateMapper();
            ISensorReading reading = null;

            switch (rawReading.Sensor.SensorType)
            {
                case SensorType.Light:
                    reading = mapper.Map<LightSensorReading>(rawReading);
                    break;
                default:
                    return null;
            }
            reading.Calculate();
            return reading;
        }
    }
}
