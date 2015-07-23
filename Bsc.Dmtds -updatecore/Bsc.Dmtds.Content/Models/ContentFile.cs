using System.IO;

namespace Bsc.Dmtds.Content.Models
{
    /// <summary>
    /// 内容上传的附件
    /// </summary>
    public class ContentFile
    {
        /// <summary>
        /// 附件对应的字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附件的文件名
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream Stream { get; set; }
    }
}