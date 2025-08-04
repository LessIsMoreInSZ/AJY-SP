using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_Ksystem
    {
        public float P_CompressedAir;
        public float auto_CycleTime;
        public bool Led_CoolingWather;

        public bool[] ButtCL_PumpUpkeep;
        public bool[] ButtCL_AirFilter_pump;
        public bool[] ButtCL_PaperFilter_D;
        public bool[] ButtCL_IronFilter_D;
        public bool ButtCL_PaperFilter_C;
        public bool ButtCL_IronFilter_C;
        public bool ButtCL_PaperFilter_E;
        public bool ButtCL_IronFilter_E;

        public float[] Pt_Hyd;
        public float[] T_Hyd;
        public bool[] Alarm_Hyd;

        public bool[] led_remote;
        public bool[] Led_hydRuning;
        public bool[] Led_valve;
        public bool[] butt_start_hyd;
        public bool[] butt_stop_hyd;
        public bool[] open_Valve;
        //  (************* 新增241008********************)
        public bool[] butt_CL_HYDpump;//: ARRAY[1..2] OF BOOL;			(* 液压站泵浦计时清零按钮*)
        public float[] countdown_HYDpump;//: ARRAY[1..2] OF REAL;			(* 液压站泵浦 保养倒时间*)
        public float[] HYDpump_runtime;//: ARRAY[1..2] OF REAL;			(* 液压站泵浦 运行时间*)
        public float[] HYDpump_timing;//: ARRAY[1..2] OF REAL;			(* 液压站泵浦 运行累时*)
    }
}
