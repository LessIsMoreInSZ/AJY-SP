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
    public partial class DTongdaoControl : UserControl
    {
        public int chnOrder;
        public string[] pgstrings=new string[5];
        public DTongdaoControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"D{chnOrder}";
            IniAllControlName();
            for(int i=0;i<5;i++)
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
        public void DisplayData(st_hmiTB_D d) {
            CommonAdsUi.SetLabelColorGrayAndGreen(uiLabel_xianwei, d.status_ls);
            CommonAdsUi.SetLightOnOff(uiLight_1, d.led_vy[0]);
            CommonAdsUi.SetLightOnOff(uiLight_2, d.led_vy[1]);
            CommonAdsUi.SetLightOnOff(uiLight_3, d.led_vy[2]);
            CommonAdsUi.SetLightOnOff(uiLight_4, d.led_vy[3]);
            CommonAdsUi.SetLightOnOff(uiLight_5, d.led_vy[4]);
            CommonAdsUi.SetLightOnOff(uiLight_6, d.led_vy[5]);
            CommonAdsUi.SetLightOnOff(uiLight_7, d.led_vy[6]);
            CommonAdsUi.SetLightOnOff(uiLight_8, d.led_vy[7]);
            CommonAdsUi.SetLightOnOff(uiLight_9, d.led_vy[8]);
            right_label_1.Text = $"{pgstrings[0]} {d.pg[0]} mbar";
            right_label_2.Text = $"{pgstrings[1]} {d.pg[1]} mbar";
            right_label_3.Text = $"{pgstrings[2]} {d.pg[2]} mbar";
            right_label_4.Text = $"{pgstrings[3]} {d.pg[3]} bar";
           right_label_5.Text = $"{pgstrings[4]} {GlobalVar.plcData.hmi_lstime[chnOrder-1]} ms";
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_1,d.butt_link_Vac);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_2, d.butt_link_Blow);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_3, d.butt_Vac);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_4, d.butt_Blow);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_5, d.butt_open);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_6, d.butt_close);
        }

        private void leftButton_1_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, true, "D", chnOrder);
        }

        private void leftButton_1_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动抽真空, false, "D", chnOrder);
        }

        private void leftButton_2_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动吹扫, true, "D", chnOrder);
        }

        private void leftButton_2_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.联动吹扫, false, "D", chnOrder);
        }

        private void leftButton_3_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽真空, true, "D", chnOrder);
        }

        private void leftButton_3_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽真空, false, "D", chnOrder);
        }

        private void leftButton_4_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽吹扫, true, "D", chnOrder);
        }

        private void leftButton_4_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽吹扫, false, "D", chnOrder);

        }

        private void leftButton_5_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽顶出, true, "D", chnOrder);
        }

        private void leftButton_5_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽顶出, false, "D", chnOrder);

        }

        private void leftButton_6_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽退回, true, "D", chnOrder);

        }

        private void leftButton_6_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteTongdaoBtnBool((int)SetTongdaoButtonOrder.单动抽退回, false, "D", chnOrder);
        }
    }
}
