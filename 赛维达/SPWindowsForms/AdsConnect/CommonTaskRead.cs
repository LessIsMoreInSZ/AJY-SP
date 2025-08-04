using SPWindowsForms.DbService;
using SPWindowsForms.ExcelHelper;
using SPWindowsForms.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect
{
    public static class CommonTaskRead
    {
        public static void Readhmi_test()
        {
            GlobalVar.plcData.hmi_test = GlobalVar.commonAdsControl.ReadStTest();
        }
        public static void ReadYSG()
        {
            //   GlobalVar.plcData.YSG = GlobalVar.commonAdsControl.Readst_ysgjz();
        }
        public static void ReadHmiLstime()
        {
            GlobalVar.plcData.hmi_lstime = GlobalVar.commonAdsControl.ReadCommonShort(".hmi_lstime", 10);
        }
        public static void Readhmi_test_end()
        {
            GlobalVar.plcData.hmi_test_end = GlobalVar.commonAdsControl.ReadCommonBool2(".hmi_test_end");
        }
        public static void Writehmi_test_end_False()
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".hmi_test_end", false);
        }
        public static void ReadHMI_Timing()
        {
            GlobalVar.plcData.HMI_Timing_AirFilter_pump = GlobalVar.commonAdsControl.ReadCommonReal(".HMI_Timing_AirFilter_pump", 3);
            GlobalVar.plcData.HMI_Timing_PaperFilter_D = GlobalVar.commonAdsControl.ReadCommonUShort(".HMI_Timing_PaperFilter_D", 10);
            GlobalVar.plcData.HMI_Timing_IronFilter_D = GlobalVar.commonAdsControl.ReadCommonUShort(".HMI_Timing_IronFilter_D", 10);
            GlobalVar.plcData.HMI_Timing_PaperFilter_C = GlobalVar.commonAdsControl.ReadCommonUShort2(".HMI_Timing_PaperFilter_C");
            GlobalVar.plcData.HMI_Timing_IronFilter_C = GlobalVar.commonAdsControl.ReadCommonUShort2(".HMI_Timing_IronFilter_C");
            GlobalVar.plcData.HMI_Timing_PaperFilter_E = GlobalVar.commonAdsControl.ReadCommonUShort2(".HMI_Timing_PaperFilter_E");
            GlobalVar.plcData.HMI_Timing_IronFilter_E = GlobalVar.commonAdsControl.ReadCommonUShort2(".HMI_Timing_IronFilter_E");
        }
        #region Top Status
        public static void ReadTopStatus()
        {
            GlobalVar.plcData.hmi_St_Layer = GlobalVar.commonAdsControl.Readhmi_st_layer();
            GlobalVar.plcData.hmi_butt_save = GlobalVar.commonAdsControl.ReadCommonBool2(GlobalVar.commonAdsControl.hmi_butt_save_name);
            GlobalVar.plcData.enable_ManAction = GlobalVar.commonAdsControl.ReadCommonBool2(GlobalVar.commonAdsControl.enable_ManAction_name);
            GlobalVar.plcData.hmi_butt_ManPUMP = GlobalVar.commonAdsControl.ReadCommonBool(GlobalVar.commonAdsControl.hmi_butt_ManPUMP_name, 3);
            GlobalVar.plcData.pump_timing = GlobalVar.commonAdsControl.ReadCommonReal(GlobalVar.commonAdsControl.pump_timing_name, 3);
            GlobalVar.plcData.hmi_feedback_kaidu = GlobalVar.commonAdsControl.ReadCommonReal2(GlobalVar.commonAdsControl.hmi_feedback_kaidu_name);
        }
        public static void WriteEnable_ManAction_name(bool flag)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(GlobalVar.commonAdsControl.enable_ManAction_name, flag);
        }

        public static void ReadZhuji()
        {
            GlobalVar.plcData.DCM_usetest = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_usetest");
            GlobalVar.plcData.DCM_buttAutoTest = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttAutoTest");
            GlobalVar.plcData.DCM_buttSemiTest = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttSemiTest");
            GlobalVar.plcData.DCM_buttMantest = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttMantest");
            GlobalVar.plcData.DCM_buttTestStart = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttTestStart");
            GlobalVar.plcData.DCM_buttClose_HotMold = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttClose_HotMold");
            GlobalVar.plcData.DCM_buttClose = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttClose");
            GlobalVar.plcData.DCM_buttOpen = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttOpen");
            GlobalVar.plcData.DCM_buttZero = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttZero");
            GlobalVar.plcData.DCM_buttPurge = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttPurge");
            GlobalVar.plcData.DCM_buttHotMold = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttHotMold");
            GlobalVar.plcData.DCM_buttPos_add = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttPos_add");
            GlobalVar.plcData.DCM_buttPos_sub = GlobalVar.commonAdsControl.ReadCommonBool2(".DCM_buttPos_sub");
        }
        public static void WirteZhujiAllfalse()
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_usetest", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttAutoTest", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttSemiTest", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttMantest", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttTestStart", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttClose_HotMold", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttClose", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttOpen", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttZero", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPurge", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttHotMold", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_add", false);
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_sub", false);
        }
        #endregion

        #region Alarms

        public static bool RefreshAlarms()
        {
            ReadAlarms();
            var alarmStatus = GetAlarmCodeStatus();
            var resultFlag = false;
            if (alarmStatus.OkCodes.Count > 0 && GlobalVar.NowAlarms.Count != 0 && GlobalVar.NowAlarms.Exists(m => alarmStatus.OkCodes.Contains(m.alarmCode)))
            {
                var ids = GlobalVar.NowAlarms.Where(m => alarmStatus.OkCodes.Contains(m.alarmCode)).Select(m => m.id).ToList();
                GlobalVar.NowAlarms = GlobalVar.NowAlarms.Where(m => !alarmStatus.OkCodes.Contains(m.alarmCode)).ToList();
                AlarmLogCommon.OffAlarms(ids);
                resultFlag = true;
            }
            var noAlarmCodes = alarmStatus.NgCodes.Except(GlobalVar.NowAlarms.Select(m => m.alarmCode).ToList()).ToList();

            if (noAlarmCodes.Count > 0)
            {
                var list = AlarmLogCommon.NewAlarmLogTables(noAlarmCodes);
                GlobalVar.NowAlarms.AddRange(list);
                resultFlag = true;
            }
            return resultFlag;

        }
        public static void ReadPfRealTime()
        {
            GlobalVar.plcData.hmi_alarmout_D = GlobalVar.commonAdsControl.ReadSt_alarm_D();
            GlobalVar.plcData.hmi_alarmout_C = GlobalVar.commonAdsControl.ReadSt_alarm_C();
            GlobalVar.plcData.hmi_alarmout_E = GlobalVar.commonAdsControl.ReadSt_alarm_E();
        }


        /// <summary>
        /// 读取Execl表中的报警
        /// </summary>
        public static void ReadAlarms()
        {
            GlobalVar.AlarmSheetNames.ForEach(a =>
            {
                bool[] _alarms;
                var _count = GlobalVar.PlcAlarms.singleReports[a].Count;
                if (a == "hmi_system") _count = _count - 1;
                _alarms = GlobalVar.commonAdsControl.ReadCommonBool("." + a, _count);

                if (!GlobalVar.plcData.st_err_dic.ContainsKey(a))
                    GlobalVar.plcData.st_err_dic.Add(a, _alarms);
                else
                    GlobalVar.plcData.st_err_dic[a] = _alarms;
            });
            //GlobalVar.plcData.st_err_systems = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_system", GlobalVar.PlcAlarms.st_err_systems.Count - 1);
            //GlobalVar.plcData.st_hmi_alarmouts = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_alarm", GlobalVar.PlcAlarms.st_hmi_alarmouts.Count);
            //GlobalVar.plcData.st_diags = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_diag", GlobalVar.PlcAlarms.st_diags.Count);
            //GlobalVar.plcData.st_upkeeps = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_upkeep", GlobalVar.PlcAlarms.st_upkeeps.Count);
            //GlobalVar.plcData.st_HYD_errs = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_HYD", GlobalVar.PlcAlarms.st_HYD_errs.Count);
            //GlobalVar.plcData.st_alarms = GlobalVar.commonAdsControl.ReadCommonBool(".hmi_err", GlobalVar.PlcAlarms.st_alarms.Count);
        }
        public static void AddAlarmCodeStatus(AlarmCodeStatus alarmCodeStatus, List<ErrorReportModel> errorReportModels, bool[] bools, int buffer = 0)
        {

            for (int i = 0; i < bools.Length; i++)
            {
                var _first = errorReportModels.FirstOrDefault(m => m.Order == i + 1 + buffer);
                if (_first == null) throw new Exception("Error Code Not Found");
                if (bools[i])
                    alarmCodeStatus.NgCodes.Add(_first.AlarmCode);
                else
                    alarmCodeStatus.OkCodes.Add(_first.AlarmCode);
            }
        }
        public static AlarmCodeStatus GetAlarmCodeStatus()
        {
            var result = new AlarmCodeStatus();
            GlobalVar.AlarmSheetNames.ForEach(a =>
            {
                var _count = 0;
                if (a == "hmi_system") _count = 1;
                AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.singleReports[a], GlobalVar.plcData.st_err_dic[a], _count);
            });
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_err_systems, GlobalVar.plcData.st_err_systems, 1);
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_diags, GlobalVar.plcData.st_diags);
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_hmi_alarmouts, GlobalVar.plcData.st_hmi_alarmouts);
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_upkeeps, GlobalVar.plcData.st_upkeeps);
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_HYD_errs, GlobalVar.plcData.st_HYD_errs);
            //AddAlarmCodeStatus(result, GlobalVar.PlcAlarms.st_alarms, GlobalVar.plcData.st_alarms);
            return result;
        }
        public static string GetAlarmInfoByCode(string code)
        {
            if (String.IsNullOrEmpty(code)) return "";
            var result = GlobalVar.PlcAlarms.allAlarmReports.FirstOrDefault(m => m.AlarmCode == code);
            if (result == null)
                return LanguageSet.SetL("AlarmTable", "AlarmNotFound");
            else
                return result.AlarmInfo;
        }
        public static string GetAlarmLevelByCode(string code)
        {
            if (String.IsNullOrEmpty(code)) return "";
            var result = GlobalVar.PlcAlarms.allAlarmReports.FirstOrDefault(m => m.AlarmCode == code);
            if (result == null)
                return LanguageSet.SetL("AlarmTable", "AlarmNotFound");
            else
                return result.AlarmLevel;
        }
        #endregion
        #region 手动 & 通道
        public static void WriteTongdaoBtnBool(int btn, bool value, string chnType, int chnOrder)
        {
            int totalStructLength = 0;
            string varName = "";
            switch (chnType)
            {
                case "D":
                    totalStructLength = 16 * 1 + 4 * 4 + 9;
                    varName = GlobalVar.commonAdsControl.hmi_D_name;
                    break;
                case "C":
                    totalStructLength = 10 * 1 + 2 * 4 + 8;
                    varName = GlobalVar.commonAdsControl.hmi_C_name;
                    break;
                case "E":
                    totalStructLength = 2 * 1 + 4 + 1;
                    varName = GlobalVar.commonAdsControl.hmi_E_name;
                    break;
                default:
                    break;
            }
            GlobalVar.commonAdsControl.WriteCommonBool(varName, btn, value, chnOrder - 1, totalStructLength);
        }
        public static void WriteManualBtnBool(int testBtn, bool value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.hmi_test_name;
            switch (testBtn)
            {
                case (int)SetManualButtonOrder.测试开始:
                    beforeLength = 1;
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    break;
                case (int)SetManualButtonOrder.测试停止:
                    beforeLength = 2;
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    break;
                case (int)SetManualButtonOrder.管道测试:
                    beforeLength = 3;
                    if (value)
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore2(varName, beforeLength, true, false);
                    else
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    break;
                case (int)SetManualButtonOrder.模腔测试:

                    if (value)
                    {
                        beforeLength = 3;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore2(varName, beforeLength, false, true);
                    }
                    else
                    {
                        beforeLength = 4;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void ReadhmiDCETime()
        {
            GlobalVar.plcData.hmi_D = GlobalVar.commonAdsControl.Readst_hmiTB_D();
            GlobalVar.plcData.hmi_C = GlobalVar.commonAdsControl.Readst_hmiTB_C();
            GlobalVar.plcData.hmi_E = GlobalVar.commonAdsControl.Readst_hmiTB_E();
        }
        public static void WriteManualShort(int num, short value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.hmi_test_name;
            switch (num)
            {
                case (int)SetManualButtonOrder.测试时间:
                    beforeLength = 5;

                    break;
                case (int)SetManualButtonOrder.保压时间:
                    beforeLength = 7;
                    break;

                default:
                    break;
            }
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(varName, beforeLength, value);
        }
        #endregion
        #region 自动
        public static void ReadAuto()
        {
            GlobalVar.plcData.hmi_auto = GlobalVar.commonAdsControl.Readst_hmiauto();
        }
        public static void WriteAutoBtnBool(int testBtn, bool value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.hmi_auto_name;
            switch (testBtn)
            {
                case (int)SetAutoButtonOrder.自动启动:
                    beforeLength = 8;
                    break;
                case (int)SetAutoButtonOrder.退出循环:
                    beforeLength = 9;
                    break;
                case (int)SetAutoButtonOrder.热模真空:
                    beforeLength = 10;
                    break;

                default:
                    break;
            }
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
        }
        #endregion
        #region 真空源
        public static void ReadUPS()
        {
            GlobalVar.plcData.UPS = GlobalVar.commonAdsControl.Readst_ups();
        }
        public static void ReadVacsourc()
        {
            GlobalVar.plcData.hmi_Vacsource = GlobalVar.commonAdsControl.Readst_VacSource();
        }
        public static void WriteVacBtnBool(int testBtn, bool value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.hmi_Vacsource_name;
            switch (testBtn)
            {
                case (int)SetVacButtonOrder.自动启动:
                    beforeLength = 0;
                    break;
                case (int)SetVacButtonOrder.自动停止:
                    beforeLength = 1;
                    break;
                default:
                    break;
            }
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
        }
        public static void Write6001Mode(int mode, bool value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.st_ups_name;
            switch (mode)
            {
                case (int)Set6001Mode.Mode1:

                    beforeLength = 12;
                    if (value)
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore3(varName, beforeLength, value, false, false);
                    else
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    break;
                case (int)Set6001Mode.Mode2:
                    if (value)
                    {
                        beforeLength = 12;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore3(varName, beforeLength, false, value, false);
                    }
                    else
                    {
                        beforeLength = 13;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    }
                    break;
                case (int)Set6001Mode.Mode3:
                    if (value)
                    {
                        beforeLength = 12;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore3(varName, beforeLength, false, false, value);
                    }
                    else
                    {
                        beforeLength = 14;
                        GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void WriteVacSetShort(int num, short value)
        {
            int beforeLength = 0;
            string varName = GlobalVar.commonAdsControl.st_ups_name;
            switch (num)
            {
                case (int)VacSetShort.VAC_D:
                    beforeLength = 16;

                    break;
                case (int)VacSetShort.VAC_C:
                    beforeLength = 18;

                    break;
                case (int)VacSetShort.VAC_E:
                    beforeLength = 20;

                    break;
                case (int)VacSetShort.PUMP_C:
                    beforeLength = 22;

                    break;
                case (int)VacSetShort.PUMP_E:
                    beforeLength = 24;

                    break;

                default:
                    break;
            }
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(varName, beforeLength, value);
        }


        #endregion
        public static void ReadIO()
        {
            GlobalVar.plcData.IO = GlobalVar.commonAdsControl.Readst_hmi_IO();
        }
        public static void ReadKsystem()
        {
            GlobalVar.plcData.hmi_K = GlobalVar.commonAdsControl.Readst_Ksystem();
        }
        public static void ReadUPS_PG()
        {
            GlobalVar.plcData.UPS_PG = GlobalVar.commonAdsControl.ReadStPg();
        }
    }
}
