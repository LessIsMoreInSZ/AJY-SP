using EF.Models.EF.Entities;
using SPWindowsForms.ExcelHelper;
using SPWindowsForms.SwitchForms;
using SPWindowsForms.Util;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public static class CommonFunction
    {
        public static List<UserSelectItem> ConvertEnumToUserSelectItem(this Type _enum)
        {
            var returnList = new List<UserSelectItem>();
            foreach (var g in Enum.GetValues(_enum))
            {
                var newRow = new UserSelectItem
                {
                    id = (int)g,
                    name = g.ToString()
                };
                returnList.Add(newRow);
            }
            return returnList;
        }

        public static Dictionary<int, string> ConvertEnumToDic(this Type _enum)
        {
            var dic = new Dictionary<int, string>();
            foreach (var g in Enum.GetValues(_enum))
            {

                dic.Add((int)g, g.ToString());
            }
            return dic;
        }
        public static bool IsSpecialChar(string str)
        {
            return Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        public static void SetComoboxData(UIComboBox box, Type type)
        {
            box.DataSource = type.ConvertEnumToUserSelectItem();
            box.ValueMember = "id";
            box.DisplayMember = "name";
        }
        public static string PLCString(this string input)
        {
            int index = input.IndexOf('\0');

            // 如果找到了'\0'，则截取到该索引之前的部分  
            if (index >= 0)
            {
                return input.Substring(0, index);
            }
            // 如果没有找到'\0'，则返回原始字符串（即不处理）  
            else
            {
                return input;
            }
        }

        public static float StringToFloat(this string str)
        {
            float number;
            bool isfloat = float.TryParse(str, out number);
            if (isfloat)
                return number;
            else
                return 0;
        }
        public static float? StringToFloatAllowNull(this string str)
        {
            float number;
            bool isfloat = float.TryParse(str, out number);
            if (isfloat)
                return number;
            else
                return null;
        }
        public static DateTime? StringToDateTimeAllowNull(this string str)
        {
            DateTime dateTime;
            bool result = DateTime.TryParse(str, out dateTime);
            if (result)
                return dateTime;
            else
                return null;
        }
        public static object DeepCopy(object obj)
        {
            BinaryFormatter Formatter = new BinaryFormatter(null,
  new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, obj);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
        public static void AddCol(UIDataGridView dataGrid, string name, string headText = "", DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.Fill, int width = 100)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = headText;
            column.Name = name;
            column.AutoSizeMode = mode;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (mode == DataGridViewAutoSizeColumnMode.NotSet)
                column.Width = width;
            dataGrid.Columns.Add(column);
        }
        public static string GetExcelCell(int row, int col)
        {
            char letter = (char)(col - 1 + 'A' /* 或者等价于 65 */);
            return $"{letter}{row}";
        }

        public static bool CheckAccessFactory()
        {
            //return true;
            return GlobalVar.UserRole >= (int)eSpecialUserRole.厂家;
        }
        public static bool CheckAccessShebei()
        {
            // return true;
            return GlobalVar.UserRole >= (int)eUserRole.设备管理员;
        }
        public static bool CheckAccessGongyi()
        {
            // return true;
            return GlobalVar.UserRole >= (int)eUserRole.工艺修改;
        }
        public static bool CheckAccessJishu()
        {
            // return true;
            return GlobalVar.UserRole >= (int)eUserRole.技术员;
        }
        public static bool StayUser()
        {
            return new List<int> { (int)NowUi.配方, (int)NowUi.登录 }
            .Contains(GlobalVar.NowUiDisplay);
        }
        public static bool CheckStrNumType(string str, bool floatFlag)
        {
            if (floatFlag) return Work.CheckFloat(str);
            else return Work.CheckInt(str);
        }
        public static float? GetFloatFromUIText(UITextBox uITextBox)
        {
            var value = uITextBox.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                return null;
            }
            else return Convert.ToSingle(value);
        }
        public static short? GetShortFromUIText(UITextBox uITextBox)
        {
            var value = uITextBox.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, false))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                return null;
            }
            else return Convert.ToInt16(value);
        }
        public static void DownloadGridViewExcel(UIDataGridView gridView)
        {
            var _excelHelper = new ExcelHelperClass();
            var workbook = _excelHelper.WriteGridViewExcel(gridView);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 保存工作簿到指定的文件  
                workbook.SaveAs(saveFileDialog.FileName);

                // 关闭工作簿（这也会释放所有资源）  
                workbook.Dispose();

                // 显示消息框以确认文件已保存  
                MessageBox.Show(LanguageSet.SetL("pf", "DownloadOk"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static BottomPageSwitch AddBottomSwitchButton(UIFlowLayoutPanel panel, NowUi nowUi)
        {
            var ctrl = new BottomPageSwitch((int)nowUi);
            panel.Controls.Add(ctrl);
            return ctrl;
        }
        public static string MathRound(this float input, int roundNumber = 1)
        {
            var round = Math.Round(input, roundNumber);
            return round.ToString();
        }
        public static string MathRound(this float? input, int roundNumber = 1)
        {
            if (input == null) return "";
            var round = Math.Round(input.Value, roundNumber);
            return round.ToString();
        }
        public static string MathRound(this double input, int roundNumber = 1)
        {
            var round = Math.Round(input, roundNumber);
            return round.ToString();
        }
        public static ConvertSampleTable DeepCopyConvertSampleTable(SampleTable sampleTable)
        {
            var copyModel = new ConvertSampleTable();
            copyModel.id = sampleTable.id;
            copyModel.use_pf_id = sampleTable.use_pf_id;
            copyModel.pg1 = sampleTable.pg1;
            copyModel.pg2 = sampleTable.pg2;
            copyModel.pg3 = sampleTable.pg3;
            copyModel.pg4 = sampleTable.pg4;
            copyModel.pg5 = sampleTable.pg5;
            copyModel.pg6 = sampleTable.pg6;
            copyModel.pg7 = sampleTable.pg7;
            copyModel.pg8 = sampleTable.pg8;
            copyModel.pg9 = sampleTable.pg9;
            copyModel.pg10 = sampleTable.pg10;
            copyModel.pg11 = sampleTable.pg11;
            copyModel.pg12 = sampleTable.pg12;
            copyModel.pg13 = sampleTable.pg13;
            copyModel.pg14 = sampleTable.pg14;
            copyModel.pg15 = sampleTable.pg15;
            copyModel.pg16 = sampleTable.pg16;
            copyModel.pg17 = sampleTable.pg17;
            copyModel.pg18 = sampleTable.pg18;
            copyModel.pg19 = sampleTable.pg19;
            copyModel.pg20 = sampleTable.pg20;
            copyModel.pos = sampleTable.pos;
            copyModel.vel = sampleTable.vel;
            copyModel.startpoint = sampleTable.startpoint;
            copyModel.endpoint = sampleTable.endpoint;
            copyModel.chuisaoyali = sampleTable.chuisaoyali;
            copyModel.duoduanjiance1 = sampleTable.duoduanjiance1;
            copyModel.duoduanjiance2 = sampleTable.duoduanjiance2;
            copyModel.duoduanjiance3 = sampleTable.duoduanjiance3;
            copyModel.duoduanjiance4 = sampleTable.duoduanjiance4;
            copyModel.duoduanjiance5 = sampleTable.duoduanjiance5;
            copyModel.fanying = sampleTable.fanying;
            copyModel.tongfeng = sampleTable.tongfeng;
            copyModel.fengbi = sampleTable.fengbi;
            copyModel.start_yeya_PG = sampleTable.start_yeya_PG;
            copyModel.stop_yeya_PG = sampleTable.stop_yeya_PG;
            copyModel.start_PG_tank = sampleTable.start_PG_tank;
            copyModel.end_PG_tank = sampleTable.end_PG_tank;
            copyModel.CA_PG = sampleTable.CA_PG;
            copyModel.zongchouqi = sampleTable.zongchouqi;
            copyModel.usechanle = sampleTable.usechanle;
            copyModel.pfname = sampleTable.pfname;
            copyModel.DCM_id = sampleTable.DCM_id;
            copyModel.product_id = sampleTable.product_id;
            copyModel.select_paiqi = sampleTable.select_paiqi;
            copyModel.select_yeya = sampleTable.select_yeya;
            copyModel.select_jixie = sampleTable.select_jixie;
            copyModel.duoduanjiance1_hmienable = sampleTable.duoduanjiance1_hmienable;
            copyModel.duoduanjiance2_hmienable = sampleTable.duoduanjiance2_hmienable;
            copyModel.duoduanjiance3_hmienable = sampleTable.duoduanjiance3_hmienable;
            copyModel.duoduanjiance4_hmienable = sampleTable.duoduanjiance4_hmienable;
            copyModel.duoduanjiance5_hmienable = sampleTable.duoduanjiance5_hmienable;
            copyModel.update_time = sampleTable.update_time;
            return copyModel;
        }
        public static List<ConvertSampleTable> DeepCopyConvertSampleTables(List<SampleTable> sampleTables)
        {
            var copyModels = new List<ConvertSampleTable> { };
            sampleTables.ForEach(s => copyModels.Add(DeepCopyConvertSampleTable(s)));

            return copyModels;
        }
        public static bool dataGridViewToCSV(UIDataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("no data", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "Save";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.GetEncoding(-0));
                string strLine = "";
                try
                {
                    //表头
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (i > 0)
                            strLine += ",";
                        strLine += dataGridView.Columns[i].HeaderText;
                    }
                    strLine.Remove(strLine.Length - 1);
                    sw.WriteLine(strLine);
                    strLine = "";
                    //表的内容
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        strLine = "";
                        int colCount = dataGridView.Columns.Count;
                        for (int k = 0; k < colCount; k++)
                        {
                            if (k > 0 && k < colCount)
                                strLine += ",";
                            if (dataGridView.Rows[j].Cells[k].Value == null)
                                strLine += "";
                            else
                            {
                                string cell = dataGridView.Rows[j].Cells[k].Value.ToString().Trim();
                                //防止里面含有特殊符号
                                cell = cell.Replace("\"", "\"\"");
                                cell = "\"" + cell + "\"";
                                strLine += cell;
                            }
                        }
                        sw.WriteLine(strLine);
                    }
                    sw.Close();
                    stream.Close();
                    MessageBox.Show(saveFileDialog.FileName.ToString(), "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

    }
}
