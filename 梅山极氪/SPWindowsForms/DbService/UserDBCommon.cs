using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{

    public static class UserDBCommon
    {


        public static List<UserTable> GetUserTables()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var userTables = db.userTable.AsNoTracking().Where(m => m.validity == true && m.userrole != (int)eSpecialUserRole.厂家).ToList();
                    return userTables;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserTables()::" + ex.Message);
            }
        }
        public static void DeleteUserTable(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var user = db.userTable.FirstOrDefault(m => m.id == id && m.validity == true);
                    if (user != null)
                    {
                        user.validity = false;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteUserTable()::" + ex.Message);
            }
        }
        public static void UpdateUserRole(int id, int role)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var user = db.userTable.FirstOrDefault(m => m.id == id && m.validity == true);
                    if (user != null)
                    {
                        user.userrole = role;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteUserTable()::" + ex.Message);
            }
        }
    }
}
