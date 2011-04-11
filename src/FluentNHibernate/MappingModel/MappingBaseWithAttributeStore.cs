using System;

namespace FluentNHibernate.MappingModel
{
    [Serializable]
    public abstract class MappingBaseWithAttributeStore<TMappingBase> : MappingBase
        where TMappingBase : MappingBaseWithAttributeStore<TMappingBase>
    {
        protected MappingBaseWithAttributeStore()
        {
            this.Attributes = new AttributeStore<TMappingBase>();
        }

        protected AttributeStore<TMappingBase> Attributes { get; private set; }

        public override bool IsSpecified(string property)
        {
            return Attributes.IsSpecified(property);
        }
    }
}
