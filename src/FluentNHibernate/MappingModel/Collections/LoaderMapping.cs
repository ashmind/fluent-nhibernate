using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class LoaderMapping : MappingBase
    {
        private readonly AttributeStore<LoaderMapping> attributes = new AttributeStore<LoaderMapping>();

        public LoaderMapping(string queryRef)
        {
            this.QueryRef = queryRef;
        }

        public string QueryRef
        {
            get { return this.attributes.Get(x => x.QueryRef); }
            set { this.attributes.Set(x => x.QueryRef, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessLoader(this);
        }

        public override bool IsSpecified(string property)
        {
            return attributes.IsSpecified(property);
        }

        public bool Equals(LoaderMapping other)
        {
            if (other == null)
                return false;

            if (this == other)
                return true;

            return other.QueryRef.Equals(this.QueryRef);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(LoaderMapping)) return false;

            return Equals((LoaderMapping)obj);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode()
                 ^ this.QueryRef.GetHashCode();
        }
    }
}