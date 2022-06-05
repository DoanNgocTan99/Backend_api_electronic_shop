using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class StatisticalController : BaseApiController
    {
        private readonly IStatisticalService _statisticalService;
        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StatisticalDto>> Get()
        {
            try
            {
                return Ok(_statisticalService.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTopCustomers")]
        public ActionResult<IEnumerable<TopCustomerDto>> GetTopCustomers()
        {
            try
            {
                return Ok(_statisticalService.GetTopCustomers());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetLatestOrders")]
        public ActionResult<IEnumerable<LatestOrder>> GetLatestOrders()
        {
            try
            {
                return Ok(_statisticalService.GetLatestOrders());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetFileExcel")]
        public ActionResult<string> GetFileExcel()
        {
            try
            {
                IEnumerable<LatestOrder> LatestOrders = _statisticalService.GetLatestOrders();
                IEnumerable<TopCustomerDto> TopCustomers = _statisticalService.GetTopCustomers();

                DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(LatestOrders), (typeof(DataTable)));
                DataTable tablewTopCustomers = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(TopCustomers), (typeof(DataTable)));

                using (SpreadsheetDocument document = SpreadsheetDocument.Create("DataStatistical.xlsx", SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                    Sheet SheetLatestOrders = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "LatestOrders" };
                    sheets.Append(SheetLatestOrders);

                    Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                    Boolean needToInsertColumns = false;
                    if (lstColumns == null)
                    {
                        lstColumns = new Columns();
                        needToInsertColumns = true;
                    }
                    lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 12, CustomWidth = true });
                    lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 26, CustomWidth = true });
                    lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 30, CustomWidth = true });
                    lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 20, CustomWidth = true });
                    lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 27, CustomWidth = true });
                    // Only insert the columns if we had to create a new columns element
                    if (needToInsertColumns)
                        worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                    // Get the sheetData cells


                    MergeCells mergeCells = new MergeCells();
                    mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:E1") });
                    worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

                    Run run1 = new Run();
                    run1.Append(new Text("Latest Orders"));

                    RunProperties run1Properties = new RunProperties();
                    run1Properties.Append(new Bold());
                    run1Properties.Append(new FontSize() { Val = 22 });

                    run1.RunProperties = run1Properties;

                    //create a new inline string and append both runs
                    InlineString inlineTitle = new InlineString();
                    inlineTitle.Append(run1);

                    Cell cellTitle = new Cell()
                    {
                        CellReference = "A1",
                        DataType = CellValues.InlineString,
                    };
                    cellTitle.Append(inlineTitle);
                    Row titleRow = new Row()
                    {
                        Height = 35.0,
                        CustomHeight = true
                    };
                    titleRow.AppendChild(cellTitle);
                    sheetData.AppendChild(titleRow);

                    //Merge cell


                    string[] list1 = new string[] {
                        "Id",
                        "Tên",
                        "Ngày mua",
                        "Tổng giá",
                        "Tình trạng đơn hàng"
                    };
                    //Tạo header cho table
                    Row headerRow = new Row();

                    List<String> columns = new List<string>();
                    int j = 0;
                    foreach (System.Data.DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);
                        Run runHeader = new Run();
                        runHeader.Append(new Text(list1[j]));

                        RunProperties runHeaderProperties = new RunProperties();
                        runHeaderProperties.Append(new Bold());
                        runHeaderProperties.Append(new FontSize() { Val = 14 });

                        runHeader.RunProperties = runHeaderProperties;

                        //create a new inline string and append both runs
                        InlineString inlineHeader = new InlineString();
                        inlineHeader.Append(runHeader);
                        Cell cell = new Cell()
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(list1[j])
                        };
                        cell.Append(inlineHeader);
                        headerRow.AppendChild(cell);
                        j++;
                    }

                    sheetData.AppendChild(headerRow);
                    foreach (DataRow dsrow in table.Rows)
                    {
                        Row newRow = new Row();
                        foreach (String col in columns)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(dsrow[col].ToString());
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                    WorksheetPart worksheetPartTopCustomers = workbookPart.AddNewPart<WorksheetPart>();

                    var sheetDataTopCustomers = new SheetData();
                    worksheetPartTopCustomers.Worksheet = new Worksheet(sheetDataTopCustomers);

                    Sheet SheetTopCustomers = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPartTopCustomers), SheetId = 2, Name = "TopCustomers" };
                    sheets.Append(SheetTopCustomers);

                    Columns lstColumns2 = worksheetPartTopCustomers.Worksheet.GetFirstChild<Columns>();
                    Boolean needToInsertColumns1 = false;
                    if (lstColumns2 == null)
                    {
                        lstColumns2 = new Columns();
                        needToInsertColumns1 = true;
                    }
                    lstColumns2.Append(new Column() { Min = 1, Max = 1, Width = 17, CustomWidth = true });
                    lstColumns2.Append(new Column() { Min = 2, Max = 2, Width = 17, CustomWidth = true });
                    lstColumns2.Append(new Column() { Min = 3, Max = 3, Width = 17, CustomWidth = true });
                    // Only insert the columns if we had to create a new columns element
                    if (needToInsertColumns1)
                        worksheetPartTopCustomers.Worksheet.InsertAt(lstColumns2, 0);


                    MergeCells mergeCellsTopCustomers = new MergeCells();
                    mergeCellsTopCustomers.Append(new MergeCell() { Reference = new StringValue("A1:C1") });
                    worksheetPartTopCustomers.Worksheet.InsertAfter(mergeCellsTopCustomers, worksheetPartTopCustomers.Worksheet.Elements<SheetData>().First());

                    Run runTopCustomers = new Run();
                    runTopCustomers.Append(new Text("Top Customers"));

                    RunProperties runTopCustomersProperties = new RunProperties();
                    runTopCustomersProperties.Append(new Bold());
                    runTopCustomersProperties.Append(new FontSize() { Val = 22 });

                    runTopCustomers.RunProperties = runTopCustomersProperties;

                    //create a new inline string and append both runs
                    InlineString inlineTitleTopCustomers = new InlineString();
                    inlineTitleTopCustomers.Append(runTopCustomers);

                    Cell cellTitleTopCustomers = new Cell()
                    {
                        CellReference = "A1",
                        DataType = CellValues.InlineString,
                    };
                    cellTitleTopCustomers.Append(inlineTitleTopCustomers);
                    Row titleRowTopCustomers = new Row()
                    {
                        Height = 25.0,
                        CustomHeight = true
                    };
                    titleRowTopCustomers.AppendChild(cellTitleTopCustomers);
                    sheetDataTopCustomers.AppendChild(titleRowTopCustomers);

                    //Merge cell


                    //Tạo header cho table
                    Row headerRowTopCustomers = new Row();
                    string[] list2 = new string[] {
                        "Tên",
                        "Tổng đơn",
                        "Tổng giá"
                    };
                    List<String> columnsTopCustomers = new List<string>();
                    int i = 0;
                    foreach (System.Data.DataColumn column_2 in tablewTopCustomers.Columns)
                    {
                        columnsTopCustomers.Add(column_2.ColumnName);
                        Run runHeaderTopCustomers = new Run();
                        runHeaderTopCustomers.Append(new Text(list2[i]));

                        RunProperties runHeaderProperties_2 = new RunProperties();
                        runHeaderProperties_2.Append(new Bold());
                        runHeaderProperties_2.Append(new FontSize() { Val = 14 });

                        runHeaderTopCustomers.RunProperties = runHeaderProperties_2;

                        //create a new inline string and append both runs
                        InlineString inlineHeader_2 = new InlineString();
                        inlineHeader_2.Append(runHeaderTopCustomers);
                        Cell cell_2 = new Cell()
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(list2[i])
                        };
                        cell_2.Append(inlineHeader_2);
                        headerRowTopCustomers.AppendChild(cell_2);
                        i++;
                    }

                    sheetDataTopCustomers.AppendChild(headerRowTopCustomers);
                    foreach (DataRow dsrow_2 in tablewTopCustomers.Rows)
                    {
                        Row newRow = new Row();
                        foreach (String col in columnsTopCustomers)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(dsrow_2[col].ToString());
                            newRow.AppendChild(cell);
                        }

                        sheetDataTopCustomers.AppendChild(newRow);
                    }

                    workbookPart.Workbook.Save();
                }
                return Ok(string.Empty);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
