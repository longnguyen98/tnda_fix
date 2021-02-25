using IronXL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models.filters;

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
        public async System.Threading.Tasks.Task<string> uploadXlsAsync(HttpPostedFileBase file)
        {
            //save to temp
            string temp_path = Path.Combine(Server.MapPath("~/temp_xls"), (DateTime.Now.Millisecond.ToString() + file.FileName).Trim());
            file.SaveAs(temp_path);
            string errorString = "";
            string read_data = "";

            //data for service
            Dictionary<string, string> form = new Dictionary<string, string>();

            //service caller
            string protocol = System.Web.HttpContext.Current.Request.Url.Scheme;
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            string port = System.Web.HttpContext.Current.Request.Url.Port.ToString();
            string controller = "Person";
            string method = "addPerson";
            string post_request = protocol + "://" + host + ":" + port + "/" + controller + "/" + method;
            HttpClient client = new HttpClient();

            //process
            try
            {
                WorkBook wb = WorkBook.Load(temp_path);
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
                        read_data += col.RangeAddressAsString + ": " + col.StringValue + "\n";
                        form.Add(keys[count], col.StringValue);
                        count++;
                    }
                    form.Add("child-role", "4");
                    //call service
                    FormUrlEncodedContent content = new FormUrlEncodedContent(form);
                    HttpResponseMessage response = await client.PostAsync(post_request, content);

                    //clear before add new record
                    count = 0;
                    form.Clear();

                }
            }
            catch (Exception e)
            {
                return e.Message + "\n" + errorString;
            }
            return "Process complete! \n" + read_data;
        }
    }
}