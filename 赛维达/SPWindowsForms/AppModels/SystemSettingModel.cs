using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.AppModels
{
    public class SystemSettingModel
    {
        public int sample_pointcount { set; get; }
        public int sample_chncount { set; get; }
        public int d_chn_count { set; get; }
        public int c_chn_count { set; get; }
        public int e_chn_count { set; get; }
        public int use_pf_id { set; get; }
        public string netID { set; get; }
        public int port { set; get; }
        public double pg_min { set; get; }
        public double pg_max { set; get; }
        public double vel_min { set; get; }
        public double vel_max { set; get; }
        public string machine_config { set; get; }
        public int tiaojiefa_kaidu_flag { set; get; }
        public List<string> language_options { set; get; } 
    }
}
