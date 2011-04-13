using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Queries;
using FluentNHibernate.Testing.DomainModel.Mapping;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class SqlQueryPartGenerationTests : BaseModelFixture
    {
        [Test]
        public void NameIsSetCorrectly()
        {
            SqlQuery("test", "some SQL")
                .ModelShouldMatch(x => x.Name.ShouldEqual("test"));
        }    
        
        [Test]
        public void TextIsSetCorrectly()
        {
            SqlQuery("test", "some SQL")
                .ModelShouldMatch(x => x.Text.ShouldEqual("some SQL"));
        }

        [Test]
        public void ReturnsCollectionWithCorrectAliasAndRole()
        {
            SqlQuery()
                .Mapping(m => m.ReturnCollection<OneToManyTarget>("test", x => x.SetOfChildren))
                .ModelShouldMatch(x => {
                    var @return = x.Returns.FirstOrDefault();
                    @return.ShouldBeOfType<LoadCollectionMapping>();
                    var load = @return as LoadCollectionMapping;

                    load.Alias.ShouldEqual("test");
                    load.Role.ShouldNotBeNull();
                    load.Role.Type.ShouldEqual(typeof (OneToManyTarget));
                    load.Role.PropertyName.ShouldEqual("SetOfChildren");
                });
        }
    }
}
