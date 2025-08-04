using EF.Models.EF.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EF.Models.EF.DLL
{
    /// <summary>
    /// orm上下文
    /// </summary>
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class DataBaseContext : DbContext
    {
        /// <summary>
        /// 添加构造函数,name为config文件中数据库连接字符串的name,构造函数为无参构造函数，否则无法启用迁移命令
        /// </summary>
        public DataBaseContext() : base("MyContext")
        {
            base.Database.CreateIfNotExists();
        }

        #region 数据集
        //示范
        public DbSet<UserTable> userTable { get; set; }
        public DbSet<PfDetailTable> pfDetailTable { get; set; }
        public DbSet<PfMainTable> pfMainTable { get; set; }
        public DbSet<SampleTable> sampleTable { get; set; }
        public DbSet<PfLogTable> pfLogTable { get; set; }
        public DbSet<AlarmLogTable> alarmLogTable { get; set; }
        #endregion
        #region Fluent API配置
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除自动建表时自动加上s的复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserTable>();
            modelBuilder.Entity<PfDetailTable>();
            modelBuilder.Entity<PfMainTable>();
            modelBuilder.Entity<SampleTable>();
            modelBuilder.Entity<PfLogTable>();
            modelBuilder.Entity<AlarmLogTable>();
        }
        #endregion

    }
}
