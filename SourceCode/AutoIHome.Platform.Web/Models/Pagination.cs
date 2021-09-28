namespace AutoIHome.Platform.Web.Models
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 初始化起始页
        /// </summary>
        private int _startIndex;
        /// <summary>
        /// 显示的当前页码数量
        /// </summary>
        private int _currentCount = 5;

        /// <summary>
        /// 起始页码
        /// </summary>
        public int StartIndex
        {
            get
            {
                //若其实页面已经赋值,则直接返回
                if (_startIndex > 0)
                    return _startIndex;
                //若当前页大于起始页+当前显示页数,起始页后移
                _startIndex = 1;
                while ((_startIndex + _currentCount) <= this.PageIndex)
                    _startIndex += _currentCount;
                //返回其起始页
                return _startIndex;
            }
            set { _startIndex = value; }
        }
        /// <summary>
        /// 结束页码
        /// </summary>
        public int EndIndex
        {
            get
            {
                //计算结束页
                int endIndex = this.StartIndex + this._currentCount - 1;
                //若总页不为空
                if (this.PageCount != null)
                {
                    //且结束页大于总页数,则结束页取总页数
                    if (endIndex > this.PageCount.Value)
                        return this.PageCount.Value;
                }
                //返回结束页
                return endIndex;
            }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int? PageCount { get; set; }
        /// <summary>
        /// 每页元素数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 元素总数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 上一分组的起始页码
        /// </summary>
        public int PreviousStartIndex
        {
            get
            {
                if (this.StartIndex <= this._currentCount)
                    return this.StartIndex;
                return this.StartIndex - this._currentCount;
            }
        }
        /// <summary>
        /// 下一分组的起始页码
        /// </summary>
        public int NextStartIndex
        {
            get
            {
                //若下一起始页超过总页数,则保持不变
                if (this.PageCount < (this.StartIndex + this._currentCount))
                    return this.StartIndex;
                //返回下一起始页
                return this.StartIndex + this._currentCount;
            }
        }
        /// <summary>
        /// 前台分页查询函数名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public Pagination() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="functionName">前台分页查询函数名</param>
        public Pagination(string functionName)
        {
            this.FunctionName = functionName;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">前台分页查询函数名</param>
        /// <param name="startIndex">起始页</param>
        public Pagination(int pageIndex, int pageCount, int pageSize, string functionName, int startIndex)
        {
            this.PageIndex = pageIndex;
            this.PageCount = pageCount;
            this.PageSize = pageSize;
            this.FunctionName = functionName;
            this._startIndex = startIndex;
        }
    }
}
