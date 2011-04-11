using System;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public class ModelTester<TFluentClass, TModel>
    {
        private readonly Func<TFluentClass, TModel> getModel;
        private readonly TFluentClass fluentClass;

        public ModelTester(Func<TFluentClass> instantiatePart, Func<TFluentClass, TModel> getModel)
        {
            this.fluentClass = instantiatePart();
            this.getModel = getModel;
        }

        public ModelTester<TFluentClass, TModel> Mapping(Action<TFluentClass> action)
        {
            action(fluentClass);
            return this;
        }

        public void ModelShouldMatch(Action<TModel> action)
        {
            var model = getModel(fluentClass);
            action(model);
        }
    }
}