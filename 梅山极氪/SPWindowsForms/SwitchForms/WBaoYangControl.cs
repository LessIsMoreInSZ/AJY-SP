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
    public partial class WBaoYangControl : UserControl
    {
        public List<AirFilterControl> airFilterControls;
        public bool loadflag = false;
        private BottomPageSwitch bSwitch;
        public WBaoYangControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            AddMyControls();
            bSwitch= CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.设备保养);
            RealTimeDataTask();
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();

        }
        public void AddMyControls()
        {
            uiFlowLayoutPanel1.Controls.Add(new AirFilterTitleControl(true));
            airFilterControls = new List<AirFilterControl>();
            for (int i = 1; i <= 3; i++)
            {
                var filterControl = new AirFilterControl(i,1,"");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            uiFlowLayoutPanel1.Controls.Add(new AirFilterTitleControl(false));
            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 2, "D");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.c_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 2, "C");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.e_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 2, "E");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 3, "D");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.c_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 3, "C");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.e_chn_count; i++)
            {
                var filterControl = new AirFilterControl(i, 3, "E");
                uiFlowLayoutPanel1.Controls.Add(filterControl);
                airFilterControls.Add(filterControl);
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.设备保养)
                        {
                            CommonTaskRead.ReadKsystem();
                            CommonTaskRead.ReadUPS();
                            CommonTaskRead.ReadHMI_Timing();
                            //SetAllData();
                            SetAllDataNoInput();
                            if (loadflag)
                            {
                                SetAllDataInput();
                                loadflag = false;
                            }

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
        private void SetAllDataInput()
        {
            try
            {
                airFilterControls.ForEach(a => { a.SetAllDataInput(); });
            }
            catch (Exception ex)
            {
                throw new Exception("SetAllDataNoInput SetAllDataInput::" + ex.Message);
            }
        }
        private void SetAllDataNoInput()
        {
            try
            {
                airFilterControls.ForEach(a => { a.SetAllDataNoInput(); });
            }
            catch (Exception ex)
            {
                throw new Exception("SetAllDataNoInput SetAllData::" + ex.Message);
            }
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            //Work.frm_main.ShowPfLog();
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowYeYang();
        }
    }
}
