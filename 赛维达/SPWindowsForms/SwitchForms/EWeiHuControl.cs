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
    public partial class EWeiHuControl : UserControl
    {
        public int chnOrder;
        public string[] pgstrings = new string[4];
        public int baseBefore = 2 * 1 + 4;
        public EWeiHuControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"E{chnOrder}";
            IniAllControlName();
            for (int i = 0; i < 4; i++)
            {
                pgstrings[i] = LanguageSet.SetL("TongDao", $"pg{i + 1}"); ;
            }
            leftButton_1.MouseDown += WeihuMouse_Down;
            leftButton_1.MouseUp += WeihuMouse_Up;
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Weihu");

            }
        }
        public void DisplayData(st_hmiTB_E d)
        {
        
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_1, d.led_vy1);
           
            right_label_1.Text = $"{pgstrings[0]} {d.pg1} mbar";

        }
        private void WeihuMouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    CommonTaskRead.WriteTongdaoBtnBool(baseBefore + _tag, true, "E", chnOrder);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void WeihuMouse_Up(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    CommonTaskRead.WriteTongdaoBtnBool(baseBefore + _tag, false, "E", chnOrder);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
    }
}
