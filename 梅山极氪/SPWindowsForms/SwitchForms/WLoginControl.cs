using EF.Models.EF.DLL;
using EF.Models.EF.Entities;
using SPWindowsForms.DbService;
using SPWindowsForms.Util;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.SwitchForms
{
    public partial class WLoginControl : UserControl
    {
        private List<string> header_strings = new List<string> { };
        //private Process kbpr;// Anders 20250121 监控进程是否启动
        public WLoginControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ShowPanel(uiPanellogin);
            CheckEmptyUser();
            GlobalVar.UserRoleDic = typeof(eUserRole).ConvertEnumToDic();
            GlobalVar.UserRoleDic[1] = LanguageSet.SetL("Login", "userrole1");
            GlobalVar.UserRoleDic[2] = LanguageSet.SetL("Login", "userrole2");
            GlobalVar.UserRoleDic[3] = LanguageSet.SetL("Login", "userrole3");
            IniTextBoxButtonText();
            //AddTxtMouseEnter();
            //AddTextDisableChar();
            //MessageBox.Show("666");
        }

        private void AddTextDisableChar()
        {
            if(zhuceNumber.Text =="") zhuceNumber.Text = " ";
            if (loginNumber.Text == "") loginNumber.Text = " ";
            if (updateNumber.Text == "") updateNumber.Text = " ";
            //zhucePassword2.Text = " ";
            //zhucePassword.Text = " ";
            //loginPassword.Text = " ";
            //updateOldpassword.Text = " ";
            //updatepassword2.Text = " ";
            //updatepassword.Text = " ";

        }

        private void AddTxtMouseEnter()
        {
            //zhucePassword2.GotFocus += TextBox_MouseEnter;
            //zhucePassword.GotFocus += TextBox_MouseEnter;
            //zhuceNumber.GotFocus += TextBox_MouseEnter;
            //loginPassword.GotFocus += TextBox_MouseEnter;
            //loginNumber.GotFocus += TextBox_MouseEnter;
            //updateOldpassword.GotFocus += TextBox_MouseEnter;
            //updatepassword2.GotFocus += TextBox_MouseEnter;
            //updatepassword.GotFocus += TextBox_MouseEnter;
            //updateNumber.GotFocus += TextBox_MouseEnter;

            zhucePassword.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            zhucePassword2.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            zhuceNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            loginPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            loginNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            updateOldpassword.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            updatepassword2.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            updatepassword.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);
            updateNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(TextBox_MouseEnter);

        }

        // 申明要使用的dll和api
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            OpenKeyBoard();
        }

        private void OpenKeyBoard()
        {
            //Hook2OpenKeyboard.ShowKeyboard();
            try
            {
                Process kbpr = Process.Start(@"C:\Windows\System32\osk.exe");
                kbpr.EnableRaisingEvents = true;
                kbpr.Exited += new EventHandler(ScreenKeyboard_Exited);
                //kbpr.Disposed += new EventHandler(ScreenKeyboard_Exited);
                IntPtr intptr = IntPtr.Zero;
                DateTime now = DateTime.Now;
                int millisecondsTimeout = (int)DateTime.Now.Subtract(now).TotalMilliseconds;
                while (IntPtr.Zero == intptr && millisecondsTimeout < 3000)
                {
                    System.Threading.Thread.Sleep(100);
                    millisecondsTimeout = (int)DateTime.Now.Subtract(now).TotalMilliseconds;
                    intptr = FindWindow(null, "屏幕键盘");
                }
                if (millisecondsTimeout >= 3000)
                    throw new Exception("Time Out");


                // 获取屏幕尺寸
                int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
                int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;


                // 设置软键盘的显示位置，底部居中
                int posX = (iActulaWidth - 1000) / 2;
                int posY = (iActulaHeight - 800);


                //设定键盘显示位置
                MoveWindow(intptr, posX, posY, 1000, 300, true);


                //设置软键盘到前端显示
                SetForegroundWindow(intptr);
            }
            catch (Exception ex)
            {
                //ShowErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void ScreenKeyboard_Exited(object sender, EventArgs e)
        {
            //MessageBox.Show("Exited窗口关闭");
            AddTextDisableChar();
        }

        private void OpenKeyBoardWithCursor()
        {
            try
            {
                //判断软键盘是否进程是否已经存在，如果不存在进行调用
                Process[] pro = Process.GetProcessesByName("osk");
                //说明已经存在，不再进行调用
                if (pro != null && pro.Length > 0)
                    return;
                IntPtr ptr = new IntPtr();
                bool isWow64FsRedirectionDisabled = Wow64DisableWow64FsRedirection(ref ptr);
                if (isWow64FsRedirectionDisabled)
                {
                    Process.Start(@"C:\WINDOWS\system32\osk.exe");
                    bool isWow64FsRedirectionReverted = Wow64RevertWow64FsRedirection(ptr);

                }

                IntPtr intptr = IntPtr.Zero;
                DateTime now = DateTime.Now;
                int millisecondsTimeout = (int)DateTime.Now.Subtract(now).TotalMilliseconds;
                while (IntPtr.Zero == intptr && millisecondsTimeout < 3000)
                {
                    System.Threading.Thread.Sleep(100);
                    millisecondsTimeout = (int)DateTime.Now.Subtract(now).TotalMilliseconds;
                    intptr = FindWindow(null, "屏幕键盘");
                }
                if (millisecondsTimeout >= 3000)
                    throw new Exception("Time Out");


                // 获取屏幕尺寸
                int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
                int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;


                // 设置软键盘的显示位置，底部居中
                int posX = (iActulaWidth - 1000) / 2;
                int posY = (iActulaHeight - 800);


                //设定键盘显示位置
                MoveWindow(intptr, posX, posY, 1000, 300, true);


                //设置软键盘到前端显示
                SetForegroundWindow(intptr);
            }
            catch (Exception ex)
            {
                //ShowErrorMessage(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        private void IniTextBoxButtonText()
        {
            loginNumber.Watermark = LanguageSet.SetL("Login", "inputAccount");
            loginPassword.Watermark = LanguageSet.SetL("Login", "inputPassword");
            uiButton3.Text = LanguageSet.SetL("Login", "update");
            uiButtonLogin.Text = LanguageSet.SetL("Login", "login");
            uiButtonNew.Text = LanguageSet.SetL("Login", "register");
            uiButton4.Text = LanguageSet.SetL("Login", "logout");
            zhuceNumber.Watermark = LanguageSet.SetL("Login", "inputAccount");
            zhucePassword.Watermark = LanguageSet.SetL("Login", "inputPassword");
            zhucePassword2.Watermark = LanguageSet.SetL("Login", "passwordCheck");
            uiButton5.Text = LanguageSet.SetL("Login", "back");
            uiButton1.Text = LanguageSet.SetL("Login", "register");
            updateNumber.Watermark = LanguageSet.SetL("Login", "inputAccount");
            updateOldpassword.Watermark = LanguageSet.SetL("Login", "inputPasswordOld");
            updatepassword.Watermark = LanguageSet.SetL("Login", "inputPasswordNew");
            updatepassword2.Watermark = LanguageSet.SetL("Login", "inputPasswordNew2");
            uiButton6.Text = LanguageSet.SetL("Login", "back");
            uiButton2.Text = LanguageSet.SetL("Login", "update");
            this.uiGroupBox_user.Text = LanguageSet.SetL("Login", "usermanage");
            header_strings.Add(LanguageSet.SetL("Login", "tableheader1"));
            header_strings.Add(LanguageSet.SetL("Login", "tableheader2"));
            header_strings.Add(LanguageSet.SetL("Login", "tableheader3"));
            header_strings.Add(LanguageSet.SetL("Login", "tableheader4"));
            header_strings.Add(LanguageSet.SetL("Login", "tableheader5"));
            this.useraccount.HeaderText = header_strings[0];
            this.userpassword.HeaderText = header_strings[1];
            this.userrole.HeaderText = header_strings[2];
            this.EditData.HeaderText = header_strings[3];
            this.EditData.Text = header_strings[3];
            this.EditData.ToolTipText = header_strings[3];
            this.DeleteData.HeaderText = header_strings[4];
            this.DeleteData.Text = header_strings[4];
            this.DeleteData.ToolTipText = header_strings[4];

        }
        public void ShowUserEdit()
        {
            if (String.IsNullOrEmpty(GlobalVar.UserNumber))
            {
                loginNumber.Text = " ";
                loginPassword.Text = "";
                ShowPanel(uiPanellogin);
            }
            if (CommonFunction.CheckAccessGongyi()/*GlobalVar.UserRole == (int)eUserRole.设备管理员 || GlobalVar.UserRole == (int)eSpecialUserRole.厂家*/)
            {
                uiGroupBox_user.Visible = true;
                LoadUserTableData();
            }
            else
                uiGroupBox_user.Visible = false;
        }
        private void CheckEmptyUser()
        {
            using (var dbContext = new DataBaseContext())
            {
                //  var user = dbContext.users.FirstOrDefault();
                var userFlag = dbContext.userTable.Any(m => m.validity == true);
                if (!userFlag)
                {
                    var newRow = new UserTable();
                    newRow.useraccount = "admin";
                    newRow.userpassword = "ajy000";
                    newRow.userrole = (int)eUserRole.设备管理员;
                    dbContext.userTable.Add(newRow);
                    var newRow2 = new UserTable();
                    newRow2.useraccount = "factory";
                    newRow2.userpassword = "ajy987";
                    newRow2.userrole = (int)eSpecialUserRole.厂家;
                    dbContext.userTable.Add(newRow2);
                    dbContext.SaveChanges();
                }

            }
        }
        private void ShowPanel(UIPanel uIPanel)
        {
            uiPanellogin.Visible = false;
            uiPanelupdate.Visible = false;
            uiPanelok.Visible = false;
            uiPanelzhuce.Visible = false;
            uIPanel.Visible = true;
            uIPanel.Location = new Point(216, 10);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(loginNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputAccount"));
                return;
            }
            if (CommonFunction.IsSpecialChar(loginNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialAccount"));
                return;
            }

            if (String.IsNullOrEmpty(loginPassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(loginPassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }
            using (var dbContext = new DataBaseContext())
            {
                //  var user = dbContext.users.FirstOrDefault();
                var user = dbContext.userTable.Where(m => m.useraccount == loginNumber.Text.Trim()
                && m.userpassword == loginPassword.Text.Trim() && m.validity == true).FirstOrDefault();
                if (user == null)
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("Login", "confirmAccount"));
                    return;
                }
                else
                {
                    GlobalVar.UserNumber = user.useraccount;
                    GlobalVar.UserRole = user.userrole;
                    ShowPanel(uiPanelok);
                    labelok.Text = $"{GlobalVar.UserNumber}";
                    GlobalVar.loginTime = DateTime.Now;
                    Work.frm_main.SetUserLable();
                    ShowUserEdit();
                }
            }

            loginPassword.Text = string.Empty;
            // 恢复键盘使用
            AddTextDisableChar();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonNew_Click(object sender, EventArgs e)
        {
            ShowPanel(uiPanelzhuce);
            zhuceNumber.Text = " ";
            //zhucePassword.Text = " ";
            //zhucePassword2.Text = " ";
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(zhuceNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputAccount"));
                return;
            }
            if (CommonFunction.IsSpecialChar(zhuceNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialAccount"));
                return;
            }

            if (String.IsNullOrEmpty(zhucePassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(zhucePassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }

            if (String.IsNullOrEmpty(zhucePassword2.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(zhucePassword2.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }
            if (zhucePassword.Text.Trim() != zhucePassword2.Text.Trim())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "passwordTwo"));
                return;
            }
            using (var dbContext = new DataBaseContext())
            {
                var userflag = dbContext.userTable.Any(m => m.useraccount == zhuceNumber.Text.Trim()
                && m.validity == true);
                if (userflag)
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("Login", "accountExist"));
                    return;
                }
                else
                {
                    var newuser = new UserTable();
                    newuser.useraccount = zhuceNumber.Text.Trim();
                    newuser.userpassword = zhucePassword.Text.Trim();
                    newuser.userrole = (int)eUserRole.技术员;
                    newuser.validity = true;
                    dbContext.userTable.Add(newuser);
                    dbContext.SaveChanges();

                    GlobalVar.UserNumber = newuser.useraccount;
                    GlobalVar.UserRole = newuser.userrole;
                    ShowPanel(uiPanelok);
                    labelok.Text = $"{GlobalVar.UserNumber}";
                    GlobalVar.loginTime = DateTime.Now;
                    Work.frm_main.SetUserLable();
                    ShowUserEdit();
                }
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton4_Click(object sender, EventArgs e)
        {
            GlobalVar.UserNumber = "";
            GlobalVar.UserRole = 0;
            ShowPanel(uiPanellogin);
            loginNumber.Text = " ";
            loginPassword.Text = "";
            Work.frm_main.SetUserLable();
            ShowUserEdit();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton3_Click(object sender, EventArgs e)
        {
            ShowPanel(uiPanelupdate);
            updateNumber.Text = " ";
            //updatepassword.Text = " ";
            //updatepassword2.Text = " ";
            //updateOldpassword.Text = " ";

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(updateNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputAccount"));
                return;
            }
            if (CommonFunction.IsSpecialChar(updateNumber.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialAccount"));
                return;
            }


            if (String.IsNullOrEmpty(updateOldpassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(updateOldpassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }

            if (String.IsNullOrEmpty(updatepassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(updatepassword.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }
            if (String.IsNullOrEmpty(updatepassword2.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "inputPassword"));
                return;
            }
            if (CommonFunction.IsSpecialChar(updatepassword2.Text.Trim()))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "nospecialPassword"));
                return;
            }
            if (updatepassword.Text.Trim() != updatepassword2.Text)
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Login", "passwordTwo"));
                return;
            }
            using (var dbContext = new DataBaseContext())
            {
                var flag = dbContext.userTable.Any(m => m.useraccount == updateNumber.Text.Trim()
                && m.validity == true);
                if (!flag)
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("Login", "accountNotExist"));
                    return;
                }
                var updateUser = dbContext.userTable.FirstOrDefault(m => m.useraccount == updateNumber.Text.Trim()
                 && m.userpassword == updateOldpassword.Text.Trim() && m.validity == true);

                if (updateUser == null)
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("Login", "passwordError"));
                    return;
                }
                else
                {
                    updateUser.userpassword = updatepassword.Text.Trim();
                    dbContext.SaveChanges();
                    GlobalVar.UserNumber = updateUser.useraccount;
                    ShowPanel(uiPanelok);
                    labelok.Text = $"{GlobalVar.UserNumber}";
                    GlobalVar.loginTime = DateTime.Now;
                    Work.frm_main.SetUserLable();
                    ShowUserEdit();
                }
            }
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            ShowPanel(uiPanellogin);
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton6_Click(object sender, EventArgs e)
        {
            ShowPanel(uiPanellogin);
        }

        #region UserTable
        public void LoadUserTableData()
        {
            var users = UserDBCommon.GetUserTables();
            this.dgvData.Rows.Clear();
            //DataGridViewButtonCell bc = new DataGridViewButtonCell();
            //bc.FlatStyle = FlatStyle.Flat;
            //bc.Style.BackColor = Color.AliceBlue;
            foreach (var row in users)
            {

                //string standValueName = row.MachineName;
                int index = this.dgvData.Rows.Add();
                this.dgvData.Rows[index].Cells["useraccount"].Value = row.useraccount ?? "";
                this.dgvData.Rows[index].Cells["userpassword"].Value = row.userpassword ?? "";
                this.dgvData.Rows[index].Cells["userrole"].Value = GlobalVar.UserRoleDic[row.userrole];
                //this.dgvData.Rows[index].Cells["standValueName"].Value = (string.IsNullOrEmpty(standValueName) ? "未命名" : standValueName);
                this.dgvData.Rows[index].Cells["Key"].Value = row.id;
                //this.dgvData.Rows[index].Cells["Key2"].Value = this.dgvData.Rows[index].Cells["kguan"].Value;

            }
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string dataPropertyName = this.dgvData.Columns[e.ColumnIndex].DataPropertyName;
            if (dataPropertyName == "DeleteData")
            {
                if (!CommonFunction.CheckAccessShebei() || this.dgvData.Rows[rowIndex].Cells["userrole"].Value.ToString() != GlobalVar.UserRoleDic[1]/*"技术员"*/) return;
                if (this.dgvData.Rows[rowIndex].Cells["useraccount"].Value.ToString() == "admin") return;
                this.UserDeleteData(rowIndex);
            }
            else if (dataPropertyName == "EditData")
            {
                if (!CommonFunction.CheckAccessShebei()) return;
                if (this.dgvData.Rows[rowIndex].Cells["useraccount"].Value.ToString() == "admin") return;
                this.UserEditData(rowIndex);
            }

        }
        private void UserDeleteData(int RowIndex)
        {
            if (MessageBox.Show(LanguageSet.SetL("Login", "confirmDelete"), "Warning:", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int key = Convert.ToInt32(this.dgvData.Rows[RowIndex].Cells["Key"].FormattedValue);
                UserDBCommon.DeleteUserTable(key);
                this.LoadUserTableData();

            }
        }
        private void UserEditData(int RowIndex)
        {

            int key = Convert.ToInt32(this.dgvData.Rows[RowIndex].Cells["Key"].FormattedValue);
            using (var _form = new SetRoleForm())
            {
                var _result = _form.ShowDialog();
                if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                {
                    UserDBCommon.UpdateUserRole(key, _form.resultRole);
                    this.LoadUserTableData();
                }

            }

        }
        #endregion

        private void loginNumber_MouseDown(object sender, MouseEventArgs e)
        {
            //loginNumber.Clear();
            if(!IsProcessOpen("osk"))
            {
                OpenKeyBoard();
                this.Focus();
                loginNumber.Focus();
                Cursor.Show();
            }
        }

        private void loginPassword_MouseDown(object sender, MouseEventArgs e)
        {
            //loginPassword.Clear();
            //if(IsProcessOpen("osk"))
            //    OpenKeyBoardWithCursor();
        }

        private void zhuceNumber_MouseDown(object sender, MouseEventArgs e)
        {
            //zhuceNumber.Clear();
            if (!IsProcessOpen("osk"))
            {
                OpenKeyBoard();
                zhuceNumber.Focus();
            }
        }

        private void zhucePassword_MouseDown(object sender, MouseEventArgs e)
        {
            //zhucePassword.Clear();
            //    OpenKeyBoardWithCursor();
        }

        private void zhucePassword2_MouseDown(object sender, MouseEventArgs e)
        {
            //zhucePassword2.Clear();
            //if (!IsProcessOpen("osk"))
            //    OpenKeyBoardWithCursor();
        }

        private void updateNumber_MouseDown(object sender, MouseEventArgs e)
        {
            //updateNumber.Clear();
            if (!IsProcessOpen("osk"))
            {
                OpenKeyBoard();
                updateNumber.Focus();
            }
        }

        private void updateOldpassword_MouseDown(object sender, MouseEventArgs e)
        {

            //updateOldpassword.Clear();
            //if (!IsProcessOpen("osk"))
            //    OpenKeyBoardWithCursor();
        }

        private void updatepassword_MouseDown(object sender, MouseEventArgs e)
        {
            //updatepassword.Clear();
            //if (!IsProcessOpen("osk"))
            //    OpenKeyBoardWithCursor();
        }

        private void updatepassword2_MouseDown(object sender, MouseEventArgs e)
        {
            //updatepassword2.Clear();
            //if (!IsProcessOpen("osk"))
            //    OpenKeyBoardWithCursor();
        }

        private void WLoginControl_Load(object sender, EventArgs e)
        {
            AddTextDisableChar();
        }

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Monitor()
        {
            IntPtr oskWindow = IntPtr.Zero;
            Process[] processes = Process.GetProcessesByName("osk");

            if (processes.Length > 0)
            {
                oskWindow = processes[0].MainWindowHandle;
            }

            if (oskWindow != IntPtr.Zero && !IsWindow(oskWindow))
            {
                //Console.WriteLine("屏幕键盘已关闭");
                MessageBox.Show("关闭喽");
            }
        }

    }
}
