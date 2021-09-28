using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;

namespace Extension.OpenXml
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 获取边框线颜色
        /// </summary>
        /// <returns>边框线颜色</returns>
        private static Color GetBorderColor()
        {
            return new Color()
            {
                Rgb = new HexBinaryValue()
                {
                    Value = "Black"
                }
            };
        }
        /// <summary>
        /// 获取边框线
        /// </summary>
        /// <returns>边框线</returns>
        private static Borders GetBorders()
        {
            //创建边框
            Borders borders = new Borders(new OpenXmlElement[]
            {
                new Border(new OpenXmlElement[]
                {
                    new LeftBorder(),
                    new RightBorder(),
                    new TopBorder(),
                    new BottomBorder(),
                    new DiagonalBorder()
                }),
                new Border(new OpenXmlElement[]
                {
                    new LeftBorder(ExcelHelper.GetBorderColor()) { Style = BorderStyleValues.Thin },
                    new RightBorder(ExcelHelper.GetBorderColor()) { Style = BorderStyleValues.Thin },
                    new TopBorder(ExcelHelper.GetBorderColor()) { Style = BorderStyleValues.Thin },
                    new BottomBorder(ExcelHelper.GetBorderColor()) { Style = BorderStyleValues.Thin },
                    new DiagonalBorder()
                })
            });
            //获取边框
            return borders;
        }
        /// <summary>
        /// 获取字体集
        /// </summary>
        /// <returns>字体集</returns>
        private static Fonts GetFonts()
        {
            return new Fonts(new OpenXmlElement[]
            {
                new Font(new OpenXmlElement[]
                {
                    new FontSize() { Val = 10D },
                    new FontName() { Val = "Microsoft YaHei UI" }
                }),
                new Font(new OpenXmlElement[]
                {
                    new FontSize() { Val = 10D },
                    new FontName() { Val = "Microsoft YaHei UI" },
                    new Bold() { Val = true }
                })
            });
        }
        /// <summary>
        /// 获取单元格的填充色
        /// </summary>
        /// <returns>单元格的填充色</returns>
        private static Fills GetFills()
        {
            //获取单元格填充颜色
            ForegroundColor foregroundColor = new ForegroundColor() { Rgb = "ADD8E6" };
            BackgroundColor backgroundColor = new BackgroundColor() { Indexed = (UInt32Value)64U };
            //获取单元格样式列表
            return new Fills(new OpenXmlElement[]
            {
                new Fill(new PatternFill() { PatternType = PatternValues.None }),
                new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }),
                new Fill(new PatternFill(foregroundColor, backgroundColor) { PatternType = PatternValues.Solid })
            });
        }
        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <returns>单元格样式</returns>
        private static CellFormats GetCellFormats()
        {
            //获取单元格样式
            return new CellFormats(new OpenXmlElement[]
            {
                //0U:Microsoft Office Excel的默认样式，不可更改，所以这里的CellFormat只占位
                new CellFormat()
                {
                    Alignment = new Alignment(),
                    NumberFormatId = 0U,
                    FontId = 0U,
                    BorderId = 0U,
                    FillId = 0U
                },
                //1U:表头单元格样式
                new CellFormat()
                {
                    Alignment = new Alignment()
                    {
                        Horizontal = new EnumValue<HorizontalAlignmentValues>(HorizontalAlignmentValues.Center)
                    },
                    NumberFormatId = 0U,
                    FontId = 1U,
                    BorderId = 1U,
                    FillId = 2U,
                    ApplyAlignment = true,
                    ApplyBorder = true,
                    ApplyFont = true,
                    ApplyFill = true
                },
                //2U:内容单元格样式
                new CellFormat()
                {
                    Alignment = new Alignment(),
                    NumberFormatId = 0U,
                    FontId = 0U,
                    BorderId = 1U,
                    FillId = 0U,
                    ApplyAlignment = true,
                    ApplyBorder = true,
                    ApplyFont = true,
                    ApplyFill = true
                }
            });
        }

        /// <summary>
        /// 新建空单元格
        /// </summary>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>空单元格</returns>
        public static Cell NewEmptyCell(uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 新建单元格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>单元格</returns>
        public static Cell NewCell(string value, uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.CellValue = new CellValue(value);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 新建数字单元格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>单元格</returns>
        public static Cell NewCell(int value, uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.CellValue = new CellValue(value);
            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 新建小数单元格
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>单元格</returns>
        public static Cell NewCell(decimal amount, uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.CellValue = new CellValue(amount);
            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 新建日期单元格
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>单元格</returns>
        public static Cell NewDateCell(DateTime? date, uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.CellValue = new CellValue(string.Format("{0:yyyy/MM/dd}", date));
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 新建时间单元格
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="styleIndex">单元格样式id</param>
        /// <returns>单元格</returns>
        public static Cell NewDateTimeCell(DateTime? time, uint styleIndex = 2U)
        {
            //创建单元格
            Cell cell = new Cell();
            //设置单元格
            cell.CellValue = new CellValue(string.Format("{0:yyyy/MM/dd HH:mm:ss}", time));
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            cell.StyleIndex = new UInt32Value(styleIndex);
            //获取单元格
            return cell;
        }
        /// <summary>
        /// 创建sheet
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="sheetId">sheet页id(第几个sheet)</param>
        /// <param name="name">sheet页名称</param>
        /// <returns>sheet页</returns>
        public static Sheet NewSheet(StringValue id, UInt32Value sheetId, StringValue name)
        {
            return new Sheet()
            {
                Id = id,
                SheetId = sheetId,
                Name = name
            };
        }
        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="document">文档</param>
        /// <param name="cell">单元格</param>
        /// <returns>单元格值</returns>
        public static string GetCellValue(this SpreadsheetDocument document, Cell cell)
        {
            //获取字符串值存储表
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            //获取初始值
            string value = cell.CellValue.InnerXml;
            //获取字符串值
            if (cell.DataType != null && (cell.DataType.Value == CellValues.SharedString || cell.DataType.Value == CellValues.String || cell.DataType.Value == CellValues.Number))
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                //获取非字符串值(浮点数和日期对应的cell.DataType都为NULL)
                // DateTime.FromOADate((double.Parse(value)); 如果确定是日期就可以直接用过该方法转换为日期对象，可是无法确定DataType==NULL的时候这个CELL 数据到底是浮点型还是日期.(日期被自动转换为浮点
                return value;
            }
        }
        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <param name="worksheet">工作表</param>
        /// <param name="title">标题(sheet页名称)</param>
        /// <param name="fileFullName">文件全名</param>
        public static void SaveExcel(this Worksheet worksheet, string title, string fileFullName)
        {
            //若文件存在，则删除
            if (File.Exists(fileFullName))
                File.Delete(fileFullName);
            //保存数据至Excel文件
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileFullName, SpreadsheetDocumentType.Workbook))
            {
                //获取WorkbookPart
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                FileVersion fileVersion = new FileVersion() { ApplicationName = "Microsoft Office Excel" };
                workbookpart.Workbook = new Workbook(fileVersion);
                //获取并设置样式
                WorkbookStylesPart stylepart = workbookpart.AddNewPart<WorkbookStylesPart>();
                stylepart.Stylesheet = new Stylesheet();
                stylepart.Stylesheet.Fonts = ExcelHelper.GetFonts();
                stylepart.Stylesheet.Fills = ExcelHelper.GetFills();
                stylepart.Stylesheet.Borders = ExcelHelper.GetBorders();
                stylepart.Stylesheet.CellFormats = ExcelHelper.GetCellFormats();
                stylepart.Stylesheet.Save();
                //获取WorksheetPart
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = worksheet;
                //获取添加sheet页
                Sheets sheets = workbookpart.Workbook.AppendChild(new Sheets());
                sheets.Append(ExcelHelper.NewSheet(workbookpart.GetIdOfPart(worksheetPart), 1, title));
                //保存
                workbookpart.Workbook.Save();
            }
        }

        /// <summary>
        /// 保存并获取Excel文件相对路径
        /// </summary>
        /// <param name="sheetData">sheet页数据</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题(sheet页名称)</param>
        /// <returns>文件相对路径</returns>
        public static string GetFileRelativePath(this SheetData sheetData, string basePath, string title)
        {
            //获取文件路径
            string path = $"{basePath}/Temp/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //获取文件名
            string fileName = string.Format("{0}.xlsx", title); ;
            //保存Excel文件
            Worksheet worksheet = new Worksheet(sheetData);
            worksheet.SaveExcel(title, $"{path}{fileName}");
            //获取文件相对路径
            return $"Temp/{fileName}";
        }
        /// <summary>
        /// 保存并获取Excel文件相对路径
        /// </summary>
        /// <param name="worksheet">工作表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题(sheet页名称)</param>
        /// <returns>文件相对路径</returns>
        public static string GetFileRelativePath(this Worksheet worksheet, string basePath, string title)
        {
            //获取文件路径
            string path = $"{basePath}/Temp/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //获取文件名
            string fileName = string.Format("{0}.xlsx", title);
            //保存Excel文件
            worksheet.SaveExcel(title, $"{path}{fileName}");
            //获取文件相对路径
            return $"Temp/{fileName}";
        }
    }
}
