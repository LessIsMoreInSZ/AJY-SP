using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_ups
    {
        public float[] runtime_pump;        //1#-3#真空泵运行时间*)

        public bool pump_mode1; // SP6001 真空源模式1设定*)
        public bool pump_mode2; // SP6001 真空源模式2设定*)
        public bool pump_mode3; // SP6001 真空源模式3设定*)


        public bool use_standbyV6;// SP9001 使用备用阀6*)
        public short Vacsource_D;   // SP9001 模腔通道真空源设定*)16
        public short Vacsource_C;   // SP9001 料筒通道真空源设定*)18
        public short Vacsource_E;   // SP9001 顶针通道真空源设定*)20
        public short set_pump_C;    // SP9001 料筒使用真空泵设置*)22
        public short set_pump_E;    // SP9001 顶针使用真空泵设置*)24



        //************************** 设备管理*******************************)
        public float[] Countdown_pump;
        public bool[] butt_i_dis;               //1-3维修停泵功能按钮*)
        public float[] set_pump_upkeepTime; //1-3泵浦保养时间设定*)

        public bool use_CycleTime;                      // 使用循环计时功能*)
        public float set_CycleTime_value;                   // 设置循环计时报警值*)
        public bool use_TestCoolingWather;              // 使用循环水检测*)
        public bool use_TestCompressedAir;              // 使用压缩空气检测*)
        public float set_CompressedAir_lo;                  // 压缩空气检测下限*)
        public bool use_atandby_vavle;
        public bool autoUse_PumpRuningCondition;            // 使用泵浦启动条件*)
        public bool hmi_Butt_BOV;
        //K3
        public float K_pos;
        public float offset_pos;
        public short Safety_value_pos;
        public short Safety_time;
        //K5
        public bool[] Use_Upkeep_pump;
        public bool[] Use_AirFilter_pump;
        public bool[] Use_PaperFilter_D;
        public bool[] Use_IronFilter_D;
        public bool Use_PaperFilter_C;
        public bool Use_IronFilter_C;
        public bool Use_PaperFilter_E;
        public bool Use_IronFilter_E;

        public float[] Time_AirFilter_pump;
        public ushort[] Time_PaperFilter_D;
        public ushort[] Time_IronFilter_D;
        public ushort Time_PaperFilter_C;
        public ushort Time_IronFilter_C;
        public ushort Time_PaperFilter_E;
        public ushort Time_IronFilter_E;

        public float[] Upkeep_pump;
        public float[] Upkeep_AirFilter_pump;
        public ushort[] Upkeep_PaperFilter_D;
        public ushort[] Upkeep_IronFilter_D;
        public ushort Upkeep_PaperFilter_C;
        public ushort Upkeep_IronFilter_C;
        public ushort Upkeep_PaperFilter_E;
        public ushort Upkeep_IronFilter_E;
        //K6
        public bool Use_HYD1;//20250220
        public bool Ust_timeControl_HYD;
        public short set_HYD_time;
        public float set_Hyd_hi;
        public float set_Hyd_lo;

        public float set_Upkeep_HYD;//: REAL;//254
        //(******* 新增241008***********)
        public bool use_DCMcloseTest;//BOOL;	(* 使用合模检测*)
        public bool use_SpoolTest;//BOOL;	(* 使用阀芯检测*)
        public bool use_openRise;//BOOL;		(* 使用开模顶出功能*)

        public float DCMcloseTest_hi;//REAL;	(* 合模检测上限*)
        public float DCMcloseTest_lo;//REAL;	(* 合模检测下限*)
        public float SpoolTest_hihi;//REAL;	(* 阀芯断裂检测上限设定*)
        public float SpoolTest_hi;//REAL;		(* 阀芯闭合不到位检测上限设定*)

        public short Spool_ProbeTime;//INT;	(* 阀芯探测时间*)
        public short Spool_testTime;//INT;		(* 阀芯检测真空时间*)
        public short ProbeTime;//INT			(* 合模探测时间*)
        public short testTime;//INT			(* 合模检测吹扫时间*)

        public short VacLatency;//INT		(* 抽真空延时*)
        public short use_Vacsign;//INT		(*0=位置 1=信号  2=位置+信号*)
        public short use_VacTime;//INT		(*0=使用真空时间 1=不使用真空时间*)
        //290
        public short SafetyVel;//INT			(* 压射速度安全阈值设定*)/254+37
        public short SafetyTime;//INT		(* 真空时间安全阈值设定*)
        public bool use_P;//BOOL;			(* 机械阀 使用P口关阀*)

        public bool Use_MBlow;//BOOL;		(* 机械阀 使用M口反吹*)

        public bool use_autoEndS;//BOOL;		(* 机械阀 使用终点S口关阀*)

        public bool select_gauging_M;//BOOL	(* 机械阀 选择M口测量*)


        public bool use_TakePos;//BOOL;		(* 使用曲线监测点检测数据*)

        public bool setUse_standbyValve;//BOOL;(* 泵站使用备用阀门----9001使用*)
        //300
        public float Set_tank_StartPG;//REAL;	(* 设定储气罐启动检测压力*)
        public float Set_RootsPump_StartPG;//REAL;	(* 设定罗茨泵启动压力*)

        public bool use_tankSET;//BOOL;		(* 使用储气罐压力调节*)
        public float Set_tankHI;//REAL;		(* 储气罐压力调节上限*)
        public float Set_tankLO;//REAL;		(* 储气罐压力调节下限*)
        public float Set_tank1HI;//REAL;		(* 外置储气罐压力调节上限--9001使用*)
        public float Set_tank1LO;//REAL;		(* 外置储气罐压力调节下限--9001使用*)

        public float[] set_Amps;//:ARRAY[1..3] OF REAL; (* 设置泵浦电流*)
        //337
        public short set_kaidu;//: INT ; 		(* 调节阀开度设定*)

        public bool Use_HYD2;//20250220
        //plc日期241216 Anders 20250219
        public float HYD_TestHI;
        public float HYD_TestLo;

        // 20250315 东风特供 337+11=348
        //public bool E_setsource;
        //public bool C_setsource;

    }
}
