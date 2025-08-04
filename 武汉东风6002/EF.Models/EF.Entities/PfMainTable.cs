using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    [Serializable]
    public class PfMainTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public int id { get; set; }
        [StringLength(200)]
        public string creat_useraccount { get; set; }
        [StringLength(200)]
        public string update_useraccount { get; set; }
  
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(200)]
        [Required]
        public string pfname { get; set; }
        [Required]
        public DateTime update_time { get; set; } = DateTime.Now;
        [Required]
        public DateTime create_time { get; set; } = DateTime.Now;
        public bool? validity { get; set; } = true;

    }
}

