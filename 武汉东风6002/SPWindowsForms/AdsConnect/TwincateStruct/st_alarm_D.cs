using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_alarm_D
    {
        public float Opentime_Pvout;                // 通道打开反应时间检测值*)
        public float Closetime_Pvout;               // 通道关闭反应时间检测值*)
        public float Close_pos_Pvout;               // 封闭位置检测值*)
        public float Vactime_Pvout;                 // 抽真空时间检测值*)
        public float P_blow_Pvout;                  // 吹扫压力检测值*)
        public float[] P_vac_Pvout;	// 多段检测1-5检测值*)
    }
}
