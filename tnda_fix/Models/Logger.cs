using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnda_fix.Models
{
    public class Logger
    {
        public static void create(string type,string detail,int userId)
        {
            using (tndaEntities entities = new tndaEntities())
            {
                Log log = new Log();
                log.Type = type;
                log.Detail = detail;
                log.UserId = userId;
                log.Time = DateTime.Now;
                entities.Logs.Add(log);
                entities.SaveChanges();
            }
        }
    }
}