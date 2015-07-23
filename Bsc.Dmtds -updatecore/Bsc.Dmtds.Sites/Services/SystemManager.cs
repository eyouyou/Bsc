using System;
using System.IO;
using System.Linq;
using System.Web.Management;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    public class DiagnosisResult
    {
        public WebApplicationInformation WebApplicationInformation { get; set; }
        public string ContentProvider { get; set; }
        public DiagnosisItem[] DiagnosisItems { get; set; }
    }
    public enum DiagnosisResultType
    {
        Passed,
        Failed,
        Warning
    }
    public class DiagnosisItem
    {
        public string Name { get; set; }
        public DiagnosisResultType Result { get; set; }
        public string Message { get; set; }
    }

    public class SystemManager
    {
        public virtual DiagnosisResult Diagnosis(Site site)
        {
            DiagnosisResult result = new DiagnosisResult();

            result.WebApplicationInformation = WebBaseEvent.ApplicationInformation;
            result.ContentProvider = Bsc.Dmtds.Content.Persistence.Providers.DefaultProviderFactory.Name;

            result.DiagnosisItems = new[] {
                CheckCms_Data(),
                CheckDbConnection(),
                CheckSmtp(site),
                CheckDomain(site)
            };

            return result;
        }

        private DiagnosisItem CheckCms_Data()
        {
            DiagnosisItem item = new DiagnosisItem() { Name = "Bsc_Data 文件夹 读/写 许可" };
            try
            {
                var baseDir = EngineContext.Current.Resolve<IBaseDir>();
                string cms_dataFolder = baseDir.DataPhysicalPath;
                var tempFileName = Path.Combine(cms_dataFolder, "test.txt");
                File.WriteAllText(tempFileName, "测试 bsc.dmtds 是否 拥有 读/写 许可 在 这个 文件夹上.");
                File.Delete(tempFileName);
                item.Result = DiagnosisResultType.Passed;
            }
            catch
            {
                item.Result = DiagnosisResultType.Failed;
                item.Message = "请确认IIS ASP ，bsc.dmtds应用程序的用户对该文件夹有读/写权限。";
            }

            return item;
        }

        private DiagnosisItem CheckDbConnection()
        {
            DiagnosisItem item = new DiagnosisItem() { Name = "内容数据库连接" };
            bool passed = true;
            try
            {
                passed = Bsc.Dmtds.Content.Persistence.Providers.RepositoryProvider.TestDbConnection();
            }
            catch
            {
                passed = false;
            }


            if (passed)
            {
                item.Result = DiagnosisResultType.Passed;
            }
            else
            {
                item.Result = DiagnosisResultType.Failed;
                item.Message = "内容存储库的连接字符串不正确配置,请检查在线文档来配置内容提供商";
            }
            return item;
        }

        private DiagnosisItem CheckSmtp(Site site)
        {
            DiagnosisItem item = new DiagnosisItem() { Name = "SMTP连通性" };
            if (site.Smtp == null || string.IsNullOrEmpty(site.Smtp.Host))
            {
                item.Result = DiagnosisResultType.Warning;
                item.Message = @"SMTP服务器没有正确设置,请在系统配置\ \ SMTP设置";
            }


            return item;
        }

        private DiagnosisItem CheckDomain(Site site)
        {
            DiagnosisItem item = new DiagnosisItem() { Name = "网站域名设置" };
            if (site.Domains == null || site.Domains.Where(it => !string.IsNullOrWhiteSpace(it)).Count() == 0)
            {
                item.Result = DiagnosisResultType.Warning;
                item.Message = @"没有这个网站的域名分配,请配置下系统\设置";
            }
            else
            {
                foreach (var domain in site.Domains)
                {
                    if (domain.Contains("http://", StringComparison.OrdinalIgnoreCase) || domain.Contains(":"))
                    {
                        item.Result = DiagnosisResultType.Failed;
                        item.Message = "域值不需要URL协议和端口.";
                        break;
                    }
                }
            }
            return item;
        }
    }
}