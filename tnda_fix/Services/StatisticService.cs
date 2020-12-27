using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tnda_fix.Models;

namespace tnda_fix.Services
{
    public class StatisticService
    {
        public List<object> findDuplicatePeople()
        {
            List<object> result = new List<object>();
            using (tndaEntities db = new tndaEntities())
            {                
                List<Person> people = db.People.Where(p => p.ID_role == 4 || p.ID_role == 7).ToList();
                foreach(Person p1 in people)
                {
                    foreach(Person p2 in people)
                    {
                        if (p1.for_search.Equals(p2.for_search))
                        {
                            object personPair = new {p1Id = p1.ID,p2Id = p2.ID};
                            result.Add(personPair);
                        }
                    }
                }
            }
            return result;    
        }     
    }
}