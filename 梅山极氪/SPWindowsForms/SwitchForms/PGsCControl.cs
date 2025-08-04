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
    public partial class PGsCControl : UserControl
    {
        private int c_order;
        private int start_before;
        private bool setflag = false;
        public PGsCControl(int _c_order, int _start_before)
        {
            InitializeComponent();
            c_order = _c_order;
            start_before = _start_before;
            SetTopTitle();
        }
        public void SetTopTitle()
        {
            label_title.Text = $"C{c_order}.PG1";
            label_title2.Text = $"C{c_order}.PG2";
        }
        private PGs_C GetPGs_C()
        {
            return GlobalVar.plcData.UPS_PG.pg_C[c_order - 1];
        }
        public void SetLabelValue()
        {
            var pgs_data = GetPGs_C();
            label_gongchen.Text = pgs_data.P_Vac.in_pg.ToString();
            label_guochen.Text = pgs_data.P_Vac.out_hmipg.ToString();
            label_gongchen2.Text = pgs_data.P_Blow.in_pg.ToString();
            label_guochen2.Text = pgs_data.P_Blow.out_hmipg.ToString();
        }
        public void SetInputValue()
        {
            try
            {
                setflag = true;
                var pgs_data = GetPGs_C();
                txt_high.Text = pgs_data.P_Vac.hi_pg.ToString();
                txt_low.Text = pgs_data.P_Vac.lo_pg.ToString();
                txt_high2.Text = pgs_data.P_Blow.hi_pg.ToString();
                txt_low2.Text = pgs_data.P_Blow.lo_pg.ToString();
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

        private void txt_high_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high, start_before + 2);
        }

        private void txt_high2_TextChanged(object sender, EventArgs e)
        {
            SetData(txt_high2, start_before + 2 + 16);
        }
    }
}
