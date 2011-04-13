using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class HibernateMappingPart : IHibernateMappingProvider
    {
        private readonly List<IQueryMappingProvider> queries;
        private readonly CascadeExpression<HibernateMappingPart> defaultCascade;
        private readonly AccessStrategyBuilder<HibernateMappingPart> defaultAccess;
        private readonly AttributeStore<HibernateMapping> attributes = new AttributeStore<HibernateMapping>();
        private bool nextBool = true;

        public HibernateMappingPart()
        {
            queries = new List<IQueryMappingProvider>();
            defaultCascade = new CascadeExpression<HibernateMappingPart>(this, value => attributes.Set(x => x.DefaultCascade, value));
            defaultAccess = new AccessStrategyBuilder<HibernateMappingPart>(this, value => attributes.Set(x => x.DefaultAccess, value));
        }

        public HibernateMappingPart Schema(string schema)
        {
            attributes.Set(x => x.Schema, schema);
            return this;
        }

        public CascadeExpression<HibernateMappingPart> DefaultCascade
        {
            get { return defaultCascade; }
        }

        public AccessStrategyBuilder<HibernateMappingPart> DefaultAccess
        {
            get { return defaultAccess; }
        }

        public HibernateMappingPart AutoImport()
        {
            attributes.Set(x => x.AutoImport, nextBool);
            nextBool = true;
            return this;
        }

        public HibernateMappingPart DefaultLazy()
        {
            attributes.Set(x => x.DefaultLazy, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HibernateMappingPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public HibernateMappingPart Catalog(string catalog)
        {
            attributes.Set(x => x.Catalog, catalog);
            return this;
        }

        public HibernateMappingPart Namespace(string ns)
        {
            attributes.Set(x => x.Namespace, ns);
            return this;
        }

        public HibernateMappingPart Assembly(string assembly)
        {
            attributes.Set(x => x.Assembly, assembly);
            return this;
        }

        /// <summary>
        /// Specify a custom sql query.
        /// </summary>
        /// <param name="name">A name of the query.</param>
        /// <param name="queryText">A text of the query.</param>
        public SqlQueryPart SqlQuery(string name, string queryText)
        {
            var part = new SqlQueryPart(name, queryText);
            this.queries.Add(part);
            return part;
        }

        HibernateMapping IHibernateMappingProvider.GetHibernateMapping()
        {
            var mapping = new HibernateMapping(attributes.CloneInner());
            foreach (var queryProvider in this.queries)
            {
                mapping.AddOrReplaceQuery(queryProvider.GetQueryMapping());    
            }

            return mapping;
        }
    }
}