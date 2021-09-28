using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.OpenXml.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Extension.OpenXml.Base.RegManagement.Services
{
    /// <summary>
    /// 省级行政区Excel业务类
    /// </summary>
    internal class ProvinceLevelExcelService : IProvinceLevelExcelService
    {
        /// <summary>
        /// 导出省级行政区列表至Excel
        /// </summary>
        /// <param name="provinceLevels">省级行政区列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        public string ExportProvinceLevels(IEnumerable<ProvinceLevel> provinceLevels, string basePath, string title)
        {
            //获取SheetData
            SheetData sheetData = new SheetData();
            //添加标题
            Row titleRow = new Row();
            titleRow.AppendChild(ExcelHelper.NewCell(title, 1U));
            for (int i = 1; i < 9; i++)
                titleRow.AppendChild(ExcelHelper.NewEmptyCell(1U));
            sheetData.AppendChild(titleRow);
            //添加表头
            Row headerRow = new Row();
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区代码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区简称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区名称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区类型", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("省级行政区英文名称", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("电话区号", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("车牌代码", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("备注", 1U));
            headerRow.AppendChild(ExcelHelper.NewCell("录入时间", 1U));
            sheetData.AppendChild(headerRow);
            //遍历并填充Excel数据
            foreach (ProvinceLevel provinceLevel in provinceLevels)
            {
                //创建行
                Row row = new Row();
                //拼接单元格
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.ProvinceCode));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.ShortName));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.ProvinceName));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.ProvinceType));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.ProvinceEnName));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.AreaCode));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.LicensePlateCode));
                row.AppendChild(ExcelHelper.NewCell(provinceLevel.Remark));
                row.AppendChild(ExcelHelper.NewDateTimeCell(provinceLevel.CreatedTime));
                //拼接行
                sheetData.AppendChild(row);
            }
            //合并标题单元格
            MergeCells mergeCells = new MergeCells();
            mergeCells.AppendChild(new MergeCell()
            {
                Reference = "A1:I1"
            });
            Worksheet worksheet = new Worksheet(sheetData, mergeCells);
            //保存Excel并获取文件相对路径
            return worksheet.GetFileRelativePath(basePath, title);
        }
    }
}
