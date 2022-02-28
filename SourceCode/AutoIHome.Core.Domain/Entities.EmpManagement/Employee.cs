using System;

namespace AutoIHome.Core.Domain.Entities.EmpManagement
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 性别[男,女]
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get
            {
                if (this.Birthday == null)
                    return 0;
                return DateTime.Now.Year - this.Birthday.Value.Year;
            }
        }
        /// <summary>
        /// 部门id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
        /// <summary>
        /// 职位id
        /// </summary>
        public string JobId { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public Job Job { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// 是否删除(0:未删除 1:已删除)
        /// </summary>
        public int? IsDeleted { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public Employee() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="employeeId">员工id</param>
        public Employee(string employeeId)
        {
            this.EmployeeId = employeeId;
        }
    }
}
