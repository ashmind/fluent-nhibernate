using System.Xml;
using FluentNHibernate.MappingModel.Queries;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlSqlQueryWriter : NullMappingModelVisitor, IXmlWriter<SqlQueryMapping>
    {
        private XmlDocument document;
        private readonly IXmlWriterServiceLocator serviceLocator;

        public XmlSqlQueryWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(SqlQueryMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessSqlQuery(SqlQueryMapping mapping)
        {
            document = new XmlDocument();
            var element = document.AddElement("sql-query");
            element.WithAtt("name", mapping.Name);

            base.ProcessSqlQuery(mapping);

            element.AppendChild(document.CreateTextNode(mapping.Text));
        }

        public override void Visit(LoadCollectionMapping mapping)
        {
            var writer = serviceLocator.GetWriter<LoadCollectionMapping>();
            var xml = writer.Write(mapping);
            
            document.ImportAndAppendChild(xml);
        }
    }
}