using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Drawing;
using System.IO;

namespace AsposeGantt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello AsposeGantt! Let's do something.");
            Console.WriteLine("Press any key to exit...");
            Aspose.Cells.License lic = new Aspose.Cells.License();
            string testOutDir = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf(@"\bin"));
            lic.SetLicense(Path.Combine(testOutDir, @"ExternalLibraries\Aspose.Total.lic"));

            // For complete examples and data files, please go to https://github.com/aspose-cells/Aspose.Cells-for-.NET
            ChartType chartType = ChartType.BarStacked; //  Bar; // LineWithDataMarkers;
            switch (chartType)
            {
                case ChartType.LineWithDataMarkers:
                    // Instantiate a workbook
                    Workbook workbook = new Workbook();

                    // Access first worksheet
                    Worksheet worksheet = workbook.Worksheets[0];

                    // Set columns title 
                    worksheet.Cells[0, 0].Value = "X";
                    worksheet.Cells[0, 1].Value = "Y";

                    // Random data shall be used for generating the chart
                    Random R = new Random();

                    // Create random data and save in the cells
                    for (int i = 1; i < 21; i++)
                    {
                        worksheet.Cells[i, 0].Value = i;
                        worksheet.Cells[i, 1].Value = 0.8;
                    }

                    for (int i = 21; i < 41; i++)
                    {
                        worksheet.Cells[i, 0].Value = i - 20;
                        worksheet.Cells[i, 1].Value = 0.9;
                    }
                    // Add a chart to the worksheet
                    int idx0 = worksheet.Charts.Add(chartType, 1, 3, 20, 20);

                    // Access the newly created chart
                    Chart chart = worksheet.Charts[idx0];

                    // Set chart style
                    chart.Style = 3;

                    // Set autoscaling value to true
                    chart.AutoScaling = true;

                    // Set foreground color white
                    chart.PlotArea.Area.ForegroundColor = Color.White;

                    // Set Properties of chart title
                    chart.Title.Text = "Sample Chart";

                    // Set chart type
                    chart.Type = ChartType.LineWithDataMarkers;

                    // Set Properties of categoryaxis title
                    chart.CategoryAxis.Title.Text = "Units";

                    //Set Properties of nseries
                    int s2_idx = chart.NSeries.Add("A2: A2", true);
                    int s3_idx = chart.NSeries.Add("A22: A22", true);

                    // Set IsColorVaried to true for varied points color
                    chart.NSeries.IsColorVaried = true;

                    // Set properties of background area and series markers
                    chart.NSeries[s2_idx].Area.Formatting = FormattingType.Custom;
                    chart.NSeries[s2_idx].Marker.Area.ForegroundColor = Color.Yellow;
                    chart.NSeries[s2_idx].Marker.Border.IsVisible = false;

                    // Set X and Y values of series chart
                    chart.NSeries[s2_idx].XValues = "A2: A21";
                    chart.NSeries[s2_idx].Values = "B2: B21";

                    // Set properties of background area and series markers
                    chart.NSeries[s3_idx].Area.Formatting = FormattingType.Custom;
                    chart.NSeries[s3_idx].Marker.Area.ForegroundColor = Color.Green;
                    chart.NSeries[s3_idx].Marker.Border.IsVisible = false;

                    // Set X and Y values of series chart
                    chart.NSeries[s3_idx].XValues = "A22: A41";
                    chart.NSeries[s3_idx].Values = "B22: B41";

                    // Save the workbook
                    workbook.Save(Path.Combine(testOutDir, @"ExternalLibraries\LineWithDataMarkerChart.xlsx"), Aspose.Cells.SaveFormat.Xlsx);
                    break;
                case ChartType.BarStacked:
                    Workbook workbook1 = new Workbook(FileFormatType.Xlsx);
                    Worksheet sheet = workbook1.Worksheets[0];

                    Cells cells = sheet.Cells;
                    cells[1, 0].PutValue("Company A");
                    cells[2, 0].PutValue("Company B");
                    cells[3, 0].PutValue("Company C");
                    cells[0, 1].PutValue(2008);
                    cells[0, 2].PutValue(2009);
                    cells[0, 3].PutValue(2010);
                    cells[1, 1].PutValue(10000);
                    cells[2, 1].PutValue(20000);
                    cells[3, 1].PutValue(30000);
                    cells[1, 2].PutValue(15000);
                    cells[2, 2].PutValue(25000);
                    cells[3, 2].PutValue(35000);
                    cells[1, 3].PutValue(18000);
                    cells[2, 3].PutValue(28000);
                    cells[3, 3].PutValue(38000);

                    int chartIndex = sheet.Charts.Add(ChartType.BarStacked, 9, 9, 21, 15);

                    Chart chart1 = sheet.Charts[chartIndex];
                    chart1.NSeries.Add("$B$2:$D$4", false);
                    chart1.NSeries.CategoryData = "$B$1:$D$1";


                    chart1.Title.Text = "Sale";
                    chart1.Style = 3;
                    workbook1.Save(Path.Combine(testOutDir, @"ExternalLibraries\BarStacked.xlsx"), Aspose.Cells.SaveFormat.Xlsx);
                    break;
                case ChartType.Bar:
                    // Create empty workbook.
                    Workbook wb = new Workbook();

                    // Access first worksheet.
                    Worksheet ws = wb.Worksheets[0];

                    // Add Bar chart in first worksheet.
                    int idx1 = ws.Charts.Add(ChartType.Bar, 5, 2, 20, 10);

                    // Access Bar chart.
                    Chart ch = ws.Charts[0];

                    // Add two number series, true means they are vertical.
                    ch.NSeries.Add("{6,3,1,7}", true);
                    ch.NSeries.Add("{2,5,7,1}", true);

                    // Set the category data to show on X-axis.
                    ch.NSeries.CategoryData = "{Apple,Pear,Orange,Mango}";

                    // Set the name of first and second series.
                    ch.NSeries[0].Name = "Cricket";
                    ch.NSeries[1].Name = "Hockey";

                    // Save the output in xlsx format.
                    wb.Save(Path.Combine(testOutDir, @"ExternalLibraries\outputBarChart.xlsx"), SaveFormat.Xlsx);
                    break;
                default:
                    break;

            }
            Console.ReadKey();
        }
    }
}
