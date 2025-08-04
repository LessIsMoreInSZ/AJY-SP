using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AdsConnect.TwincateStruct
{
    public struct st_Pg//640+96=736
    {
        public PGs_D[] pg_D;//640
        public PGs_C[] pg_C;//32
        public PGs_Vac[] PG_E;//16
        public PGs_Vac PG_Tank1;//16
        public PGs_Vac PG_Tank2;//16
        public PGs_Vac PG_air;//16

    }
}
