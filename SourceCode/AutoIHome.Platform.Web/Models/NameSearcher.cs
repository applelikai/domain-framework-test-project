using AutoIHome.Infrastructure.Models;

namespace AutoIHome.Platform.Web.Models
{
    /// <summary>
    /// 名称项查询对象
    /// </summary>
    public class NameSearcher : INameSearcher
    {
        /// <summary>
        /// 查询名称
        /// </summary>
        public string SearchName { get; set; }
    }
}
