using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    [Serializable]
    public class PfLogTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public int id { get; set; }
        [StringLength(200)]
        public string useraccount { get; set; }
  
        public string pfname { get; set; }
        public int logType { get; set; }
        [StringLength(200)]
        public string logData { get; set; }
        [StringLength(200)]
        public string logBefore { get; set; }
        [StringLength(200)]
        public string logAfter { get; set; }
        [Required]
        public DateTime create_time { get; set; } = DateTime.Now;

    }
}

