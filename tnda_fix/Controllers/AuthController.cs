using System.Linq;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class AuthController : Controller
    {
        public JsonResult getAuthStatus()
        {
            bool login = Session["accountName"] != null;
            return Json(login, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult login()
        {
            string username, password;
            username = Request["username"];
            password = Request["password"];
            if (auth(username, password))
            {
                Session.Add("accountName", username);

            }
            return Redirect("~/internal/index");
        }
        public ActionResult logout()
        {
            Session.Clear();
            return Redirect("~/external/index");
        }
        private bool auth(string username, string password)
        {
            tndaEntities db = new tndaEntities();
            ACC account = db.ACCs.Where(acc => acc.UserName.Equals(username)).FirstOrDefault();
            //
            if (account != null)
            {
                if (account.Pwd.Trim().Equals(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}