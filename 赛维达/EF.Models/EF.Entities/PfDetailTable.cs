using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    [Serializable]
    public class PfDetailTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? id { get; set; }

        public int? pfid { get; set; }


        public int? chnorder { get; set; }
        [StringLength(10)]
        public string chnType { get; set; }
        [StringLength(10)]
        public string chnName { get; set; }

        public float? Opentime_hi { get; set; }
        public float? Opentime_lo { get; set; }

        public float? Closetime_hi { get; set; }
        public float? Closetime_lo { get; set; }


        public float? Close_pos_hi { get; set; }
        public float? Close_pos_lo { get; set; }


        public float? Vactime_hi { get; set; }
        public float? Vactime_lo { get; set; }

        public float? P_blow_hi { get; set; }
        public float? P_blow_lo { get; set; }


        public float? P_vac_hi { get; set; }
        public float? P_vac_lo { get; set; }
        public float? P_vac_pos { get; set; }


        public float? P_vac_hi2 { get; set; }
        public float? P_vac_lo2 { get; set; }
        public float? P_vac_pos2 { get; set; }

        public float? P_vac_hi3 { get; set; }
        public float? P_vac_lo3 { get; set; }
        public float? P_vac_pos3 { get; set; }

        public float? P_vac_hi4 { get; set; }
        public float? P_vac_lo4 { get; set; }
        public float? P_vac_pos4 { get; set; }

        public float? P_vac_hi5 { get; set; }
        public float? P_vac_lo5 { get; set; }
        public float? P_vac_pos5 { get; set; }

        public float? auto_startpoint { get; set; }
        public float? auto_endpoint { get; set; }

        public short? filter_time { get; set; }
        public short? Blow_Delay_time { get; set; }
        public short? Blow_time { get; set; }
        public short? VAC_time { get; set; }


        public bool? use_ch { get; set; }
        public bool? select_paiqi { get; set; }
        public bool? select_yeya { get; set; }
        public bool? select_jixie { get; set; }
        public bool? use_chuisao_M { get; set; }
        public bool? select_gauging_M { get; set; }
        public bool? select_auto_S { get; set; }
        public bool? use_VAC_time { get; set; }


        public bool? enable_Opentime { get; set; }
        public bool? enable_Closetime { get; set; }
        public bool? enable_Vactime { get; set; }
        public bool? enable_P_blow { get; set; }
        public bool? enable_Close_pos { get; set; }

        public bool? enable_P_vac { get; set; }
        public bool? enable_P_vac2 { get; set; }
        public bool? enable_P_vac3 { get; set; }
        public bool? enable_P_vac4 { get; set; }
        public bool? enable_P_vac5 { get; set; }

        //st_recipe_C1
        public float? auto_startpoint2 { get; set; }
        public float? auto_startpoint3 { get; set; }
        public float? auto_endpoint2 { get; set; }
        public float? auto_endpoint3 { get; set; }

        public float? P_blow_hi2 { get; set; }
        public float? P_blow_lo2 { get; set; }

        public float? P_blow_hi3 { get; set; }
        public float? P_blow_lo3 { get; set; }
        public float? set_CheckPoint_C { get; set; }
        public float? set_CheckPointHi_C { get; set; }
        public float? set_CheckPointLo_C { get; set; }

        public short? VAC_time2 { get; set; }
        public short? VAC_time3 { get; set; }

        public short? Blow_INR_time { get; set; }
        public short? Blow_time2 { get; set; }
        public short? Blow_time3 { get; set; }

        public bool? use_blow { get; set; }

        public bool? enable_P_blow2 { get; set; }
        public bool? enable_P_blow3 { get; set; }
        public bool? enable_zu1 { get; set; }
        public bool? enable_zu2 { get; set; }
        public bool? enable_zu3 { get; set; }
        //st_recipe_E1
        public bool? use_VAC_hemu { get; set; }
        public bool? use_VAC_stop { get; set; }
        public bool? use_checkPoint_C { get; set; }
        [Required]
        public DateTime update_time { get; set; } = DateTime.Now;
        [Required]
        public DateTime create_time { get; set; } = DateTime.Now;
    }
}

