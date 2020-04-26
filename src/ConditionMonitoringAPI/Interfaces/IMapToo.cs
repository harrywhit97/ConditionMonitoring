using AutoMapper;

namespace ConditionMonitoringAPI.Interfaces
{
    public interface IMapToo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
