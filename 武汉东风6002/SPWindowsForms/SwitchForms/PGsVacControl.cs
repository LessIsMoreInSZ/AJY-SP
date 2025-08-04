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
    public partial class PGsVacControl : UserControl
    {
        private string top_type;
        private int top_count;
        private int start_before;
        private bool setflag = false;
        public PGsVacControl(string _top_type, int _top_count, int _start_before)
        {
            InitializeComponent();

            top_type = _top_type;
            top_count = _top_count;
            var _count= top_count == 0 ? "" : top_count.ToString();
            label_title.Text = $"{top_type}{_count}.PG";
            start_before = _start_before;
        }
        private PGs_Vac GetPGs_Vac()
        {
            switch (top_type)
            {
                case "TANK":
                    if(top_count==1)
                    return GlobalVar.plcData.UPS_PG.PG_Tank1;
                    else
                        return GlobalVar.plcData.UPS_PG.PG_Tank2;
                case "E":
                    return GlobalVar.plcData.UPS_PG.PG_E[top_count-1];
                //case "Air":
                //    return GlobalVar.plcData.UPS_PG.PG_air;
                default:
                    return GlobalVar.plcData.UPS_PG.PG_air; 
            }
        }
        public void SetLabelValue()
        {
            var pgs_data = GetPGs_Vac();
            label_gongchen.Text = pgs_data.P_Vac.in_pg.ToString();
            label_guochen.Text = pgs_data.P_Vac.out_hmipg.ToString();
        }
        public void SetInputValue()
        {
            try
            {
                setflag = true;
                var pgs_data = GetPGs_Vac();
            txt_high.Text = pgs_data.P_Vac.hi_pg.ToString();
            txt_low.Text = pgs_data.P_Vac.lo_pg.ToString();
        }
            catch (Exception ex)
            {
                throw new Exception("SetInputValue" + ex.Message);
    }
            finally
            {
                setflag = false;
            }
        }
        private void SetData(UITextBox uITextBox, int beforecount)
        {
            if (setflag) return;
            var value = CommonFunction.GetFloatFromUIText(uITextBox);
            if (value == null)
            {
                SetInputValue();
                return;
            } GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_Pg_name, beforecount, value.Value);

        }
        private void txt_low_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_low, start_before + 6);
        }

        private void txt_high_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high, start_before + 2);
        }
    }
}
