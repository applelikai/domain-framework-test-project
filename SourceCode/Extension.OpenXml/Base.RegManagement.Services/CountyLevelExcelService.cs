using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.OpenXml.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Extension.OpenXml.Base.RegManagement.Services
{
    /// <summary>
    /// 县级行政区Excel业务类
    /// </summary>
    internal class CountyLevelExcelService : ICountyLevelExcelService
    {
        /// <summary>
        /// 导出县级行政区列表至Excel
        /// </summary>
        /// <param name="countyLevels">县级行政区列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        public string ExportCountyLevels(IEnumerable<CountyLevel> countyLevels, string basePath, string title)
        {
            //获取SheetData
            SheetData sheetData = new SheetData();
            //添加标题
            Row titleRow = new Row();
            titleRow.AppendChild(ExcelHelper.NewCell(title, 1U));
            for (int i = 1; i < 10; i++)
                titleRow.AppendChild(ExcelHelper.NewEmptyCell(1U));
            sheetData.AppendChild(titleRow);
            //添加表头
            Row headerRow = new Row();
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区代码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区名称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("地级行政区代码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("地级行政区名称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("县级行政区代码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("县级行政区名称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("县级行政区类型", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("邮政编码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("备注", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("录入时间", 1U));
            sheetData.AppendChild(headerRow);
            //遍历并填充Excel数据
            foreach (CountyLevel countyLevel in countyLevels)
            {
                //创建行
                Row row = new Row();
                //拼接单元格
                row.AppendChild(ExcelHelper.NewCell(countyLevel.ProvinceCode));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.ProvinceLevel.ProvinceName));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.PrefectureCode));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.PrefectureLevel.PrefectureName));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.CountyCode));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.CountyName));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.CountyType));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.PostalCode));
                row.AppendChild(ExcelHelper.NewCell(countyLevel.Remark));
                row.AppendChild(ExcelHelper.NewDateTimeCell(countyLevel.CreatedTime));
                //拼接行
                sheetData.AppendChild(row);
            }
            //合并标题单元格
            MergeCells mergeCells = new MergeCells();
            mergeCells.AppendChild(new MergeCell()
            {
                Reference = "A1:J1"
            });
            Worksheet worksheet = new Worksheet(sheetData, mergeCells);
            //保存Excel并获取文件相对路径
            return worksheet.GetFileRelativePath(basePath, title);
        }
    }
}
