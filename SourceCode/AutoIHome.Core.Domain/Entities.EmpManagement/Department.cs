using System;

namespace AutoIHome.Core.Domain.Entities.EmpManagement
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentNo { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门领导
        /// </summary>
        public string DepartmentManager { get; set; }
        /// <summary>
        /// 上级部门编号
        /// </summary>
        public string ParentDepartmentNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public Department() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="departmentId">部门id</param>
        public Department(string departmentId)
        {
            this.DepartmentNo = departmentId;
        }
    }
}
