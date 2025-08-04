using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{

    public static class PFDBCommon
    {


        public static List<PfMainTable> GetPfMainTable()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var pfMainTables = db.pfMainTable.AsNoTracking().Where(m => m.validity == true).ToList();
                    return pfMainTables;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPfMainTable()::" + ex.Message);
            }
        }
        public static PfMainTable NewPfMainTable(string name)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var newRow = new PfMainTable();
                    newRow.pfname = name;
                    newRow.creat_useraccount = GlobalVar.UserNumber;
                    db.pfMainTable.Add(newRow);
                    db.SaveChanges();
                    var newLogRow = PFLogCommon.NewPfLogModel(name, (int)ePfLog.新建);
                    PFLogCommon.InsertLogs(new List<PfLogTable> { newLogRow });
                    return newRow;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("NewPfMainTable()::" + ex.Message);
            }
        }
        public static PfMainTable GetPfMainTableById(int _id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {

                    return db.pfMainTable.FirstOrDefault(m => m.id == _id && m.validity == true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPfMainTableById()::" + ex.Message);
            }
        }
        public static void RenamePfMainTable(int _id,string _name)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.pfMainTable.Any(m => m.pfname.ToUpper() == _name.ToUpper() && m.id != _id))
                    {
                        throw new Exception("已存在该配方名，重命名失败");
                    }

                    var row = db.pfMainTable.FirstOrDefault(m=>m.id == _id);
                    var newLogRow = PFLogCommon.NewPfLogModel(_name, (int)ePfLog.重命名, row.pfname,_name);
                    row.pfname = _name;
                    row.update_useraccount = GlobalVar.UserNumber;
                    row.update_time = DateTime.Now;
                    db.SaveChanges();
                   
                    PFLogCommon.InsertLogs(new List<PfLogTable> { newLogRow });
                 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RenamePfMainTable()::" + ex.Message);
            }
        }
        public static void DeletePfMainTable(int _id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var row = db.pfMainTable.FirstOrDefault(m => m.id == _id);
                    if (row != null)
                    {
                        row.validity =false;
                        row.update_useraccount = GlobalVar.UserNumber;
                        row.update_time = DateTime.Now;
                        db.SaveChanges();
                        var newLogRow = PFLogCommon.NewPfLogModel(row.pfname,  (int)ePfLog.删除);
                        PFLogCommon.InsertLogs(new List<PfLogTable> { newLogRow });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeletePfMainTable()::" + ex.Message);
            }
        }
    }
}
