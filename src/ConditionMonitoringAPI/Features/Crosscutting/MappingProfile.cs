using AutoMapper;
using ConditionMonitoringAPI.Features.Boards;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RawSensorReadingDto, ISensorReading>();
            CreateMap<LightSensorReadingDto, LightSensorReading>();
            CreateMap<BoardDto, Board>();
            CreateMap<SensorDto, Sensor<ISensorReading>>();
        }
    }
}
