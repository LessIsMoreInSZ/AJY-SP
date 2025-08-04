using SPWindowsForms.AdsConnect;
using SPWindowsForms.AdsConnect.TwincateStruct;
using Sunny.UI;
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
    public partial class ETongdaoControl : UserControl
    {
        public int chnOrder;
        public string pgstring;
        public ETongdaoControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"E{chnOrder}";
            IniAllControlName();
            pgstring = LanguageSet.SetL("TongDao", $"pg1");
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "TongDao");
                if (ctrl is UIPanel)
                {
                    var _panel = (UIPanel)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "TongDao");
                    }
                }
            }
        }
        public void DisplayData(st_hmiTB_E e)
        {
            CommonAdsUi.SetLightOnOff(uiLight_1, e.led_vy1);
            right_label_1.Text = $"{pgstring} {e.pg1} mbar";
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_1, e.butt_link_Vac);
        }

        private void leftButton_1_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, true, "E", chnOrder);

        }

        private void leftButton_1_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, false, "E", chnOrder);
        }
    }
}
