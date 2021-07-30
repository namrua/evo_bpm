using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CleanEvoBPM.WebAPI.Helper
{
    public class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            { return "application/octet-stream"; }
        }

        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static byte[] ExportExcel(DataTable dataTable, List<string> colunmHeaders, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {
            byte[] result = null;
            var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CleanEvoBPM.WebAPI.Template.ProjectList.xlsx");
            using (ExcelPackage package = new ExcelPackage(templateStream))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
                int startRowFrom = String.IsNullOrEmpty(heading) ? 4 : 4;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                for (int i = 0; i < colunmHeaders.Count; i++)
                {
                    workSheet.Cells[startRowFrom, (i + 1)].Value = colunmHeaders[i];
                }

                workSheet.Cells["A3:E3"].Merge = true;

                 var richText = workSheet.Cells["A3:E3"].RichText.Add("The following list has been exported at ");

                var colorRichText = workSheet.Cells["A3:E3"].RichText.Add(DateTime.UtcNow.ToLocalTime().ToString("HH:mm"));
                colorRichText.Color = System.Drawing.ColorTranslator.FromHtml("#FF0000");

                richText = workSheet.Cells["A3:E3"].RichText.Add(" on ");
                richText.Color = System.Drawing.ColorTranslator.FromHtml("#000000");

                colorRichText = workSheet.Cells["A3:E3"].RichText.Add(DateTime.UtcNow.ToString("dd-MMM-yyyy"));
                colorRichText.Color = System.Drawing.ColorTranslator.FromHtml("#FF0000");

                workSheet.Cells["A" + (startRowFrom + 1)].LoadFromDataTable(dataTable, false);

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();


                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (columnsToTake.Length != 0)
                {
                    for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                    {
                        if (i == 0 && showSrNo)
                        {
                            continue;
                        }
                        if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                        {
                            workSheet.DeleteColumn(i + 1);
                        }
                    }
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;
                }

                result = package.GetAsByteArray();
            }

            return result;
        }

        public static byte[] ExportExcel<T>(List<T> data, List<string> colunmHeaders, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), colunmHeaders, Heading, showSlno, ColumnsToTake);
        }
    }
}