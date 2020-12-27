using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Services
{
    public class PersonService
    {
        public Object getPersonById(int id)
        {
            using (tndaEntities db = new tndaEntities())
            {
                Person child = db.People.Find(id);
                //
                if (child.ID_role == 4 || child.ID_role == 7)
                {
                    Family family = db.Families.Find(child.ID_Farmily);
                    //
                    Person father = db.People.Find(family.ID_Dad);
                    //            
                    Person mother = db.People.Where(p => p.ID_Farmily == family.ID && p.ID != father.ID && p.ID != child.ID).FirstOrDefault();
                    //
                    Person glv = db.People.Where(p => p.ID_Class == child.ID_Class && (p.ID_role == 1 || p.ID_role == 2) && p.ID_Class != null).FirstOrDefault();
                    string glv_name = glv != null ? (glv.ChristianName + " " + glv.FirstName + " " + glv.Name) : "";
                    string img = child.Image;
                    if (string.IsNullOrEmpty(img))
                    {
                        img = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                    }
                    string birthString = child.Birth != null ? child.Birth.Value.ToString("dd.MM.yyy") : "";
                    string classString = child.Class != null ? (child.Class.Grade.GradeName + " " + child.Class.ClassName) : "";
                    //
                    var fatherJson = new { fa_id = father.ID, ch_name = father.ChristianName, fname = father.FirstName, name = father.Name, role = father.Role.RoleName, phone = father.Phone };
                    var motherJson = new { mo_id = mother.ID, ch_name = mother.ChristianName, fname = mother.FirstName, name = mother.Name, role = mother.Role.RoleName, phone = mother.Phone, role_id = child.ID_role };
                    var glvId = glv != null ? glv.ID : 91;
                    var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, pclass = classString, role = child.Role.RoleName, glv = glv_name, id_class = child.ID_Class, birth = birthString, address = child.Address, father = fatherJson, mother = motherJson, role_id = child.ID_role, glv_id = glvId, img = img, note = child.Note };
                    //
                    return json;
                }
                else
                {
                    string image = child.Image;
                    if (string.IsNullOrEmpty(image))
                    {
                        image = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                    }
                    string birthString = child.Birth != null ? child.Birth.Value.ToString("dd.MM.yyy") : "";
                    string classString = child.Class != null ? (child.Class.Grade.GradeName + " " + child.Class.ClassName) : "";
                    var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, pclass = classString, id_class = child.ID_Class, role = child.Role.RoleName, birth = birthString, address = child.Address, phone = child.Phone, role_id = child.ID_role, img = image };
                    return json;
                }
            }
        }
    }
}