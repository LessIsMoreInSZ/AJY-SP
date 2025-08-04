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
    public partial class WBenzuControl : UserControl
    {
        public List<UIButton> uIButtons = new List<UIButton>();
        public List<UIButton> uIBengPuButtons = new List<UIButton>();
        public List<UIButton> tank6002Buttons = new List<UIButton>();
        private BottomPageSwitch bSwitch;
        public WBenzuControl()
        {
            InitializeComponent();
            SetControlVisiable();
            IniAllControlName();
            uIButtons.Add(danDongButton1);
            uIButtons.Add(danDongButton2);
            uIButtons.Add(danDongButton3);
            uIButtons.Add(danDongButton4);
            uIButtons.Add(danDongButton5);
            uIButtons.Add(danDongButton6);
            uIButtons.Add(danDongButton7);
            uIButtons.Add(danDongButton8);
            uIButtons.Add(danDongButton9);
            uIButtons.Add(danDongButton10);
            uIButtons.Add(danDongButton11);
            uIButtons.Add(danDongButton12);
            uIButtons.Add(danDongButton13);
            uIButtons.Add(danDongButton14);
            uIButtons.Add(danDongButton15);
            uIBengPuButtons.Add(danDongButton16);
            uIBengPuButtons.Add(danDongButton17);
            uIBengPuButtons.Add(danDongButton18);

            tank6002Buttons.Add(btntankv1);
            tank6002Buttons.Add(btntankv2);
            tank6002Buttons.Add(btntankv3);
            tank6002Buttons.Add(btntankv4);
            tank6002Buttons.Add(btntankv5);

            uIButtons.ForEach(b =>
            {
                b.MouseDown += WeihuMouse_Down;
                b.MouseUp += WeihuMouse_Up;
            });
            uIBengPuButtons.ForEach(b =>
            {
                b.MouseDown += BengPuMouse_Down;
                b.MouseUp += BengPuMouse_Up;
            });
            tank6002Buttons.ForEach(b =>
            {
                b.MouseDown += TankV6002Mouse_Down;
                b.MouseUp += TankV6002Mouse_Up;
            });
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.泵组维护);
            RealTimeDataTask();
        }

        public void SetControlVisiable()
        {
            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP9001")
            {
                danDongButton7.Visible = false;
                danDongButton8.Visible = false;
                danDongButton9.Visible = false;
                danDongButton10.Visible = false;
                danDongButton11.Visible = false;
                danDongButton12.Visible = false;
                danDongButton13.Visible = false;
                danDongButton14.Visible = false;
                danDongButton15.Visible = false;
            }

            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP6002")
            {
                groupBoxTank6002.Visible = false;
                btntankv1.Visible = false;
                btntankv2.Visible = false;
                btntankv3.Visible = false;
                btntankv4.Visible = false;
                btntankv5.Visible = false;
            }
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Bengzu");
                if (ctrl is UIGroupBox)
                {
                    var _c = (UIGroupBox)ctrl;
                    foreach (var gctrl in _c.Controls)
                    {
                        LanguageSet.SetLanguageByData(gctrl, "Bengzu");
                    }
                }
            }
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowWeihu();
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowIO();
        }
        private void WeihuMouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_Vacsource_name, _tag, true);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void WeihuMouse_Up(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_Vacsource_name, _tag,false);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }


        private void TankV6002Mouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_Vacsource_name, _tag, true);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void TankV6002Mouse_Up(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_Vacsource_name, _tag, false);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void BengPuMouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBool(GlobalVar.commonAdsControl.hmi_butt_ManPUMP_name, _tag, true);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void BengPuMouse_Up(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBool(GlobalVar.commonAdsControl.hmi_butt_ManPUMP_name, _tag, false);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void RealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.泵组维护)
                        {
                            CommonTaskRead.ReadVacsourc();
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
        public void SetCheckBox()
        {
            checkbox_famen.Checked = GlobalVar.plcData.enable_ManAction;
        }
        private void SetAllData()
        {
            try
            {
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton1, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton2, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton3, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[2]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton4, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[3]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton5, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[4]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton6, GlobalVar.plcData.hmi_Vacsource.hmi_butt_pumps_V[5]);

                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton7, GlobalVar.plcData.hmi_Vacsource.hmi_butt_VY1_pump[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton8, GlobalVar.plcData.hmi_Vacsource.hmi_butt_VY1_pump[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton9, GlobalVar.plcData.hmi_Vacsource.hmi_butt_VY1_pump[2]);

                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton10, GlobalVar.plcData.hmi_Vacsource.hmi_butt_V2_pump[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton11, GlobalVar.plcData.hmi_Vacsource.hmi_butt_V2_pump[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton12, GlobalVar.plcData.hmi_Vacsource.hmi_butt_V2_pump[2]);

                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton13, GlobalVar.plcData.hmi_Vacsource.hmi_butt_tank1_v[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton14, GlobalVar.plcData.hmi_Vacsource.hmi_butt_tank1_v[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton15, GlobalVar.plcData.hmi_Vacsource.hmi_butt_tank2_v1);

                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton16, GlobalVar.plcData.hmi_Vacsource.status_pump[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton17, GlobalVar.plcData.hmi_Vacsource.status_pump[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(danDongButton18, GlobalVar.plcData.hmi_Vacsource.status_pump[2]);


            }
            catch (Exception ex)
            {
                throw new Exception("Bengzu SetAllData::" + ex.Message);
            }
        }

        private void checkbox_famen_CheckedChanged(object sender, EventArgs e)
        {
            CommonTaskRead.WriteEnable_ManAction_name(checkbox_famen.Checked);
        }
    }
}
