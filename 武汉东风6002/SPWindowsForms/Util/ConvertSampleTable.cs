using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms
{
    [Serializable]
    public class ConvertSampleTable
    {
        public int id { get; set; }
        public int use_pf_id { get; set; }

        public string pg1 { set; get; } = "";
        public string pg2 { set; get; } = "";
        public string pg3 { set; get; } = "";
        public string pg4 { set; get; } = "";
        public string pg5 { set; get; } = "";
        public string pg6 { set; get; } = "";
        public string pg7 { set; get; } = "";
        public string pg8 { set; get; } = "";
        public string pg9 { set; get; } = "";
        public string pg10 { set; get; } = "";
        public string pg11 { set; get; } = "";
        public string pg12 { set; get; } = "";
        public string pg13 { set; get; } = "";
        public string pg14 { set; get; } = "";
        public string pg15 { set; get; } = "";
        public string pg16 { set; get; } = "";
        public string pg17 { set; get; } = "";
        public string pg18 { set; get; } = "";
        public string pg19 { set; get; } = "";
        public string pg20 { set; get; } = "";
        public string pos { set; get; } = "";
        public string vel { set; get; } = "";
        public string startpoint { get; set; }
        public string endpoint { get; set; }
        public string chuisaoyali { get; set; }
        public string duoduanjiance1 { get; set; }
        public string duoduanjiance2 { get; set; }
        public string duoduanjiance3 { get; set; }
        public string duoduanjiance4 { get; set; }
        public string duoduanjiance5 { get; set; }
        public string fanying { get; set; }
        public string tongfeng { get; set; }
        public string fengbi { get; set; }
        public string start_yeya_PG { get; set; }
        public string stop_yeya_PG { get; set; }
        public string start_PG_tank { get; set; }
        public string end_PG_tank { get; set; }
        public float? CA_PG { get; set; }
        public float? zongchouqi { get; set; }
        public string usechanle { get; set; }
        public string pfname { get; set; }
        public string DCM_id { get; set; }
        public string product_id { get; set; }
        public string select_paiqi { get; set; }
        public string select_yeya { get; set; }
        public string select_jixie { get; set; }
        public string duoduanjiance1_hmienable { get; set; }
        public string duoduanjiance2_hmienable { get; set; }
        public string duoduanjiance3_hmienable { get; set; }
        public string duoduanjiance4_hmienable { get; set; }
        public string duoduanjiance5_hmienable { get; set; }
        public DateTime update_time { get; set; } = DateTime.Now;

    }
}
