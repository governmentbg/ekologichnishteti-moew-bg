using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Zopoesht.Application.Reports.Dtos.Summary;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Application.Applications.ApplicationsOne.Dtos;

namespace Zopoesht.Application.Common
{
    public static class WordService
    {
        private static string bg = "bg-BG";
        private static string en = "en-EN";

        // BG
        private const string finishedApplications = "1. ПРИКЛЮЧЕНИ ПРОЦЕДУРИ ПО ЗОПОЕЩ:";
        private const string onGoingAppplications = "2. ПРОЦЕДУРИ В ХОД НА ИЗПЪЛНЕНИЕ:";
        private const string necessarySummaryInfo = "Задължителна информация за докладване:";
        private const string additionalSummaryInfo = "Допълнителна информация по случая:";
        private const string noApplicationsOfThisType = "Няма процедури от този тип спрямо зададените критерии";
        private const string threatType = $"Вид на непосредствената заплаха за възникване на екологични щети (по чл. 4 от ЗОПОЕЩ):\n{species}{water}{soil}";
        private const string damageType = $"Вид на причинените екологични щети (по чл. 4 от ЗОПОЕЩ):\n{species}{water}{soil}";
        private const string species = "а) защитени видове и местообитания;\n";
        private const string water = "б) води;\n";
        private const string soil = "в) почва;";
        private const string threatOccuranceDate = "Дата на възникване на непосрествената заплаха за възникване на екологични щети и/или датата, на която това е установено";
        private const string damageOccuranceDate = "Дата на възникване на причинените екологични щети и/или датата, на която това е установено";
        private const string activityOffering = "Дейност, съгласно приложение № 1 на ЗОПОЕЩ, в резултат на която са причинени екологичните щети";
        private const string unknown = "Неизвестно";
        private const string initiatedProcedureDate = "Дата, на която е започнала процедура по предотвратяване на непосредствената заплаха за възникване на екологични щети";
        private const string removalProcedureDate = "Дата, на която е започнала процедура за отстраняване на причинените екологични щети";
        private const string preventionProcedureApplicant = "Заявител на процедурата по предотвратяване на непосредствената заплаха за възникване на екологични щети";
        private const string removalProcedureApplicant = "Заявител на процедурата по отстраняване на причинени екологични щети";
        private const string actionsTaken = "Предприети действия по ЗОПОЕЩ";
        private const string noActions = "Не са предприети действия";

        private const string applicationOneTypeDamage = "Случай на причинена екологична щета";
        private const string applicationOneTypeThreat = "Случай на непосредствена заплаха за екологична щета";
        private const string applicationOneTypeReportedDamage = "Случай с искане за предприемане на действия";

        private const string operatorString = "Оператор";
        private const string authority = "Компетентен орган";
        private const string individual = "ФЛ";
        private const string legalEntityAndNonGovernmentalOrganization = "ЮЛ/НПО";

        // EN
        private const string finishedApplicationsEn = "1. COMPLETED ZOPOESHT PROCEDURES:";
        private const string onGoingAppplicationsEn = "2. PROCEDURES IN THE COURSE OF IMPLEMENTATION:";
        private const string necessarySummaryInfoEn = "Mandatory reporting information:";
        private const string additionalSummaryInfoEn = "Further information on the case:";
        private const string noApplicationsOfThisTypeEn = "There are no procedures of this type against the set criteria";
        private const string threatTypeEn = $"Type of imminent threat of environmental damage (according to Article 4 of the ZOPOESHT):\n{speciesEn}{waterEn}{soilEn}";
        private const string damageTypeEn = $"Type of environmental damage caused (according to Article 4 of the ZOPOESHT):\n{speciesEn}{waterEn}{soilEn}";
        private const string speciesEn = "a) protected species and habitats;\n";
        private const string waterEn = "b) water;\n";
        private const string soilEn = "c) soil;";
        private const string threatOccuranceDateEn = "Date of occurrence of the unconfirmed threat of environmental damage and/or the date on which this was established";
        private const string damageOccuranceDateEn = "Date of occurrence of the environmental damage caused and/or the date on which this was established";
        private const string activityOfferingEn = "Activity, as defined in application №1 of ZOPOESHT, resulting in environmental damage";
        private const string unknownEn = "Unknown";
        private const string initiatedProcedureDateEn = "Date on which the procedure to prevent the imminent threat of environmental damage was initiated";
        private const string removalProcedureDateEn = "Date on which the procedure to remedy the environmental damage was initiated";
        private const string preventionProcedureApplicantEn = "Applicant for the procedure to prevent the imminent threat of environmental damage";
        private const string removalProcedureApplicantEn = "Applicant for the procedure for remedying environmental damage";
        private const string actionsTakenEn = "Actions taken under the ZOPOESHT";
        private const string noActionsEn = "No actions have been taken";

        private const string applicationOneTypeDamageEn = "Case of environmental damage";
        private const string applicationOneTypeThreatEn = "Case of imminent threat of environmental damage";
        private const string applicationOneTypeReportedDamageEn = "Action request case";

        private const string operatorStringEn = "Operator";
        private const string authorityEn = "Competent authority";
        private const string individualEn = "Individual";
        private const string legalEntityAndNonGovernmentalOrganizationEn = "Legal entity/Non-governmental organization";

        private static string language = bg;
        private static bool isBg = true;

        public static byte[] CreateWordDoc(List<SummaryDto> finishedSummaries, List<SummaryDto> onGoingSummaries, bool isBgFilter)
        {
            language = isBg ? bg : en;
            isBg = isBgFilter;

            MemoryStream memoryStream = new MemoryStream();

            using (var document = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = document.AddMainDocumentPart();
                mainPart.Document = new Document();

                mainPart.Document.AppendChild(new Body());
                Body body = mainPart.Document.Body;

                AddFinishedSummaries(body, finishedSummaries);
                AddOnGoingSummaries(body, onGoingSummaries);

                mainPart.Document.Save();
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }

        private static void AddFinishedSummaries(Body body, List<SummaryDto> finishedSummaries)
        {
            string finishedApplicationsText = isBg ? finishedApplications : finishedApplicationsEn;
            Paragraph paragraphFinishedApplicationsHeader = CreateFormattedParagraph(finishedApplicationsText, 18, true, JustificationValues.Left, false);
            body.Append(paragraphFinishedApplicationsHeader);

            if (finishedSummaries.Any())
            {
                AddSummariesInfo(body, finishedSummaries);
            }
            else
            {
                string noApplicationsOfThisTypeText = isBg ? noApplicationsOfThisType : noApplicationsOfThisTypeEn;
                Paragraph noFinishedSummaries = CreateFormattedParagraph(noApplicationsOfThisTypeText, 14, false, JustificationValues.Center, false);
                body.Append(noFinishedSummaries);
                body.Append(new Paragraph(new Run(new Break())));
            }
        }

        private static void AddOnGoingSummaries(Body body, List<SummaryDto> onGoingSummaries)
        {
            string onGoingAppplicationssText = isBg ? onGoingAppplications : onGoingAppplicationsEn;
            Paragraph paragraphOnGoingApplicationsHeader = CreateFormattedParagraph(onGoingAppplicationssText, 18, true, JustificationValues.Left, false);
            body.Append(paragraphOnGoingApplicationsHeader);

            if (onGoingSummaries.Any())
            {
                AddSummariesInfo(body, onGoingSummaries);
            }
            else
            {
                string noApplicationsOfThisTypeText = isBg ? noApplicationsOfThisType : noApplicationsOfThisTypeEn;
                Paragraph noOnGoingSSummaries = CreateFormattedParagraph(noApplicationsOfThisTypeText, 14, false, JustificationValues.Center, false);
                body.Append(noOnGoingSSummaries);
                body.Append(new Paragraph(new Run(new Break())));
            }
        }

        private static void AddSummariesInfo(Body body, List<SummaryDto> summaries)
        {
            foreach (SummaryDto summary in summaries)
            {
                AddSummaryTitle(body, summary.ApplicationOneType);
                AddSummaryPeriod(body, summary.StartYear, summary.EndYear);
                AddNecessarySummaryInfo(body, summary);
                AddAdditionalSummaryInfo(body, summary);

                body.Append(new Paragraph(new Run(new Break())));
            }
        }

        private static void AddSummaryTitle(Body body, ApplicationOneType applicationOneType)
        {
            string title = GetSummaryTypeBasedOnApplicationOne(applicationOneType);
            Paragraph paragraphApplicationOneType = CreateFormattedParagraph(title, 12, true, JustificationValues.Center, false);
            body.Append(paragraphApplicationOneType);
        }

        private static void AddSummaryPeriod(Body body, int? startYear, int? endYear)
        {
            string periodResult = $"{startYear}";
            if (endYear.HasValue)
            {
                periodResult += $" - {endYear}";
            }

            Table table = CreateTable();

            Paragraph periodParagraph = CreateFormattedParagraph(periodResult, 12, false, JustificationValues.Center, false);

            TableRow row = new TableRow();
            TableCell cell = new TableCell(periodParagraph);

            cell.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            row.Append(cell);
            table.Append(row);
            body.Append(table);
        }

        private static void AddNecessarySummaryInfo(Body body, SummaryDto summary)
        {
            string necessarySummaryInfoText = isBg ?
                necessarySummaryInfo :
                necessarySummaryInfoEn;
            Paragraph titleParagraph = CreateFormattedParagraph(necessarySummaryInfoText, 12, true, JustificationValues.Center, true);
            body.Append(titleParagraph);

            Table table = CreateTable();

            string threatTypeText = isBg ? threatType : threatTypeEn;
            string damageTypeText = isBg ? damageType : damageTypeEn;
            string speciesText = isBg ? species : speciesEn;
            string waterText = isBg ? water : waterEn;
            string soilText = isBg ? soil : soilEn;

            table.AppendChild(CreateRow(
                "1.",
                summary.ApplicationOneType == ApplicationOneType.Threat
                    ? threatTypeText
                    : damageTypeText,
                $"{(summary.HasSpeciesDamage ? speciesText : "")}" +
                    $"{(summary.HasWaterDamage ? waterText : "")}" +
                    $"{(summary.HasSoilDamage ? soilText : "")}",
                JustificationValues.Start
            ));

            string threatOccuranceDateText = isBg ? threatOccuranceDate : threatOccuranceDateEn;
            string damageOccuranceDateText = isBg ? damageOccuranceDate : damageOccuranceDateEn;
            string dateText = isBg ? 
                $"{summary.OccurrenceDate?.ToString("dd.MM.yyyy")}г." : 
                summary.OccurrenceDate?.ToString("dd.MM.yyyy");
            string dateDescriptionText = isBg ? 
                summary.OccurrenceDateDescription : 
                summary.OccurrenceDateDescriptionAlt;

            table.AppendChild(CreateRow(
                "2.",
                summary.ApplicationOneType == ApplicationOneType.Threat
                    ? threatOccuranceDateText
                    : damageOccuranceDateText,
                $"{dateText}\n{dateDescriptionText}",
                JustificationValues.Start
            ));

            string activityOfferingsText = string.Empty;
            if (summary.ActivityOfferings != null && summary.ActivityOfferings.Any())
            {
                if (isBg)
                {
                    activityOfferingsText = string.Join("\n\n", summary.ActivityOfferings.Select(a => a.Activity.Name));
                }
                else
                {
                    activityOfferingsText = string.Join("\n\n", summary.ActivityOfferings.Select(a => a.Activity.NameAlt));
                }
            }
            else
            {
                activityOfferingsText = isBg ? unknown : unknownEn;
            }

            table.AppendChild(CreateRow(
                "3.",
                isBg ? activityOffering : activityOfferingEn,
                activityOfferingsText,
                JustificationValues.Start
            ));

            body.Append(table);
        }

        private static void AddAdditionalSummaryInfo(Body body, SummaryDto summary)
        {
            string additionalSummaryInfoText = isBg ? additionalSummaryInfo : additionalSummaryInfoEn;
            Paragraph titleParagraph = CreateFormattedParagraph(additionalSummaryInfoText, 12, true, JustificationValues.Center, true);
            body.Append(titleParagraph);

            Table table = CreateTable();

            string initiatedProcedureDateText = isBg ? initiatedProcedureDate : initiatedProcedureDateEn;
            string removalProcedureDateText = isBg ? removalProcedureDate : removalProcedureDateEn;
            string date = isBg ? 
                $"{summary.ProcedureInitiatedDate?.ToString("dd.MM.yyyy")}г." : 
                summary.ProcedureInitiatedDate?.ToString("dd.MM.yyyy");
            string dateDescription = isBg ? 
                summary.ProcedureInitiatedDateDescription : 
                summary.ProcedureInitiatedDateDescriptionAlt;

            table.AppendChild(CreateRow(
                "4.",
                summary.ApplicationOneType == ApplicationOneType.Threat
                    ? initiatedProcedureDateText
                    : removalProcedureDateText,
                $"{date}\n{dateDescription}",
                JustificationValues.Start
            ));

            string preventionProcedureApplicantText = isBg ? preventionProcedureApplicant : preventionProcedureApplicantEn;
            string removalProcedureApplicantText = isBg ? removalProcedureApplicant : removalProcedureApplicantEn;

            table.AppendChild(CreateRow(
                "5.",
                summary.ApplicationOneType == ApplicationOneType.Threat
                    ? preventionProcedureApplicantText
                    : removalProcedureApplicantText,
                GetApplicantDescription(summary.Applicant),
                JustificationValues.Start
            ));

            string actionsTakenText = string.Empty;
            if (!string.IsNullOrWhiteSpace(summary.ActionsTaken))
            {
                actionsTakenText = isBg ? summary.ActionsTaken : summary.ActionsTakenAlt;
            }
            else
            {
                actionsTakenText = isBg ? noActions : noActionsEn;
            }

            table.AppendChild(CreateRow(
                "6.",
                isBg ? actionsTaken : actionsTakenEn,
                actionsTakenText,
                JustificationValues.Start
            ));

            body.Append(table);
        }

        private static Paragraph CreateFormattedParagraph(string text, int fontSize, bool isBold, JustificationValues justification, bool hasMargin)
        {
            ParagraphProperties paragraphProps = new ParagraphProperties();

            Justification paragraphJustification = new Justification() { Val = justification };

            if (hasMargin)
            {
                SpacingBetweenLines spacing = new SpacingBetweenLines
                {
                    Before = "200"
                };
                paragraphProps.Append(spacing);
            }

            Run run = CreateFormattedRun(text, fontSize, isBold);

            paragraphProps.Append(paragraphJustification);
            Paragraph paragraph = new Paragraph(paragraphProps);
            paragraph.Append(run);

            return paragraph;
        }

        private static Run CreateFormattedRun(string text, int fontSize, bool isBold)
        {
            RunProperties runProperties = new RunProperties()
            {
                Languages = new Languages()
                {
                    Bidi = new StringValue(language),
                    Val = new StringValue(language)
                }
            };

            if (isBold)
            {
                runProperties.Append(new Bold());
            }
            runProperties.Append(new FontSize { Val = (fontSize * 2).ToString() });

            Run run = new Run();
            run.Append(runProperties);

            // Handle new lines
            string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                run.Append(new Text(lines[i]) { Space = SpaceProcessingModeValues.Preserve });

                if (i != lines.Length - 1)
                {
                    run.Append(new Break()); // Add a line break after each line
                }
            }

            return run;
        }

        private static Table CreateTable()
        {
            Table table = new Table();

            TableProperties tableProperties = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = BorderValues.Single, Size = 4 },
                    new BottomBorder { Val = BorderValues.Single, Size = 4 },
                    new LeftBorder { Val = BorderValues.Single, Size = 4 },
                    new RightBorder { Val = BorderValues.Single, Size = 4 },
                    new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4 },
                    new InsideVerticalBorder { Val = BorderValues.Single, Size = 4 }
                ),
                new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
                new TableCellMarginDefault(
                    new TopMargin { Width = "150", Type = TableWidthUnitValues.Dxa },
                    new StartMargin { Width = "100", Type = TableWidthUnitValues.Dxa },
                    new BottomMargin { Width = "0", Type = TableWidthUnitValues.Dxa },
                    new EndMargin { Width = "100", Type = TableWidthUnitValues.Dxa }
                )
            );

            TableLayout tl = new TableLayout() { Type = TableLayoutValues.Fixed };
            tableProperties.TableLayout = tl;

            table.AppendChild(tableProperties);

            return table;
        }

        private static TableRow CreateRow(string col1, string col2, string col3, JustificationValues justification)
        {
            TableRow row = new TableRow();

            Paragraph paragraph1 = CreateFormattedParagraph(col1, 12, false, justification, false);
            Paragraph paragraph2 = CreateFormattedParagraph(col2, 12, false, justification, false);
            Paragraph paragraph3 = CreateFormattedParagraph(col3, 12, false, justification, false);

            TableCell tableCell1 = new TableCell(paragraph1);
            TableCell tableCell2 = new TableCell(paragraph2);
            TableCell tableCell3 = new TableCell(paragraph3);

            tableCell1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "300" }));
            tableCell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2000" }));
            tableCell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2700" }));

            row.Append(tableCell1);
            row.Append(tableCell2);
            row.Append(tableCell3);

            return row;
        }

        private static string GetSummaryTypeBasedOnApplicationOne(ApplicationOneType applicationOneType)
        {
            switch (applicationOneType)
            {
                case ApplicationOneType.Damage:
                    return isBg ? applicationOneTypeDamage : applicationOneTypeDamageEn;
                case ApplicationOneType.Threat:
                    return isBg ? applicationOneTypeThreat : applicationOneTypeThreatEn;
                case ApplicationOneType.ReportedDamage:
                    return isBg ? applicationOneTypeReportedDamage : applicationOneTypeReportedDamageEn;
                default:
                    return string.Empty;
            }
        }

        private static string GetApplicantDescription(ApplicantDto applicant)
        {
            switch (applicant.ApplicantType)
            {
                case ApplicantType.Operator:
                    return $"{(isBg ? operatorString : operatorStringEn)}{Environment.NewLine}({applicant.Operator?.Name})";
                case ApplicantType.Authority:
                    return $"{(isBg ? authority : authorityEn)}{Environment.NewLine}({applicant.Authority?.Name})";
                case ApplicantType.Individual:
                    return $"{(isBg ? individual : individualEn)}{Environment.NewLine}({applicant.Individual?.FirstName} {applicant.Individual?.LastName})";
                case ApplicantType.LegalEntity:
                case ApplicantType.NonGovernmentalOrganization:
                    return $"{(isBg ? legalEntityAndNonGovernmentalOrganization : legalEntityAndNonGovernmentalOrganizationEn)}{Environment.NewLine}({applicant.LegalEntity?.LegalEntityName})";
                default:
                    return unknown;

            }
        }
    }
}
