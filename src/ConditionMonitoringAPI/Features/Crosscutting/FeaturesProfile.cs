using AutoMapper;
using ConditionMonitoringAPI.Features.Boards;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class FeaturesProfile : Profile
    {
        public FeaturesProfile()
        {
            CreateMap<RawSensorReadingDto, ISensorReading>();
            CreateMap<LightSensorReadingDto, LightSensorReading>();

            CreateMap<BoardDto, Board>();
            CreateMap<SensorDto, Sensor<ISensorReading>>();
        }
    }
}
