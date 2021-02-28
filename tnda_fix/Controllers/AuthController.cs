using System.Linq;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class AuthController : Controller
    {
        public JsonResult getAuthStatus()
        {
            bool login = Session["personId"] != null;
            return Json(login, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPersonFromSession()
        {
            int person_id = (int)Session["personId"];
            Person p = (Person)Session["person"];
            var json = new
            {
                id = p.ID,
                ch_name = p.ChristianName,
                fname = p.FirstName,
                name = p.Name,
                birth = p.Birth.Value.ToShortDateString(),
                address = p.Address,
                phone = p.Phone,
                role_id = p.ID_role,
                id_class = p.ID_Class
            };
            return Json(json, JsonRequestBehavior.AllowGet);
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
                Session.Timeout = 1440;
                Logger.create("LOGIN", username + " has logged in", (int)Session["personId"]);
                //admin
                if (username.EndsWith("admin"))
                {
                    return Redirect("~/Admin/index");
                }
                else
                {
                    return Redirect("~/internal/index?id=" + (int)Session["personId"]);
                }
            }
            //
            else
            {
                return Redirect("~/external/index");
            }
        }
        public ActionResult logout()
        {
            Logger.create("LOGOUT", Session["accountName"] + " has logged out", (int)Session["personId"]);
            Session.Clear();
            return Redirect("~/external/index");
        }
        private bool auth(string username, string password)
        {
            ACC account = null;
            using (tndaEntities db = new tndaEntities())
            {
                account = db.ACCs.Where(acc => acc.UserName.Equals(username)).FirstOrDefault();
                if (account != null)
                {
                    if (account.Pwd.Trim().Equals(password))
                    {
                        Person p = account.Person;
                        Session.Add("personId", p.ID);
                        Session.Add("person", p);
                        if (p.ID_Class != null)
                        {
                            Session.Add("classId", p.Class.ID);
                        }
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
}