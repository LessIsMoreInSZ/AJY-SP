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

namespace SPWindowsForms.AppForms
{
    public partial class DownLoadForm : Form
    {
        private List<PfMainTable> _pfMainTables;
        public List<PfMainTable> _downloadPfs;
        public DownLoadForm()
        {
            InitializeComponent();
            IniForm();
        }
        private void IniForm()
        {

            _pfMainTables = PFDBCommon.GetPfMainTable();
            IniListBox();
            IniAllControlName();
        }
        private void IniListBox()
        {
            uiListBox_pfName.DataSource = _pfMainTables;
            uiListBox_pfName.DisplayMember = "pfname";
            uiListBox_pfName.SelectedIndex = 0;

        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "DownloadPf");
            }
        }
        private void GetPfValue()
        {
            if (uiListBox_pfName.SelectedItems.Count == 0)
                throw new Exception(LanguageSet.SetL("LoadPf", "noselect"));
            _downloadPfs = new List<PfMainTable>();
            for(int i=0;i < uiListBox_pfName.SelectedItems.Count; i++)
            {
                _downloadPfs.Add((PfMainTable)uiListBox_pfName.SelectedItems[i]);
            }
         
           
        }
        private void uiButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                GetPfValue();
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
