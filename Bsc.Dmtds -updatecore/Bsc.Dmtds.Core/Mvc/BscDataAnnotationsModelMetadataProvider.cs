using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Bsc.Dmtds.Core.Mvc
{
    public class BscDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var baseModelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var result = new BscModelMetadata(this, containerType, modelAccessor, modelType, propertyName,
                attributes.OfType<DisplayColumnAttribute>().FirstOrDefault(), attributes)
            {
                TemplateHint = baseModelMetadata.TemplateHint,
                HideSurroundingHtml = baseModelMetadata.HideSurroundingHtml,
                DataTypeName = baseModelMetadata.DataTypeName,
                IsReadOnly = baseModelMetadata.IsReadOnly,
                NullDisplayText = baseModelMetadata.NullDisplayText,
                DisplayFormatString = baseModelMetadata.DisplayFormatString,
                ConvertEmptyStringToNull = false,
                EditFormatString = baseModelMetadata.EditFormatString,
                ShowForDisplay = baseModelMetadata.ShowForDisplay,
                Description = baseModelMetadata.Description,
                ShortDisplayName = baseModelMetadata.ShortDisplayName,
                Watermark = baseModelMetadata.Watermark,
                Order = baseModelMetadata.Order,
                ShowForEdit = baseModelMetadata.ShowForEdit,
                DisplayName = baseModelMetadata.DisplayName,
                IsRequired = baseModelMetadata.IsRequired
            };
            return result;
        }
        protected override System.ComponentModel.ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            var descriptor = TypeDescriptorHelper.Get(type);
            if (descriptor == null)
            {
                descriptor = base.GetTypeDescriptor(type);
            }
            return descriptor;
        }
    }
}