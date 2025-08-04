using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_hmiauto
    {
        public bool auto_condition;
        //public bool dis_HYDTest;
        public bool[] condition;
        public bool status_autoruning;
        public bool butt_autostart;
        public bool butt_autostop;
        public bool butt_useVac_hotmould;
        public st_ledauto[] led_D;
        public st_ledauto[] led_C;
        public st_ledauto[] led_E;
    }
}
