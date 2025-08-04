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
    public partial class MachinePfLogControl : UserControl
    {
        private bool isUpdating = false;
        private BottomPageSwitch bSwitch;
        public MachinePfLogControl()
        {
            InitializeComponent();
            IniPfTable1Head();
            IniSearchBox();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.配方Log);
            uiLabel_starttime.Text = LanguageSet.SetL("chart", "uiLabel_starttime");
            uiLabel_endtime.Text = LanguageSet.SetL("chart", "uiLabel_endtime");
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();

        }
        private void IniPfTable1Head()
        {
            uiDataGridViewlog.RowHeadersVisible = false;
            uiDataGridViewlog.AllowUserToAddRows = false;
            uiDataGridViewlog.AllowUserToDeleteRows = false;
            uiDataGridViewlog.AllowUserToResizeRows = false;
            uiDataGridViewlog.AllowUserToResizeColumns = false;
            uiDataGridViewlog.ReadOnly = true;
            for (int i = 1; i <= 7; i++)
            {
                CommonFunction.AddCol(uiDataGridViewlog, $"pflogcol{i}", LanguageSet.SetL("Pflog", $"pflogcol{i}"), i == 7 ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells);
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
        private string GetLogType(int? _logtype)
        {
            switch (_logtype)
            {
                case (int)ePfLog.新建:
                    return LanguageSet.SetL("Pflog", "logtypenew");
                case (int)ePfLog.删除:
                    return LanguageSet.SetL("Pflog", "logtypedelete");
                case (int)ePfLog.修改:
                    return LanguageSet.SetL("Pflog", "logtypemodify");
                case (int)ePfLog.重命名:
                    return LanguageSet.SetL("Pflog", "logtyperename");
                default:
                    return "";
            }
        }
         private async Task UpdateTableData(List<PfLogTable> pfLogTables)
        {
            uiDataGridViewlog.Rows.Clear();
            if (pfLogTables.Count == 0) return;
            uiDataGridViewlog.Rows.Add(pfLogTables.Count);
            await Task.Run(() =>
            {
                for (int i = 0; i < pfLogTables.Count; i++)
            {
                var _log = pfLogTables[i];
                uiDataGridViewlog.Rows[i].Cells[0].Value = _log.create_time.ToString("MM/dd/yyyy HH:mm:ss");
                uiDataGridViewlog.Rows[i].Cells[1].Value = _log.useraccount;
                uiDataGridViewlog.Rows[i].Cells[2].Value = _log.pfname;
                uiDataGridViewlog.Rows[i].Cells[3].Value = GetLogType(_log.logType);
                uiDataGridViewlog.Rows[i].Cells[4].Value = _log.logBefore;
                uiDataGridViewlog.Rows[i].Cells[5].Value = _log.logAfter;
                uiDataGridViewlog.Rows[i].Cells[6].Value = _log.logData;
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

                var searchs =await PFLogCommon.GetPfLogTables(s_dateTimeStart, s_dateTimeEnd);
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

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowYSG();
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowBaoYang();
        }

        private void uiSymbolButton_excel_Click(object sender, EventArgs e)
        {
            CommonFunction.DownloadGridViewExcel(uiDataGridViewlog);
        }
    }
}
