using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel.Queries;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class SqlQueryPart : IQueryMappingProvider
    {
        private readonly string name;
        private readonly string queryText;
        private readonly IList<IReturnMapping> returnMappings = new List<IReturnMapping>();

        public SqlQueryPart(string name, string queryText)
        {
            this.name = name;
            this.queryText = queryText;
        }

        public SqlQueryPart ReturnCollection<TRoleOwner>(string alias, Expression<Func<TRoleOwner, object>> roleExpression)
        {
            return ReturnCollection(alias, roleExpression.ToMember());
        }

        internal SqlQueryPart ReturnCollection(string alias, Member roleMember)
        {
            returnMappings.Add(new LoadCollectionMapping(alias, roleMember.DeclaringType.Name + "." + roleMember.Name));
            return this;
        }

        IQueryMapping IQueryMappingProvider.GetQueryMapping()
        {
            return new SqlQueryMapping(name, queryText, returnMappings);
        }
    }
}