using System.Security.Cryptography;
using System.Text;

namespace AutoIHome.Infrastructure
{
    /// <summary>
    /// MD5帮助类
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// 获取MD5加密后的字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMd5(string value)
        {
            MD5 md5 = MD5.Create();
            byte[] md5b = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            foreach (var item in md5b)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
