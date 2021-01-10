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
        public ActionResult AllClasses()
        {
            return View();
        }
        public ActionResult dashBoard()
        {
            return View();
        }

        //import from excel
        [HttpPost]
        public async System.Threading.Tasks.Task<string> uploadXlsAsync(int id, HttpPostedFileBase file)
        {
            //save to temp
            string temp_path = Path.Combine(Server.MapPath("~/temp_xls"), (System.DateTime.Now.Millisecond.ToString() + file.FileName).Trim());
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
                List<string> keys = new List<string>();
                keys.Add("child-ch-name");
                keys.Add("child-fname");
                keys.Add("child-name");
                keys.Add("child-birth");
                keys.Add("child-gender");
                keys.Add("child-class");
                keys.Add("child-address");
                keys.Add("fa-ch-name");
                keys.Add("fa-fname");
                keys.Add("fa-name");
                keys.Add("fa-phone");
                keys.Add("mo-ch-name");
                keys.Add("mo-fname");
                keys.Add("mo-name");
                keys.Add("mo-phone");
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
                    foreach (RangeColumn col in row.Columns)
                    {
                        //logging for error
                        errorString = col.RangeAddressAsString;

                        //add value to form
                        read_data += col.RangeAddressAsString + ": " + col.StringValue + "\t";
                        form.Add(keys[count], col.StringValue);
                        count++;
                    }
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
            return "Process complete! \t" + read_data;
        }
    }
}