

namespace Bsc.Dmtds.Form.Html.Controls
{
    /// <summary>
    /// 图片裁剪
    /// </summary>
    public class ImageCrop : Input
    {
        public const string BscImageCropInputName = "BSC-Image-Crop-Field";

        public override string Type
        {
            get { return "ImageCrop"; }
        }

        public override string Name
        {
            get { return "ImageCrop"; }
        }

        protected override string RenderInput(IColumn column)
        {
            string formater = @"<input class=""medium"" id=""{0}"" name=""{0}"" type=""text"" value=""@(Model.{0} ?? """")"" {1} readonly=""readonly""/>
<a href=""javascript:;"" class=""image-croper action"" inputid = ""{0}"">@Html.IconImage(""plus"")</a>
<input id=""{0}-hidden"" name=""{0}-hidden"" type=""hidden"" value=""@(Model.{0} ?? """")""/>
<input type=""hidden"" name=""{2}"" value=""{0}""/>";

            return string.Format(formater, column.Name, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column), BscImageCropInputName);
        }
    }
}
