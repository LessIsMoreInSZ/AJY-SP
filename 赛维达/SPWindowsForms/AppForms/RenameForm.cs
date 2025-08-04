using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public partial class RenameForm : Form
    {
        private string oldName;
        public string newName;
        public RenameForm(string name)
        {
            InitializeComponent();
            IniAllControlName();
            oldName = name;
            uiTextBox_pfname.Text = oldName;
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "RenamePf");
            }
        }
        private void GetNewPfValue()
        {
            if (uiTextBox_pfname.Text.Trim() == "") throw new Exception("输入配方名");
            if (uiTextBox_pfname.Text.Trim().ToUpper() == oldName.ToUpper())
                throw new Exception("配方名没有变化");
            newName = uiTextBox_pfname.Text.Trim();
        }
        private void uiButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                GetNewPfValue();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiButton_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
