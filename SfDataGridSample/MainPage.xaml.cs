using Syncfusion.Maui.DataGrid.Exporting;
using Syncfusion.XlsIO;
using System.Data;

namespace SfDataGridSample
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        void Export_Clicked(System.Object sender, System.EventArgs e)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            DataGridExcelExportingOption option = new DataGridExcelExportingOption();
            var excelEngine = excelExport.ExportToExcel(dataGrid, option);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();
            string OutputFilename = "ExportFeature.xlsx";
            SaveService saveService = new();
            saveService.SaveAndView(OutputFilename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
        }

        void Import_Clicked(System.Object sender, System.EventArgs e)
        {
            LoadDataGridAsync();
        }

        private async Task LoadDataGridAsync()
        {
            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            //Initialize IApplication
            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;


            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    // iOS: using Uniform Type Identifiers (UTIs)
                    { DevicePlatform.iOS, new[] { "public.data" } }, 
        
                    // Android: using MIME types
                    { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } }, 
        
                    // Windows: using file extensions
                    { DevicePlatform.WinUI, new[] { ".xlsx" } },
                });

            PickOptions pickOptions = new PickOptions()
            {
                PickerTitle = "Please select DataGrid file",
                FileTypes = customFileType,
            };

            var result = await FilePicker.Default.PickAsync(pickOptions);
            if (result != null)
            {
                //Load the file into stream
                Stream inputStream = await result.OpenReadAsync();

                //Loads or open an existing workbook through Open method of IWorkbooks
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(inputStream);

                IWorksheet worksheet = workbook.Worksheets[0];

                DataTable customersTable = worksheet.ExportDataTable(1, 1, 10, 6, ExcelExportDataTableOptions.ColumnNames);

                this.dataGrid.ItemsSource = customersTable;

                workbook.Close();
            }

            excelEngine.Dispose();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            dataGrid.ItemsSource = null;
        }
    }
}
