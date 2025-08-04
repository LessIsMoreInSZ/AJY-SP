using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPWindowsForms.AdsConnect.TwincateStruct;
using Sunny.UI;

namespace SPWindowsForms.SwitchForms
{
    public partial class PGsDControl : UserControl
    {
        private int d_order;
        private int start_before;
        private bool setflag = false;
        public PGsDControl(int _d_order, int _start_before)
        {
            InitializeComponent();
            d_order = _d_order;
            start_before = _start_before;
            SetTopTitle();
        }
        public void SetTopTitle()
        {
            label_title.Text = $"D{d_order}.PG1";
            label_title2.Text = $"D{d_order}.PG2";
            label_title3.Text = $"D{d_order}.PG3";
            label_title4.Text = $"D{d_order}.PG4";
        }
        private PGs_D GetPGs_D()
        {
            return GlobalVar.plcData.UPS_PG.pg_D[d_order - 1];
        }
        public void SetLabelValue()
        {
            var pgs_data = GetPGs_D();
            label_gongchen.Text = pgs_data.P_Vac.in_pg.ToString();
            label_guochen.Text = pgs_data.P_Vac.out_hmipg.ToString();
            label_gongchen2.Text = pgs_data.P_Blow.in_pg.ToString();
            label_guochen2.Text = pgs_data.P_Blow.out_hmipg.ToString();
            label_gongchen3.Text = pgs_data.P_M.in_pg.ToString();
            label_guochen3.Text = pgs_data.P_M.out_hmipg.ToString();
            label_gongchen4.Text = pgs_data.P_HYD.in_pg.ToString();
            label_guochen4.Text = pgs_data.P_HYD.out_hmipg.ToString();
        }
        public void SetInputValue()
        {
            try
            {
                setflag = true;
                var pgs_data = GetPGs_D();
                txt_high.Text = pgs_data.P_Vac.hi_pg.ToString();
                txt_low.Text = pgs_data.P_Vac.lo_pg.ToString();
                txt_high2.Text = pgs_data.P_Blow.hi_pg.ToString();
                txt_low2.Text = pgs_data.P_Blow.lo_pg.ToString();
                txt_high3.Text = pgs_data.P_M.hi_pg.ToString();
                txt_low3.Text = pgs_data.P_M.lo_pg.ToString();
                txt_high4.Text = pgs_data.P_HYD.hi_pg.ToString();
                txt_low4.Text = pgs_data.P_HYD.lo_pg.ToString();
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
            }
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_Pg_name, beforecount, value.Value);
        }
        private void txt_low_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_low, start_before + 6);
        }

        private void txt_low2_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_low2, start_before + 6 + 16);
        }

        private void txt_low3_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_low3, start_before + 6 + 16 * 2);
        }

        private void txt_low4_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_low4, start_before + 6 + 16 * 3);
        }

        private void txt_high_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high, start_before + 2);
        }

        private void txt_high2_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high2, start_before + 2 + 16);
        }

        private void txt_high3_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high3, start_before + 2 + 16 * 2);
        }

        private void txt_high4_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high4, start_before + 2 + 16 * 3);
        }
    }
}
