using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public interface IAdvancedVisitableMappingBase : IMappingBase
    {
        void AcceptVisitorForVisitOnly(IMappingModelVisitor visitor);
    }
}
