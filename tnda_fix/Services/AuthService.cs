using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tnda_fix.Models;

namespace tnda_fix.Services
{
    public class AuthService : IDisposable
    {
        private tndaEntities db = new tndaEntities();

        public Response changePassword(int personId, string oldPass, string newPass)
        {
            Response res = new Response
            {
                success = false,
                message = ""
            };
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<ACC> accs = db.ACCs.Where(a => a.ID_Person == personId).ToList();
                    foreach (ACC acc in accs)
                    {
                        if (acc == null)
                        {
                            res.message = "ACCOUNT_NOT_EXITS";
                            return res;
                        }
                        if (acc.Pwd.Trim() != oldPass)
                        {
                            res.message = "WRONG_CURRENT_PASSWORD";
                            return res;
                        }
                        acc.Pwd = Tools.encodeBase64(newPass);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    res.success = true;
                    return res;
                }
                catch (Exception e)
                {
                    res.success = false;
                    res.message = e.Message;
                    transaction.Rollback();
                    return res;
                }
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}