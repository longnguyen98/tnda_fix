using IronXL;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
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
            return View();
        }

        public ActionResult dashBoard()
        {
            return View();
        }

        public ActionResult import()
        {
            return View();
        }

        //import from excel
        [HttpPost]
        public async System.Threading.Tasks.Task<string> UploadXlsAsync(HttpPostedFileBase file)
        {
            string errorString = "";
            string readData = "";
            //data for service
            FormCollection formCollection = new FormCollection();

            //process
            try
            {
                WorkBook wb = WorkBook.Load(file.InputStream);
                WorkSheet ws = wb.WorkSheets.First();
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
                bool firstRow = true;
                foreach (RangeRow row in ws.Rows)
                {
                    if (firstRow)
                    {
                        firstRow = false;
                        continue;
                    }
                    if (row.IsEmpty)
                    {
                        break;
                    }
                    foreach (RangeColumn col in row.Columns)
                    {
                        //logging for error
                        errorString = col.RangeAddressAsString;

                        //add value to form
                        readData += col.RangeAddressAsString + ": " + col.StringValue + "\n";
                        formCollection.Set(keys[count], col.StringValue);
                        count++;
                    }
                    formCollection.Set("child-role", "4");
                    //call service

                    using (PersonService personService = new PersonService())
                    {
                        Response res = personService.addPerson(formCollection);
                        if (!res.success)
                        {
                            readData += "---" + res.message + "---";
                        }
                    }
                    //clear before add new record
                    count = 0;
                    formCollection.Clear();
                }
            }
            catch (Exception e)
            {
                return e.Message + "\n" + errorString;
            }
            return "Processed" + readData;
        }
    }
}