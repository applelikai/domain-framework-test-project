using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.OpenXml.Services.EmpManagement;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Extension.OpenXml.AutoIHome.Core.Services.EmpManagement
{
    /// <summary>
    /// 员工Excel业务类
    /// </summary>
    internal class EmployeeExcelService : IEmployeeExcelService
    {
        /// <summary>
        /// 导出员工列表至Excel
        /// </summary>
        /// <param name="employees">员工列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        public string ExportEmployees(IEnumerable<Employee> employees, string basePath, string title)
        {
            //获取SheetData
            SheetData sheetData = new SheetData();
            //添加标题
            Row titleRow = new Row();
            titleRow.AppendChild(ExcelHelper.NewCell(title, 1U));
            for (int i = 1; i < 8; i++)
                titleRow.AppendChild(ExcelHelper.NewEmptyCell(1U));
            sheetData.AppendChild(titleRow);
            //添加表头
            Row headerRow = new Row();
            headerRow.AppendChild(ExcelHelper.NewCell("员工编号", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("员工姓名", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("性别", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("年龄", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("部门", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("职位", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("入职日期", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("录入时间", 1U));
            sheetData.AppendChild(headerRow);
            //遍历并填充Excel数据
            foreach (Employee employee in employees)
            {
                //创建行
                Row row = new Row();
                //拼接单元格
                row.AppendChild(ExcelHelper.NewCell(employee.EmployeeNo));
                row.AppendChild(ExcelHelper.NewCell(employee.EmployeeName));
                row.AppendChild(ExcelHelper.NewCell(employee.Sex));
                row.AppendChild(ExcelHelper.NewCell(employee.Age));
                row.AppendChild(ExcelHelper.NewCell(employee.Department.DepartmentName));
                row.AppendChild(ExcelHelper.NewCell(employee.Job.JobName));
                row.AppendChild(ExcelHelper.NewDateCell(employee.JoinDate));
                row.AppendChild(ExcelHelper.NewDateTimeCell(employee.CreatedTime));
                //拼接行
                sheetData.AppendChild(row);
            }
            //合并标题单元格
            MergeCells mergeCells = new MergeCells();
            mergeCells.AppendChild(new MergeCell()
            {
                Reference = "A1:H1"
            });
            Worksheet worksheet = new Worksheet(sheetData, mergeCells);
            //保存Excel并获取文件相对路径
            return worksheet.GetFileRelativePath(basePath, title);
        }
    }
}
