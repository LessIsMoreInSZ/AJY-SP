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

    public static class AlarmLogCommon
    {
        public static Task<List<AlarmLogTable>> GetAlarmLogTables(DateTime? startTime, DateTime? stopTime)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (startTime == null) startTime = new DateTime(2000, 1, 1, 0, 0, 0);
                    if (stopTime == null) stopTime = new DateTime(2200, 1, 1, 0, 0, 0);
                    var alarmLogTables = db.alarmLogTable.AsNoTracking().Where(m => m.create_time >= startTime && m.create_time <= stopTime).OrderByDescending(m=>m.create_time).ToListAsync();
                    return alarmLogTables;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetAlarmLogTables()::" + ex.Message);
            }
        }
        public static List<AlarmLogTable> GetNowAlarm()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var alarmLogTables = db.alarmLogTable.AsNoTracking().Where(m => m.alarmFlag==true).OrderByDescending(m => m.create_time).ToList();
                    return alarmLogTables;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetAlarmLogTables()::" + ex.Message);
            }
        }
        public static List<AlarmLogTable> NewAlarmLogTables(List<string> alarmCodes)
        {
            try
            {
                var newRows = new List<AlarmLogTable>();
                alarmCodes.ForEach(a =>
                {
                    var newRow = new AlarmLogTable
                    {
                        alarmCode=a,
                        alarmFlag=true,
                create_time=DateTime.Now
                    };
                    newRows.Add(newRow);
                });
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.alarmLogTable.AddRange(newRows);
                    db.SaveChanges();
                    return newRows;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("NewAlarmLogTables()::" + ex.Message);
            }
        }
        public static void OffAlarms(List<int> ids)
        {
            try
            {
                if (ids.Count > 0)
                {
                    using (DataBaseContext db = new DataBaseContext())
                    {
                        var alarmLogTables = db.alarmLogTable.Where(m => m.alarmFlag == true && ids.Contains(m.id)).ToList();
                        if (alarmLogTables.Count > 0)
                        {
                            alarmLogTables.ForEach(m => m.alarmFlag = false);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InsertLogs()::" + ex.Message);
            }
        }

    }
}
