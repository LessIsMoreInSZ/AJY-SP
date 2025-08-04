using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_recipe_E1
    {
        public float zuizhongyali_hi;               // 真空度上限报警值*)
        public float zuizhongyali_lo;               // 真空度下限报警值*)

       
        public bool use_ch;             // 通道使用*)
        public bool use_VAC_hemu;             // 使用合模开始真空   不使用--位置控制*)
        public bool use_VAC_stop;           // 使用真空结束停止真空  不使用--合模下降沿控制*)

        public bool enable_zuizhongyali;           // 使用最终压力报警*)

    }
}
