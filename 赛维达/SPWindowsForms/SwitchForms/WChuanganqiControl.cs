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
    public partial class WChuanganqiControl : UserControl
    {
        public bool loadflag = false;
        public List<PGsVacControl> pGsVacControls;
        public List<PGsDControl> pGsDVacControls;
        public List<PGsCControl> pGsCVacControls;
        private BottomPageSwitch bSwitch;
        public WChuanganqiControl()
        {
            InitializeComponent();
        
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            AddMyControls();
            bSwitch= CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.传感器校准);
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
                LanguageSet.SetLanguageByData(ctrl, "Chuanganqi");
                if (ctrl is UIGroupBox)
                {
                    var _c = (UIGroupBox)ctrl;
                    foreach (var gctrl in _c.Controls)
                    {
                        LanguageSet.SetLanguageByData(gctrl, "Chuanganqi");
                    }
                }
            }
        }
        public void AddMyControls()
        {
            #region tank
            pGsVacControls = new List<PGsVacControl>();
            var tank1 = new PGsVacControl("TANK", 1, 64 * 10 + 32 * GlobalVar.systemSetting.c_chn_count + 16 * GlobalVar.systemSetting.e_chn_count);
            var tank2 = new PGsVacControl("TANK", 2, 64 * 10 + 32 * GlobalVar.systemSetting.c_chn_count + 16 * GlobalVar.systemSetting.e_chn_count + 16);
            uiFlowLayoutPanel1.Controls.Add(tank1);
            uiFlowLayoutPanel1.Controls.Add(tank2);
            pGsVacControls.Add(tank1);
            pGsVacControls.Add(tank2);

            #endregion
            #region PGSD
            pGsDVacControls = new List<PGsDControl>();
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                var dpgControl = new PGsDControl(i + 1, 64 * i);
                uiFlowLayoutPanel1.Controls.Add(dpgControl);
                pGsDVacControls.Add(dpgControl);
            }
            #endregion
            #region PGSC
            pGsCVacControls = new List<PGsCControl>();
            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                var cpgControl = new PGsCControl(i + 1, 64 * 10 + 32 * i);
                uiFlowLayoutPanel1.Controls.Add(cpgControl);
                pGsCVacControls.Add(cpgControl);
            }
            #endregion
            #region E AIR
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                var epgControl = new PGsVacControl("E", i + 1, 64 * 10 + 32 * GlobalVar.systemSetting.c_chn_count + i * 16);
                uiFlowLayoutPanel1.Controls.Add(epgControl);
                pGsVacControls.Add(epgControl);
            }
            var airControl = new PGsVacControl("Air", 0, 64 * 10 + 32 * GlobalVar.systemSetting.c_chn_count + 16 * GlobalVar.systemSetting.e_chn_count + 32);
            uiFlowLayoutPanel1.Controls.Add(airControl);
            pGsVacControls.Add(airControl);
            #endregion
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.传感器校准)
                        {
                            CommonTaskRead.ReadUPS_PG();
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
            pGsVacControls.ForEach(p => p.SetInputValue());
            pGsDVacControls.ForEach(p => p.SetInputValue());
            pGsCVacControls.ForEach(p => p.SetInputValue());
        }
        private void SetAllDataNoInput()
        {
            pGsVacControls.ForEach(p => p.SetLabelValue());
            pGsDVacControls.ForEach(p => p.SetLabelValue());
            pGsCVacControls.ForEach(p => p.SetLabelValue());

        }
    }
}
