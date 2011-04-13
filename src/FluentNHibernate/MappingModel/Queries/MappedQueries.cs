using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Queries
{
    [Serializable]
    public class MappedQueries : IMappingBase, IEnumerable<IQueryMapping>
    {
        private readonly List<IQueryMapping> list = new List<IQueryMapping>();

        public void Add(IQueryMapping mapping)
        {
            if (list.Exists(x => x.Name == mapping.Name))
                throw new ArgumentException("Query '" + mapping.Name + "' is already defined.", "mapping");

            list.Add(mapping);
        }

        public void AddOrReplace(IQueryMapping mapping)
        {
            list.RemoveAll(x => x.Name == mapping.Name);
            list.Add(mapping);
        }

        public void Remove(IQueryMapping mapping)
        {
            list.Remove(mapping);
        }

        public void AcceptVisitor(IMappingModelVisitor visitor)
        {
            foreach (var query in list)
                query.AcceptVisitorForVisitOnly(visitor);
        }

        public bool IsSpecified(string property)
        {
            return false;
        }

        public IEnumerator<IQueryMapping> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
}
