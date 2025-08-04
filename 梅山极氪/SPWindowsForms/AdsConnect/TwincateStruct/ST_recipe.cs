using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct ST_recipe
    {
        public float fanyingshijian_hi;         //反应时间上限报警值*)
        public float fanyingshijian_lo;         //反应时间下限报警值*)

        public float tongfengshijian_hi;            //通风时间上限报警值*)
        public float tongfengshijian_lo;            //通风时间下限报警值*)

        public float chuisaoyali_hi;                //吹扫压力上限报警值*)
        public float chuisaoyali_lo;                //吹扫压力下限报警值*)

        public float fengbiweizhi_hi;               //封闭位置上限报警值*)
        public float fengbiweizhi_lo;               //封闭位置下限报警值*)

        public float[] duoduanjiance_hi;        //多段检测1--5上限报警值*)
        public float[] duoduanjiance_lo;        //多段检测1--5下限报警值*)
        public float[] duoduanjiance_pos;       //多段检测1--5检测位置*)

        public float tongdaodakai_hi;               //通道打开上限报警值*)
        public float tongdaodakai_lo;               //通道打开下限报警值*)

        public float auto_startpoint;               //真空开始位置*)
        public float auto_endpoint;             //真空结束位置*)

        public int filter_time;             //过滤器报警次数*)
        public int chuisao_time;                //吹扫时间*)
        public int VAC_time;                    //抽真空时间*)

        public bool use_ch;             //通道使用*)
        public bool select_paiqi;           //选择排气块*)
        public bool select_yeya;            //选择液压阀*)
        public bool select_jixie;           //选择机械阀*)
        public bool use_chuisao_M;          //使用M口吹扫*)
        public bool select_gauging_M;           //选择M口测量*)
        public bool select_auto_S;          //选择自动使用S口*)
        public bool use_VAC_time;           //使用抽真空时间*)

        public bool[] duoduanjiance_hmienabl;  //选择多段检测1-5*)
        public bool fanyingshijian_hmienable;           //选择通道关闭反应时间*)
        public bool tongfengshijian_hmienable;          //选择抽真空时间*)
        public bool chuisaoyali_hmienable;          //选择吹扫压力*)
        public bool fengbiweizhi_hmienable;         //选择封闭位置*)
        public bool tongdaodakai_hmienable;			//选择通道打开反应时间*)
    }
}
