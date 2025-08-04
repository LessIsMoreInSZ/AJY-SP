using EF.Models.EF.Entities;
using SPWindowsForms.DbService;
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
    public partial class NewPfForm : Form
    {
        private List<PfMainTable> _pfMainTables;
        public string NewName { get; private set; }
        public int? CopyedId { get; private set; }
        public bool CopyChecked { get; private set; }
        public PfMainTable createPf { get; private set; }
       // public List<PfDetailTable> createDetails { get; private set; }
        public List<PfDetailTable> copyDetails { get; private set; }
        public NewPfForm()
        {
            InitializeComponent();
            IniAllControlName();
            IniForm();
        }
        private void IniForm()
        {
            uiTextBox_pfname.Focus();
            _pfMainTables = PFDBCommon.GetPfMainTable();
            IniListBox();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "NewPf");
            }
        }
        private void IniListBox()
        {
            uiListBox_copyName.DataSource = _pfMainTables;
            uiListBox_copyName.DisplayMember = "pfname";
            uiListBox_copyName.SelectedIndex = 0;

        }
        private void GetNewPfValue()
        {
            if (uiTextBox_pfname.Text.Trim() == "") throw new Exception("输入配方名");
            if (_pfMainTables.Exists(m => m.pfname.ToUpper() == uiTextBox_pfname.Text.ToUpper()))
                throw new Exception("已存在的配方名");
            if (uiCheckBox_copy.Checked)
            {
                if (uiListBox_copyName.SelectedItem == null)
                    throw new Exception("选择复制的配方");
            }
            NewName = uiTextBox_pfname.Text.Trim();
            CopyChecked = uiCheckBox_copy.Checked;
            if (!CopyChecked)
            {
                CopyedId = null;
            }
            else
            {
                var _copy = (PfMainTable)uiListBox_copyName.SelectedItem;
                CopyedId = _copy.id;
                copyDetails= PFDetailDBCommon.GetPfDetailTable(_copy.id);
            }
            createPf = PFDBCommon.NewPfMainTable(NewName);
           // createDetails = PFDetailDBCommon.GetPfDetailTable(createPf.id);
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

        private void uiCheckBox_copy_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBox_copy.Checked)
            {
                uiLabel_copyName.Visible = true;
                uiListBox_copyName.Visible = true;
            }
            else
            {
                uiLabel_copyName.Visible = false;
                uiListBox_copyName.Visible = false;
            }
        }
    }
}
