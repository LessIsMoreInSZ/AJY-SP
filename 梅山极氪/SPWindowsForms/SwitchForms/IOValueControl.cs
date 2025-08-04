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
    public partial class IOValueControl : UserControl
    {
        private string _str_iotext;
        public IOValueControl(string str_ioname, string str_iotext)
        {
            InitializeComponent();
            ioname.Text = str_ioname;
            iotext.Text = str_iotext;
            _str_iotext = str_iotext;
        }
        public void SetIOText(short text)
        {
            iotext.Text = $"{_str_iotext}: {text}";
        }
    }
}
