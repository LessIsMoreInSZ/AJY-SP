using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect;
using SPWindowsForms.DbService;
using SPWindowsForms.ExcelHelper;
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
    public partial class WAlarmTableControl : UserControl
    {
        private bool isUpdating = false;
        public WAlarmTableControl()
        {
            InitializeComponent();
            IniErrorTable1Head();
            IniSearchBox();
            uiLabel_starttime.Text = LanguageSet.SetL("chart", "uiLabel_starttime");
            uiLabel_endtime.Text = LanguageSet.SetL("chart", "uiLabel_endtime");
        }
        private void IniErrorTable1Head()
        {
            uiDataGridViewlog.RowHeadersVisible = false;
            uiDataGridViewlog.AllowUserToAddRows = false;
            uiDataGridViewlog.AllowUserToDeleteRows = false;
            uiDataGridViewlog.AllowUserToResizeRows = false;
            uiDataGridViewlog.AllowUserToResizeColumns = false;
            uiDataGridViewlog.ReadOnly = true;
            for (int i = 1; i <= 5; i++)
            {
                CommonFunction.AddCol(uiDataGridViewlog, $"logcol{i}", LanguageSet.SetL("AlarmTable", $"Header{i}"), i == 4 ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells);
            }

        }
        private void IniSearchBox()
        {
            DateTime now = DateTime.Now.AddDays(1); // 获取当前日期和时间  
            DateTime endDateToday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime startDateToday = endDateToday.AddDays(-7);
            uiDatetimePicker_start.Text = startDateToday.ToString("yyyy-MM-dd HH:mm:ss");
            uiDatetimePicker_stop.Text = endDateToday.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private async Task UpdateTableData(List<AlarmLogTable> alarmLogTables)
        {
            uiDataGridViewlog.Rows.Clear();
            if (alarmLogTables.Count == 0) return;
            uiDataGridViewlog.Rows.Add(alarmLogTables.Count);
            await Task.Run(() =>
            {
                for (int i = 0; i < alarmLogTables.Count; i++)
                {
                    var a = alarmLogTables[i];
                    uiDataGridViewlog.Rows[i].DefaultCellStyle.SelectionBackColor = a.alarmFlag ? Color.Red : Color.White;
                    uiDataGridViewlog.Rows[i].DefaultCellStyle.BackColor = a.alarmFlag ? Color.Red : Color.White;
                    uiDataGridViewlog.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                    uiDataGridViewlog.Rows[i].Cells[0].Value = a.alarmFlag ? LanguageSet.SetL("AlarmTable", "AlarmOn") : LanguageSet.SetL("AlarmTable", "AlarmOff");
                    uiDataGridViewlog.Rows[i].Cells[1].Value = a.create_time.ToString("MM/dd/yyyy HH:mm:ss");
                    uiDataGridViewlog.Rows[i].Cells[2].Value = a.alarmCode;
                    uiDataGridViewlog.Rows[i].Cells[3].Value = CommonTaskRead.GetAlarmInfoByCode(a.alarmCode);
                    uiDataGridViewlog.Rows[i].Cells[4].Value = CommonTaskRead.GetAlarmLevelByCode(a.alarmCode);
                }
            });

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

                var searchs = await AlarmLogCommon.GetAlarmLogTables(s_dateTimeStart, s_dateTimeEnd);
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
        private async void uiSymbolButton_search_Click(object sender, EventArgs e)
        {
            await DoSearch();
        }

        private void uiSymbolButton_excel_Click(object sender, EventArgs e)
        {
            CommonFunction.DownloadGridViewExcel(uiDataGridViewlog);
        }
    }
}
