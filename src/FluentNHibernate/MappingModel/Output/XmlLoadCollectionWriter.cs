using System.Xml;
using FluentNHibernate.MappingModel.Queries;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlLoadCollectionWriter : NullMappingModelVisitor, IXmlWriter<LoadCollectionMapping>
    {
        private XmlDocument document;

        public XmlDocument Write(LoadCollectionMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessLoadCollection(LoadCollectionMapping mapping)
        {
            document = new XmlDocument();
            var element = document.AddElement("load-collection");
            element.WithAtt("alias", mapping.Alias);
            element.WithAtt("role", mapping.Role);

            base.ProcessLoadCollection(mapping);
        }
    }
}