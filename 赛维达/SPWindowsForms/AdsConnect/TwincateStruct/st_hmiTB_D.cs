using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_hmiTB_D
    {
        public bool butt_link_Vac;          // 联动抽真空*)
        public bool butt_link_Blow;         // 联动吹扫*)
        public bool butt_Vac;                   // 单动抽真空*)
        public bool butt_Blow;              // 单动抽吹扫*)
        public bool butt_open;              // 单动抽顶出*)
        public bool butt_close;             // 单动抽退回*)
        public bool status_ls;              // 限位开关反馈*)
        public bool[] led_vy;   // 通道VY1-9 反馈*)
        public float[] pg;          // 通道传感器压力1：真空 2：吹扫 3：M口 4：液压 5反应时间*)
        public bool[] hmi_butt_vy;
    }
}
