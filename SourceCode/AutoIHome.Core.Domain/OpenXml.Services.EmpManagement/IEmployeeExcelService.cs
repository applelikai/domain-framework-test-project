using AutoIHome.Core.Domain.Entities.EmpManagement;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.OpenXml.Services.EmpManagement
{
    /// <summary>
    /// 员工Excel业务接口
    /// </summary>
    public interface IEmployeeExcelService
    {
        /// <summary>
        /// 导出员工列表至Excel
        /// </summary>
        /// <param name="employees">员工列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        string ExportEmployees(IEnumerable<Employee> employees, string basePath, string title);
    }
}
