using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_hmiTB_E
    {
        public bool butt_link_Vac;          // 联动抽真空*)
        public bool led_vy1;   // 通道VY1-8 反馈*)
        public float pg1;           // 通道传感器压力1：真空 2：吹扫 3：M口 4：液压*)
        public bool hmi_butt_vy1;
    }
}
