namespace AutoIHome.Infrastructure.Models
{
    /// <summary>
    /// 名称项查询对象
    /// </summary>
    public interface INameSearcher
    {
        /// <summary>
        /// 查询名称
        /// </summary>
        string SearchName { get; }
    }
}
