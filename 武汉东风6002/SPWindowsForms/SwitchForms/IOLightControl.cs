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
    public partial class IOLightControl : UserControl
    {
        public IOLightControl(string str_ioname, string str_iotext)
        {
            InitializeComponent();
            ioname.Text = str_ioname;
            iotext.Text = str_iotext;
        }
        public void SetIOOnOff(bool flag)
        {
            CommonAdsUi.SetLightOnOff(iolight, flag);
        }
    }
}
