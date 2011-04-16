using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentNHibernate.MappingModel.Output.Sorting
{
    public class XmlQueryNodeSorter : BaseXmlNodeSorter
    {
        protected override IDictionary<string, SortValue> GetSorting()
        {
            return new Dictionary<string, SortValue>();
        }

        protected override void SortChildren(XmlNode node)
        {
            if (node.Name != "sql-query")
                return;

            var texts = node.ChildNodes.OfType<XmlText>();
            foreach (var text in texts) {
                node.RemoveChild(text);
                node.AppendChild(text);
            }
        }
    }
}