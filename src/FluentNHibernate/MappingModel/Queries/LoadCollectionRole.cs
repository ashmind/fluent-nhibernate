using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentNHibernate.MappingModel.Queries
{
    public class LoadCollectionRole
    {
        public LoadCollectionRole(Type type, string propertyName)
        {
            Type = type;
            PropertyName = propertyName;
        }

        public Type Type           { get; private set; }
        public string PropertyName { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            
            var role = obj as LoadCollectionRole;
            return this.Type == role.Type
                && this.PropertyName == role.PropertyName;
        }

        public override int GetHashCode() {
            return this.GetType().GetHashCode()
                 ^ this.Type.GetHashCode()
                 ^ this.PropertyName.GetHashCode();
        }
    }
}
