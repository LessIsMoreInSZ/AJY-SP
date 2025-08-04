using SPWindowsForms.BasicTool;
using SPWindowsForms.ExcelHelper;
using SPWindowsForms.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public static class Work
    {
        public static FormMain frm_main;
        private static string iniSystemName= Environment.CurrentDirectory.ToString() + "\\UserData\\system.ini";
        public static LogHelper _logHelper;
        public static LogHelper _logDbHelper;
        public static void MyAppStart()
        {
            try
            {
                StartIni();
                StartExcelRead();
                _logHelper = new LogHelper(Environment.CurrentDirectory.ToString() + "\\Logs");
                _logDbHelper = new LogHelper(Environment.CurrentDirectory.ToString() + "\\DbLogs");
                //  throw new Exception("hehe");
            }
            catch(Exception ex)
            {
               
                ShowErrorMessage(ex.Message);
            }
        }

        public static void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _logHelper.WriteLog(errorMessage);
        }
        public static void StartIni()
        {
            GlobalVar.ini.iniSystem = new INIFile(iniSystemName);
            GetSystemSetting();
            var _language = GlobalVar.ini.iniSystem.ReadString("DATA", "language", "");
            GlobalVar.ini.iniLanguage = new INIFile(GetLanguageName(_language));
            GlobalVar.ini.iniLanguage.allData = GlobalVar.ini.iniLanguage.TraverseIni();
        }
        public static void StartExcelRead()
        {
           
            var _language = GlobalVar.ini.iniSystem.ReadString("DATA", "language", "");
           var excelReader= new ExcelReaderClass(GetAlarmExcelLanguageName(_language));
            GlobalVar.AlarmSheetNames = excelReader.GetSheetNames();
            GlobalVar.AlarmSheetNames.ForEach(a => {
                var singleAlarm = excelReader.GetErrorReportModels(a);
                GlobalVar.PlcAlarms.singleReports.Add(a, singleAlarm);
                GlobalVar.PlcAlarms.allAlarmReports.AddRange(singleAlarm);
            });

            //GlobalVar.PlcAlarms.st_err_systems = excelReader.GetErrorReportModels("st_err_system");
            //GlobalVar.PlcAlarms.st_hmi_alarmouts = excelReader.GetErrorReportModels("st_hmi_alarmout");
            //GlobalVar.PlcAlarms.st_diags = excelReader.GetErrorReportModels("st_diag");
            //GlobalVar.PlcAlarms.st_upkeeps = excelReader.GetErrorReportModels("st_upkeep");
            //GlobalVar.PlcAlarms.st_HYD_errs = excelReader.GetErrorReportModels("st_HYD_err");
            //GlobalVar.PlcAlarms.st_alarms = excelReader.GetErrorReportModels("st_alarm");
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_err_systems);
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_hmi_alarmouts);
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_diags);
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_upkeeps);
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_HYD_errs);
            //GlobalVar.PlcAlarms.allAlarmReports.AddRange(GlobalVar.PlcAlarms.st_alarms);
        }
        public static void GetSystemSetting()
        {
            try
            {
                GlobalVar.systemSetting.d_chn_count = GlobalVar.ini.iniSystem.ReadInt("DATA", "d_chn_count", 8);
                GlobalVar.systemSetting.c_chn_count = GlobalVar.ini.iniSystem.ReadInt("DATA", "c_chn_count", 1);
                GlobalVar.systemSetting.e_chn_count = GlobalVar.ini.iniSystem.ReadInt("DATA", "e_chn_count", 1);
                GlobalVar.systemSetting.use_pf_id = GlobalVar.ini.iniSystem.ReadInt("DATA", "use_pf_id", 0);
                GlobalVar.systemSetting.netID = GlobalVar.ini.iniSystem.ReadString("DATA", "netID", "");
                GlobalVar.systemSetting.port = GlobalVar.ini.iniSystem.ReadInt("DATA", "port", 801);
                GlobalVar.systemSetting.sample_pointcount = GlobalVar.ini.iniSystem.ReadInt("DATA", "sample_pointcount", 2000);
                GlobalVar.systemSetting.sample_chncount = GlobalVar.ini.iniSystem.ReadInt("DATA", "sample_chncount", 10);
                GlobalVar.systemSetting.pg_min = GlobalVar.ini.iniSystem.ReadDouble("DATA", "pg_min", 0);
                GlobalVar.systemSetting.pg_max = GlobalVar.ini.iniSystem.ReadDouble("DATA", "pg_max", 1100);
                GlobalVar.systemSetting.vel_min = GlobalVar.ini.iniSystem.ReadDouble("DATA", "vel_min", 0);
                GlobalVar.systemSetting.vel_max = GlobalVar.ini.iniSystem.ReadDouble("DATA", "vel_max", 12000);
                GlobalVar.systemSetting.machine_config= GlobalVar.ini.iniSystem.ReadString("DATA", "machine_config", "");
                GlobalVar.systemSetting.tiaojiefa_kaidu_flag = GlobalVar.ini.iniSystem.ReadInt("DATA", "tiaojiefa_kaidu_flag", -1);
                if (GlobalVar.systemSetting.tiaojiefa_kaidu_flag == -1)
                {
                    GlobalVar.ini.iniSystem.WriteString("DATA", "tiaojiefa_kaidu_flag", "0");
                    GlobalVar.systemSetting.tiaojiefa_kaidu_flag = GlobalVar.ini.iniSystem.ReadInt("DATA", "tiaojiefa_kaidu_flag", 0);
                }
                var _language_options= GlobalVar.ini.iniSystem.ReadString("DATA", "language_options", "");
                GlobalVar.systemSetting.language_options = _language_options.Split(',').ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("GetSystemSetting::"+ex.Message);
            }
        }
        public static void SetSystemSettingPfId()
        {
            try
            {
               GlobalVar.ini.iniSystem.WriteString("DATA", "use_pf_id", GlobalVar.systemSetting.use_pf_id.ToString());
                
            }
            catch (Exception ex)
            {

                throw new Exception("SetSystemSettingPfId::" + ex.Message);
            }
        }
        public static string GetLanguageName(string name)
        {   
            return $"{Environment.CurrentDirectory}\\UserData\\language_{name.ToLower()}.ini"; 
        }

        public static string GetAlarmExcelLanguageName(string name)
        {
            return $"{Environment.CurrentDirectory}\\UserData\\alarm_{name.ToLower()}.xlsx";
        }
        public static string GetIOExcelLanguageName(string name)
        {
            return $"{Environment.CurrentDirectory}\\UserData\\IO_{name.ToLower()}.xlsx";
        }

        #region Numbers
        public static bool CheckInt(string str)
        {
            int number;
            bool isInteger = int.TryParse(str, out number);
            return isInteger;
        }
        public static bool CheckFloat(string str)
        {
            float number;
            bool isfloat = float.TryParse(str, out number);
            return isfloat;
        }
        public static int CheckAndGetInt(string str)
        {
            int number;
            bool isInteger = int.TryParse(str, out number);
            if (isInteger)
                return number;
            else
                return 0;
        }
        public static float CheckAndGetFloat(string str)
        {
            float number;
            bool isfloat = float.TryParse(str, out number);
            if (isfloat)
                return number;
            else
                return 0;
        }
        #endregion
    }
}
