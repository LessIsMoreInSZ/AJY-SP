using EF.Models.EF.Entities;
using SPWindowsForms.DbService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.SwitchForms
{
    public partial class DownloadTableControl : UserControl
    {
        private bool isUpdating = false;
        private int parmeterCount = 14;
        private List<string> mycols;
        public DownloadTableControl()
        {
            InitializeComponent();
            IniTable1Head();
            IniSearchBox();
            uiLabel_starttime.Text = LanguageSet.SetL("chart", "uiLabel_starttime");
            uiLabel_endtime.Text = LanguageSet.SetL("chart", "uiLabel_endtime");
        }
        private void IniTable1Head()
        {
            uiDataGridViewlog.RowHeadersVisible = false;
            uiDataGridViewlog.AllowUserToAddRows = false;
            uiDataGridViewlog.AllowUserToDeleteRows = false;
            uiDataGridViewlog.AllowUserToResizeRows = false;
            uiDataGridViewlog.AllowUserToResizeColumns = false;
            uiDataGridViewlog.ReadOnly = true;
            mycols = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                string colname = LanguageSet.SetL("Download", $"tableCol{i}");
                //  CommonFunction.AddCol(uiDataGridViewlog, $"downloadcol{i}", colname, DataGridViewAutoSizeColumnMode.AllCells);

                mycols.Add(colname);
            }

            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                for (int j = 1; j <= 14; j++)
                {
                    string colname = $"D{i} " + LanguageSet.SetL("Download", $"tableChnCol{j}");
                    // CommonFunction.AddCol(uiDataGridViewlog, $"D_par_{i}_{j}", colname, DataGridViewAutoSizeColumnMode.AllCells);
                    mycols.Add(colname);
                }

            }
            for (int i = 1; i <= GlobalVar.systemSetting.c_chn_count; i++)
            {
                for (int j = 1; j <= 14; j++)
                {
                    string colname = $"C{i} " + LanguageSet.SetL("Download", $"tableChnCol{j}");
                    // CommonFunction.AddCol(uiDataGridViewlog, $"C_par_{i}_{j}", colname, DataGridViewAutoSizeColumnMode.AllCells);
                    mycols.Add(colname);
                }
            }

        }
        private void IniSearchBox()
        {
            DateTime now = DateTime.Now.AddDays(1); // 获取当前日期和时间  
            DateTime endDateToday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime startDateToday = endDateToday.AddDays(-1);
            uiDatetimePicker_start.Text = startDateToday.ToString("yyyy-MM-dd HH:mm:ss");
            uiDatetimePicker_stop.Text = endDateToday.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public async Task DoSearch()
        {
            if (isUpdating) return;
            isUpdating = true;
            try
            {

                var s_dateTimeStart = uiDatetimePicker_start.Text.Trim().StringToDateTimeAllowNull();
                var s_dateTimeEnd = uiDatetimePicker_stop.Text.Trim().StringToDateTimeAllowNull();
                if (s_dateTimeStart != null && s_dateTimeEnd != null)
                {
                    if (s_dateTimeEnd <= s_dateTimeStart)
                        throw new Exception(LanguageSet.SetL("chart", "checksearch"));
                }

                var searchs = await SampleDBCommon.GetSampleTableDownload(s_dateTimeStart, s_dateTimeEnd);
                await UpdateTableData(searchs);
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage("DoSearch()" + ex.Message);
            }
            finally
            {
                isUpdating = false;
            }
        }
        private bool GetListBoolFlag(List<bool> bools, int index)
        {
            if (bools.Count <= index) return false;
            else return bools[index];
        }
        private string GetListFloatFlag(List<float?> floats, int index)
        {
            if (floats.Count <= index) return "";
            else
            {
                //if (floats[index] == null)
                //    return "";
                //else
                return floats[index].MathRound();
            }
        }
        private string GetFaLeixing(bool select_paiqi, bool select_yeya, bool select_jixie)
        {
            if (select_yeya) return LanguageSet.SetL("chart", "yeya");
            if (select_paiqi) return LanguageSet.SetL("chart", "paiqi");
            if (select_jixie) return LanguageSet.SetL("chart", "jixie");
            else return "-";
        }
        private void SetCellDisable(int roworder, int colorder)
        {
            this.uiDataGridViewlog.Rows[roworder].Cells[colorder].Value = "";
            this.uiDataGridViewlog.Rows[roworder].Cells[colorder].Style.BackColor = Color.LightGray;
        }
        private async Task UpdateTableData(List<DownloadSampleModel> sampleTables)
        {
            List<List<string>> data;

            data = PrepareData(sampleTables);
            DataTable dataTable = new DataTable();
            for (int i = 0; i < mycols.Count; i++)
            {
                dataTable.Columns.Add(new DataColumn(mycols[i], typeof(string)));
            }

            foreach (var row in data)
            {
                DataRow newRow = dataTable.NewRow();

                // 填充行数据
                for (int i = 0; i < row.Count; i++)
                {
                    newRow[mycols[i]] = row[i];
                }

                // 将行添加到 DataTable
                dataTable.Rows.Add(newRow);
            }

            // 绑定 DataTable 到 DataGridView
            uiDataGridViewlog.DataSource = null; // 先设置为null可以避免某些绑定问题
            uiDataGridViewlog.DataSource = dataTable;
            foreach (DataGridViewColumn column in uiDataGridViewlog.Columns)
            {
                column.Width = -1; // 这通常是将宽度设置为自动的必要步骤，但不是所有情况下都必需
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private async Task UpdateTableData2(List<SampleTable> sampleTables)
        {
            uiDataGridViewlog.Rows.Clear();
            if (sampleTables.Count == 0) return;
            uiDataGridViewlog.Rows.Add(sampleTables.Count);
            await Task.Run(() =>
            {
                for (int j = 0; j < sampleTables.Count; j++)
                {
                    var sampleTable = sampleTables[j];
                    var uschs = (sampleTable.usechanle ?? "").Split(',').Select(m => m == "True").ToList();
                    var select_paiqis = (sampleTable.select_paiqi ?? "").Split(',').Select(m => m == "True").ToList();
                    var select_yeyas = (sampleTable.select_yeya ?? "").Split(',').Select(m => m == "True").ToList();
                    var select_jixies = (sampleTable.select_jixie ?? "").Split(',').Select(m => m == "True").ToList();
                    var duoduanjiance1_hmienables = (sampleTable.duoduanjiance1_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                    var duoduanjiance2_hmienables = (sampleTable.duoduanjiance2_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                    var duoduanjiance3_hmienables = (sampleTable.duoduanjiance3_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                    var duoduanjiance4_hmienables = (sampleTable.duoduanjiance4_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                    var duoduanjiance5_hmienables = (sampleTable.duoduanjiance5_hmienable ?? "").Split(',').Select(m => m == "True").ToList();


                    var startpoints = (sampleTable.startpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var endpoints = (sampleTable.endpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var chuisaoyalis = (sampleTable.chuisaoyali ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance1s = (sampleTable.duoduanjiance1 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance2s = (sampleTable.duoduanjiance2 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance3s = (sampleTable.duoduanjiance3 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance4s = (sampleTable.duoduanjiance4 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance5s = (sampleTable.duoduanjiance5 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var tongfengs = (sampleTable.tongfeng ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var fanyings = (sampleTable.fanying ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var fengbis = (sampleTable.fengbi ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var start_yeya_PGs = (sampleTable.start_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var stop_yeya_PGs = (sampleTable.stop_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var start_PG_tanks = (sampleTable.start_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var end_PG_tanks = (sampleTable.end_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();

                    uiDataGridViewlog.Rows[j].Cells[0].Value = sampleTable.update_time.ToString("MM/dd/yyyy HH:mm:ss");
                    uiDataGridViewlog.Rows[j].Cells[1].Value = sampleTable.product_id;
                    uiDataGridViewlog.Rows[j].Cells[2].Value = sampleTable.DCM_id;
                    uiDataGridViewlog.Rows[j].Cells[3].Value = sampleTable.pfname;
                    uiDataGridViewlog.Rows[j].Cells[4].Value = sampleTable.CA_PG;
                    uiDataGridViewlog.Rows[j].Cells[5].Value = sampleTable.zongchouqi;
                    uiDataGridViewlog.Rows[j].Cells[6].Value = GetListFloatFlag(start_PG_tanks, 0);
                    uiDataGridViewlog.Rows[j].Cells[7].Value = GetListFloatFlag(start_PG_tanks, 1);
                    uiDataGridViewlog.Rows[j].Cells[8].Value = GetListFloatFlag(end_PG_tanks, 0);
                    uiDataGridViewlog.Rows[j].Cells[9].Value = GetListFloatFlag(end_PG_tanks, 1);
                    int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
                    var bools = new bool[totalCount];
                    int startCount = 9;

                    for (int i = 0; i < totalCount; i++)
                    {
                        if (GetListBoolFlag(uschs, i))
                        {
                            this.uiDataGridViewlog.Rows[j].Cells[startCount + 1].Value = GetFaLeixing(GetListBoolFlag(select_paiqis, i), GetListBoolFlag(select_yeyas, i), GetListBoolFlag(select_jixies, i));
                            this.uiDataGridViewlog.Rows[j].Cells[startCount + 2].Value = GetListFloatFlag(startpoints, i);
                            this.uiDataGridViewlog.Rows[j].Cells[startCount + 3].Value = GetListFloatFlag(endpoints, i);
                            this.uiDataGridViewlog.Rows[j].Cells[startCount + 4].Value = GetListFloatFlag(chuisaoyalis, i);

                            if (GetListBoolFlag(duoduanjiance1_hmienables, i))
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 5].Value = GetListFloatFlag(duoduanjiance1s, i);
                            else
                                SetCellDisable(j, startCount + 5);

                            if (GetListBoolFlag(duoduanjiance2_hmienables, i))
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 6].Value = GetListFloatFlag(duoduanjiance2s, i);
                            else
                                SetCellDisable(j, startCount + 6);

                            if (GetListBoolFlag(duoduanjiance3_hmienables, i))
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 7].Value = GetListFloatFlag(duoduanjiance3s, i);
                            else
                                SetCellDisable(6, startCount + 7);

                            if (GetListBoolFlag(duoduanjiance4_hmienables, i))
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 8].Value = GetListFloatFlag(duoduanjiance4s, i);
                            else
                                SetCellDisable(j, startCount + 8);

                            if (GetListBoolFlag(duoduanjiance5_hmienables, i))
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 9].Value = GetListFloatFlag(duoduanjiance5s, i);
                            else
                                SetCellDisable(j, startCount + 9);

                            this.uiDataGridViewlog.Rows[j].Cells[startCount + 10].Value = GetListFloatFlag(tongfengs, i);

                            if (GetListBoolFlag(select_yeyas, i))
                            {
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 11].Value = GetListFloatFlag(fanyings, i);
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 12].Value = GetListFloatFlag(fengbis, i);
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 13].Value = GetListFloatFlag(start_yeya_PGs, i);
                                this.uiDataGridViewlog.Rows[j].Cells[startCount + 14].Value = GetListFloatFlag(stop_yeya_PGs, i);
                            }
                            else
                            {
                                for (int k = startCount + 11; k <= startCount + 14; k++)
                                {
                                    SetCellDisable(j, k);
                                }
                            }
                        }
                        else
                        {
                            for (int k = startCount + 1; k <= startCount + 14; k++)
                            {
                                SetCellDisable(j, k);
                            }
                        }
                        startCount = parmeterCount + startCount;
                    }
                }
            });

        }
        private List<List<string>> PrepareData(List<DownloadSampleModel> sampleTables)
        {
            var result = new List<List<string>>();
            for (int j = 0; j < sampleTables.Count; j++)
            {
                var row = new List<string>();
                var sampleTable = sampleTables[j];
                var uschs = (sampleTable.usechanle ?? "").Split(',').Select(m => m == "True").ToList();
                var select_paiqis = (sampleTable.select_paiqi ?? "").Split(',').Select(m => m == "True").ToList();
                var select_yeyas = (sampleTable.select_yeya ?? "").Split(',').Select(m => m == "True").ToList();
                var select_jixies = (sampleTable.select_jixie ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance1_hmienables = (sampleTable.duoduanjiance1_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance2_hmienables = (sampleTable.duoduanjiance2_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance3_hmienables = (sampleTable.duoduanjiance3_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance4_hmienables = (sampleTable.duoduanjiance4_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance5_hmienables = (sampleTable.duoduanjiance5_hmienable ?? "").Split(',').Select(m => m == "True").ToList();


                var startpoints = (sampleTable.startpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var endpoints = (sampleTable.endpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var chuisaoyalis = (sampleTable.chuisaoyali ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance1s = (sampleTable.duoduanjiance1 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance2s = (sampleTable.duoduanjiance2 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance3s = (sampleTable.duoduanjiance3 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance4s = (sampleTable.duoduanjiance4 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance5s = (sampleTable.duoduanjiance5 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var tongfengs = (sampleTable.tongfeng ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var fanyings = (sampleTable.fanying ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var fengbis = (sampleTable.fengbi ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var start_yeya_PGs = (sampleTable.start_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var stop_yeya_PGs = (sampleTable.stop_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var start_PG_tanks = (sampleTable.start_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var end_PG_tanks = (sampleTable.end_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();

                row.Add(sampleTable.update_time.ToString("MM/dd/yyyy HH:mm:ss"));
                row.Add(sampleTable.product_id);
                row.Add(sampleTable.DCM_id);
                row.Add(sampleTable.pfname);
                row.Add(sampleTable.CA_PG == null ? "" : sampleTable.CA_PG.ToString());
                row.Add(sampleTable.zongchouqi == null ? "" : sampleTable.zongchouqi.ToString());
                row.Add(GetListFloatFlag(start_PG_tanks, 0));
                row.Add(GetListFloatFlag(start_PG_tanks, 1));
                row.Add(GetListFloatFlag(end_PG_tanks, 0));
                row.Add(GetListFloatFlag(end_PG_tanks, 1));
                int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
                var bools = new bool[totalCount];
                int startCount = 9;

                for (int i = 0; i < totalCount; i++)
                {
                    if (GetListBoolFlag(uschs, i))
                    {
                        row.Add(GetFaLeixing(GetListBoolFlag(select_paiqis, i), GetListBoolFlag(select_yeyas, i), GetListBoolFlag(select_jixies, i)));
                        row.Add(GetListFloatFlag(startpoints, i));
                        row.Add(GetListFloatFlag(endpoints, i));
                        row.Add(GetListFloatFlag(chuisaoyalis, i));

                        if (GetListBoolFlag(duoduanjiance1_hmienables, i))
                            row.Add(GetListFloatFlag(duoduanjiance1s, i));
                        else
                            row.Add("-");

                        if (GetListBoolFlag(duoduanjiance2_hmienables, i))
                            row.Add(GetListFloatFlag(duoduanjiance2s, i));
                        else
                            row.Add("-");

                        if (GetListBoolFlag(duoduanjiance3_hmienables, i))
                            row.Add(GetListFloatFlag(duoduanjiance3s, i));
                        else
                            row.Add("-");

                        if (GetListBoolFlag(duoduanjiance4_hmienables, i))
                            row.Add(GetListFloatFlag(duoduanjiance4s, i));
                        else
                            row.Add("-");

                        if (GetListBoolFlag(duoduanjiance5_hmienables, i))
                            row.Add(GetListFloatFlag(duoduanjiance5s, i));
                        else
                            row.Add("-");

                        row.Add(GetListFloatFlag(tongfengs, i));

                        if (GetListBoolFlag(select_yeyas, i))
                        {
                            row.Add(GetListFloatFlag(fanyings, i));
                            row.Add(GetListFloatFlag(fengbis, i));
                            row.Add(GetListFloatFlag(start_yeya_PGs, i));
                            row.Add(GetListFloatFlag(stop_yeya_PGs, i));
                        }
                        else
                        {
                            for (int k = startCount + 11; k <= startCount + 14; k++)
                            {
                                row.Add("-");
                            }
                        }
                    }
                    else
                    {
                        for (int k = startCount + 1; k <= startCount + 14; k++)
                        {
                            row.Add("-");
                        }
                    }
                    startCount = parmeterCount + startCount;
            
                }
                result.Add(row);
            }
            return result;

        }
        private void uiSymbolButton_excel_Click(object sender, EventArgs e)
        {
            //CommonFunction.DownloadGridViewExcel(uiDataGridViewlog);
            CommonFunction.dataGridViewToCSV(uiDataGridViewlog);
        }

        private async void uiSymbolButton_search_Click(object sender, EventArgs e)
        {
            await DoSearch();
        }
    }
}
