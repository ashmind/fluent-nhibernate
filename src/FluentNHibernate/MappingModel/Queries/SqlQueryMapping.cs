﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Queries
{
    [Serializable]
    public class SqlQueryMapping : MappingBaseWithAttributeStore<SqlQueryMapping>, IQueryMappingInternal
    {
        public SqlQueryMapping(string name, string text, params IReturnMapping[] returns)
            : this(name, text, (IList<IReturnMapping>)returns)
        {
        }

        public SqlQueryMapping(string name, string text, IList<IReturnMapping> returns)
        {
            Returns = returns;
            Name = name;
            Text = text;
        }

        public string Name
        {
            get { return Attributes.Get(x => x.Name); }
            set { Attributes.Set(x => x.Name, value); }
        }

        public string Text
        {
            get { return Attributes.Get(x => x.Text); }
            set { Attributes.Set(x => x.Text, value); }
        }

        /// <summary>
        /// Defines whether the query was generated internally, 
        /// as a side-effect of some other code (for example, in
        /// <see cref="FluentNHibernate.Mapping.ToManyBase{T,TChild,TRelationshipAttributes}.LoadUsingSqlQuery" />).
        /// </summary>
        /// <seealso cref="AutomaticQueryVisitor" />
        internal bool Automatic
        {
            get { return Attributes.Get(x => x.Automatic); }
            set { Attributes.Set(x => x.Automatic, value); }
        }

        bool IQueryMappingInternal.Automatic
        {
            get { return this.Automatic; }
        }

        public IList<IReturnMapping> Returns { get; private set; }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSqlQuery(this);

            foreach (var @return in Returns)
                @return.AcceptVisitorForVisitOnly(visitor);
        }

        public void AcceptVisitorForVisitOnly(IMappingModelVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool Equals(SqlQueryMapping other) {
            if (other == null)
                return false;

            if (this == other)
                return true;

            return other.Name.Equals(this.Name)
                && other.Text.Equals(this.Text)
                && other.Returns.SequenceEqual(this.Returns);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SqlQueryMapping)) return false;

            return Equals((SqlQueryMapping)obj);
        }

        public override int GetHashCode() {
            var hashCode = this.GetType().GetHashCode()
                         ^ this.Name.GetHashCode()
                         ^ this.Text.GetHashCode();

            return this.Returns.Aggregate(hashCode, (hc, r) => hc ^ r.GetHashCode());
        }
    }
}
