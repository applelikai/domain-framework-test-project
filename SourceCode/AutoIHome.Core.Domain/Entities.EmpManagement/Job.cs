using System;

namespace AutoIHome.Core.Domain.Entities.EmpManagement
{
    /// <summary>
    /// 职位
    /// </summary>
    public class Job
    {
        /// <summary>
        /// 职位id
        /// </summary>
        public string JobId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public Department Department { get; set; }
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
        public Job() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="jobId">岗位id</param>
        public Job(string jobId)
        {
            this.JobId = jobId;
        }
    }
}
