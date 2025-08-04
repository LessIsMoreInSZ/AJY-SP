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
    public partial class AirFilterTitleControl : UserControl
    {
        private bool timeflag;
        public AirFilterTitleControl(bool flag)
        {
            InitializeComponent();
            timeflag = flag;
            IniAllControlName();
        }
        private void IniAllControlName()
        {
            top_label_2.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_2");
            top_label_4.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_4");
            if (timeflag)
            {
                top_label_1.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_1_time");
                top_label_3.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_3_time");
                top_label_5.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_5_time");
            }
            else
            {
                top_label_1.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_1_moci");
                top_label_3.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_3_moci");
                top_label_5.Text = LanguageSet.SetL("SheBeiBaoYang", "top_label_5_time");
            }
                
        }
    }
}
