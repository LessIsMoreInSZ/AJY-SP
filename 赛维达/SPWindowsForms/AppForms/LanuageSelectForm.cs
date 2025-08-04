using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.AppForms
{
    public partial class LanuageSelectForm : Form
    {
        public string resultLanuage;
        public LanuageSelectForm()
        {
            InitializeComponent();
         
            this.uiComboBoxLanguage.DataSource = GlobalVar.systemSetting.language_options;

        }
        private void GetLanguage()
        {
            var _language = uiComboBoxLanguage.SelectedItem;
            if (_language == null|| _language.ToString() == "") throw new Exception("Please Select the language");
            resultLanuage = _language.ToString();
        }

        private void uiButton_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void uiButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                GetLanguage();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
    }
}
