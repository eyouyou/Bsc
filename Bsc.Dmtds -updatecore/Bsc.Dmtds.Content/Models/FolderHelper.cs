using System;
using System.Collections.Generic;
using System.Linq;

namespace Bsc.Dmtds.Content.Models
{
    public class FolderHelper
    {
        /// <summary>
        /// 合成完整名称
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        public static string CombineFullName(IEnumerable<string> names)
        {
            return string.Join("~", names.ToArray());
        }
        /// <summary>
        /// 分隔完整名称
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public static IEnumerable<string> SplitFullName(string fullName)
        {
            return fullName.Split(new char[] { '~', '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 根据完整名称取得目录对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>        
        public static T Parse<T>(Repository repository, string fullName)
            where T : Folder
        {
            var names = SplitFullName(fullName);
            if (typeof(T) == typeof(Content.Models.TextFolder))
            {
                return (T)((object)new TextFolder(repository, names));
            }
            else
            {
                return (T)((object)new MediaFolder(repository, names));
            }



        } 
    }
}