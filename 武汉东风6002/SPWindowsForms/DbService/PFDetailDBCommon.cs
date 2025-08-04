using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{
   
    public static class PFDetailDBCommon
    {
        public static List<PfDetailTable> GetPfDetailTable(int pfid)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var pfDetailTables = db.pfDetailTable.AsNoTracking().Where(m => m.pfid == pfid).ToList();
                    return pfDetailTables;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("GetPfDetailTable()::" + ex.Message);
            }
        }
        public static PfLogTable CheckSameFloat(string pfname, string chname, string varname,float? value1, float? value2)
        {
            if (value1 == null) return null;
            if (value2 == null) return null;
            if (value1 == value2) return null;
            else
            {
                var lVarname = GetLanguageParName(varname);
                return PFLogCommon.NewPfLogModel(pfname,  (int)ePfLog.修改, $"{value1}", $"{value2}", $"{chname}-{lVarname}");
            }
        }
        public static PfLogTable CheckSameShort(string pfname, string chname, string varname, short? value1, short? value2)
        {
            if (value1 == null) return null;
            if (value2 == null) return null;
            if (value1 == value2) return null;
            else
            {
                var lVarname = GetLanguageParName(varname);
                return PFLogCommon.NewPfLogModel(pfname, (int)ePfLog.修改, $"{value1}", $"{value2}", $"{chname}-{lVarname}");
            }
        }
        public static PfLogTable CheckSameBool(string pfname, string chname, string varname, bool? value1, bool? value2)
        {
            if (value1 == null) return null;
            if (value2 == null) return null;
            if (value1 == value2) return null;
            else
            {
                var lVarname = GetLanguageParName(varname);
                return PFLogCommon.NewPfLogModel(pfname, (int)ePfLog.修改, $"{value1}", $"{value2}", $"{chname}-{lVarname}");
            }
        }
        private static string GetLanguageParName(string input)
        {
            var result= LanguageSet.SetL("ParametersDbLog", input);
            if (result == "Language Error") result = input;
            return result;
        } 
        public static List<PfLogTable> GetUpdateLogTables(string pfname,string chname, PfDetailTable dbTable, PfDetailTable formTable)
        {
            var results = new List<PfLogTable>();
            results.Add(CheckSameFloat(pfname, chname, "Opentime_hi", dbTable.Opentime_hi, formTable.Opentime_hi));
            results.Add(CheckSameFloat(pfname, chname, "Opentime_lo", dbTable.Opentime_lo, formTable.Opentime_lo));

            results.Add(CheckSameFloat(pfname, chname, "Closetime_hi", dbTable.Closetime_hi, formTable.Closetime_hi));
            results.Add(CheckSameFloat(pfname, chname, "Closetime_lo", dbTable.Closetime_lo, formTable.Closetime_lo));

            results.Add(CheckSameFloat(pfname, chname, "Close_pos_hi", dbTable.Close_pos_hi, formTable.Close_pos_hi));
            results.Add(CheckSameFloat(pfname, chname, "Close_pos_lo", dbTable.Close_pos_lo, formTable.Close_pos_lo));

            results.Add(CheckSameFloat(pfname, chname, "Vactime_hi", dbTable.Vactime_hi, formTable.Vactime_hi));
            results.Add(CheckSameFloat(pfname, chname, "Vactime_lo", dbTable.Vactime_lo, formTable.Vactime_lo));

            results.Add(CheckSameFloat(pfname, chname, "P_blow_hi", dbTable.P_blow_hi, formTable.P_blow_hi));
            results.Add(CheckSameFloat(pfname, chname, "P_blow_lo", dbTable.P_blow_lo, formTable.P_blow_lo));

            results.Add(CheckSameFloat(pfname, chname, "P_vac_hi", dbTable.P_vac_hi, formTable.P_vac_hi));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_lo", dbTable.P_vac_lo, formTable.P_vac_lo));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_pos", dbTable.P_vac_pos, formTable.P_vac_pos));

            results.Add(CheckSameFloat(pfname, chname, "P_vac_hi2", dbTable.P_vac_hi2, formTable.P_vac_hi2));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_lo2", dbTable.P_vac_lo2, formTable.P_vac_lo2));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_pos2", dbTable.P_vac_pos2, formTable.P_vac_pos2));

            results.Add(CheckSameFloat(pfname, chname, "P_vac_hi3", dbTable.P_vac_hi3, formTable.P_vac_hi3));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_lo3", dbTable.P_vac_lo3, formTable.P_vac_lo3));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_pos3", dbTable.P_vac_pos3, formTable.P_vac_pos3));

            results.Add(CheckSameFloat(pfname, chname, "P_vac_hi4", dbTable.P_vac_hi4, formTable.P_vac_hi4));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_lo4", dbTable.P_vac_lo4, formTable.P_vac_lo4));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_pos4", dbTable.P_vac_pos4, formTable.P_vac_pos4));

            results.Add(CheckSameFloat(pfname, chname, "P_vac_hi5", dbTable.P_vac_hi5, formTable.P_vac_hi5));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_lo5", dbTable.P_vac_lo5, formTable.P_vac_lo5));
            results.Add(CheckSameFloat(pfname, chname, "P_vac_pos5", dbTable.P_vac_pos5, formTable.P_vac_pos5));


            results.Add(CheckSameFloat(pfname, chname, "auto_startpoint", dbTable.auto_startpoint, formTable.auto_startpoint));
            results.Add(CheckSameFloat(pfname, chname, "auto_endpoint", dbTable.auto_endpoint, formTable.auto_endpoint));

            results.Add(CheckSameShort(pfname, chname, "filter_time", dbTable.filter_time, formTable.filter_time));
            results.Add(CheckSameShort(pfname, chname, "Blow_Delay_time", dbTable.Blow_Delay_time, formTable.Blow_Delay_time));
            results.Add(CheckSameShort(pfname, chname, "Blow_time", dbTable.Blow_time, formTable.Blow_time));
            results.Add(CheckSameShort(pfname, chname, "VAC_time", dbTable.VAC_time, formTable.VAC_time));

            results.Add(CheckSameBool(pfname, chname, "use_ch", dbTable.use_ch, formTable.use_ch));
            results.Add(CheckSameBool(pfname, chname, "select_paiqi", dbTable.select_paiqi, formTable.select_paiqi));
            results.Add(CheckSameBool(pfname, chname, "select_yeya", dbTable.select_yeya, formTable.select_yeya));
            results.Add(CheckSameBool(pfname, chname, "select_jixie", dbTable.select_jixie, formTable.select_jixie));
            results.Add(CheckSameBool(pfname, chname, "use_chuisao_M", dbTable.use_chuisao_M, formTable.use_chuisao_M));
            results.Add(CheckSameBool(pfname, chname, "select_gauging_M", dbTable.select_gauging_M, formTable.select_gauging_M));
            results.Add(CheckSameBool(pfname, chname, "select_auto_S", dbTable.select_auto_S, formTable.select_auto_S));
            results.Add(CheckSameBool(pfname, chname, "use_VAC_time", dbTable.use_VAC_time, formTable.use_VAC_time));

            results.Add(CheckSameBool(pfname, chname, "enable_Opentime", dbTable.enable_Opentime, formTable.enable_Opentime));
            results.Add(CheckSameBool(pfname, chname, "enable_Closetime", dbTable.enable_Closetime, formTable.enable_Closetime));
            results.Add(CheckSameBool(pfname, chname, "enable_Vactime", dbTable.enable_Vactime, formTable.enable_Vactime));
            results.Add(CheckSameBool(pfname, chname, "enable_P_blow", dbTable.enable_P_blow, formTable.enable_P_blow));
            results.Add(CheckSameBool(pfname, chname, "enable_Close_pos", dbTable.enable_Close_pos, formTable.enable_Close_pos));

            results.Add(CheckSameBool(pfname, chname, "enable_P_vac", dbTable.enable_P_vac, formTable.enable_P_vac));
            results.Add(CheckSameBool(pfname, chname, "enable_P_vac2", dbTable.enable_P_vac2, formTable.enable_P_vac2));
            results.Add(CheckSameBool(pfname, chname, "enable_P_vac3", dbTable.enable_P_vac3, formTable.enable_P_vac3));
            results.Add(CheckSameBool(pfname, chname, "enable_P_vac4", dbTable.enable_P_vac4, formTable.enable_P_vac4));
            results.Add(CheckSameBool(pfname, chname, "enable_P_vac5", dbTable.enable_P_vac5, formTable.enable_P_vac5));

            //st_recipe_C1
            results.Add(CheckSameFloat(pfname, chname, "auto_startpoint2", dbTable.auto_startpoint2, formTable.auto_startpoint2));
            results.Add(CheckSameFloat(pfname, chname, "auto_startpoint3", dbTable.auto_startpoint3, formTable.auto_startpoint3));

            results.Add(CheckSameFloat(pfname, chname, "auto_endpoint2", dbTable.auto_endpoint2, formTable.auto_endpoint2));
            results.Add(CheckSameFloat(pfname, chname, "auto_endpoint3", dbTable.auto_endpoint3, formTable.auto_endpoint3));

            results.Add(CheckSameFloat(pfname, chname, "P_blow_hi2", dbTable.P_blow_hi2, formTable.P_blow_hi2));
            results.Add(CheckSameFloat(pfname, chname, "P_blow_lo2", dbTable.P_blow_lo2, formTable.P_blow_lo2));

            results.Add(CheckSameFloat(pfname, chname, "P_blow_hi3", dbTable.P_blow_hi3, formTable.P_blow_hi3));
            results.Add(CheckSameFloat(pfname, chname, "P_blow_lo3", dbTable.P_blow_lo3, formTable.P_blow_lo3));
            results.Add(CheckSameShort(pfname, chname, "VAC_time2", dbTable.VAC_time2, formTable.VAC_time2));
            results.Add(CheckSameShort(pfname, chname, "VAC_time3", dbTable.VAC_time3, formTable.VAC_time3));

            results.Add(CheckSameShort(pfname, chname, "Blow_INR_time", dbTable.Blow_INR_time, formTable.Blow_INR_time));
            results.Add(CheckSameShort(pfname, chname, "Blow_time2", dbTable.Blow_time2, formTable.Blow_time2));
            results.Add(CheckSameShort(pfname, chname, "Blow_time3", dbTable.Blow_time3, formTable.Blow_time3));
            results.Add(CheckSameBool(pfname, chname, "use_blow", dbTable.use_blow, formTable.use_blow));

            results.Add(CheckSameBool(pfname, chname, "enable_P_blow2", dbTable.enable_P_blow2, formTable.enable_P_blow2));
            results.Add(CheckSameBool(pfname, chname, "enable_P_blow3", dbTable.enable_P_blow3, formTable.enable_P_blow3));
            results.Add(CheckSameBool(pfname, chname, "enable_zu1", dbTable.enable_zu1, formTable.enable_zu1));
            results.Add(CheckSameBool(pfname, chname, "enable_zu2", dbTable.enable_zu2, formTable.enable_zu2));
            results.Add(CheckSameBool(pfname, chname, "enable_zu3", dbTable.enable_zu3, formTable.enable_zu3));

            results.Add(CheckSameBool(pfname, chname, "use_VAC_hemu", dbTable.use_VAC_hemu, formTable.use_VAC_hemu));
            results.Add(CheckSameBool(pfname, chname, "use_VAC_stop", dbTable.use_VAC_stop, formTable.use_VAC_stop));
            return results.Where(m=>m!=null).ToList();


        }
        public static void SetPfDetails(PfDetailTable dbTable, PfDetailTable formTable)
        {
            dbTable.Opentime_hi = formTable.Opentime_hi;
            dbTable.Opentime_lo = formTable.Opentime_lo;

            dbTable.Closetime_hi = formTable.Closetime_hi;
            dbTable.Closetime_lo = formTable.Closetime_lo;

            dbTable.Close_pos_hi = formTable.Close_pos_hi;
            dbTable.Close_pos_lo = formTable.Close_pos_lo;

            dbTable.Vactime_hi = formTable.Vactime_hi;
            dbTable.Vactime_lo = formTable.Vactime_lo;

            dbTable.P_blow_hi = formTable.P_blow_hi;
            dbTable.P_blow_lo = formTable.P_blow_lo;

            dbTable.P_vac_hi = formTable.P_vac_hi;
            dbTable.P_vac_lo = formTable.P_vac_lo;
            dbTable.P_vac_pos = formTable.P_vac_pos;

            dbTable.P_vac_hi2 = formTable.P_vac_hi2;
            dbTable.P_vac_lo2 = formTable.P_vac_lo2;
            dbTable.P_vac_pos2 = formTable.P_vac_pos2;

            dbTable.P_vac_hi3 = formTable.P_vac_hi3;
            dbTable.P_vac_lo3 = formTable.P_vac_lo3;
            dbTable.P_vac_pos3 = formTable.P_vac_pos3;

            dbTable.P_vac_hi4 = formTable.P_vac_hi4;
            dbTable.P_vac_lo4 = formTable.P_vac_lo4;
            dbTable.P_vac_pos4 = formTable.P_vac_pos4;

            dbTable.P_vac_hi5 = formTable.P_vac_hi5;
            dbTable.P_vac_lo5 = formTable.P_vac_lo5;
            dbTable.P_vac_pos5 = formTable.P_vac_pos5;


            dbTable.auto_startpoint = formTable.auto_startpoint;
            dbTable.auto_endpoint = formTable.auto_endpoint;

            dbTable.filter_time = formTable.filter_time;
            dbTable.Blow_Delay_time = formTable.Blow_Delay_time;
            dbTable.Blow_time = formTable.Blow_time;
            dbTable.VAC_time = formTable.VAC_time;

            dbTable.use_ch = formTable.use_ch;
            dbTable.select_paiqi = formTable.select_paiqi;
            dbTable.select_yeya = formTable.select_yeya;
            dbTable.select_jixie = formTable.select_jixie;
            dbTable.use_chuisao_M = formTable.use_chuisao_M;
            dbTable.select_gauging_M = formTable.select_gauging_M;
            dbTable.select_auto_S = formTable.select_auto_S;
            dbTable.use_VAC_time = formTable.use_VAC_time;

            dbTable.enable_Opentime = formTable.enable_Opentime; 
            dbTable.enable_Closetime = formTable.enable_Closetime;
            dbTable.enable_Vactime = formTable.enable_Vactime;
            dbTable.enable_P_blow = formTable.enable_P_blow;
            dbTable.enable_Close_pos = formTable.enable_Close_pos;

            dbTable.enable_P_vac = formTable.enable_P_vac;
            dbTable.enable_P_vac2 = formTable.enable_P_vac2;
            dbTable.enable_P_vac3 = formTable.enable_P_vac3;
            dbTable.enable_P_vac4 = formTable.enable_P_vac4;
            dbTable.enable_P_vac5 = formTable.enable_P_vac5;

            //st_recipe_C1
            dbTable.auto_startpoint2 = formTable.auto_startpoint2;
            dbTable.auto_startpoint3 = formTable.auto_startpoint3;

            dbTable.auto_endpoint2 = formTable.auto_endpoint2;
            dbTable.auto_endpoint3 = formTable.auto_endpoint3;

            dbTable.P_blow_hi2 = formTable.P_blow_hi2;
            dbTable.P_blow_lo2 = formTable.P_blow_lo2;

            dbTable.P_blow_hi3 = formTable.P_blow_hi3;
            dbTable.P_blow_lo3 = formTable.P_blow_lo3;
            dbTable.VAC_time2 = formTable.VAC_time2;
            dbTable.VAC_time3 = formTable.VAC_time3;

            dbTable.Blow_INR_time = formTable.Blow_INR_time;
            dbTable.Blow_time2 = formTable.Blow_time2;
            dbTable.Blow_time3 = formTable.Blow_time3;
            dbTable.use_blow = formTable.use_blow;

            dbTable.enable_P_blow2 = formTable.enable_P_blow2;
            dbTable.enable_P_blow3 = formTable.enable_P_blow3;

            dbTable.set_CheckPoint_C = formTable.set_CheckPoint_C;
            dbTable.set_CheckPointHi_C = formTable.set_CheckPointHi_C;
            dbTable.set_CheckPointLo_C = formTable.set_CheckPointLo_C;
            dbTable.use_checkPoint_C = formTable.use_checkPoint_C;

            dbTable.enable_zu1 = formTable.enable_zu1;
            dbTable.enable_zu2 = formTable.enable_zu2;
            dbTable.enable_zu3 = formTable.enable_zu3;

            dbTable.use_VAC_hemu = formTable.use_VAC_hemu;
            dbTable.use_VAC_stop = formTable.use_VAC_stop;

        }
        public static void SavePfDetailTable(int pfid, List<PfDetailTable> list,string pfname)
        {
            try
            {
                var loglist = new List<PfLogTable>();
                using (DataBaseContext db = new DataBaseContext())
                {
                    list.ForEach(l => {
                        if (db.pfDetailTable.Any(m => m.pfid == pfid && m.chnType == l.chnType && m.chnorder == l.chnorder))
                        {
                            var row = db.pfDetailTable.FirstOrDefault(m => m.pfid == pfid && m.chnType == l.chnType && m.chnorder == l.chnorder);
                            row.chnName = $"{l.chnType}{l.chnorder}";
                            row.update_time= DateTime.Now;
                            loglist.AddRange(GetUpdateLogTables(pfname, row.chnName, row, l));
                            SetPfDetails(row, l);
                        }
                        else
                        {
                            var row = new PfDetailTable();
                            row.pfid = pfid;
                            row.chnType = l.chnType;
                            row.chnorder = l.chnorder;
                            row.chnName = $"{l.chnType}{l.chnorder}";
                            row.update_time = DateTime.Now;
                            SetPfDetails(row, l);
                            db.pfDetailTable.Add(row);
                        }
                    });

                    db.SaveChanges();
                }
                if (loglist.Count > 0)
                {
                    PFLogCommon.InsertLogs(loglist);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SavePfDetailTable()::" + ex.Message);
            }
        }

    }
}
