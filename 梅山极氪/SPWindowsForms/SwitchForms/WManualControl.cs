using SPWindowsForms.AdsConnect;
using SPWindowsForms.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.SwitchForms
{
    public partial class WManualControl : UserControl
    {
        private List<DManualControl> dManualControls = new List<DManualControl>();
        public bool alreadyStart = false;
        private List<ManualTestResultModel> manualTestResults = new List<ManualTestResultModel>();
        private int test_count = 5;
        public WManualControl()
        {
            InitializeComponent();
            IniAllControlName();
            IniAllControls();
            IniRealTable1Head();
            IniRealTableRows();
            SdRealTimeDataTask();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Manual");
            }
        }
        private void IniAllControls()
        {
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                var dManualControl = new DManualControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(dManualControl);
                dManualControls.Add(dManualControl);
            }

        }
        private void IniRealTable1Head()
        {
            uiDataGridViewreal.RowHeadersVisible = false;
            uiDataGridViewreal.AllowUserToAddRows = false;
            uiDataGridViewreal.AllowUserToDeleteRows = false;
            uiDataGridViewreal.AllowUserToResizeRows = false;
            uiDataGridViewreal.AllowUserToResizeColumns = false;
            uiDataGridViewreal.AllowUserToOrderColumns = false;
            uiDataGridViewreal.ReadOnly = true;
            CommonFunction.AddCol(uiDataGridViewreal, "test_order", LanguageSet.SetL("Manual", "test_order"), DataGridViewAutoSizeColumnMode.AllCells);
            CommonFunction.AddCol(uiDataGridViewreal, "test_datetime", LanguageSet.SetL("Manual", "test_datetime"), DataGridViewAutoSizeColumnMode.AllCells);
            CommonFunction.AddCol(uiDataGridViewreal, "test_item", LanguageSet.SetL("Manual", "test_item"), DataGridViewAutoSizeColumnMode.AllCells);
            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                CommonFunction.AddCol(uiDataGridViewreal, $"test_chn{i}", $"D{i}");
            }
        }
        private void IniRealTableRows()
        {
            this.uiDataGridViewreal.Rows.Clear();
            #region 第一列
            this.uiDataGridViewreal.Rows.Add(test_count * 5);

            #endregion
        }
        private void SetTableData()
        {
            for (int i = 0; i < test_count; i++)
            {
                if (manualTestResults.Count > i)
                {
                    var _data = manualTestResults[i];
                    uiDataGridViewreal.Rows[5 * i].Cells[0].Value = i + 1;
                    uiDataGridViewreal.Rows[5 * i].Cells[1].Value = _data.dateTime.ToString("MM/dd/yyyy HH:mm:ss");
                    uiDataGridViewreal.Rows[5 * i].Cells[2].Value = LanguageSet.SetL("Manual", "uiLabel_result3");
                    uiDataGridViewreal.Rows[5 * i+1].Cells[2].Value = LanguageSet.SetL("Manual", "uiLabel_result4");
                    uiDataGridViewreal.Rows[5 * i+2].Cells[2].Value = LanguageSet.SetL("Manual", "uiLabel_result5");
                    uiDataGridViewreal.Rows[5 * i+3].Cells[2].Value = LanguageSet.SetL("Manual", "uiLabel_result6");
                    uiDataGridViewreal.Rows[5 * i+4].Cells[2].Value = LanguageSet.SetL("Manual", "uiLabel_result7");
                    for (int c = 0; c < 3; c++)
                    {
                        for (int r = 0; r < 5; r++)
                        {
                            uiDataGridViewreal.Rows[r + 5 * i].Cells[c].Style.BackColor = Color.White;
                        }
                    }
                    for (int n = 1; n <= GlobalVar.systemSetting.d_chn_count; n++)
                    {
                        var _chn = _data.results.FirstOrDefault(m => m.chn == n);
                        if (_chn != null)
                        {
                            uiDataGridViewreal.Rows[5 * i].Cells[2 + n].Value = _chn.Fr_P;
                            uiDataGridViewreal.Rows[5 * i + 1].Cells[2 + n].Value = _chn.Se_P;
                            uiDataGridViewreal.Rows[5 * i + 2].Cells[2 + n].Value = _chn.close_time;
                            uiDataGridViewreal.Rows[5 * i + 3].Cells[2 + n].Value = _chn.deltaP;
                            uiDataGridViewreal.Rows[5 * i + 4].Cells[2 + n].Value = _chn.sulv_P;
                            for (int r = 0; r < 5; r++)
                            {
                                uiDataGridViewreal.Rows[5 * i + r].Cells[2 + n].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            for (int r = 0; r < 5; r++)
                            {
                                uiDataGridViewreal.Rows[5 * i + r].Cells[2 + n].Value = "";
                                uiDataGridViewreal.Rows[5 * i + r].Cells[2 + n].Style.BackColor = Color.LightGray;
                            }

                        }
                    }
                }
                else
                {
                    for (int c = 0; c < uiDataGridViewreal.ColumnCount; c++)
                    {
                        for (int r = 0; r < 5; r++)
                        {
                            uiDataGridViewreal.Rows[r + 5 * i].Cells[c].Value = "";
                            uiDataGridViewreal.Rows[r + 5 * i].Cells[c].Style.BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }
       
        private void GetTestData()
        {
            try
            {
                var result = new ManualTestResultModel();
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    if (GlobalVar.plcData.hmi_test.use_ch[i])
                    {
                        var data = new ManualTestResultChnModel();
                        data.chn = i + 1;
                        data.Fr_P = GlobalVar.plcData.hmi_test._out[i].Fr_P;
                        data.Se_P = GlobalVar.plcData.hmi_test._out[i].Se_P;
                        data.close_time = GlobalVar.plcData.hmi_test._out[i].close_time;
                        data.deltaP = GlobalVar.plcData.hmi_test._out[i].deltaP;
                        data.sulv_P = GlobalVar.plcData.hmi_test._out[i].sulv_P;
                        result.results.Add(data);
                    }
                }
                result.dateTime = DateTime.Now;
                manualTestResults.Add(result);
                manualTestResults = manualTestResults.OrderByDescending(m => m.dateTime).ToList();
                if (manualTestResults.Count > test_count)
                    manualTestResults = manualTestResults.Take(test_count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GetTestData::" + ex.Message);
            }
        }
        private void SdRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.手动测试)
                        {
                            CommonTaskRead.ReadhmiDCETime();
                            CommonTaskRead.Readhmi_test();
                            if (!alreadyStart)
                            {
                                IniAllData();
                                dManualControls.ForEach(d => { d.SetCheckBoxEnable(CommonFunction.CheckAccessGongyi()); });
                            }
                            SetAllData();
                            CommonTaskRead.Readhmi_test_end();
                            if (GlobalVar.plcData.hmi_test_end)
                            {
                                CommonTaskRead.Writehmi_test_end_False();
                                GetTestData();
                                SetTableData();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Work.frm_main.ShowErrorMessage(ex.Message);

                    }
                    finally
                    {
                        int millisecondsTimeout = 200 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }
                }

            });
        }

        private void SetAllData()
        {
            try
            {
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    dManualControls[i].ShowData(GlobalVar.plcData.hmi_D[i], GlobalVar.plcData.hmi_test._out[i]);
                }
                CommonAdsUi.SetBtnColorGrayAndGreen(button_guandao, GlobalVar.plcData.hmi_test.butt_test_guandao);
                CommonAdsUi.SetBtnColorGrayAndGreen(button_moqiang, GlobalVar.plcData.hmi_test.butt_test_moqiang);
                CommonAdsUi.SetBtnColorGrayAndGreen(button_start, GlobalVar.plcData.hmi_test.test_busy);
                CommonAdsUi.SetBtnColorGrayAndGreen(button_stop, GlobalVar.plcData.hmi_test.butt_test_stop);
                if (!CommonFunction.CheckAccessGongyi())
                {
                    button_guandao.Enabled = false;
                    button_moqiang.Enabled = false;
                    button_start.Enabled = false;
                    button_stop.Enabled = false;
                    numericUpDown1.Enabled = false;
                    numericUpDown2.Enabled = false;
                }
                else
                {
                    button_guandao.Enabled = true;
                    button_moqiang.Enabled = true;
                    button_start.Enabled = true;
                    button_stop.Enabled = true;
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                }
                CommonAdsUi.SetLightOnOff(light_test_conditions1, GlobalVar.plcData.hmi_test.Test_Conditions);
                CommonAdsUi.SetLightOnOff(light_test_conditions2, GlobalVar.plcData.hmi_test.TestConditions[0]);
                CommonAdsUi.SetLightOnOff(light_test_conditions3, GlobalVar.plcData.hmi_test.TestConditions[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Manual SetAllData::" + ex.Message);
            }
        }

        private void IniAllData()
        {
            try
            {
                this.numericUpDown1.Value = GlobalVar.plcData.hmi_test.test_time;
                this.numericUpDown2.Value = GlobalVar.plcData.hmi_test.holding_time;
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    dManualControls[i].IniShowData(GlobalVar.plcData.hmi_test.use_ch[i]);
                }

                alreadyStart = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Manual IniAllData::" + ex.Message);
            }
        }

        private void button_guandao_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.管道测试, !GlobalVar.plcData.hmi_test.butt_test_guandao);

        }

        private void button_moqiang_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.模腔测试, !GlobalVar.plcData.hmi_test.butt_test_moqiang);
        }

        private void button_start_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.测试开始, true);
        }

        private void button_start_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.测试开始, false);
        }

        private void button_stop_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.测试停止, true);
        }

        private void button_stop_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteManualBtnBool((int)SetManualButtonOrder.测试停止, false);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!alreadyStart) return;
                CommonTaskRead.WriteManualShort((int)SetManualButtonOrder.测试时间, (short)numericUpDown1.Value);
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!alreadyStart) return;
                CommonTaskRead.WriteManualShort((int)SetManualButtonOrder.保压时间, (short)numericUpDown2.Value);
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
    }
}
