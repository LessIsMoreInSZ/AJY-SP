using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct hmi_st_layer
    {
        public bool DcmClose;               // 压铸机合模到位*)
        public bool DcmOpen;                // 压铸机开模到位*)
        public bool DcmAuto;                // 压铸机自动信号*)
        public bool DcmHotMould;            // 压铸机热模信号*)
        public bool DcmBlow;                // 压铸机吹扫信号/顶针前限*)
        public bool DcmPosZero;         // 压铸机压射零位*)
        public float DcmPos;                // 压铸机压射位置*)
        public float DcmVel;                // 压铸机压射速度*)
        public string DcmPosCoding; // 压铸机压射编码/刻印码*)

        public bool Zero_Calibration;           // 零位校准信号*)
        public bool Es_pump;                    // 泵站急停 *)
        public bool Es_Controls;                // 立柱急停*)
        public bool Status_auto;                // 自动状态*)
        public bool Status_err;             // 故障状态*)
        public bool Status_ready;               // 准备状态*)
        public bool Status_pumpruning;      // 泵站运行*)

        public float[] P_Tank;  //1#、2#储气罐压力*)

        public bool[] Use_ch_E; // 顶针通道使用E1-E2*)
        public bool[] Use_ch_C; // 料筒通道使用C1-C2*)
        public bool[] Use_ch_D; // 模腔通道使用D1-D10*)
        public bool Status_remote_pump; // 泵站1：远控状态  0:本控状态
    }
}
