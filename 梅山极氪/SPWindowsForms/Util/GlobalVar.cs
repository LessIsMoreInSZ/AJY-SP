using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect;
using SPWindowsForms.AdsConnect.TwincateStruct;
using SPWindowsForms.AppModels;
using SPWindowsForms.ExcelHelper;
using SPWindowsForms.SwitchForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms
{
    public static class GlobalVar
    {
        public static bool IsAuto = false;// 20250220 增加是否自动的flag
        public static int NowUiDisplay { set; get; } = 0;
        public static int BottomUiDisplay { set; get; } = 0;
        public static CommonIniFileModel ini = new CommonIniFileModel();
        public static SystemSettingModel systemSetting = new SystemSettingModel();
        public static string UserNumber { set; get; } = "";
        public static int UserRole { set; get; } = 0;
        public static DateTime loginTime { set; get; } = DateTime.Now;
        public static Dictionary<int, string> UserRoleDic { set; get; }
        public static CommonAdsControl commonAdsControl { set; get; }
        public static AllPLCData plcData { set; get; } = new AllPLCData();
        public static ConvertSampleTable nowSample { set; get; } = null;
        public static PfMainTable usePf = null;
        public static List<PfDetailTable> usePfDetails = null;
        public static CutomerColors cutomerColors = new CutomerColors();
        public static PfPageConfigModel D_PfPageConfigModel = new PfPageConfigModel();
        public static PfPageConfigModel C_PfPageConfigModel = new PfPageConfigModel();
        public static PfPageConfigModel E_PfPageConfigModel = new PfPageConfigModel();
        public static Alarms PlcAlarms = new Alarms();
        public static List<AlarmLogTable> NowAlarms = new List<AlarmLogTable>();
        public static List<string> AlarmSheetNames = new List<string>();

    }
    public class Alarms
    {
        public Dictionary<string, List<ErrorReportModel>> singleReports { set; get; } = new Dictionary<string, List<ErrorReportModel>>();
        //public List<ErrorReportModel> st_err_systems { set; get; }
        //public List<ErrorReportModel> st_hmi_alarmouts { set; get; }
        //public List<ErrorReportModel> st_diags { set; get; }
        //public List<ErrorReportModel> st_upkeeps { set; get; }
        //public List<ErrorReportModel> st_HYD_errs { set; get; }
        //public List<ErrorReportModel> st_alarms { set; get; }
        public List<ErrorReportModel> allAlarmReports { set; get; } = new List<ErrorReportModel>();
    }
    public class CutomerColors
    {
        public Color RedColor = Color.Red;
        public Color ActiveColor = Color.Lime;
        public Color NotActiveColor = Color.Silver;
    }
    public class NowAlarmChecks
    {
        public List<string> OkCodes { set; get; } = new List<string>();
        public List<string> NgCodes { set; get; } = new List<string>();
    }
    public class AllPLCData
    {
        public st_sample sample { set; get; } = new st_sample();
        public hmi_st_layer hmi_St_Layer { set; get; } = new hmi_st_layer();
        public bool hmi_butt_save { set; get; }
        public bool enable_ManAction { set; get; }
        public bool[] hmi_butt_ManPUMP { set; get; }
        public float[] pump_timing { set; get; }
        public float hmi_feedback_kaidu { set; get; }
        
        public Dictionary<string, bool[]> st_err_dic { set; get; } = new Dictionary<string, bool[]>();
        //public bool[] st_err_systems { set; get; }
        //    public bool[] st_hmi_alarmouts { set; get; }
        //    public bool[] st_diags { set; get; }
        //    public bool[] st_upkeeps { set; get; }
        //    public bool[] st_HYD_errs { set; get; }
        //    public bool[] st_alarms { set; get; }
        public st_alarm_D[] hmi_alarmout_D { set; get; }
        public st_alarm_C[] hmi_alarmout_C { set; get; }
        public st_alarm_E[] hmi_alarmout_E { set; get; }
        public st_hmiTB_D[] hmi_D { set; get; }
        public st_hmiTB_C[] hmi_C { set; get; }
        public st_hmiTB_E[] hmi_E { set; get; }
        public st_test hmi_test { set; get; }
        public st_VacSource hmi_Vacsource { set; get; }
        public st_ups UPS { set; get; } 
        public st_hmiauto hmi_auto { set; get; }
        //  public st_ysgjz YSG { set; get; }
        public st_hmi_IO IO { set; get; }
        public st_Ksystem hmi_K { set; get; }
        public st_Pg UPS_PG { set; get; }

        public bool DCM_usetest { set; get; }//				(* 使用主机信号模拟测试*)
        public bool DCM_buttAutoTest { set; get; }//			(* 全自动信号模拟测试*)
        public bool DCM_buttSemiTest { set; get; }//			(* 半自动信号模拟测试*)
        public bool DCM_buttMantest { set; get; }//			(* 手动信号模拟测试*)
        public bool DCM_buttTestStart { set; get; }//		(* 信号模拟测试 开始按钮*)

        public bool DCM_buttClose_HotMold { set; get; }//	(* 信号模拟---自动热模信号*)
        public bool DCM_buttClose { set; get; }//			(* 手动信号模拟---合模信号*)
        public bool DCM_buttOpen { set; get; }//				(* 手动信号模拟---开模信号*)
        public bool DCM_buttZero { set; get; }//				(* 手动信号模拟---零位信号*)
        public bool DCM_buttPurge { set; get; }//			(* 手动信号模拟---吹扫信号*)
        public bool DCM_buttHotMold { set; get; }//			(* 手动信号模拟---热模信号*)
        public bool DCM_buttPos_add { set; get; }//			(* 手动信号模拟---位置手动增*)
        public bool DCM_buttPos_sub { set; get; }//			(* 手动信号模拟---位置手动减*)
        public short[] hmi_lstime { set; get; }//(*关闭反应时间 ms *)
        public bool hmi_test_end { set; get; }//:BOOL;						(* 测试结束保存位*)
        public float[] HMI_Timing_AirFilter_pump { set; get; }//:ARRAY[1..3] OF REAL;	(*1-3泵空滤累计时间*)
        public ushort[] HMI_Timing_PaperFilter_D { set; get; }//:ARRAY[1..10] OF WORD;	(* 模腔通道纸滤累计模次*)
        public ushort[] HMI_Timing_IronFilter_D { set; get; }//:ARRAY[1..10] OF WORD;	(* 模腔通道铁滤累计模次*)
        public ushort HMI_Timing_PaperFilter_C { set; get; }//:WORD;					(* 料筒通道纸滤累计模次*)
        public ushort HMI_Timing_IronFilter_C { set; get; }//:WORD;					(* 料筒通道铁滤累计模次*)
        public ushort HMI_Timing_PaperFilter_E { set; get; }//:WORD;					(* 顶针通道纸滤累计模次*)
        public ushort HMI_Timing_IronFilter_E { set; get; }//:WORD;					(* 顶针通道铁滤累计模次*)

        /// <summary>
        /// 20250805 Anders 限位报警
        /// </summary>
        public bool[] HMI_enable_ls { get; set; } = new bool[10];

        public float[] hmi_spool_pg { get; set; } = new float[10];

    }
}
