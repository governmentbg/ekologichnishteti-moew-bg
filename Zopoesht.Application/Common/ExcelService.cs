using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Application.Reports.Enums;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Zopoesht.Application.Common
{
    public static class ExcelService
    {
        private const string applicationsName = "Случаи";
        private const string territorialRange = "Териториален обхват";
        private const string applicant = "Заявител";
        private const string threat = "Непосредствена заплаха за екологична щета";
        private const string damage = "Причинена екологична щета";
        private const string reportedDamage = "Случай с искане за предприемане на действия";
        private const string damageType = "Вид щета";
        private const string count = "Брой";
        private const string water = "Води";
        private const string soil = "Почви";
        private const string species = "Защитени видове и местообитания";
        private const string applicationsTypeCount = "Брой заявления от тип";
        private const string applicationsTotalCount = "Общ брой заявления";

        public static byte[] CreateExcelDoc(List<ReportSearchResultDto> applications, ReportSearchFilterDto filter)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (ExcelPackage excel = new ExcelPackage(memoryStream)) { 
                var worksheet = excel.Workbook.Worksheets.Add(applicationsName);

                worksheet.TabColor = System.Drawing.Color.Black;
                worksheet.DefaultRowHeight = 12;

                // Adjust column widths
                worksheet.Column(1).Width = 25;  // Column A
                worksheet.Column(2).Width = 35;  // Column B
                worksheet.Column(3).Width = 15;  // Column C
                worksheet.Column(4).Width = 35;  // Column D
                worksheet.Column(5).Width = 15;  // Column E
                worksheet.Column(6).Width = 35;  // Column F
                worksheet.Column(7).Width = 15;  // Column G

                ConstructHeader(worksheet, filter);
                ConstructTableData(worksheet, applications);

                excel.Save();
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }

        private static void ConstructHeader(ExcelWorksheet worksheet, ReportSearchFilterDto filter)
        {
            worksheet.Row(1).Height = 20;
            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Row(2).Height = 20;
            worksheet.Row(2).Style.Font.Bold = true;

            string reportTypeLocalization = GetReportTypeLocalization(filter);

            worksheet.Cells["B1:C1"].Merge = true;
            worksheet.Cells["D1:E1"].Merge = true;
            worksheet.Cells["F1:G1"].Merge = true;

            worksheet.Cells[1, 1].Value = reportTypeLocalization;
            worksheet.Cells[1, 2].Value = threat;
            worksheet.Cells[1, 3].Value = null;
            worksheet.Cells[1, 4].Value = damage;
            worksheet.Cells[1, 5].Value = null;
            worksheet.Cells[1, 6].Value = reportedDamage;
            worksheet.Cells[1, 7].Value = null;

            worksheet.Cells["A1:A2"].Merge = true;
            worksheet.Cells["A1:A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            worksheet.Cells[2, 1].Value = null;
            worksheet.Cells[2, 2].Value = damageType;
            worksheet.Cells[2, 3].Value = count;
            worksheet.Cells[2, 4].Value = damageType;
            worksheet.Cells[2, 5].Value = count;
            worksheet.Cells[2, 6].Value = damageType;
            worksheet.Cells[2, 7].Value = count;

            worksheet.Cells[$"A2:G2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private static void ConstructTableData(ExcelWorksheet worksheet, List<ReportSearchResultDto> dataResult)
        {
            int startRow = 3;
            int rowsCountPerRecord = 5;

            foreach (var data in dataResult)
            {
                worksheet.Cells[startRow, 1].Value = data.ItemName;
                worksheet.Cells[startRow, 2].Value = water;
                worksheet.Cells[startRow, 3].Value = data.Threat.WaterDamageCount;
                worksheet.Cells[startRow, 4].Value = water;
                worksheet.Cells[startRow, 5].Value = data.Damage.WaterDamageCount;
                worksheet.Cells[startRow, 6].Value = water;
                worksheet.Cells[startRow, 7].Value = data.ReportedDamage.WaterDamageCount;
                startRow++;

                worksheet.Cells[startRow, 2].Value = soil;
                worksheet.Cells[startRow, 3].Value = data.Threat.SoilDamageCount;
                worksheet.Cells[startRow, 4].Value = soil;
                worksheet.Cells[startRow, 5].Value = data.Damage.SoilDamageCount;
                worksheet.Cells[startRow, 6].Value = soil;
                worksheet.Cells[startRow, 7].Value = data.ReportedDamage.SoilDamageCount;
                startRow++;

                worksheet.Cells[startRow, 2].Value = species;
                worksheet.Cells[startRow, 3].Value = data.Threat.SpeciesDamageCount;
                worksheet.Cells[startRow, 4].Value = species;
                worksheet.Cells[startRow, 5].Value = data.Damage.SpeciesDamageCount;
                worksheet.Cells[startRow, 6].Value = species;
                worksheet.Cells[startRow, 7].Value = data.ReportedDamage.SpeciesDamageCount;
                worksheet.Cells[$"B{startRow}:G{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                startRow++;

                worksheet.Cells[startRow, 2].Value = applicationsTypeCount;
                worksheet.Cells[startRow, 3].Value = data.Threat.TotalCount;
                worksheet.Cells[startRow, 4].Value = applicationsTypeCount;
                worksheet.Cells[startRow, 5].Value = data.Damage.TotalCount;
                worksheet.Cells[startRow, 6].Value = applicationsTypeCount;
                worksheet.Cells[startRow, 7].Value = data.ReportedDamage.TotalCount;
                startRow++;

                worksheet.Cells[startRow, 2].Value = $"{applicationsTotalCount}: {data.ItemCount}";
                worksheet.Cells[startRow, 3].Value = null;
                worksheet.Cells[startRow, 4].Value = null;
                worksheet.Cells[startRow, 5].Value = null;
                worksheet.Cells[startRow, 6].Value = null;
                worksheet.Cells[startRow, 7].Value = null;

                worksheet.Cells[$"B{startRow}:G{startRow}"].Merge = true;
                worksheet.Cells[$"B{startRow}:G{startRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[$"A{startRow - rowsCountPerRecord + 1}:A{startRow}"].Merge = true;
                worksheet.Cells[$"A{startRow - rowsCountPerRecord + 1}:A{startRow}"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                worksheet.Cells[$"A{startRow}:G{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                startRow++;
            }
        }

        private static string GetReportTypeLocalization(ReportSearchFilterDto filter)
        {
            switch(filter.ReportType)
            {
                case ReportType.TerritorialRange:
                    return territorialRange;
                case ReportType.ApplicantType:
                    return applicant;
            }

            return string.Empty;
        }
    }
}
