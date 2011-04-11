using System.Xml;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlLoaderWriter : NullMappingModelVisitor, IXmlWriter<LoaderMapping>
    {
        private XmlDocument document;

        public XmlDocument Write(LoaderMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessLoader(LoaderMapping mapping)
        {
            document = new XmlDocument();
            var element = document.AddElement("loader");
            element.WithAtt("query-ref", mapping.QueryRef);

            base.ProcessLoader(mapping);
        }
    }
}