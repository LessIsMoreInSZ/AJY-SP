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
    public partial class ProcessControl : UserControl
    {
        public int chnOrder;
        public string chnType;
        public ProcessControl(string _chnType, int _chnOrder)
        {
            InitializeComponent();
            chnOrder = _chnOrder;
            chnType = _chnType;
            uiLabelName.Text = $"{chnType}{chnOrder}";
            IniAllControlName();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Process");
            }
        }
        public void ShowData(st_ledauto _st_Ledauto)
        {
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_1, _st_Ledauto.led_course[0]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_2, _st_Ledauto.led_course[1]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_3, _st_Ledauto.led_course[2]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_4, _st_Ledauto.led_course[3]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_5, _st_Ledauto.led_course[4]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_6, _st_Ledauto.led_course[5]);
        }
        public void ShowDataE(st_ledauto _st_Ledauto)
        {
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_1, _st_Ledauto.led_course[0]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_2, _st_Ledauto.led_course[1]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_3, _st_Ledauto.led_course[2]);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_4, _st_Ledauto.led_course[3]);
            // CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_5, _st_Ledauto.led_course[4]);
            label_process_5.Visible = false;
            label_process_mark_4.Visible = false;
            label_process_4.Size = new Size(229,35);
            CommonAdsUi.SetSLabelColorGrayAndGreen(label_process_6, _st_Ledauto.led_course[5]);
        }
    }
}
