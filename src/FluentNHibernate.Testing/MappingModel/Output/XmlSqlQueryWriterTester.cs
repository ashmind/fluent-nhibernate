using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.MappingModel.Queries;

using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class XmlSqlQueryWriterTester
    {
        private IXmlWriter<ClassMapping> writer;

        [SetUp]
        public void GetWriterFromContainer()
        {
            var container = new XmlWriterContainer();
            writer = container.Resolve<IXmlWriter<ClassMapping>>();
        }

        [Test]
        public void ShouldWriteSqlQueryNameAndText()
        {
            var mapping = new ClassMapping();

            mapping.AddQuery(new SqlQueryMapping("test-query", "some SQL", new IReturnMapping[0]));

            writer.VerifyXml(mapping)
                .Element("sql-query").Exists()
                .HasAttribute("name", "test-query")
                .ValueEquals("some SQL");
        }

        [Test]
        public void ShouldWriteLoadCollection()
        {
            var mapping = new ClassMapping();
            mapping.AddQuery(new SqlQueryMapping(null, null, new[] { new LoadCollectionMapping("test-alias", "test.role") }));

            writer.VerifyXml(mapping)
                  .Element("sql-query/load-collection").Exists()
                  .HasAttribute("alias", "test-alias")
                  .HasAttribute("role", "test.role");
        }
    }
}
