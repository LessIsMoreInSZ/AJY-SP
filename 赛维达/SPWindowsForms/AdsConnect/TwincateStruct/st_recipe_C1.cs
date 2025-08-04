using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_recipe_C1
    {
        public float auto_startpoint1;              // 真空开始位置1*)
        public float auto_startpoint2;              // 真空开始位置2*)
        public float auto_startpoint3;              // 真空开始位置3*)
        public float auto_endpoint1;                // 真空结束位置1*)
        public float auto_endpoint2;                // 真空结束位置2*)
        public float auto_endpoint3;                // 真空结束位置3*)

        public float zuizhongyali_hi;               // 真空度上限报警值*)
        public float zuizhongyali_lo;               // 真空度下限报警值*)

        public float chuisaoyali_hi;                // 吹扫压力上限报警值1*)
        public float chuisaoyali_lo;                // 吹扫压力下限报警值1*)

        public float chuisaoyali_hi2;               // 吹扫压力上限报警值2*)
        public float chuisaoyali_lo2;               // 吹扫压力下限报警值2*)

        public float chuisaoyali_hi3;               // 吹扫压力上限报警值3*)
        public float chuisaoyali_lo3;               // 吹扫压力下限报警值3*)

        public int VAC_time1;                   // 抽真空时间1*)
        public int VAC_time2;                   // 抽真空时间2*)
        public int VAC_time3;                   // 抽真空时间3*)

        public int chuisao_INR_time;                // 吹扫间隔时间*)
        public int chuisao_time1;               // 吹扫时间1*)
        public int chuisao_time2;               // 吹扫时间2*)
        public int chuisao_time3;               // 吹扫时间3*)

        public bool use_ch;             // 通道使用*)
        public bool use_CA;             // 使用吹扫功能*)
        public bool use_VAC_time;           // 使用抽真空时间*)

        //-------------------新增选择按钮---------------------*)
        public bool enable_zuizhongyali;        // 使用最终压力报警*)
        public bool enable_chuisaogyali1;       // 使用吹扫压力1 报警*)
        public bool enable_chuisaogyali2;       // 使用吹扫压力2 报警*)
        public bool enable_chuisaogyali3;       // 使用吹扫压力3 报警*)
        public bool enable_zu1;         // 使用TB1*)
        public bool enable_zu2;         // 使用TB2*)
        public bool enable_zu3;			// 使用TB3*)
    }
}
