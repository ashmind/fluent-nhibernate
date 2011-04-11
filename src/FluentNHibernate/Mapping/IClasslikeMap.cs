using System;

namespace FluentNHibernate.Mapping
{
    public interface IClasslikeMap
    {
        Type EntityType { get; }
        SqlQueryPart SqlQuery(string name, string queryText);
    }
}