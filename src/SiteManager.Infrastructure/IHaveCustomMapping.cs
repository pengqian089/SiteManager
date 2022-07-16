using AutoMapper;

namespace SiteManager.Infrastructure
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(MapperConfigurationExpression cfg);
    }
}