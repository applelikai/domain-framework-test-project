using System;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Entities
{
    /// <summary>
    /// 省级行政区
    /// </summary>
    public class ProvinceLevel
    {
        /// <summary>
        /// 行政区类型数组
        /// </summary>
        private static string[] _provinceTypes;

        /// <summary>
        /// 行政区代码
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 行政区简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 行政区名称(不包含行政区类型)
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 行政区类型[省 自治区 直辖市 特别行政区]
        /// </summary>
        public string ProvinceType { get; set; }
        /// <summary>
        /// 行政区全名
        /// </summary>
        public string ProvinceFullName
        {
            get
            { 
                if (!string.IsNullOrEmpty(this.Remark))
                    return this.Remark;
                return string.Concat(this.ProvinceName, this.ProvinceType);
            }
        }
        /// <summary>
        /// 行政区英文名称
        /// </summary>
        public string ProvinceEnName { get; set; }
        /// <summary>
        /// 电话区号
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 车牌代码
        /// </summary>
        public string LicensePlateCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// 行政区类型列表
        /// </summary>
        public static IEnumerable<string> ProvinceTypes
        {
            get { return _provinceTypes; }
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static ProvinceLevel()
        {
            _provinceTypes = new string[] { "省", "自治区", "直辖市", "特别行政区" };
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public ProvinceLevel() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="provinceCode">行政区代码</param>
        public ProvinceLevel(string provinceCode)
        {
            this.ProvinceCode = provinceCode;
        }
    }
}
