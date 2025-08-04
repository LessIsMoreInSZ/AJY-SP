using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_VacSource
    {
        public bool butt_pump_start;                // 真空源启动按钮*)
        public bool butt_pump_stop;             // 真空源停止按钮*)
        public bool status_runing;              // 真空源运行状态*)
        public bool busy_reveal_C;              // 料筒通道模式选择显示标志 SP9001*)
        public bool busy_reveal_E;              // 顶针通道模式选择显示标志 SP9001*)
        public bool[] status_pump;  //1-3真空泵运行状态*)
        public float[] current_pump;    //1-3真空泵电流*)
        public float[] PG_tank;     //1-2储气罐压力*)
        public bool[] led_pumps_v;  // pumps_v1-6状态*)
        public bool[] led_VY1_pump; //1-3真空泵VY1状态 9001使用*)
        public bool[] led_V2_pump;  //1-3真空泵V2状态 9001使用*)
        public bool[] led_tank1_v;  // tank1——V1、V2状态 9001使用*)
        public bool led_tank2_v1;                   // tank2——V1状态 9001使用*)
        //before 47
        //******240528 新增 ~~******)
        public bool[] hmi_butt_VY1_pump;    //1-3真空泵VY1 手动按钮~~*)
        public bool[] hmi_butt_V2_pump; //1-3真空泵V2 手动按钮~~*)
        public bool[] hmi_butt_pumps_V; // pumps_v1-6 手动按钮 ~~*)
        public bool[] hmi_butt_tank1_v; // tank1——V1、V2手动按钮 ~~*)
        public bool hmi_butt_tank2_v1;				// tank2——V1手动按钮 ~~*)
        // 20250315 anders 东风特供
        //public bool[] led_ExTank_v; // 状态反馈
        //public bool[] butt_ExTank_v; // 手动按钮
    }
}
