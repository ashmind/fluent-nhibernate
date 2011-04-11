using FluentNHibernate.MappingModel.Queries;

namespace FluentNHibernate.Mapping.Providers
{
    public interface IQueryMappingProvider
    {
        IQueryMapping GetQueryMapping();
    }
}