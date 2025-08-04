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
    public partial class CopyOutForm : Form
    {
        private List<PfMainTable> _pfMainTables;
        public PfMainTable loadPf { get; private set; }
        public List<PfDetailTable> loadDetails { get; private set; }
        public CopyOutForm(int id)
        {
            InitializeComponent();
            IniAllControlName();
            IniForm(id);
        }
        private void IniForm(int id)
        {

            _pfMainTables = PFDBCommon.GetPfMainTable();
            _pfMainTables = _pfMainTables.Where(m => m.id != id).ToList();
            IniListBox();
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "CopyOut");
            }
        }
        private void IniListBox()
        {
            uiListBox_pfName.DataSource = _pfMainTables;
            uiListBox_pfName.DisplayMember = "pfname";
            uiListBox_pfName.SelectedIndex = 0;

        }
        private void GetNewPfValue()
        {
            if (uiListBox_pfName.SelectedItem == null)
                throw new Exception("请选择配方");
            loadPf = (PfMainTable)uiListBox_pfName.SelectedItem;
            loadDetails = PFDetailDBCommon.GetPfDetailTable(loadPf.id);
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
