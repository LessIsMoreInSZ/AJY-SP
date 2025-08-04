using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_alarm_C
    {
        public float Vactime_Pvout;                // 抽真空时间检测值*)
        public float P_vac_Pvout;               // 真空压力检测值*)
        public float P_blow1_Pvout;               // 吹扫压力检测值*)
        public float P_blow2_Pvout;                 // 吹扫压力检测值*)
        public float P_blow3_Pvout;                  // 吹扫压力检测值*)
        public float P_CheckPoint_Pvout;
    }
}
