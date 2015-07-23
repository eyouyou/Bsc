using System.IO;
using System.Text;

namespace Bsc.Dmtds.Common.IO
{
    public static class StreamExtensions
    {
        #region 读数据

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] ReadData(this Stream stream)
        {
            byte[] data = new byte[stream.Length];

            stream.Read(data, 0, data.Length);

            return data;
        }
        #endregion

        #region 读字符串utf-8

        /// <summary>
        /// 读字符串utf-8
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadString(this Stream stream)
        {
            return stream.ReadString(Encoding.UTF8);
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string ReadString(this Stream stream, Encoding encoding)
        {
            stream.Seek(0, SeekOrigin.Begin);
            TextReader reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }
        #endregion

        #region SaveAs
        /// <summary>
        /// 拷贝到.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(this Stream src, Stream dest)
        {
            byte[] buffer = new byte[0x10000];
            int bytes;
            try
            {
                while ((bytes = src.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, bytes);
                }
            }
            finally
            {
                dest.Flush();
            }
        }


        /// <summary>
        /// 保存.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="filePath">文件路径</param>
        public static string SaveAs(this Stream stream, string filePath)
        {
            return SaveAs(stream, filePath, true);
        }

        /// <summary>
        /// Saves as.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="isOverwrite">if set to <c>true</c> [is overwrite].</param>
        public static string SaveAs(this Stream stream, string filePath, bool isOverwrite)
        {
            var data = new byte[stream.Length];
            var length = stream.Read(data, 0, (int)stream.Length);
            return SaveAs(data, filePath, isOverwrite);
        }

        /// <summary>
        /// Saves as.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="isOverwrite">if set to <c>true</c> [is overwrite].</param>
        /// <returns>绝对路径</returns>
        public static string SaveAs(byte[] data, string filePath, bool isOverwrite)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (isOverwrite)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!isOverwrite && File.Exists(filePath))
            {
                string fileNameWithoutEx = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);

                int i = 1;
                do
                {
                    filePath = Path.Combine(directory, string.Format("{0}-{1}{2}", fileNameWithoutEx, i, extension));
                    i++;
                } while (File.Exists(filePath));
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Write(data, 0, data.Length);
            }
            return filePath;
        }
        #endregion


        #region 写字符串
        /// <summary>
        /// 写字符串.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="s">The s.</param>
        public static void WriteString(this Stream src, string s)
        {
            WriteString(src, s, Encoding.UTF8);
        }
        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="s">The s.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteString(this Stream src, string s, Encoding encoding)
        {
            TextWriter writer = new StreamWriter(src, encoding);
            writer.Write(s);
            writer.Flush();
        }
        #endregion 
    }
}