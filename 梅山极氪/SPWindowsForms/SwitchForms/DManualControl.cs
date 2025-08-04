using SPWindowsForms.AdsConnect.TwincateStruct;
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
    public partial class DManualControl : UserControl
    {
        public int chnOrder;
        public DManualControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"D{chnOrder}";
            IniAllControlName();
        }
        public void SetCheckBoxEnable(bool flag)
        {
            if (uiCheckBox_use.Enabled != flag)
                uiCheckBox_use.Enabled = flag;

        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Manual");

            }
        }

        public void IniShowData(bool use_flag)
        {
            this.uiCheckBox_use.Checked = use_flag;
        }
        public void ShowData(st_hmiTB_D td, stsub_testdisplay test)
        {
            CommonAdsUi.SetLabelColorGrayAndGreen(uiLabel_xianwei, td.status_ls);
            uiLabel_1.Text = td.pg[3].ToString();
            uiLabel_2.Text = td.pg[0].ToString();
            uiLabel_3.Text = test.Fr_P.ToString();
            uiLabel_4.Text = test.Se_P.ToString();
            uiLabel_5.Text = test.close_time.ToString();
            uiLabel_6.Text = test.deltaP.ToString();
            uiLabel_7.Text = test.sulv_P.ToString();
        }

        private void uiCheckBox_use_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_test_name, 9 + (chnOrder - 1), uiCheckBox_use.Checked);
        }
    }
}
