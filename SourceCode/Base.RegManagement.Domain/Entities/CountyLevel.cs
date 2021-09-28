using System;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Entities
{
    /// <summary>
    /// 县级行政区
    /// </summary>
    public class CountyLevel
    {
        /// <summary>
        /// (县级)行政区类型数组
        /// </summary>
        private static string[] _countyTypes;

        /// <summary>
        /// (县级)行政区代码
        /// </summary>
        public string CountyCode { get; set; }
        /// <summary>
        /// (县级)行政区名称
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// (县级)行政区类型[市辖区 县级市 县 自治县 旗 自治旗 特区 林区]
        /// </summary>
        public string CountyType { get; set; }
        /// <summary>
        /// 省级(行政区)代码
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省级行政区
        /// </summary>
        public ProvinceLevel ProvinceLevel { get; set; }
        /// <summary>
        /// 地级(行政区)代码
        /// </summary>
        public string PrefectureCode { get; set; }
        /// <summary>
        /// 地级行政区
        /// </summary>
        public PrefectureLevel PrefectureLevel { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// (县级)行政区类型数组
        /// </summary>
        public static IEnumerable<string> CountyTypes
        {
            get { return _countyTypes; }
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static CountyLevel()
        {
            _countyTypes = new string[] { "市辖区", "县级市", "县", "自治县", "旗", "自治旗", "特区", "林区" };
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public CountyLevel() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="countyCode">(县级)行政区代码</param>
        public CountyLevel(string countyCode)
        {
            this.CountyCode = countyCode;
        }
    }
}
