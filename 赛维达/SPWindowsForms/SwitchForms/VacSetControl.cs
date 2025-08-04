using SPWindowsForms.AdsConnect;
using Sunny.UI;
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
    public partial class VacSetControl : UserControl
    {
        public VacSetControl()
        {
            InitializeComponent();
            IniAllControlName();
            AutoRealTimeDataTask();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "VacConfig");
                if (ctrl is UIGroupBox)
                {
                    var _c = (UIGroupBox)ctrl;
                    foreach (var gctrl in _c.Controls)
                    {
                        LanguageSet.SetLanguageByData(gctrl, "VacConfig");
                    }
                }
            }
            var u = LanguageSet.SetL("VacConfig", "btn_use");
            btn_moqiang_1.Text = u;
            btn_moqiang_2.Text = u;
            btn_moqiang_3.Text = u;
            btn_moqiang_4.Text = u;
            btn_moqiang_5.Text = u;
            btn_moqiang_6.Text = u;
            btn_moqiang_7.Text = u;
            btn_moqiang_8.Text = u;
            btn_liaotong_1_1.Text = u;
            btn_liaotong_1_2.Text = u;
            btn_liaotong_1_3.Text = u;
            btn_liaotong_1_4.Text = u;
            btn_liaotong_1_5.Text = u;
            btn_liaotong_1_6.Text = u;
            btn_liaotong_1_7.Text = u;
            btn_liaotong_1_8.Text = u;
            btn_liaotong_1_9.Text = u;
            btn_liaotong_1_10.Text = u;
            btn_liaotong_2_1.Text = u;
            btn_liaotong_2_2.Text = u;
            btn_liaotong_2_3.Text = u;
            btn_liaotong_2_4.Text = u;
            btn_liaotong_2_5.Text = u;
            btn_liaotong_2_6.Text = u;
            btn_liaotong_2_7.Text = u;
            btn_liaotong_2_8.Text = u;
            btn_liaotong_2_9.Text = u;
            btn_liaotong_2_10.Text = u;
        }
        private void AutoRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.真空源设置)
                        {
                            CommonTaskRead.ReadVacsourc();
                            CommonTaskRead.ReadUPS();
                            SetAllData();

                        }
                    }
                    catch (Exception ex)
                    {
                        Work.frm_main.ShowErrorMessage(ex.Message);

                    }
                    finally
                    {
                        int millisecondsTimeout = 500 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
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
                CommonAdsUi.SetUiButtonByInt(new List<UIButton> { btn_moqiang_1,btn_moqiang_2,btn_moqiang_3,btn_moqiang_4,btn_moqiang_5,btn_moqiang_6,btn_moqiang_7
                }, GlobalVar.plcData.UPS.Vacsource_D);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_moqiang_8, GlobalVar.plcData.UPS.use_standbyV6);

                CommonAdsUi.SetUiButtonByInt(new List<UIButton> { btn_liaotong_1_1,btn_liaotong_1_2,btn_liaotong_1_3
                }, GlobalVar.plcData.UPS.Vacsource_C);
                CommonAdsUi.SetUiButtonByInt(new List<UIButton> { btn_liaotong_2_1,btn_liaotong_2_2,btn_liaotong_2_3
                }, GlobalVar.plcData.UPS.Vacsource_E);

                CommonAdsUi.SetUiButtonByInt(new List<UIButton> { btn_liaotong_1_4,btn_liaotong_1_5,btn_liaotong_1_6,
                    btn_liaotong_1_7,btn_liaotong_1_8,btn_liaotong_1_9,btn_liaotong_1_10
                }, GlobalVar.plcData.UPS.set_pump_C);
                CommonAdsUi.SetUiButtonVisible(new List<UIButton> { btn_liaotong_1_4,btn_liaotong_1_5,btn_liaotong_1_6,
                    btn_liaotong_1_7,btn_liaotong_1_8,btn_liaotong_1_9,btn_liaotong_1_10
                }, GlobalVar.plcData.hmi_Vacsource.busy_reveal_C);

                CommonAdsUi.SetUiButtonByInt(new List<UIButton> { btn_liaotong_2_4,btn_liaotong_2_5,btn_liaotong_2_6,
                    btn_liaotong_2_7,btn_liaotong_2_8,btn_liaotong_2_9,btn_liaotong_2_10
                }, GlobalVar.plcData.UPS.set_pump_E);
                CommonAdsUi.SetUiButtonVisible(new List<UIButton> { btn_liaotong_2_4,btn_liaotong_2_5,btn_liaotong_2_6,
                    btn_liaotong_2_7,btn_liaotong_2_8,btn_liaotong_2_9,btn_liaotong_2_10
                }, GlobalVar.plcData.hmi_Vacsource.busy_reveal_E);
            }
            catch (Exception ex)
            {
                throw new Exception(" SetAllData::" + ex.Message);
            }
        }
        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.Show9001Pic();
        }

        private void btn_moqiang_1_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 1);
        }

        private void btn_moqiang_2_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 2);
        }

        private void btn_moqiang_3_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 4);
        }

        private void btn_moqiang_4_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 8);
        }

        private void btn_moqiang_5_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 16);
        }

        private void btn_moqiang_6_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 32);
        }

        private void btn_moqiang_7_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_D, 64);
        }

        private void btn_liaotong_1_1_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_C, 1);
        }

        private void btn_liaotong_1_2_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_C, 2);
        }

        private void btn_liaotong_1_3_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_C, 4);
        }

        private void btn_liaotong_2_1_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_E, 1);
        }

        private void btn_liaotong_2_2_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_E, 2);
        }

        private void btn_liaotong_2_3_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.VAC_E, 4);
        }

        private void btn_liaotong_1_4_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 1);
        }

        private void btn_liaotong_1_5_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 2);
        }

        private void btn_liaotong_1_6_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 4);
        }

        private void btn_liaotong_1_7_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 8);
        }

        private void btn_liaotong_1_8_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 16);
        }

        private void btn_liaotong_1_9_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 32);
        }

        private void btn_liaotong_1_10_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_C, 64);
        }

        private void btn_liaotong_2_4_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 1);
        }

        private void btn_liaotong_2_5_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 2);
        }

        private void btn_liaotong_2_6_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 4);
        }

        private void btn_liaotong_2_7_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 8);
        }

        private void btn_liaotong_2_8_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 16);
        }

        private void btn_liaotong_2_9_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 32);
        }

        private void btn_liaotong_2_10_Click(object sender, EventArgs e)
        {
            CommonTaskRead.WriteVacSetShort((int)VacSetShort.PUMP_E, 64);
        }

        private void btn_moqiang_8_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 15, !GlobalVar.plcData.UPS.use_standbyV6);
        }
    }
}
