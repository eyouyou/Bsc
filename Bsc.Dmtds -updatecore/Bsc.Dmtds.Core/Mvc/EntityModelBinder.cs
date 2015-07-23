using System;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Core.Runtime;
using IEntity = Bsc.Dmtds.Core.Persistence.Relational.IEntity;

namespace Bsc.Dmtds.Core.Mvc
{
    public class EntityModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext,
            Type modelType)
        {
            if (typeof (IEntity).IsAssignableFrom(modelType))
            {
                var idValue = bindingContext.ValueProvider.GetValue("id");
                int id = 0;

                if (idValue != null && int.TryParse(idValue.AttemptedValue, out id))
                {
                    var providerType = typeof(IProvider<>).MakeGenericType(modelType);
                    dynamic provider = EngineContext.Current.TryResolve(providerType);
                    if (provider != null)
                    {
                        return provider.Get();
                    }
                }

            }
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}