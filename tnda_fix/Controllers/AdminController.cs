using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;
using tnda_fix.Models.filters;
using tnda_fix.Services;

namespace tnda_fix.Controllers
{
    [Auth]
    [AdminAuth]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Internal/index.cshtml");
        }

        public ActionResult AllGLV()
        {
            return View();
        }

        public ActionResult CheckNew()
        {
            return View();
        }

        public ActionResult allClasses()
        {
            return View("Internal/allClasses");
        }

        public ActionResult dashBoard()
        {
            return View();
        }

        public ActionResult import()
        {
            return View();
        }
        public ActionResult listByClassAdminView()
        {
            return View();
        }
        public ActionResult detailAdminView()
        {
            return View();
        }
        //import from excel
        [HttpPost]
        public string processExcel(HttpPostedFileBase file)
        {
            int readData = 0;
            //data for service
            FormCollection formCollection = new FormCollection();

            //process
            try
            {
                IWorkbook wb = new XSSFWorkbook(file.InputStream);
                ISheet sheet = wb.GetSheetAt(wb.ActiveSheetIndex);
                List<string> keys = new List<string>
                {
                    "child-ch-name",
                    "child-fname",
                    "child-name",
                    "child-birth",
                    "child-gender",
                    "child-class",
                    "child-address",
                    "fa-ch-name",
                    "fa-fname",
                    "fa-name",
                    "fa-phone",
                    "mo-ch-name",
                    "mo-fname",
                    "mo-name",
                    "mo-phone"
                };
                //
                int count = 0;
                IRow row;
                ICell cell;
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    row = sheet.GetRow(rowIndex);
                    for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                    {
                        try
                        {
                            cell = row.GetCell(cellIndex);
                            cell.SetCellType(CellType.String);
                            formCollection.Set(keys[count], cell.StringCellValue);
                        }
                        catch (Exception)
                        {
                            formCollection.Set(keys[count], "");
                        }
                        count++;
                    }
                    formCollection.Set("child-role", "4");
                    using (PersonService personService = new PersonService())
                    {
                        Response res = personService.addPerson(formCollection);
                        if (res.success)
                        {
                            readData++;
                        }
                    }
                    //clear before add new record
                    count = 0;
                    formCollection.Clear();
                }
            }
            catch (Exception e)
            {
                return "ERROR" + e.Message;
            }
            return "Processed " + readData + " records";
        }
    }
}