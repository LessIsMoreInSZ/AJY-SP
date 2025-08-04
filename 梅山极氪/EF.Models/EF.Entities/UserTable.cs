using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models.EF.Entities
{
    public class UserTable
    {
        /// <summary>
        /// 按照时间生成
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public int id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(100)]
        [Required]
        public string useraccount { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(100)]
        [Required]
        public string userpassword { get; set; }
        [Required]
        public int userrole { get; set; }
        public bool validity { get; set; } = true;

    }
}

