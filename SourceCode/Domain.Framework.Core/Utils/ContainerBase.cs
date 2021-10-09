using System.Collections.Generic;

namespace Domain.Framework.Core
{
    /// <summary>
    /// 容器基类
    /// </summary>
    /// <typeparam name="TElement">元素类型</typeparam>
    public abstract class ContainerBase<TElement>
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private object _locker;
        /// <summary>
        /// 元素缓存
        /// </summary>
        private IDictionary<string, TElement> _cache;

        /// <summary>
        /// 创建元素对象
        /// </summary>
        /// <param name="key">元素key</param>
        /// <returns>元素对象</returns>
        protected abstract TElement Create(string key);

        /// <summary>
        /// 初始化
        /// </summary>
        public ContainerBase()
        {
            _locker = new object();
            _cache = new Dictionary<string, TElement>();
        }
        /// <summary>
        /// 获取元素
        /// </summary>
        /// <param name="key">元素key</param>
        /// <returns>元素对象</returns>
        public TElement Get(string key)
        {
            Start:
            //若缓存中包含当前key对应的元素,则直接获取
            if (_cache.ContainsKey(key))
                return _cache[key];
            //进入线程锁
            lock (_locker)
            {
                //若缓存中不包含当前元素,则添加
                if (!_cache.ContainsKey(key))
                    _cache.Add(key, this.Create(key));
            }
            //回到Start
            goto Start;
        }
    }
}
