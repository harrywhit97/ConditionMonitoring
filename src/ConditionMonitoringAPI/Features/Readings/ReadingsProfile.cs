using AutoMapper;
using ConditionMonitoringAPI.Dtos;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class ReadingsProfile : Profile
    {
        public ReadingsProfile()
        {
            CreateMap<RawSensorReadingDto, RawSensorReading>();
        }
    }
}
