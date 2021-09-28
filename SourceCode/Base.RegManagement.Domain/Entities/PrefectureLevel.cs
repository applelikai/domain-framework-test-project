using System;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Entities
{
    /// <summary>
    /// 地级行政区
    /// </summary>
    public class PrefectureLevel
    {
        /// <summary>
        /// (地级)行政区类型数组
        /// </summary>
        private static string[] _prefectureTypes;

        /// <summary>
        /// (地级)行政区代码
        /// </summary>
        public string PrefectureCode { get; set; }
        /// <summary>
        /// (地级)行政区名称(不包含行政区类型)
        /// </summary>
        public string PrefectureName { get; set; }
        /// <summary>
        /// (地级)行政区类型[市 地区 自治州 盟]
        /// </summary>
        public string PrefectureType { get; set; }
        /// <summary>
        /// 行政区英文名称
        /// </summary>
        public string PrefectureEnName { get; set; }
        /// <summary>
        /// 省级代码
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省级行政区
        /// </summary>
        public ProvinceLevel ProvinceLevel { get; set; }
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
        public static IEnumerable<string> PrefectureTypes
        {
            get { return _prefectureTypes; }
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static PrefectureLevel()
        {
            _prefectureTypes = new string[] { "市", "地区", "自治州", "盟" };
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public PrefectureLevel() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="prefectureCode">行政区代码</param>
        public PrefectureLevel(string prefectureCode)
        {
            this.PrefectureCode = prefectureCode;
        }
    }
}
