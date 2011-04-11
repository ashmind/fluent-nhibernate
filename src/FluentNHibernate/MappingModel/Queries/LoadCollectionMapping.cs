using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Queries
{
    public class LoadCollectionMapping : MappingBaseWithAttributeStore<LoadCollectionMapping>, IReturnMapping
    {
        public LoadCollectionMapping(string alias, string role)
        {
            this.Alias = alias;
            this.Role = role;
        }

        public string Alias
        {
            get { return Attributes.Get(x => x.Alias); }
            set { Attributes.Set(x => x.Alias, value); }
        }

        public string Role
        {
            get { return Attributes.Get(x => x.Role); }
            set { Attributes.Set(x => x.Role, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessLoadCollection(this);
        }

        public void AcceptVisitorForVisitOnly(IMappingModelVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool Equals(LoadCollectionMapping other) {
            if (other == null)
                return false;

            if (this == other)
                return true;

            return other.Alias.Equals(this.Alias)
                && other.Role.Equals(this.Role);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(LoadCollectionMapping)) return false;

            return Equals((LoadCollectionMapping)obj);
        }

        public override int GetHashCode() {
            return this.GetType().GetHashCode()
                 ^ this.Alias.GetHashCode()
                 ^ this.Role.GetHashCode();
        }
    }
}
