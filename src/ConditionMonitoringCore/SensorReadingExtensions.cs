using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using System;

namespace ConditionMonitoringCore
{
    public static class SensorReadingExtensions
    {
        public static void Calculate(this ISensorReading sensorReading)
        {
            switch (sensorReading.Sensor.SensorType) 
            {
                case SensorType.Light:
                    var lightReading = sensorReading as LightSensorReading;
                    lightReading.Brightness = (int)Math.Round(lightReading.RawReading * 2); //example
                    break;
            }
        }
    }
}
