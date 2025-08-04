using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect.TwincateStruct;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{
    public class PfToSampleModel
    {
        public List<bool> select_paiqis { set; get; } = new List<bool>();
        public List<bool> select_yeyas { set; get; } = new List<bool>();
        public List<bool> select_jixies { set; get; } = new List<bool>();
        public List<bool> duoduanjiance1s { set; get; } = new List<bool>();
        public List<bool> duoduanjiance2s { set; get; } = new List<bool>();
        public List<bool> duoduanjiance3s { set; get; } = new List<bool>();
        public List<bool> duoduanjiance4s { set; get; } = new List<bool>();
        public List<bool> duoduanjiance5s { set; get; } = new List<bool>();
    }
    public static class SampleDBCommon
    {
        private static void SetPfListByType(List<PfDetailTable> pfDetails, PfToSampleModel pfToSampleModel, string type, int count)
        {
            if (count == 0) return;
            for (int i = 1; i <= count; i++)
            {
                var pfrow = pfDetails.FirstOrDefault(m => m.chnType == type && m.chnorder == i);
                SetPfList(pfrow, pfToSampleModel);
            }
        }
        private static void SetPfList(PfDetailTable pfrow, PfToSampleModel pfToSampleModel)
        {
            if (pfrow != null)
            {
                pfToSampleModel.select_paiqis.Add(pfrow.select_paiqi ?? false);
                pfToSampleModel.select_yeyas.Add(pfrow.select_yeya ?? false);
                pfToSampleModel.select_jixies.Add(pfrow.select_jixie ?? false);
                pfToSampleModel.duoduanjiance1s.Add(pfrow.enable_P_vac ?? false);
                pfToSampleModel.duoduanjiance2s.Add(pfrow.enable_P_vac2 ?? false);
                pfToSampleModel.duoduanjiance3s.Add(pfrow.enable_P_vac3 ?? false);
                pfToSampleModel.duoduanjiance4s.Add(pfrow.enable_P_vac4 ?? false);
                pfToSampleModel.duoduanjiance5s.Add(pfrow.enable_P_vac5 ?? false);
            }
            else
            {
                pfToSampleModel.select_paiqis.Add(false);
                pfToSampleModel.select_yeyas.Add(false);
                pfToSampleModel.select_jixies.Add(false);
                pfToSampleModel.duoduanjiance1s.Add(false);
                pfToSampleModel.duoduanjiance2s.Add(false);
                pfToSampleModel.duoduanjiance3s.Add(false);
                pfToSampleModel.duoduanjiance4s.Add(false);
                pfToSampleModel.duoduanjiance5s.Add(false);
            }
        }
        public static SampleTable NewSampleTable(st_sample st, List<PfDetailTable> pfDetails)
        {
            SampleTable newRow = null;
            int retryCount = 0;
            const int maxRetries = 2; // 设置最大重试次数为2

            while (retryCount <= maxRetries)
            {
                try
                {
                    using (DataBaseContext db = new DataBaseContext())
                    {
                        var starttime = DateTime.Now; Work._logDbHelper.WriteLog($"Start New Sample DCM_id:{st.DCM_id},retryCount:{retryCount}");
                        newRow = new SampleTable();
                        newRow.use_pf_id = GlobalVar.systemSetting.use_pf_id;
                        newRow.pg1 = string.Join(",", st.pg1);
                        newRow.pg2 = string.Join(",", st.pg2);
                        newRow.pg3 = string.Join(",", st.pg3);
                        newRow.pg4 = string.Join(",", st.pg4);
                        newRow.pg5 = string.Join(",", st.pg5);
                        newRow.pg6 = string.Join(",", st.pg6);
                        newRow.pg7 = string.Join(",", st.pg7);
                        newRow.pg8 = string.Join(",", st.pg8);
                        newRow.pg9 = string.Join(",", st.pg9);
                        newRow.pg10 = string.Join(",", st.pg10);
                        newRow.pg11 = string.Join(",", st.pg11);
                        newRow.vel = string.Join(",", st.vel);
                        newRow.pos = string.Join(",", st.pos);
                        newRow.startpoint = string.Join(",", st.startpoint);
                        newRow.endpoint = string.Join(",", st.endpoint);
                        newRow.chuisaoyali = string.Join(",", st.chuisaoyali);
                        newRow.duoduanjiance1 = string.Join(",", st.duoduanjiance1);
                        newRow.duoduanjiance2 = string.Join(",", st.duoduanjiance2);
                        newRow.duoduanjiance3 = string.Join(",", st.duoduanjiance3);
                        newRow.duoduanjiance4 = string.Join(",", st.duoduanjiance4);
                        newRow.duoduanjiance5 = string.Join(",", st.duoduanjiance5);
                        newRow.fanying = string.Join(",", st.fanying);
                        newRow.tongfeng = string.Join(",", st.tongfeng);
                        newRow.fengbi = string.Join(",", st.fengbi);
                        newRow.start_yeya_PG = string.Join(",", st.start_yeya_PG);
                        newRow.stop_yeya_PG = string.Join(",", st.stop_yeya_PG);
                        newRow.start_PG_tank = string.Join(",", st.start_PG_tank);
                        newRow.end_PG_tank = string.Join(",", st.end_PG_tank);
                        newRow.CA_PG = st.CA_PG;
                        newRow.zongchouqi = st.zongchouqi;
                        newRow.usechanle = string.Join(",", st.usechanle);
                        newRow.pfname = st.pfname;
                        newRow.DCM_id = st.DCM_id;
                        newRow.product_id = DateTime.Now.ToString("yyyyMMddHHmmss");
                        if (pfDetails != null)
                        {
                            var pfToSampleModel = new PfToSampleModel();
                            SetPfListByType(pfDetails, pfToSampleModel, "D", GlobalVar.systemSetting.d_chn_count);
                            SetPfListByType(pfDetails, pfToSampleModel, "C", GlobalVar.systemSetting.c_chn_count);
                            SetPfListByType(pfDetails, pfToSampleModel, "E", GlobalVar.systemSetting.e_chn_count);
                            newRow.select_paiqi = string.Join(",", pfToSampleModel.select_paiqis);
                            newRow.select_yeya = string.Join(",", pfToSampleModel.select_yeyas);
                            newRow.select_jixie = string.Join(",", pfToSampleModel.select_jixies);
                            newRow.duoduanjiance1_hmienable = string.Join(",", pfToSampleModel.duoduanjiance1s);
                            newRow.duoduanjiance2_hmienable = string.Join(",", pfToSampleModel.duoduanjiance2s);
                            newRow.duoduanjiance3_hmienable = string.Join(",", pfToSampleModel.duoduanjiance3s);
                            newRow.duoduanjiance4_hmienable = string.Join(",", pfToSampleModel.duoduanjiance4s);
                            newRow.duoduanjiance5_hmienable = string.Join(",", pfToSampleModel.duoduanjiance5s);
                        }
                        db.Database.CommandTimeout = 10;
                        db.sampleTable.Add(newRow);
                        db.SaveChanges();
                        Work._logDbHelper.WriteLog($"Finish New Sample DCM_id:{st.DCM_id},Spend time:{DateTime.Now.Subtract(starttime).TotalMilliseconds/1000}s");
                        //    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:OK\r\n");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    string innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                    innerExceptionMessage = ex.Message + " | " + innerExceptionMessage;
                    Work._logDbHelper.WriteLog($"Error, DCM_id:{st.DCM_id},retryCount:{retryCount},errormessage:{innerExceptionMessage}");
                    retryCount++; // 增加重试计数

                    if (retryCount > maxRetries)
                    {

                        throw new Exception("NewSampleTable()::" + innerExceptionMessage);
                    }
                    // 可选：在重试前等待一段时间（例如500毫秒）
                    Thread.Sleep(500);
                }
            }
            return newRow;
        }
        public static Task<List<SearchSampleModel>> SearchSampleTable(DateTime? startTime, DateTime? stopTime, string pf, string ys)
        {
            try
            {
                if (startTime == null) startTime = new DateTime(2000, 1, 1, 0, 0, 0);
                if (stopTime == null) stopTime = new DateTime(2200, 1, 1, 0, 0, 0);

                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Database.CommandTimeout = 60;
                    //var result = db.sampleTable.Where(m => m.update_time >= startTime && m.update_time <= stopTime && m.pfname.Contains(pf) && m.DCM_id.Contains(ys)).Select(x => new SearchSampleModel { id = x.id, product_id = x.product_id }).ToList();
                    if (!String.IsNullOrEmpty(ys))
                        return db.sampleTable.AsNoTracking().Where(m => m.DCM_id != null && ys.ToUpper() == m.DCM_id.ToUpper()).OrderByDescending(m => m.update_time).Select(x => new SearchSampleModel { id = x.id, product_id = x.product_id }).ToListAsync();
                    else if (!String.IsNullOrEmpty(pf))
                        return db.sampleTable.AsNoTracking().Where(m => m.update_time >= startTime && m.update_time <= stopTime && m.pfname != null && pf.ToUpper() == m.pfname.ToUpper()).OrderByDescending(m => m.update_time).Select(x => new SearchSampleModel { id = x.id, product_id = x.product_id }).ToListAsync();
                    else
                        return db.sampleTable.AsNoTracking().Where(m => m.update_time >= startTime && m.update_time <= stopTime).OrderByDescending(m => m.update_time).Select(x => new SearchSampleModel { id = x.id, product_id = x.product_id }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SearchSampleTable()::" + ex.Message);
            }
        }
        public static Task<List<DownloadSampleModel>> GetSampleTableDownload(DateTime? startTime, DateTime? stopTime)
        {
            try
            {
                if (startTime == null) startTime = new DateTime(2000, 1, 1, 0, 0, 0);
                if (stopTime == null) stopTime = new DateTime(2200, 1, 1, 0, 0, 0);

                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Database.CommandTimeout = 120;
                    var list = db.sampleTable.AsNoTracking().OrderByDescending(m => m.update_time).Where(m => m.update_time >= startTime && m.update_time <= stopTime)
                        .Select(m => new DownloadSampleModel
                        {
                            startpoint = m.startpoint,
                            endpoint = m.endpoint,
                            chuisaoyali = m.chuisaoyali,
                            duoduanjiance1 = m.duoduanjiance1,
                            duoduanjiance2 = m.duoduanjiance2,
                            duoduanjiance3 = m.duoduanjiance3,
                            duoduanjiance4 = m.duoduanjiance4,
                            duoduanjiance5 = m.duoduanjiance5,
                            fanying = m.fanying,
                            tongfeng = m.tongfeng,
                            fengbi = m.fengbi,
                            start_yeya_PG = m.start_yeya_PG,
                            stop_yeya_PG = m.stop_yeya_PG,
                            start_PG_tank = m.start_PG_tank,
                            end_PG_tank = m.end_PG_tank,
                            CA_PG = m.CA_PG,
                            zongchouqi = m.zongchouqi,
                            usechanle = m.usechanle,
                            pfname = m.pfname,
                            DCM_id = m.DCM_id,
                            update_time = m.update_time

                        })
                      .ToListAsync();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SearchSampleTable()::" + ex.Message);
            }
        }
        public static SearchSamples GetSampleTableById(int id, bool gettenflag = true)
        {
            try
            {
                var model = new SearchSamples();
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Database.CommandTimeout = 10;
                    var result = db.sampleTable.AsNoTracking().FirstOrDefault(m => m.id == id);
                    model.sample = result;
                    if (result != null && gettenflag)
                    {
                        model.last_samples = db.sampleTable.AsNoTracking()
            .Where(e => e.id <= result.id) // 确保ID小于目标ID  
            .OrderByDescending(e => e.id) // 按ID降序排列（这样最近的数据会排在前面）  
            .Take(10) // 取前10条数据  
            .ToList(); // 执行查询并返回结果列表  

                    }
                    return model;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetSampleTableById()::" + ex.Message);
            }
        }
        public static List<SampleTable> GetAllLastSamples()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Database.CommandTimeout = 30;
                    var results = db.sampleTable.AsNoTracking()
            .OrderByDescending(e => e.id) // 按ID降序排列（这样最近的数据会排在前面）  
            .Take(10) // 取前10条数据  
            .ToList(); // 执行查询并返回结果列表  

                    return results;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetSampleTableById()::" + ex.Message);
            }
        }

    }
}
