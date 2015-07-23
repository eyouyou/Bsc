using System.IO;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public static class ModuleContextExtensions
    {
        #region Module settings
        public static void SetModuleSettings(this ModuleContext moduleContext, ModuleSettings moduleSettings)
        {
            var settingFile = moduleContext.ModulePath.GetModuleLocalFilePath("settings.config").PhysicalPath;
            DataContractSerializationHelper.Serialize(moduleSettings, settingFile);
        }
        public static ModuleSettings GetModuleSettings(this ModuleContext moduleContext)
        {
            var settingFile = moduleContext.ModulePath.GetModuleLocalFilePath("settings.config").PhysicalPath;

            if (File.Exists(settingFile))
            {
                return DataContractSerializationHelper.Deserialize<ModuleSettings>(settingFile);
            }
            else
            {
                return moduleContext.ModuleInfo.DefaultSettings;
            }
        }
        #endregion 
    }
}