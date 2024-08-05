# How can we fetch a excel file with file picker and import data into the .NET MAUI DataGrid?
In this article, we will show you how can we fetch a excel file with file picker and import data into the [.Net Maui DataGrid](https://www.syncfusion.com/maui-controls/maui-datagrid).

## C#
The below code illustrates how to fetch a excel file with file picker and import data into the DataGrid.
```
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
```

 ![FilePicker.png](https://support.syncfusion.com/kb/agent/attachment/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI3NzczIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.lNSPuqqSHipgdcTfTf8_nieKs-jomRpzqVzN5sxg7gc)

[View sample in GitHub](https://github.com/SyncfusionExamples/How-can-we-fetch-excel-file-with-file-picker-in-.NET-MAUI-DataGrid)

Take a moment to explore this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more information about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples. Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid (SfDataGrid).
 
##### Conclusion
 
I hope you enjoyed learning about how to fetch a excel file with file picker and import data into .NET MAUI DataGrid (SfDataGrid) Pdf Exporting.
 
You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.
 
If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!