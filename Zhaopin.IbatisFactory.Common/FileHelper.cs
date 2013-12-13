
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
namespace Zhaopin.IbatisFactory.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        public static void CreateFile(string fileName, string content)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            CreateOrAppendFile(fileName, content, false);
        }
        /// <summary>
        /// 写入文件(新建文件或者追加内容)
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        /// <param name="append">是否为追加文件，为false时创建或覆盖原有文件</param>
        private static void CreateOrAppendFile(string fileName, string content, bool append)
        {
            FileMode fileMode = FileMode.Create;
            FileAccess fileAccess = FileAccess.ReadWrite;
            if (append)
            {
                fileMode = FileMode.Append;
                fileAccess = FileAccess.Write;
            }
            if (fileName.IndexOf('/') > -1)
                fileName = GetMapPath(fileName);
            string dir = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (fileMode == FileMode.Append && !File.Exists(fileName))
            {
                CreateFile(fileName, "");
            }

            using (var fs = new FileStream(fileName, fileMode, fileAccess, FileShare.ReadWrite))
            {
                byte[] info = Encoding.UTF8.GetBytes(content);
                fs.Write(info, 0, info.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取文件中的内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件内容</returns>
        public static string GetFileContent(string fileName)
        {
            string result = string.Empty;
            // if (fileName.IndexOf('/') > -1)
            // fileName = GetMapPath(fileName);
            fileName = AppDomain.CurrentDomain.BaseDirectory + fileName.Replace("/", "\\"); ;


            if (!File.Exists(fileName)) return result;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {

            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        /// <summary>
        /// 生成静态文件
        /// </summary>
        /// <param name="path">生成文件被保存的路径（相对或绝对）包括文件名和扩展名</param>
        /// <param name="text">需要生成静态文件的文本内容</param>
        /// <param name="mode">指定生成文件时文件的打开方式</param>
        /// <param name="encoding">生成文件的编码格式，穿空字符串默认使用gb2312</param>
        public static void GenerateStaticFiles(string path, string text, FileMode mode, string encoding)
        {
            if (encoding == "")
            {
                encoding = "gb2312";
            }
            FileStream stream = new FileStream(path, mode);
            StreamWriter sw = new StreamWriter(stream, Encoding.GetEncoding(encoding));

            sw.WriteLine(text);
            sw.Close();
            sw.Dispose();
            stream.Close();
            stream.Dispose();
        }
    }
}
