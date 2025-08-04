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
    public partial class CWeiHuControl : UserControl
    {
        public int chnOrder;
        public string[] pgstrings = new string[4];
        public int baseBefore = 10 * 1 + 2 * 4;
        public List<UIButton> uIButtons = new List<UIButton>();
        public CWeiHuControl(int _chnorder)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            chnOrder = _chnorder;
            uiLabelName.Text = $"C{chnOrder}";
            IniAllControlName();
            for (int i = 0; i < 4; i++)
            {
                pgstrings[i] = LanguageSet.SetL("TongDao", $"pg{i + 1}"); ;
            }
            uIButtons.Add(leftButton_1);
            uIButtons.Add(leftButton_2);
            uIButtons.Add(leftButton_3);
            uIButtons.Add(leftButton_4);
            uIButtons.Add(leftButton_5);
            uIButtons.Add(leftButton_6);
            uIButtons.Add(leftButton_7);
            uIButtons.Add(leftButton_8);
            uIButtons.ForEach(b =>
            {
                b.MouseDown += WeihuMouse_Down;
                b.MouseUp += WeihuMouse_Up;
            });
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "WeihuC");

            }
        }
        public void DisplayData(st_hmiTB_C d)
        {
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_1, d.led_vy[0]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_2, d.led_vy[1]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_3, d.led_vy[2]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_4, d.led_vy[3]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_5, d.led_vy[4]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_6, d.led_vy[5]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_7, d.led_vy[6]);
            CommonAdsUi.SetBtnColorGrayAndGreen(leftButton_8, d.led_vy[7]);
            right_label_1.Text = $"{pgstrings[0]} {d.pg[0]} mbar";
            right_label_2.Text = $"{pgstrings[1]} {d.pg[1]} mbar";

        }
        private void WeihuMouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    CommonTaskRead.WriteTongdaoBtnBool(baseBefore + _tag, true, "C", chnOrder);

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
                    CommonTaskRead.WriteTongdaoBtnBool(baseBefore + _tag, false, "C", chnOrder);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
    }
}
