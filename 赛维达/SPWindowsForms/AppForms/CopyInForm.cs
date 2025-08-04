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
    public partial class CopyInForm : Form
    {
        public List<int> CheckedItems=new List<int>();
        private string _copyType;
        public CopyInForm(List<int> copyChns, string copyType)
        {
            InitializeComponent();
            _copyType = copyType;
            var chns = copyChns.Select(m => copyType+ m.ToString()
            ).ToArray();
            uiCheckBoxGroup_copy.Items.AddRange(chns);
            uiCheckBoxGroup_copy.SelectAll();

        }
        private void GetCheckedItems()
        {
            if (uiCheckBoxGroup_copy.SelectedItems.Count == 0) throw new Exception("请至少选择一个通道");
            uiCheckBoxGroup_copy.SelectedItems.ForEach(s => { string _chn = s.ToString().Replace(_copyType, "");
                CheckedItems.Add(Convert.ToInt32(_chn));
            });

        }
        private void uiButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                GetCheckedItems();
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
