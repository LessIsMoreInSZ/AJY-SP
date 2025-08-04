using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_sample
    {
        public bool enablereading;              // 曲线读取标志位*)
        public float[] pg1;     // TB1真空*)
        public float[] pg2;     // TB2真空*)
        public float[] pg3;     // TB3真空*)
        public float[] pg4;     // TB4真空*)
        public float[] pg5;     // TB5真空*)
        public float[] pg6;     // TB6真空*)
        public float[] pg7;     // TB7真空*)
        public float[] pg8;     // TB8真空*)
        public float[] pg9;     // TB9真空*)
        public float[] pg10;        // TB10真空*)
        public float[] pos;     // 压射位置——固定横坐标*)
        public float[] vel;     // 压射速度——固定纵坐标*)
        public float[] startpoint;  // 真空开始位置*)
        public float[] endpoint;    // 真空结束位置*)
        public float[] chuisaoyali; // 吹扫压力*)
        public float[] duoduanjiance1;// 多段检测压力1*)
        public float[] duoduanjiance2;// 多段检测压力2*)
        public float[] duoduanjiance3;// 多段检测压力3*)
        public float[] duoduanjiance4;// 多段检测压力4*)
        public float[] duoduanjiance5;// 多段检测压力5*)
        public float[] fanying; // 反应时间*)
        public float[] tongfeng;    // 抽真空时间*)
        public float[] fengbi;  // 封闭位置*)
        public float[] start_yeya_PG;// 循环前储能器压力*)
        public float[] stop_yeya_PG;    // 循环后储能器压力*)
        public float[] start_PG_tank;   // 储气罐抽前压力*)
        public float[] end_PG_tank; // 储气罐抽后压力*)
        public float CA_PG;                     // 压缩空气气源压力*)
        public float zongchouqi;                    // 总抽气量*)
        public bool[] usechanle;   // 通道选择*)
        public string pfname; // STRING(16)hmi120*)// 配方名称*)
        public string DCM_id; // STRING(50)  hmi120*)// YSBM*)
        //public float[] pg11;        // TB10真空*)
    }
}
