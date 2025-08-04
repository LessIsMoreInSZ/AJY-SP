using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_hmi_IO
    {
        public bool[] hmi_P1_DI1;
        public bool[] hmi_P1_DI2;
        public bool[] hmi_P1_DI3;
        public bool[] hmi_P1_DI4;
        public bool[] hmi_P1_DI5;
        public bool[] hmi_P1_DI6;
        public bool[] hmi_P1_DI7;
        public short[] hmi_P1_AI1;
        public short[] hmi_P1_AI2;
        public bool[] hmi_P1_DO1;
        public bool[] hmi_P1_DO2;
        public bool[] hmi_P1_DO3;
        public bool[] hmi_P1_DO4;
        public bool[] hmi_P1_DO5;
        public bool[] hmi_P1_DO6;
        public bool[] hmi_P1_DO7;
        public st_IO_E[] hmi_E;
        public st_IO_C[] hmi_C;
        public st_IO_D[] hmi_D;
    }
}
