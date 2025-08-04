using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{

    public static class PFLogCommon
    {


        public static Task<List<PfLogTable>> GetPfLogTables(DateTime? startTime, DateTime? stopTime)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (startTime == null) startTime = new DateTime(2000, 1, 1, 0, 0, 0);
                    if (stopTime == null) stopTime = new DateTime(2200, 1, 1, 0, 0, 0);
                    var pfLogTables = db.pfLogTable.AsNoTracking().Where(m => m.create_time >= startTime && m.create_time <= stopTime).OrderByDescending(m=>m.create_time).ToListAsync();
                    return pfLogTables;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPfLogTables()::" + ex.Message);
            }
        }
        public static PfLogTable NewPfLogModel(string pfname, int type, string logBefore="", string logAfter = "", string logData = "")
        {
            try
            {
                var newRow = new PfLogTable();
                newRow.useraccount = GlobalVar.UserNumber;
                newRow.pfname = pfname;
                newRow.create_time = DateTime.Now;
                newRow.logData = logData;
                newRow.logBefore = logBefore;
                newRow.logAfter = logAfter;
                newRow.logType = type;
                return newRow;
            }
            catch (Exception ex)
            {
                throw new Exception("NewPfLogModel()::" + ex.Message);
            }
        }
        public static void InsertLogs(List<PfLogTable> rows)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {

                    db.pfLogTable.AddRange(rows);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InsertLogs()::" + ex.Message);
            }
        }

    }
}
