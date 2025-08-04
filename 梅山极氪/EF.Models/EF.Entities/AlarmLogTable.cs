using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    [Serializable]
    public class AlarmLogTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public int id { get; set; }
     
        [StringLength(20)]
        public string alarmCode { get; set; }
      
        [Required]
        public bool alarmFlag { set; get; } = true;
        [Required]
        public DateTime create_time { get; set; } = DateTime.Now;

    }
}

