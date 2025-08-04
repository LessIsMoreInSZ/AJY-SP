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
    public partial class SetRoleForm : Form
    {
        public int resultRole;
        public SetRoleForm()
        {
            InitializeComponent();
            var list = new List<UserSelectItem> { };
            list.Add(new UserSelectItem { id=1, name =GlobalVar.UserRoleDic[1]}) ;
            list.Add(new UserSelectItem { id = 2, name = GlobalVar.UserRoleDic[2] });
            list.Add(new UserSelectItem { id = 3, name = GlobalVar.UserRoleDic[3] });
            uiComboBoxRole.DataSource = list;
            uiComboBoxRole.ValueMember = "id";
            uiComboBoxRole.DisplayMember = "name";
       //     CommonFunction.SetComoboxData(uiComboBoxRole, typeof(eUserRole));
        }
        private void GetRole()
        {
            var _role = uiComboBoxRole.SelectedItem;
            if (_role == null) throw new Exception("请选择权限");
            else resultRole = ((UserSelectItem)_role).id;
        }
        private void uiButton_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void uiButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                GetRole();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
    }
}
