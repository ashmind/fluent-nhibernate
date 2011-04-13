using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernate.MappingModel.Queries
{
    /// <summary>
    /// Needed for AutomaticQueryVisitor.
    /// </summary>
    internal interface IQueryMappingInternal : IQueryMapping
    {
        bool Automatic { get; }
    }
}
