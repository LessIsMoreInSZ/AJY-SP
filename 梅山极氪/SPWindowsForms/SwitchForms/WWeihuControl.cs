using SPWindowsForms.AdsConnect;
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
    public partial class WWeihuControl : UserControl
    {
        private List<DWeiHuControl> dWeiHuControls = new List<DWeiHuControl>();
        private List<CWeiHuControl> cWeiHuControls = new List<CWeiHuControl>();
        private List<EWeiHuControl> eWeiHuControls = new List<EWeiHuControl>();
        private BottomPageSwitch bSwitch;
        public WWeihuControl()
        {
            InitializeComponent();
            IniAllControls();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.设备维护);
            WHRealTimeDataTask();
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();
        }
        private void IniAllControls()
        {
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                var dWeiHuControl = new DWeiHuControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(dWeiHuControl);
                dWeiHuControls.Add(dWeiHuControl);
            }

            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                var cWeiHuControl = new CWeiHuControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(cWeiHuControl);
                cWeiHuControls.Add(cWeiHuControl);
            }
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                var eWeiHuControl = new EWeiHuControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(eWeiHuControl);
                eWeiHuControls.Add(eWeiHuControl);
            }

        }
        private void SetAllData()
        {
            try
            {
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    dWeiHuControls[i].DisplayData(GlobalVar.plcData.hmi_D[i]);
                }
                for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                {
                    cWeiHuControls[i].DisplayData(GlobalVar.plcData.hmi_C[i]);
                }
                for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                {
                    eWeiHuControls[i].DisplayData(GlobalVar.plcData.hmi_E[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Weihu SetAllData::" + ex.Message);
            }
        }
        private void WHRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.设备维护)
                        {
                            CommonTaskRead.ReadhmiDCETime();
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

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowBengzu();
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowSheBeiGuanLi();
        }
    }
}
