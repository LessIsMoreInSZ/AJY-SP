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
    public partial class WAutoControl : UserControl
    {
        private List<UILight> uILights = new List<UILight>();
        public WAutoControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            AutoRealTimeDataTask();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Auto");
            }
            uILights.Add(uiLight_c1);
            uILights.Add(uiLight_c2);
            uILights.Add(uiLight_c3);
            uILights.Add(uiLight_c4);
            uILights.Add(uiLight_c5);
            uILights.Add(uiLight_c6);
            uILights.Add(uiLight7);
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.自动)
                        {
                            CommonTaskRead.ReadAuto();
                            //GlobalVar.plcData.HMI_enable_ls = GlobalVar.commonAdsControl.ReadCommonBool2(".HMI_enable_ls");
                            SetAllData();

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
                CommonAdsUi.SetLightOnOff(uiLight_auto, GlobalVar.plcData.hmi_auto.auto_condition);
                for(int i = 0; i < 6; i++)
                {
                    CommonAdsUi.SetLightOnOff(uILights[i], GlobalVar.plcData.hmi_auto.condition[i]);
                }
                CommonAdsUi.SetLightOnOff(uILights[6], GlobalVar.plcData.hmi_St_Layer.DcmOpen);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_start, GlobalVar.plcData.hmi_auto.status_autoruning);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_stop, GlobalVar.plcData.hmi_auto.butt_autostop);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_air, GlobalVar.plcData.hmi_auto.butt_useVac_hotmould);

                // 限位报警
                //CommonAdsUi.SetBtnColorGrayAndGreen(uiSymbolButton1, GlobalVar.plcData.HMI_enable_ls);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Auto SetAllData::" + ex.Message);
            }
        }

        private void btn_start_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteAutoBtnBool((int)SetAutoButtonOrder.自动启动, true);
        }

        private void btn_start_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteAutoBtnBool((int)SetAutoButtonOrder.自动启动, false);
        }

        private void btn_stop_MouseDown(object sender, MouseEventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.WriteAutoBtnBool((int)SetAutoButtonOrder.退出循环, true);
        }

        private void btn_stop_MouseUp(object sender, MouseEventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.WriteAutoBtnBool((int)SetAutoButtonOrder.退出循环, false);
        }

        private void btn_air_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.WriteAutoBtnBool((int)SetAutoButtonOrder.热模真空, !GlobalVar.plcData.hmi_auto.butt_useVac_hotmould) ;
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            //GlobalVar.commonAdsControl.WriteCommonBool2(".HMI_enable_ls", !GlobalVar.plcData.HMI_enable_ls);
        }
    }
}
