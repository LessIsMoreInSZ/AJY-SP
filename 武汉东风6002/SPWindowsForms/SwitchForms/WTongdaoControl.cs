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
    public partial class WTongdaoControl : UserControl
    {
        private List<DTongdaoControl> dTongdaoControls = new List<DTongdaoControl>();
        private List<CTongdaoControl> cTongdaoControls = new List<CTongdaoControl>();
        private List<ETongdaoControl> eTongdaoControls = new List<ETongdaoControl>();
        public WTongdaoControl()
        {
            InitializeComponent();
            IniAllControls();
            TdRealTimeDataTask();
        }
        private void IniAllControls()
        {
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                var dTongdaoControl = new DTongdaoControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(dTongdaoControl);
                dTongdaoControls.Add(dTongdaoControl);
            }

            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                var cTongdaoControl = new CTongdaoControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(cTongdaoControl);
                cTongdaoControls.Add(cTongdaoControl);
            }
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                var eTongdaoControl = new ETongdaoControl(i + 1);
                uiFlowLayoutPanel1.Controls.Add(eTongdaoControl);
                eTongdaoControls.Add(eTongdaoControl);
            }

        }
        private void SetAllData()
        {
            try
            {
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    dTongdaoControls[i].DisplayData(GlobalVar.plcData.hmi_D[i]);
                }
                for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                {
                    cTongdaoControls[i].DisplayData(GlobalVar.plcData.hmi_C[i]);
                }
                for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                {
                    eTongdaoControls[i].DisplayData(GlobalVar.plcData.hmi_E[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Tongdao SetAllData::" + ex.Message);
            }
        }
        private void TdRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.通道概览)
                        {
                            CommonTaskRead.ReadhmiDCETime();
                            CommonTaskRead.ReadHmiLstime();
                         
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
    }
}
