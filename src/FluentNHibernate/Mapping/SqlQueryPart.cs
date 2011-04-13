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
        private bool automatic;
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
            var role = new LoadCollectionRole(roleMember.DeclaringType, roleMember.Name);
            returnMappings.Add(new LoadCollectionMapping(alias, role));
            return this;
        }

        /// <summary>
        /// Defines that the query was generated internally, 
        /// as a side-effect of some other code (for example, in
        /// <see cref="ToManyBase{T,TChild,TRelationshipAttributes}.LoadUsingSqlQuery" />).
        /// </summary>
        internal SqlQueryPart Automatic
        {
            get {
                automatic = true;
                return this;
            }
        }

        IQueryMapping IQueryMappingProvider.GetQueryMapping()
        {
            return new SqlQueryMapping(name, queryText, returnMappings)
            {
                Automatic = automatic
            };
        }
    }
}