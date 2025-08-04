using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_test
    {
        public bool test_busy;                          // 测试标志*)
        public bool butt_test_start;                    // 测试开始按钮*)
        public bool butt_test_stop;                 // 测试停止按钮*)
        public bool butt_test_guandao;                  // 管道测试按钮*)
        public bool butt_test_moqiang;                  // 模腔测试按钮*)
        public short test_time;                           // 测试时间*)
        public short holding_time;                        // 保压时间*)

        public bool[] use_ch;           // 测试通道选择*)
        public stsub_testdisplay[] _out;
        //       (*********241016 新增************)
        public bool Test_Conditions;// BOOL ;					(* 测试条件 OK*)

        public bool[] TestConditions;//: ARRAY[1..10] OF BOOL ;	(*1=未在自动  2=未处于故障*)
    }
}
