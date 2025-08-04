using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    public class SampleTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

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
        [StringLength(500)]
        public string startpoint { get; set; }
        [StringLength(500)]
        public string endpoint { get; set; }
        [StringLength(500)]
        public string chuisaoyali { get; set; }
        [StringLength(500)]
        public string duoduanjiance1 { get; set; }
        [StringLength(500)]
        public string duoduanjiance2 { get; set; }
        [StringLength(500)]
        public string duoduanjiance3 { get; set; }
        [StringLength(500)]
        public string duoduanjiance4 { get; set; }
        [StringLength(500)]
        public string duoduanjiance5 { get; set; }
        [StringLength(500)]
        public string fanying { get; set; }
        [StringLength(500)]
        public string tongfeng { get; set; }
        [StringLength(500)]
        public string fengbi { get; set; }
        [StringLength(500)]
        public string start_yeya_PG { get; set; }
        [StringLength(500)]
        public string stop_yeya_PG { get; set; }
        [StringLength(500)]
        public string start_PG_tank { get; set; }
        [StringLength(500)]
        public string end_PG_tank { get; set; }
        public float? CA_PG { get; set; }
        public float? zongchouqi { get; set; }
        [StringLength(100)]
        public string usechanle { get; set; }
        [StringLength(100)]
        public string pfname { get; set; }
        [StringLength(100)]
        public string DCM_id { get; set; }
        [StringLength(100)]
        public string product_id { get; set; }
        [StringLength(100)]
        public string select_paiqi { get; set; }
        [StringLength(100)]
        public string select_yeya { get; set; }
        [StringLength(100)]
        public string select_jixie { get; set; }
        [StringLength(100)]
        public string duoduanjiance1_hmienable { get; set; }
        [StringLength(100)]
        public string duoduanjiance2_hmienable { get; set; }
        [StringLength(100)]
        public string duoduanjiance3_hmienable { get; set; }
        [StringLength(100)]
        public string duoduanjiance4_hmienable { get; set; }
        [StringLength(100)]
        public string duoduanjiance5_hmienable { get; set; }
        [Required]
        public DateTime update_time { get; set; } = DateTime.Now;

    }
}

