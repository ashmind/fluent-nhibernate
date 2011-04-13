using System;
using System.Collections.Generic;
using System.Linq;

using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Queries;

namespace FluentNHibernate.Visitors
{
    /// <summary>
    /// NHibernate seems to ignore 'sql-query' if defined within a 'class'/'subclass'.
    /// On the other hand, it is completely legal according to NH schema.
    /// 
    /// So it may be assumed that either the NH schema will be changed do disallow 
    /// this, or NH configuration loader will be fixed. Since Fluent NHibernate 
    /// mapping API seems to be rather similar to the schema, it makes sense to allow
    /// Queries in SubclassMapping, even if it will have no effect.
    /// 
    /// On the other hand, if some abstraction such as 
    /// <see cref="ToManyBase{T,TChild,TRelationshipAttributes}.LoadUsingSqlQuery" /> is used,
    /// it makes sense to make it work, since user does not control where the query will be
    /// placed. So this visitor moves such 'automatic' queries to the 'hibernate-mapping'.
    /// </summary>
    public class AutomaticQueryVisitor : DefaultMappingModelVisitor
    {
        public override void ProcessHibernateMapping(HibernateMapping hibernateMapping)
        {
            MoveQueries(hibernateMapping, hibernateMapping.Classes);
        }

        private void MoveQueries<TClassMapping>(HibernateMapping mapping, IEnumerable<TClassMapping> classes)
            where TClassMapping : ClassMappingBase
        {
            foreach (var @class in classes)
            {
                MoveQueries(mapping, @class);
            }
        }

        private void MoveQueries(HibernateMapping mapping, ClassMappingBase @class)
        {
            var subclassQueries = @class.Queries
                                        .Cast<IQueryMappingInternal>()
                                        .ToArray(); /* copy to allow removal within foreach */

            foreach (var query in subclassQueries)
            {
                if (!query.Automatic)
                    continue;

                mapping.AddQuery(query);
                @class.RemoveQuery(query);
            }

            MoveQueries(mapping, @class.Subclasses);
        }
    }
}
