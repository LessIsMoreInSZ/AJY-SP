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
    public partial class WZhujiControl : UserControl
    {
        private BottomPageSwitch bSwitch;
        public WZhujiControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            bSwitch= CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.主机信号模拟);
            RealTimeDataTask();
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Zhuji");
                if (ctrl is UIGroupBox)
                {
                    var _c = (UIGroupBox)ctrl;
                    foreach (var gctrl in _c.Controls)
                    {
                        LanguageSet.SetLanguageByData(gctrl, "Zhuji");
                    }
                }
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.主机信号模拟)
                        {
                            CommonTaskRead.ReadZhuji();
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
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_usetest, GlobalVar.plcData.DCM_usetest);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttAutoTest, GlobalVar.plcData.DCM_buttAutoTest);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttSemiTest, GlobalVar.plcData.DCM_buttSemiTest);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttMantest, GlobalVar.plcData.DCM_buttMantest);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttTestStart, GlobalVar.plcData.DCM_buttTestStart);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttClose_HotMold, GlobalVar.plcData.DCM_buttClose_HotMold);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttClose, GlobalVar.plcData.DCM_buttClose);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttOpen, GlobalVar.plcData.DCM_buttOpen);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttZero, GlobalVar.plcData.DCM_buttZero);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttPurge, GlobalVar.plcData.DCM_buttPurge);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttHotMold, GlobalVar.plcData.DCM_buttHotMold);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttPos_add, GlobalVar.plcData.DCM_buttPos_add);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_DCM_buttPos_sub, GlobalVar.plcData.DCM_buttPos_sub);


            }
            catch (Exception ex)
            {
                throw new Exception("Zhuji SetAllData::" + ex.Message);
            }
        }

        private void btn_DCM_buttTestStart_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttTestStart", true);
        }

        private void btn_DCM_buttTestStart_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttTestStart", false);
        }

        private void btn_DCM_buttPos_add_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_add", true);
        }

        private void btn_DCM_buttPos_add_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_add", false);
        }

        private void btn_DCM_buttPos_sub_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_sub", true);
        }

        private void btn_DCM_buttPos_sub_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPos_sub", false);
        }

        private void btn_DCM_usetest_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_usetest", !GlobalVar.plcData.DCM_usetest);
        }

        private void btn_DCM_buttAutoTest_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttAutoTest", !GlobalVar.plcData.DCM_buttAutoTest);
        }

        private void btn_DCM_buttSemiTest_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttSemiTest", !GlobalVar.plcData.DCM_buttSemiTest);
        }

        private void btn_DCM_buttMantest_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttMantest", !GlobalVar.plcData.DCM_buttMantest);
        }

        /// <summary>
        /// 信号模拟自动热模
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DCM_buttClose_HotMold_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttClose_HotMold", !GlobalVar.plcData.DCM_buttClose_HotMold);
        }

        /// <summary>
        /// 信号模拟测试 开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DCM_buttClose_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttClose", !GlobalVar.plcData.DCM_buttClose);
        }

        /// <summary>
        /// 手动开模
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DCM_buttOpen_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttOpen", !GlobalVar.plcData.DCM_buttOpen);
        }

        /// <summary>
        /// 手动零位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DCM_buttZero_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttZero", !GlobalVar.plcData.DCM_buttZero);
        }

        private void btn_DCM_buttPurge_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttPurge", !GlobalVar.plcData.DCM_buttPurge);
        }

        private void btn_DCM_buttHotMold_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(".DCM_buttHotMold", !GlobalVar.plcData.DCM_buttHotMold);
        }
    }
}
