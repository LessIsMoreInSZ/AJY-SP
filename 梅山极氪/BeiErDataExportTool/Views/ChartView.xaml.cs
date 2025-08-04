using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeiErDataExportTool.Views
{
    /// <summary>
    /// ChartView.xaml 的交互逻辑
    /// </summary>
    public partial class ChartView : UserControl
    {
        public ChartView()
        {
            InitializeComponent();
        }

        const string DatabasePath = @"C:\\Users\\25348\\Desktop\\北尔曲线导出\\Database\\Database.db";
        const string TableName = "DataLogger1";
        const int StartColumnIndex = 3; // 第四列（索引从0开始）
        const int GroupSize = 2000;
        const int GroupsToRetrieve = 12;

        public static void Main()
        {
            try
            {
                var groups = RetrieveDataGroups();

                // 打印结果
                for (int i = 0; i < groups.Count; i++)
                {
                    Console.WriteLine($"Group {i + 1} (Items: {groups[i].Count}):");
                    Console.WriteLine(string.Join(", ", groups[i].Take(10)) + (groups[i].Count > 10 ? "..." : ""));
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static List<List<object>> RetrieveDataGroups()
        {
            // 存储所有值（按行顺序）
            var allValues = new List<object>();

            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                // 获取总列数
                int totalColumns = GetColumnCount(connection, TableName);

                // 构建列名列表（从第四列开始）
                var columnsToRead = new List<string>();
                using (var cmd = new SQLiteCommand($"PRAGMA table_info({TableName})", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int columnIndex = reader.GetInt32(0); // cid
                        if (columnIndex >= StartColumnIndex)
                        {
                            columnsToRead.Add(reader.GetString(1)); // name
                        }
                    }
                }

                // 读取数据
                using (var cmd = new SQLiteCommand($"SELECT {string.Join(", ", columnsToRead)} FROM {TableName}", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            allValues.Add(reader.IsDBNull(i) ? null : reader.GetValue(i));
                        }
                    }
                }
            }

            // 分组处理
            var resultGroups = new List<List<object>>();
            int valuesToTake = Math.Min(allValues.Count, GroupSize * GroupsToRetrieve);

            for (int i = 0; i < valuesToTake; i += GroupSize)
            {
                int takeCount = Math.Min(GroupSize, valuesToTake - i);
                resultGroups.Add(allValues.GetRange(i, takeCount));
            }

            return resultGroups;
        }

        private static int GetColumnCount(SQLiteConnection connection, string tableName)
        {
            using (var cmd = new SQLiteCommand($"SELECT * FROM {tableName} LIMIT 0", connection))
            using (var reader = cmd.ExecuteReader())
            {
                return reader.FieldCount;
            }
        }
    }
}
