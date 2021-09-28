using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Models.EmpManagement;
using AutoIHome.Core.Domain.OpenXml.Services.EmpManagement;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.Services.EmpManagement
{
    /// <summary>
    /// 员工业务接口
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <returns>员工列表</returns>
        IEnumerable<Employee> GetEmployees(IEmployeeSearcher searcher);
        /// <summary>
        /// 分页获取员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>员工分页列表</returns>
        IPagedList<Employee> GetEmployees(IEmployeeSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 员工业务扩展类
    /// </summary>
    public static class EmployeeServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IEmployeeService _Service
        {
            get { return ServiceContainer.Get<IEmployeeService>(); }
        }

        /// <summary>
        /// 查询导出员工列表至Excel
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="basePath">文件基础路径</param>
        /// <returns>Excel文件相对路径</returns>
        public static string ExportEmployees(this IEmployeeSearcher searcher, string basePath)
        {
            //获取员工列表
            IEnumerable<Employee> employees = _Service.GetEmployees(searcher);
            //导出员工列表至Excel
            return ServiceContainer.Get<IEmployeeExcelService>().ExportEmployees(employees, basePath, "员工列表");
        }
        /// <summary>
        /// 分页获取员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>员工分页列表</returns>
        public static IPagedList<Employee> GetEmployees(this IEmployeeSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetEmployees(searcher, pageIndex, pageSize);
        }
    }
}
