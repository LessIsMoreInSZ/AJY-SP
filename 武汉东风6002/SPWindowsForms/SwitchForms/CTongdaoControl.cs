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
    public partial class CTongdaoControl : UserControl
    {
        public int chnOrder;
        public string[] pgstrings = new string[2];
        public CTongdaoControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"C{chnOrder}";
            IniAllControlName();
            for (int i = 0; i < 2; i++)
            {
                pgstrings[i] = LanguageSet.SetL("TongDao", $"pg{i + 1}"); 
            }
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
        public void DisplayData(st_hmiTB_C c)
        {
            CommonAdsUi.SetLightOnOff(uiLight_1, c.led_vy[0]);
            CommonAdsUi.SetLightOnOff(uiLight_2, c.led_vy[1]);
            CommonAdsUi.SetLightOnOff(uiLight_3, c.led_vy[2]);
            CommonAdsUi.SetLightOnOff(uiLight_4, c.led_vy[3]);
            CommonAdsUi.SetLightOnOff(uiLight_5, c.led_vy[4]);
            CommonAdsUi.SetLightOnOff(uiLight_6, c.led_vy[5]);
            CommonAdsUi.SetLightOnOff(uiLight_7, c.led_vy[6]);
            CommonAdsUi.SetLightOnOff(uiLight_8, c.led_vy[7]);
            right_label_1.Text = $"{pgstrings[0]} {c.pg[0]} mbar";
            right_label_2.Text = $"{pgstrings[1]} {c.pg[1]} mbar";
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_1, c.butt_link_Vac);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_2, c.butt_link_Blow);
        }

        private void leftButton_1_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, true, "C", chnOrder);
        }

        private void leftButton_1_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, false, "C", chnOrder);

        }

        private void leftButton_2_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动吹扫, true, "C", chnOrder);
        }

        private void leftButton_2_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动吹扫, false, "C", chnOrder);
        }
    }
}
