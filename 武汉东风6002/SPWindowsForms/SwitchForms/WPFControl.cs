using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect;
using SPWindowsForms.AppForms;
using SPWindowsForms.DbService;
using SPWindowsForms.ExcelHelper;
using Sunny.UI;

namespace SPWindowsForms.SwitchForms
{
    public partial class WPFControl : UserControl
    {
        /// <summary>
        /// pf
        private PfMainTable editPf = null;
        private List<PfDetailTable> editPfDetails = null;

        /// </summary>
        private int leftStart = 220;
        private int rightEnd = 10;
        private int topBtnY = 48;
        private int topBtnHeight = 30;
        private int topBtnMaxWidth = 100;
        private int sysbuttonSpace = /*23*/4;
        private int sysLabelWidth = 210;
        private int sysLabelHeight = /*23*/28;
        private int sysLabelSpace = /*23*/4;
        private int sysLabelStartX = 4;
        private int sysLabelStartY = 80;
        private string nowTopType = "D";
        private int nowEditAlarm = 1;
        private List<UIButton> adminButtons = new List<UIButton>();
        private List<UIButton> chButtons = new List<UIButton>();
        private List<UILabel> sysLabels = new List<UILabel>();
        private List<List<UITextBox>> sysTextBoxs = new List<List<UITextBox>>();
        private List<List<UICheckBox>> sysCheckBoxs = new List<List<UICheckBox>>();
        private List<UILabel> alarmLeftLabels = new List<UILabel>();
        private List<UICheckBox> alarmCheckBoxes = new List<UICheckBox>();
        private List<List<UITextBox>> alarmNumbers = new List<List<UITextBox>>();
        private Font TopBtnFont;
        private Font SysLabelFont;
        private bool alarmRealbusy = false;
        //  public bool taskFlag =true;
        public WPFControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            IniTopChnButtonVisible();
            IniFont();
            ListAllControls();
            BuildAllPageModels();
            FirstLoadPf(GlobalVar.systemSetting.use_pf_id);
            SwitchTopType(btn_switch_type_d, null);
            PfRealTimeDataTask();
        }
        private void IniTopChnButtonVisible()
        {
            if (GlobalVar.systemSetting.d_chn_count == 0)
                btn_switch_type_d.Visible = false;
            if (GlobalVar.systemSetting.c_chn_count == 0)
                btn_switch_type_c.Visible = false;
            if (GlobalVar.systemSetting.e_chn_count == 0)
                btn_switch_type_e.Visible = false;
        }
        public void FirstLoadPf(int _id)
        {
            try
            {
                var getpf = PFDBCommon.GetPfMainTableById(_id);

                if (getpf != null)
                {
                    GlobalVar.usePf = (PfMainTable)CommonFunction.DeepCopy(getpf);
                    editPf = (PfMainTable)CommonFunction.DeepCopy(getpf);
                    GlobalVar.usePfDetails = (List<PfDetailTable>)CommonFunction.DeepCopy(PFDetailDBCommon.GetPfDetailTable(GlobalVar.usePf.id));
                    editPfDetails = (List<PfDetailTable>)CommonFunction.DeepCopy(PFDetailDBCommon.GetPfDetailTable(editPf.id));
                    GlobalVar.commonAdsControl.WriteSTAll(GlobalVar.usePfDetails);
                    GlobalVar.commonAdsControl.WriteCommonString(".pf_name", 20, GlobalVar.usePf.pfname);
                }
                IniTopName();
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }
        public void LoadEditPf(PfMainTable _editPf, List<PfDetailTable> _editPfDetails)
        {
            try
            {

                editPf = (PfMainTable)CommonFunction.DeepCopy(_editPf);
                editPfDetails = (List<PfDetailTable>)CommonFunction.DeepCopy(_editPfDetails);
                IniTopName();
                SwitchTopType(btn_switch_type_d, null);
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }
        public void IniTopName()
        {
            if (GlobalVar.usePf != null)
            {
                uiLabel_usepf.Text = GlobalVar.usePf.pfname;
                uiLabel_usepf.BackColor = Color.LightGreen;
            }
            else
            {
                uiLabel_usepf.Text = "Error";
                uiLabel_usepf.BackColor = Color.Red;
            }
            if (editPf != null)
            {
                uiLabel_editpf.Text = editPf.pfname;
                uiLabel_editpf.BackColor = Color.Yellow;
            }
            else
            {
                uiLabel_editpf.Text = "Error";
                uiLabel_editpf.BackColor = Color.Red;
            }
        }
        private void HideAllEles()
        {
            alarmLeftLabels.ForEach(a => a.Visible = false);
            alarmCheckBoxes.ForEach(a => a.Visible = false);
            alarmNumbers.ForEach(a => a.ForEach(n => n.Visible = false));

        }
        private void ListAllControls()
        {
            #region alarmLeftLabels
            alarmLeftLabels.Add(uiLabel_alarmleft1);
            alarmLeftLabels.Add(uiLabel_alarmleft2);
            alarmLeftLabels.Add(uiLabel_alarmleft3);
            alarmLeftLabels.Add(uiLabel_alarmleft4);
            alarmLeftLabels.Add(uiLabel_alarmleft5);
            alarmLeftLabels.Add(uiLabel_alarmleft6);
            alarmLeftLabels.Add(uiLabel_alarmleft7);
            alarmLeftLabels.Add(uiLabel_alarmleft8);
            alarmLeftLabels.Add(uiLabel_alarmleft9);
            alarmLeftLabels.Add(uiLabel_alarmleft10);
            #endregion
            #region alarmCheckBoxes
            alarmCheckBoxes.Add(uiCheckBox_alarm1);
            alarmCheckBoxes.Add(uiCheckBox_alarm2);
            alarmCheckBoxes.Add(uiCheckBox_alarm3);
            alarmCheckBoxes.Add(uiCheckBox_alarm4);
            alarmCheckBoxes.Add(uiCheckBox_alarm5);
            alarmCheckBoxes.Add(uiCheckBox_alarm6);
            alarmCheckBoxes.Add(uiCheckBox_alarm7);
            alarmCheckBoxes.Add(uiCheckBox_alarm8);
            alarmCheckBoxes.Add(uiCheckBox_alarm9);
            alarmCheckBoxes.Add(uiCheckBox_alarm10);
            for (int i = 0; i < alarmCheckBoxes.Count; i++)
            {
                alarmCheckBoxes[i].Tag = $"{i + 1}_1";
                alarmCheckBoxes[i].CheckedChanged += EditAlarm_Change;
            }
            #endregion
            #region alarmNumbers
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_1_1, uiTextBox_alarm_1_2, uiTextBox_alarm_1_3, uiTextBox_alarm_1_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_2_1, uiTextBox_alarm_2_2, uiTextBox_alarm_2_3, uiTextBox_alarm_2_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_3_1, uiTextBox_alarm_3_2, uiTextBox_alarm_3_3, uiTextBox_alarm_3_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_4_1, uiTextBox_alarm_4_2, uiTextBox_alarm_4_3, uiTextBox_alarm_4_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_5_1, uiTextBox_alarm_5_2, uiTextBox_alarm_5_3, uiTextBox_alarm_5_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_6_1, uiTextBox_alarm_6_2, uiTextBox_alarm_6_3, uiTextBox_alarm_6_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_7_1, uiTextBox_alarm_7_2, uiTextBox_alarm_7_3, uiTextBox_alarm_7_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_8_1, uiTextBox_alarm_8_2, uiTextBox_alarm_8_3, uiTextBox_alarm_8_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_9_1, uiTextBox_alarm_9_2, uiTextBox_alarm_9_3, uiTextBox_alarm_9_4 });
            alarmNumbers.Add(new List<UITextBox> { uiTextBox_alarm_10_1, uiTextBox_alarm_10_2, uiTextBox_alarm_10_3, uiTextBox_alarm_10_4 });
            for (int i = 0; i < alarmNumbers.Count; i++)
            {
                var _chnAlarm = alarmNumbers[i];
                for (var j = 1; j < _chnAlarm.Count; j++)
                {
                    alarmNumbers[i][j].Tag = $"{i + 1}_{j + 1}";
                    alarmNumbers[i][j].TextChanged += EditAlarm_Change;
                }
            }
            #endregion
            #region adminButtons
            adminButtons = new List<UIButton> { uiSymbolButton_rename, uiSymbolButton_copy_in, /*uiSymbolButton_copy_out,*/ uiSymbolButton_download, uiSymbolButton_New, uiSymbolButton_delete, uiSymbolButton_save };
            #endregion
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "pf");
                if (ctrl is UIGroupBox)
                {
                    var _panel = (UIGroupBox)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "pf");
                    }
                }
            }
        }
        private void SetObjectEnable(object ctrl, bool flag)
        {
            string pattern = @"^uiTextBox_alarm_\d+_1$";
            if (ctrl is UICheckBox)
            {
                var _ctrl = (UICheckBox)ctrl;
                _ctrl.Enabled = flag;
            }
            else if (ctrl is UITextBox)
            {
                var _ctrl = (UITextBox)ctrl;
                if (!Regex.IsMatch(_ctrl.Name, pattern))
                    _ctrl.Enabled = flag;
            }
        }
        public void HideAdminButtons()
        {
            bool flag = CommonFunction.CheckAccessGongyi();
            adminButtons.ForEach(m => m.Visible = flag);
            SetAllControlEnable();
        }
        private void SetAllControlEnable()
        {
            bool flag = CommonFunction.CheckAccessGongyi();
            foreach (var ctrl in this.Controls)
            {
                SetObjectEnable(ctrl, flag);
                if (ctrl is UIGroupBox)
                {
                    var _panel = (UIGroupBox)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        SetObjectEnable(panelctrl, flag);
                    }
                }
            }
        }
        private void BuildAllPageModels()
        {
            GlobalVar.D_PfPageConfigModel = BuildDSystemPageData();
            GlobalVar.C_PfPageConfigModel = BuildCSystemPageData();
            GlobalVar.E_PfPageConfigModel = BuildESystemPageData();
        }
        #region 字体
        private void IniFont()
        {
            TopBtnFont = SetTopBtnFont();
            SysLabelFont = SetSysLableBtnFont();
        }
        private Font SetTopBtnFont()
        {
            var font = new Font("Arial", 10);
            return font;
        }
        private Font SetSysLableBtnFont()
        {
            var font = new Font("Arial", 9);
            return font;
        }
        #endregion
        #region Auto New TopButtons
        private void SetAllBtnColor()
        {
            chButtons.ForEach(c => c.FillColor = Color.DodgerBlue);
        }
        private void SwitchChnClickHandler(object sender, EventArgs e)
        {
            UIButton clickedButton = (UIButton)sender;
            int i = Convert.ToInt32(clickedButton.Tag);
            SetAllBtnColor();
            chButtons[i - 1].FillColor = Color.DarkSeaGreen;
            uiGroupBox_Alarm.Text = $"[{clickedButton.Text}]" + " Alarm";
            nowEditAlarm = i;
            switch (nowTopType)
            {
                case "D":
                    GetAllAlarmsValue(GlobalVar.D_PfPageConfigModel);
                    break;
                case "C":
                    GetAllAlarmsValue(GlobalVar.C_PfPageConfigModel);
                    break;
                case "E":
                    GetAllAlarmsValue(GlobalVar.E_PfPageConfigModel);
                    break;
                default:
                    break;
            }

        }
        private int GetNewButtonWidth(int count)
        {
            int width = (this.Size.Width - leftStart - rightEnd) / count;
            if (width > topBtnMaxWidth)
                return topBtnMaxWidth;
            else
                return width;
        }
        private void NewAllButtons(int newWidth, int count, string baseType)
        {
            chButtons.ForEach(b => this.Controls.Remove(b));
            chButtons = new List<UIButton>();
            //   int newWidth = GetNewButtonWidth();
            if (count == 0) return;
            for (int i = 1; i <= count; i++)
            {
                var btnName = $"{baseType}{i}";

                var _x = leftStart + (i - 1) * newWidth;
                var newBtn = NewUIButton(i, btnName, _x, topBtnY, newWidth - sysbuttonSpace, topBtnHeight, TopBtnFont);
                this.Controls.Add(newBtn);
                chButtons.Add(newBtn);
            }
            SwitchChnClickHandler(chButtons[0], null);

        }
        private UIButton NewUIButton(int order, string text, int x, int y, int width, int height, Font font, string baseName = "button_switch_CH")
        {
            var btn = new UIButton();
            btn.Size = new Size(width, height);
            btn.Location = new Point(x, y);
            btn.Name = $"{baseName}{order}";
            btn.Text = text;
            btn.Font = font;
            btn.Tag = order;
            btn.RectColor = Color.White;
            btn.Click += SwitchChnClickHandler;
            return btn;
        }

        #endregion
        #region Auto New sys

        private void NewAllSysLabels(PfPageConfigModel model)
        {
            sysLabels.ForEach(b => this.Controls.Remove(b));
            sysLabels = new List<UILabel>();
            //   int newWidth = GetNewButtonWidth();
            int _count = 0;
            for (int i = 1; i <= model.systemModel.checks.Count; i++)
            {
                var c = model.systemModel.checks[i - 1];
                var lblName = LanguageSet.SetL("pf", c.parName);

                var _y = sysLabelStartY + _count * (sysLabelHeight + sysLabelSpace);
                var newlbl = NewUiSysLabel(_count, lblName, sysLabelStartX, _y, sysLabelWidth, sysLabelHeight, SysLabelFont);
                this.Controls.Add(newlbl);
                sysLabels.Add(newlbl);
                _count++;
            }
            for (int i = 1; i <= model.systemModel.inputs.Count; i++)
            {
                var c = model.systemModel.inputs[i - 1];
                var lblName = LanguageSet.SetL("pf", c.parName);

                var _y = sysLabelStartY + _count * (sysLabelHeight + sysLabelSpace);
                var newlbl = NewUiSysLabel(_count, lblName, sysLabelStartX, _y, sysLabelWidth, sysLabelHeight, SysLabelFont);
                this.Controls.Add(newlbl);
                sysLabels.Add(newlbl);
                _count++;
            }

        }
        private void NewAllSysTextAndChecks(PfPageConfigModel model, int count, int newWidth, string topType)
        {
            sysTextBoxs.ForEach(b => b.ForEach(
               s => this.Controls.Remove(s)
                ));
            sysCheckBoxs.ForEach(b => b.ForEach(
   s => this.Controls.Remove(s)
    ));
            sysTextBoxs = new List<List<UITextBox>>();
            sysCheckBoxs = new List<List<UICheckBox>>();
            //   int newWidth = GetNewButtonWidth();

            for (int chn = 1; chn <= count; chn++)
            {
                int _count = 0;
                var list = new List<UICheckBox>();
                var _x = leftStart + (chn - 1) * newWidth;
                for (int i = 1; i <= model.systemModel.checks.Count; i++)
                {
                    //var c = model.systemModel.checks[i - 1];
                    //var lblName = LanguageSet.SetL("pf", c.parName);

                    var _y = sysLabelStartY + _count * (sysLabelHeight + sysLabelSpace);
                    var newck = NewUiSysCheck(chn, i, _x, _y, newWidth - sysbuttonSpace, sysLabelHeight, SysLabelFont, topType);
                    this.Controls.Add(newck);
                    list.Add(newck);
                    _count++;
                }
                sysCheckBoxs.Add(list);
            }
            // return;
            for (int chn = 1; chn <= count; chn++)
            {
                int _count = model.systemModel.checks.Count;
                var list = new List<UITextBox>();
                var _x = leftStart + (chn - 1) * newWidth;
                for (int i = 1; i <= model.systemModel.inputs.Count; i++)
                {
                    //var c = model.systemModel.checks[i - 1];
                    //var lblName = LanguageSet.SetL("pf", c.parName);

                    var _y = sysLabelStartY + _count * (sysLabelHeight + sysLabelSpace);
                    var newT = NewUiSysText(model.systemModel.inputs[i - 1].numType, chn, i, _x, _y, newWidth - sysbuttonSpace, sysLabelHeight, SysLabelFont, topType);
                    this.Controls.Add(newT);
                    list.Add(newT);
                    _count++;
                }
                sysTextBoxs.Add(list);
            }

            //for (int i = 1; i <= model.systemModel.inputs.Count; i++)
            //{
            //    var c = model.systemModel.inputs[i - 1];
            //    var lblName = LanguageSet.SetL("pf", c.parName);

            //    var _y = sysLabelStartY + _count * sysLabelHeight;
            //    var newlbl = NewUiSysLabel(_count, lblName, sysLabelStartX, _y, sysLabelWidth, sysLabelHeight, SysLabelFont);
            //    this.Controls.Add(newlbl);
            //    sysLabels.Add(newlbl);
            //    _count++;
            //}

        }
        private async void NewAllAlarms(PfPageConfigModel model)
        {
            try
            {
                HideAllEles();
                for (int i = 0; i < model.alarmModel.alarms.Count; i++)
                {
                    var a = model.alarmModel.alarms[i];
                    alarmLeftLabels[i].Visible = true;
                    alarmLeftLabels[i].Text = LanguageSet.SetL("pf", a.parName);
                    alarmCheckBoxes[i].Visible = true;
                    alarmNumbers[i][0].Visible = true;
                    for (int j = 0; j < a.numUse.Count; j++)
                    {
                        var _use = a.numUse[j];
                        //if (j == 0)
                        //{
                        //    if (_use)
                        //        alarmCheckBoxes[i].Visible = true;
                        //}
                        //else
                        //{
                        if (_use)
                            alarmNumbers[i][j + 1].Visible = true;
                        // }

                    }

                }
                GetAllAlarmsValue(model);
            }
            catch (Exception ex)
            {
                throw new Exception("NewAllAlarms" + ex.Message);
            }
        }
        private void GetAllAlarmsValue(PfPageConfigModel model)
        {
            for (int i = 0; i < model.alarmModel.alarms.Count; i++)
            {
                var a = model.alarmModel.alarms[i];

                alarmCheckBoxes[i].Checked = GetAlarmCheckBox(nowTopType, i + 1, nowEditAlarm);

                for (int j = 0; j < a.numUse.Count; j++)
                {
                    var _use = a.numUse[j];
                    if (_use)
                        alarmNumbers[i][j + 1].Text = GetAlarmInput(nowTopType, i + 1, nowEditAlarm, j + 2);

                }

            }
        }
        private UILabel NewUiSysLabel(int order, string text, int x, int y, int width, int height, Font font, string baseName = "sys_lbl")
        {
            var lbl = new UILabel();
            lbl.Size = new Size(width, height);
            lbl.Location = new Point(x, y);
            lbl.Name = $"{baseName}{order}";
            lbl.Text = text;
            lbl.Font = font;

            return lbl;

        }
        private UITextBox NewUiSysText(bool floatFlag, int chnOrder, int parOrder, int x, int y, int width, int height, Font font, string topType, string baseName = "sys_text")
        {
            var txt = new UITextBox();
            txt.Size = new Size(width, height);
            txt.Location = new Point(x, y);
            txt.Name = $"{baseName}{chnOrder}_{parOrder}";
            txt.Font = font;
            txt.Tag = $"{topType}_{chnOrder}_{parOrder}";
            txt.Text = GetSysInput(topType, parOrder, chnOrder);
            txt.TextChanged += EditPF_Change;
            // 20240124 Anders 暂时不考虑事件销毁，影响性能时再做处理
            txt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtBox_MouseDown);
            if (floatFlag) txt.RectColor = Color.DarkViolet;
            else txt.RectColor = Color.DeepSkyBlue;
            return txt;
        }

        #region 屏幕弹窗有关的代码
        // 申明要使用的dll和api
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void txtBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsProcessOpen("osk"))
            {
                OpenKeyBoard();
            }
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

        private void OpenKeyBoard()
        {
            //Hook2OpenKeyboard.ShowKeyboard();
            try
            {
                Process kbpr = Process.Start(@"C:\Windows\System32\osk.exe");
                kbpr.EnableRaisingEvents = true;
                //kbpr.Exited += new EventHandler(ScreenKeyboard_Exited);
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

        #endregion

        private UICheckBox NewUiSysCheck(int chnOrder, int parOrder, int x, int y, int width, int height, Font font, string topType, string baseName = "sys_check")
        {
            var c = new UICheckBox();
            c.Size = new Size(width, height);
            c.Location = new Point(x, y);
            c.Name = $"{baseName}{chnOrder}_{parOrder}";
            c.Font = font;
            c.Tag = $"{topType}_{chnOrder}_{parOrder}";
            c.CheckBoxSize = height;
            c.Checked = GetSysCheckBox(topType, parOrder, chnOrder);
            c.CheckedChanged += EditPF_Change;
            return c;
        }

        #endregion
        #region 切换CDE

        /// <summary>
        /// 切换CDE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchTopType(object sender, EventArgs e)
        {

            try
            {
                nowEditAlarm = 1;
                HideAllEles();
                if (editPf == null) return;
                UISymbolButton clickedButton = (UISymbolButton)sender;
                btn_switch_type_c.FillColor = Color.Gray;
                btn_switch_type_d.FillColor = Color.Gray;
                btn_switch_type_e.FillColor = Color.Gray;
                clickedButton.FillColor = Color.Tomato;
                int _newWidth = 0;
                int _count = 0;
                switch (clickedButton.Tag)
                {
                    case "D":
                        _count = GlobalVar.systemSetting.d_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "D";
                        NewAllSysLabels(GlobalVar.D_PfPageConfigModel);
                        NewAllSysTextAndChecks(GlobalVar.D_PfPageConfigModel, _count, _newWidth, nowTopType);
                        NewAllAlarms(GlobalVar.D_PfPageConfigModel);
                        break;
                    case "C":
                        _count = GlobalVar.systemSetting.c_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "C";
                        NewAllSysLabels(GlobalVar.C_PfPageConfigModel);
                        NewAllSysTextAndChecks(GlobalVar.C_PfPageConfigModel, _count, _newWidth, nowTopType);
                        NewAllAlarms(GlobalVar.C_PfPageConfigModel);
                        break;
                    case "E":
                        _count = GlobalVar.systemSetting.e_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "E";
                        NewAllSysLabels(GlobalVar.E_PfPageConfigModel);
                        NewAllSysTextAndChecks(GlobalVar.E_PfPageConfigModel, _count, _newWidth, nowTopType);
                        NewAllAlarms(GlobalVar.E_PfPageConfigModel);
                        break;
                    default:
                        break;
                }
                NewAllButtons(_newWidth, _count, nowTopType);
                SetAllControlEnable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SwitchTopType::" + ex.Message);
            }


        }

        private void OnlyRebuildSys()
        {

            try
            {
                int _newWidth = 0;
                int _count = 0;
                switch (nowTopType)
                {
                    case "D":
                        _count = GlobalVar.systemSetting.d_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "D";

                        NewAllSysTextAndChecks(GlobalVar.D_PfPageConfigModel, _count, _newWidth, nowTopType);
                        break;
                    case "C":
                        _count = GlobalVar.systemSetting.c_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "C";
                        NewAllSysTextAndChecks(GlobalVar.C_PfPageConfigModel, _count, _newWidth, nowTopType);
                        break;
                    case "E":
                        _count = GlobalVar.systemSetting.e_chn_count;
                        _newWidth = GetNewButtonWidth(_count);
                        nowTopType = "E";
                        NewAllSysTextAndChecks(GlobalVar.E_PfPageConfigModel, _count, _newWidth, nowTopType);
                        break;
                    default:
                        break;
                }
                SwitchChnClickHandler(chButtons[nowEditAlarm - 1], null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("SwitchTopType::" + ex.Message);
            }


        }
        #endregion
        #region DCE Model
        private PfPageConfigModel BuildDSystemPageData()
        {
            var model = new PfPageConfigModel();
            #region systemModel input

            for (int i = 1; i <= 6; i++)
            {
                model.systemModel.inputs.Add(new PfPageConfigSystemSingleModel
                {
                    parName = $"D_System{i}"
                });
            }
            model.systemModel.inputs[0].numType = true;
            model.systemModel.inputs[1].numType = true;
            #endregion
            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= /*8*/4; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"D_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 10; i++)
            {
                if (i > 5)//多段检测1--5
                    model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                    {
                        parName = $"D_Alarm{i}"
                    });
                else
                    model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                    {
                        parName = $"D_Alarm{i}",
                        numUse = new List<bool> { true, true, false }
                    });


            }

            #endregion
            return model;
        }
        private PfPageConfigModel BuildCSystemPageData()
        {
            var model = new PfPageConfigModel();
            #region systemModel input

            for (int i = 1; i <= 14; i++)
            {
                model.systemModel.inputs.Add(new PfPageConfigSystemSingleModel
                {
                    parName = $"C_System{i}"
                });
            }
            model.systemModel.inputs[0].numType = true;
            model.systemModel.inputs[1].numType = true;
            model.systemModel.inputs[2].numType = true;
            model.systemModel.inputs[3].numType = true;
            model.systemModel.inputs[4].numType = true;
            model.systemModel.inputs[5].numType = true;

            #endregion
            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= 6; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"C_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 6; i++)
            {
                if (i > 5)//多段检测1--5
                    model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                    {
                        parName = $"C_Alarm{i}"
                    });
                else
                    model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                    {
                        parName = $"C_Alarm{i}",
                        numUse = new List<bool> { true, true, false }
                    });

            }
            #endregion
            return model;
        }
        private PfPageConfigModel BuildESystemPageData()
        {

            var model = new PfPageConfigModel();

            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= 3; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"E_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 1; i++)
            {
                model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                {
                    parName = $"E_Alarm{i}",
                    numUse = new List<bool> { true, true, false }
                });
            }
            #endregion
            return model;
        }
        #endregion
        #region 编辑配方
        private void EditPF_Change(object sender, EventArgs e)
        {
            try
            {
                if (sender is UITextBox textBox)
                {
                    var strs = textBox.Tag.ToString().Split("_");
                    EditPF("text", strs[0], Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]));

                }
                else if (sender is UICheckBox checkBox)
                {
                    var strs = checkBox.Tag.ToString().Split("_");
                    EditPF("check", strs[0], Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]));
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void EditPF(string edittype, string topType, int chn, int order/*DCE*/)
        {
            try
            {
                if (editPf == null)
                {
                    Work.ShowErrorMessage("请先加载编辑配方");
                    return;
                }
                if (editPfDetails == null)
                    editPfDetails = new List<PfDetailTable>();
                //var _topModel = new PfPageConfigModel();
                //switch (topType)
                //{
                //    case "D":
                //        _topModel = D_PfPageConfigModel;
                //        break;
                //    case "C":
                //        _topModel = C_PfPageConfigModel;
                //        break;
                //    case "E":
                //        _topModel = E_PfPageConfigModel;
                //        break;
                //    default:
                //        throw new Exception("Get Chn Type Error");
                //}
                var _exist = editPfDetails.Exists(m => m.chnType == topType && m.chnorder == chn);
                if (!_exist)
                {
                    editPfDetails.Add(new PfDetailTable { chnType = topType, chnorder = chn, chnName = $"{topType}{chn}" });
                }
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == topType && m.chnorder == chn);
                switch (edittype)
                {
                    case "check":
                        SetSysCheckBox(_edit, topType, order, chn);
                        break;
                    case "text":
                        SetSysInput(_edit, topType, order, chn);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }

        private void EditAlarm_Change(object sender, EventArgs e)
        {
            try
            {
                if (sender is UITextBox textBox)
                {
                    var strs = textBox.Tag.ToString().Split("_");
                    EditAlarm("text", Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]));

                }
                else if (sender is UICheckBox checkBox)
                {
                    var strs = checkBox.Tag.ToString().Split("_");
                    EditAlarm("check", Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]));
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void EditAlarm(string edittype, int order, int colorder = 1/*DCE*/)
        {
            try
            {
                if (editPf == null)
                {
                    Work.ShowErrorMessage("请先加载编辑配方");
                    return;
                }
                if (editPfDetails == null)
                    editPfDetails = new List<PfDetailTable>();
                var _exist = editPfDetails.Exists(m => m.chnType == nowTopType && m.chnorder == nowEditAlarm);
                if (!_exist)
                {
                    editPfDetails.Add(new PfDetailTable { chnType = nowTopType, chnorder = nowEditAlarm, chnName = $"{nowTopType}{nowEditAlarm}" });
                }
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == nowTopType && m.chnorder == nowEditAlarm);
                switch (edittype)
                {
                    case "check":
                        SetAlarmCheckBox(_edit, nowTopType, order);
                        break;
                    case "text":
                        SetAlarmInput(_edit, nowTopType, order, colorder);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }
        private bool SetSysCheckBox(PfDetailTable _edit, string topType, int order, int chn)
        {
            try
            {
                bool value = sysCheckBoxs[chn - 1][order - 1].Checked;
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                _edit.use_ch = value;
                                break;
                            case 2:
                                if (value == true && (_edit.select_yeya == true || _edit.select_jixie == true))
                                {
                                    _edit.select_paiqi = false;
                                    sysCheckBoxs[chn - 1][order - 1].Checked = false;
                                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "facheck"));
                                }
                                else
                                    _edit.select_paiqi = value;
                                break;
                            case 3:
                                if (value == true && (_edit.select_paiqi == true || _edit.select_jixie == true))
                                {
                                    _edit.select_yeya = false;
                                    sysCheckBoxs[chn - 1][order - 1].Checked = false;
                                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "facheck"));
                                }
                                else
                                    _edit.select_yeya = value;
                                break;
                            case 4:
                                if (value == true && (_edit.select_paiqi == true || _edit.select_yeya == true))
                                {
                                    _edit.select_jixie = false;
                                    sysCheckBoxs[chn - 1][order - 1].Checked = false;
                                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "facheck"));
                                }
                                else
                                    _edit.select_jixie = value;
                                break;
                            case 5:
                                _edit.use_chuisao_M = value;
                                break;
                            case 6:
                                _edit.select_gauging_M = value;
                                break;
                            case 7:
                                _edit.select_auto_S = value;
                                break;
                            case 8:
                                _edit.use_VAC_time = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                _edit.use_ch = value;
                                break;
                            case 2:
                                _edit.use_blow = value;
                                break;
                            case 3:
                                _edit.use_VAC_time = value;
                                break;
                            case 4:
                                _edit.enable_zu1 = value;
                                break;
                            case 5:
                                _edit.enable_zu2 = value;
                                break;
                            case 6:
                                _edit.enable_zu3 = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                _edit.use_ch = value;
                                break;
                            case 2:
                                _edit.use_VAC_hemu = value;
                                break;
                            case 3:
                                _edit.use_VAC_stop = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private bool GetSysCheckBox(string topType, int order, int chn)
        {
            try
            {
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == topType && m.chnorder == chn);
                if (_edit == null) return false;
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                return _edit.use_ch ?? false;
                            case 2:
                                return _edit.select_paiqi ?? false;
                            case 3:
                                return _edit.select_yeya ?? false;
                            case 4:
                                return _edit.select_jixie ?? false;
                            case 5:
                                return _edit.use_chuisao_M ?? false;
                            case 6:
                                return _edit.select_gauging_M ?? false;
                            case 7:
                                return _edit.select_auto_S ?? false;
                            case 8:
                                return _edit.use_VAC_time ?? false;
                            default:
                                break;
                        }
                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                return _edit.use_ch ?? false;
                            case 2:
                                return _edit.use_blow ?? false;
                            case 3:
                                return _edit.use_VAC_time ?? false;
                            case 4:
                                return _edit.enable_zu1 ?? false;
                            case 5:
                                return _edit.enable_zu2 ?? false;
                            case 6:
                                return _edit.enable_zu3 ?? false;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                return _edit.use_ch ?? false;
                            case 2:
                                return _edit.use_VAC_hemu ?? false;
                            case 3:
                                return _edit.use_VAC_stop ?? false;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }


                return false;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private bool SetSysInput(PfDetailTable _edit, string topType, int order, int chn)
        {
            try
            {
                string value = sysTextBoxs[chn - 1][order - 1].Text.Trim();
                if (value == "")
                {
                    //sysTextBoxs[chn - 1][order - 1].Text = "";
                    //return true;
                    value = "0";
                }
                bool _parType = false;
                switch (topType)
                {
                    case "D":
                        _parType = GlobalVar.D_PfPageConfigModel.systemModel.inputs[order - 1].numType;

                        break;
                    case "C":
                        _parType = GlobalVar.C_PfPageConfigModel.systemModel.inputs[order - 1].numType;

                        break;
                    case "E":
                        _parType = GlobalVar.E_PfPageConfigModel.systemModel.inputs[order - 1].numType;
                        break;
                    default:
                        break;
                }
                if (!CommonFunction.CheckStrNumType(value, _parType))
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                    sysTextBoxs[chn - 1][order - 1].Text = "";
                    return false;
                }
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                _edit.auto_startpoint = Convert.ToSingle(value);
                                break;
                            case 2:
                                _edit.auto_endpoint = Convert.ToSingle(value);
                                break;
                            case 3:
                                _edit.filter_time = Convert.ToInt16(value);
                                break;
                            case 4:
                                _edit.Blow_Delay_time = Convert.ToInt16(value);
                                break;
                            case 5:
                                _edit.Blow_time = Convert.ToInt16(value);
                                break;
                            case 6:
                                _edit.VAC_time = Convert.ToInt16(value);
                                break;
                            default:
                                break;
                        }

                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                _edit.auto_startpoint = Convert.ToSingle(value);
                                break;
                            case 2:
                                _edit.auto_startpoint2 = Convert.ToSingle(value);
                                break;
                            case 3:
                                _edit.auto_startpoint3 = Convert.ToSingle(value);
                                break;
                            case 4:
                                _edit.auto_endpoint = Convert.ToSingle(value);
                                break;
                            case 5:
                                _edit.auto_endpoint2 = Convert.ToSingle(value);
                                break;
                            case 6:
                                _edit.auto_endpoint3 = Convert.ToSingle(value);
                                break;
                            case 7:
                                _edit.VAC_time = Convert.ToInt16(value);
                                break;
                            case 8:
                                _edit.VAC_time2 = Convert.ToInt16(value);
                                break;
                            case 9:
                                _edit.VAC_time3 = Convert.ToInt16(value);
                                break;
                            case 10:
                                _edit.Blow_Delay_time = Convert.ToInt16(value);
                                break;
                            case 11:
                                _edit.Blow_INR_time = Convert.ToInt16(value);
                                break;
                            case 12:
                                _edit.Blow_time = Convert.ToInt16(value);
                                break;
                            case 13:
                                _edit.Blow_time2 = Convert.ToInt16(value);
                                break;
                            case 14:
                                _edit.Blow_time3 = Convert.ToInt16(value);
                                break;
                            default:
                                break;
                        }
                        break;
                    case "E":

                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private string GetSysInput(string topType, int order, int chn)
        {
            try
            {
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == topType && m.chnorder == chn);
                if (_edit == null) return "0";

                //bool _parType = false;
                //switch (topType)
                //{
                //    case "D":
                //        _parType = D_PfPageConfigModel.systemModel.inputs[order - 1].numType;

                //        break;
                //    case "C":
                //        _parType = C_PfPageConfigModel.systemModel.inputs[order - 1].numType;

                //        break;
                //    case "E":
                //        _parType = E_PfPageConfigModel.systemModel.inputs[order - 1].numType;
                //        break;
                //    default:
                //        break;
                //}
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                return (_edit.auto_startpoint ?? 0).ToString();
                            case 2:
                                return (_edit.auto_endpoint ?? 0).ToString();
                            case 3:
                                return (_edit.filter_time ?? 0).ToString();
                            case 4:
                                return (_edit.Blow_Delay_time ?? 0).ToString();
                            case 5:
                                return (_edit.Blow_time ?? 0).ToString();
                            case 6:
                                return (_edit.VAC_time ?? 0).ToString();
                            default:
                                break;
                        }

                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                return (_edit.auto_startpoint ?? 0).ToString();
                            case 2:
                                return (_edit.auto_startpoint2 ?? 0).ToString();
                            case 3:
                                return (_edit.auto_startpoint3 ?? 0).ToString();
                            case 4:
                                return (_edit.auto_endpoint ?? 0).ToString();
                            case 5:
                                return (_edit.auto_endpoint2 ?? 0).ToString();
                            case 6:
                                return (_edit.auto_endpoint3 ?? 0).ToString();
                            case 7:
                                return (_edit.VAC_time ?? 0).ToString();
                            case 8:
                                return (_edit.VAC_time2 ?? 0).ToString();
                            case 9:
                                return (_edit.VAC_time3 ?? 0).ToString();
                            case 10:
                                return (_edit.Blow_Delay_time ?? 0).ToString();
                            case 11:
                                return (_edit.Blow_INR_time ?? 0).ToString();
                            case 12:
                                return (_edit.Blow_time ?? 0).ToString();
                            case 13:
                                return (_edit.Blow_time2 ?? 0).ToString();
                            case 14:
                                return (_edit.Blow_time3 ?? 0).ToString();
                            default:
                                break;
                        }
                        break;
                    case "E":

                        break;
                    default:
                        break;
                }
                return "0";
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return "0";
            }
        }

        private bool SetAlarmCheckBox(PfDetailTable _edit, string topType, int order)
        {
            try
            {
                bool value = alarmCheckBoxes[order - 1].Checked;
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                _edit.enable_Opentime = value;
                                break;
                            case 2:
                                _edit.enable_Closetime = value;
                                break;
                            case 3:
                                _edit.enable_Vactime = value;
                                break;
                            case 4:
                                _edit.enable_P_blow = value;
                                break;
                            case 5:
                                _edit.enable_Close_pos = value;
                                break;
                            case 6:
                                _edit.enable_P_vac = value;
                                break;
                            case 7:
                                _edit.enable_P_vac2 = value;
                                break;
                            case 8:
                                _edit.enable_P_vac3 = value;
                                break;
                            case 9:
                                _edit.enable_P_vac4 = value;
                                break;
                            case 10:
                                _edit.enable_P_vac5 = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                _edit.enable_Vactime = value;
                                break;
                            case 2:
                                _edit.enable_P_vac = value;
                                break;
                            case 3:
                                _edit.enable_P_blow = value;
                                break;
                            case 4:
                                _edit.enable_P_blow2 = value;
                                break;
                            case 5:
                                _edit.enable_P_blow3 = value;
                                break;
                            case 6:
                                _edit.use_checkPoint_C = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                _edit.enable_P_vac = value;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private bool GetAlarmCheckBox(string topType, int order, int chn)
        {
            try
            {
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == topType && m.chnorder == chn);
                if (_edit == null) return false;

                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_Opentime ?? false;
                            case 2:
                                return _edit.enable_Closetime ?? false;
                            case 3:
                                return _edit.enable_Vactime ?? false;
                            case 4:
                                return _edit.enable_P_blow ?? false;
                            case 5:
                                return _edit.enable_Close_pos ?? false;
                            case 6:
                                return _edit.enable_P_vac ?? false;
                            case 7:
                                return _edit.enable_P_vac2 ?? false;
                            case 8:
                                return _edit.enable_P_vac3 ?? false;
                            case 9:
                                return _edit.enable_P_vac4 ?? false;
                            case 10:
                                return _edit.enable_P_vac5 ?? false;
                            default:
                                break;
                        }
                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_Vactime ?? false;
                            case 2:
                                return _edit.enable_P_vac ?? false;
                            case 3:
                                return _edit.enable_P_blow ?? false;
                            case 4:
                                return _edit.enable_P_blow2 ?? false;
                            case 5:
                                return _edit.enable_P_blow3 ?? false;
                            case 6:
                                return _edit.use_checkPoint_C ?? false;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_P_vac ?? false;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private bool SetAlarmInput(PfDetailTable _edit, string topType, int order, int colOrder)
        {
            try
            {
                string value = alarmNumbers[order - 1][colOrder - 1].Text.Trim();
                if (value == "")
                {
                    //  alarmNumbers[order - 1][colOrder - 1].Text = "0";
                    value = "0";
                    return true;
                }

                if (!CommonFunction.CheckStrNumType(value, true))
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                    alarmNumbers[order - 1][colOrder - 1].Text = "";
                    return false;
                }
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    _edit.Opentime_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.Opentime_lo = Convert.ToSingle(value);
                                break;
                            case 2:
                                if (colOrder == 2)
                                    _edit.Closetime_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.Closetime_lo = Convert.ToSingle(value);
                                break;
                            case 3:
                                if (colOrder == 2)
                                    _edit.Vactime_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.Vactime_lo = Convert.ToSingle(value);
                                break;
                            case 4:
                                if (colOrder == 2)
                                    _edit.P_blow_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_blow_lo = Convert.ToSingle(value);
                                break;
                            case 5:
                                if (colOrder == 2)
                                    _edit.Close_pos_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.Close_pos_lo = Convert.ToSingle(value);
                                break;
                            case 6:
                                if (colOrder == 2)
                                    _edit.P_vac_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.P_vac_pos = Convert.ToSingle(value);
                                break;
                            case 7:
                                if (colOrder == 2)
                                    _edit.P_vac_hi2 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo2 = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.P_vac_pos2 = Convert.ToSingle(value);
                                break;
                            case 8:
                                if (colOrder == 2)
                                    _edit.P_vac_hi3 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo3 = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.P_vac_pos3 = Convert.ToSingle(value);
                                break;
                            case 9:
                                if (colOrder == 2)
                                    _edit.P_vac_hi4 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo4 = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.P_vac_pos4 = Convert.ToSingle(value);
                                break;
                            case 10:
                                if (colOrder == 2)
                                    _edit.P_vac_hi5 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo5 = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.P_vac_pos5 = Convert.ToSingle(value);
                                break;
                            default:
                                break;
                        }

                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    _edit.Vactime_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.Vactime_lo = Convert.ToSingle(value);
                                break;
                            case 2:
                                if (colOrder == 2)
                                    _edit.P_vac_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo = Convert.ToSingle(value);
                                break;
                            case 3:
                                if (colOrder == 2)
                                    _edit.P_blow_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_blow_lo = Convert.ToSingle(value);
                                break;
                            case 4:
                                if (colOrder == 2)
                                    _edit.P_blow_hi2 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_blow_lo2 = Convert.ToSingle(value);
                                break;
                            case 5:
                                if (colOrder == 2)
                                    _edit.P_blow_hi3 = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_blow_lo3 = Convert.ToSingle(value);
                                break;
                            case 6:
                                if (colOrder == 2)
                                    _edit.set_CheckPointHi_C = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.set_CheckPointLo_C = Convert.ToSingle(value);
                                else if (colOrder == 4)
                                    _edit.set_CheckPoint_C = Convert.ToSingle(value);
                                break;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    _edit.P_vac_hi = Convert.ToSingle(value);
                                else if (colOrder == 3)
                                    _edit.P_vac_lo = Convert.ToSingle(value);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private string GetAlarmInput(string topType, int order, int chn, int colOrder)
        {
            try
            {
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == topType && m.chnorder == chn);
                if (_edit == null) return "0";
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.Opentime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Opentime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 2:
                                if (colOrder == 2)
                                    return (_edit.Closetime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Closetime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 3:
                                if (colOrder == 2)
                                    return (_edit.Vactime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Vactime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 4:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 5:
                                if (colOrder == 2)
                                    return (_edit.Close_pos_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Close_pos_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 6:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos ?? 0).ToString();
                                else
                                    return "0";
                            case 7:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi2 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo2 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos2 ?? 0).ToString();
                                else
                                    return "0";
                            case 8:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi3 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo3 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos3 ?? 0).ToString();
                                else
                                    return "0";
                            case 9:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi4 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo4 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos4 ?? 0).ToString();
                                else
                                    return "0";
                            case 10:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi5 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo5 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos5 ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }

                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.Vactime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Vactime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 2:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 3:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 4:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi2 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo2 ?? 0).ToString();
                                else
                                    return "0";
                            case 5:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi3 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo3 ?? 0).ToString();
                                else
                                    return "0";
                            case 6:
                                if (colOrder == 2)
                                    return (_edit.set_CheckPointHi_C ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.set_CheckPointLo_C ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.set_CheckPoint_C ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return "0";
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return "0";
            }
        }


        #endregion
        private void uiSymbolButton_New_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {

                using (var _form = new NewPfForm())
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        if (_form.CopyChecked)
                        {
                            CopyFrowOthers(_form.copyDetails);
                        }
                        else
                        {
                            editPfDetails = new List<PfDetailTable>();
                        }
                        LoadEditPf(_form.createPf, editPfDetails);

                    }

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void VerificateAuto()
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }
        }

        private void uiSymbolButton_Load_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }
            try
            {
                using (var _form = new LoadPfForm())
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        LoadEditPf(_form.loadPf, _form.loadDetails);

                    }

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiSymbolButton_save_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                if (editPfDetails == null || editPfDetails.Count == 0)
                    throw new Exception("请先编辑配方");
                if (GlobalVar.usePf != null && editPf.id == GlobalVar.usePf.id)
                {
                    DialogResult result = MessageBox.Show("确认保存吗?当前配方正在使用,保存的同时会写入PLC", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        PFDetailDBCommon.SavePfDetailTable(editPf.id, editPfDetails, editPf.pfname);
                        ActivePlc();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("确认保存吗?", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        PFDetailDBCommon.SavePfDetailTable(editPf.id, editPfDetails, editPf.pfname);
                    }

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }
        private void IniAllAutoEles()
        {
            IniTopName();
            HideAllEles();
            sysLabels.ForEach(b => this.Controls.Remove(b));
            sysLabels = new List<UILabel>();
            sysTextBoxs.ForEach(b => b.ForEach(
    s => this.Controls.Remove(s)
     ));
            sysCheckBoxs.ForEach(b => b.ForEach(
   s => this.Controls.Remove(s)
    ));
            sysTextBoxs = new List<List<UITextBox>>();
            sysCheckBoxs = new List<List<UICheckBox>>();

        }
        private void uiSymbolButton_delete_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                if (GlobalVar.usePf != null && GlobalVar.usePf.id == editPf.id)
                    throw new Exception("该配方正在使用,不可删除");
                DialogResult result = MessageBox.Show("确认删除该配方吗?", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    PFDBCommon.DeletePfMainTable(editPf.id);
                    editPf = null;
                    editPfDetails = null;
                    IniAllAutoEles();
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void ActivePlc()
        {
            GlobalVar.commonAdsControl.WriteSTAll(editPfDetails);
            GlobalVar.commonAdsControl.WriteCommonString(".pf_name", 20, editPf.pfname);
            GlobalVar.systemSetting.use_pf_id = editPf.id;
            GlobalVar.usePf = (PfMainTable)CommonFunction.DeepCopy(editPf);
            GlobalVar.usePfDetails = (List<PfDetailTable>)CommonFunction.DeepCopy(editPfDetails);
            Work.SetSystemSettingPfId();
            IniTopName();
        }
        private void uiSymbolButton_Use_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                if (editPfDetails == null || editPfDetails.Count == 0)
                    throw new Exception("请先编辑配方");
                DialogResult result = MessageBox.Show("确认保存当前编辑配方,并载入PLC吗?", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    PFDetailDBCommon.SavePfDetailTable(editPf.id, editPfDetails, editPf.pfname);
                    ActivePlc();
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiSymbolButton_rename_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                using (var _form = new RenameForm(editPf.pfname))
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        PFDBCommon.RenamePfMainTable(editPf.id, _form.newName);
                        editPf.pfname = _form.newName;
                        uiLabel_editpf.Text = editPf.pfname;
                        uiLabel_editpf.BackColor = Color.Yellow;
                        if (editPf.id == GlobalVar.usePf.id)
                        {
                            GlobalVar.commonAdsControl.WriteCommonString(".pf_name", 20, editPf.pfname);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiSymbolButton_copy_in_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                if (editPfDetails == null || editPfDetails.Count == 0)
                    throw new Exception("请先编辑配方");
                if (!editPfDetails.Exists(m => m.chnorder == nowEditAlarm && m.chnType == nowTopType))
                    throw new Exception("请先编辑通道");
                var _copy = editPfDetails.FirstOrDefault(m => m.chnorder == nowEditAlarm && m.chnType == nowTopType);
                if ((nowTopType == "D" && GlobalVar.systemSetting.d_chn_count <= 1)
                 || (nowTopType == "C" && GlobalVar.systemSetting.c_chn_count <= 1)
                                 || (nowTopType == "E" && GlobalVar.systemSetting.e_chn_count <= 1))
                {
                    MessageBox.Show(nowTopType + "仅有一个通道,不可复制");
                    return;
                }
                int count = 0;
                var _list = new List<int>();
                if (nowTopType == "D") count = GlobalVar.systemSetting.d_chn_count;
                else if (nowTopType == "C") count = GlobalVar.systemSetting.c_chn_count;
                else if (nowTopType == "E") count = GlobalVar.systemSetting.e_chn_count;
                for (int i = 1; i <= count; i++)
                {
                    if (i != nowEditAlarm)
                        _list.Add(i);
                }
                using (var _form = new CopyInForm(_list, nowTopType))
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        _form.CheckedItems.ForEach(chn =>
                        {
                            var _exist = editPfDetails.Exists(m => m.chnType == nowTopType && m.chnorder == chn);
                            if (!_exist)
                            {
                                editPfDetails.Add(new PfDetailTable { chnType = nowTopType, chnorder = chn, chnName = $"{nowTopType}{chn}" });
                            }
                            var _edit = editPfDetails.FirstOrDefault(m => m.chnType == nowTopType && m.chnorder == chn);
                            PFDetailDBCommon.SetPfDetails(_edit, _copy);
                        });
                        OnlyRebuildSys();
                    }

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }
        private void CopyFrowOthers(List<PfDetailTable> rows)
        {
            if (rows == null || rows.Count == 0) return;
            if (editPfDetails == null) editPfDetails = new List<PfDetailTable>();
            rows.ForEach(r =>
            {
                var _exist = editPfDetails.Exists(m => m.chnType == r.chnType && m.chnorder == r.chnorder);
                if (!_exist)
                {
                    editPfDetails.Add(new PfDetailTable { chnType = r.chnType, chnorder = r.chnorder, chnName = $"{ r.chnType}{r.chnorder}" });
                }
                var _edit = editPfDetails.FirstOrDefault(m => m.chnType == r.chnType && m.chnorder == r.chnorder);
                PFDetailDBCommon.SetPfDetails(_edit, r);
            });
        }
        private void uiSymbolButton_copy_out_Click(object sender, EventArgs e)
        {
            try
            {
                if (editPf == null)
                    throw new Exception("请先载入配方");
                using (var _form = new CopyOutForm(editPf.id))
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        CopyFrowOthers(_form.loadDetails);
                    }
                    SwitchTopType(btn_switch_type_d, null);
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiSymbolButton_download_Click(object sender, EventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }

            try
            {
                using (var _form = new DownLoadForm())
                {
                    var _result = _form.ShowDialog();
                    if (_result == DialogResult.OK) // ShowDialog将阻塞，直到用户关闭窗口  
                    {
                        var _excelHelper = new ExcelHelperClass();
                        var workbook = _excelHelper.WriteAllpfMainTables(_form._downloadPfs);
                        // LoadEditPf(_form.loadPf, _form.loadDetails);
                        // 设置保存文件的对话框  
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                        saveFileDialog.Title = "Save an Excel File";

                        // 显示保存对话框  
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // 保存工作簿到指定的文件  
                            workbook.SaveAs(saveFileDialog.FileName);

                            // 关闭工作簿（这也会释放所有资源）  
                            workbook.Dispose();

                            // 显示消息框以确认文件已保存  
                            MessageBox.Show(LanguageSet.SetL("pf", "DownloadOk"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
                return;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }

        private void PfRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.配方 && !alarmRealbusy)
                        {
                            alarmRealbusy = true;
                            CommonTaskRead.ReadPfRealTime();
                            switch (nowTopType)
                            {
                                case "D":
                                    if (alarmNumbers[0][0].Visible)
                                        alarmNumbers[0][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].Opentime_Pvout.MathRound();
                                    if (alarmNumbers[1][0].Visible)
                                        alarmNumbers[1][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].Closetime_Pvout.MathRound();
                                    if (alarmNumbers[2][0].Visible)
                                        alarmNumbers[2][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].Vactime_Pvout.MathRound();
                                    if (alarmNumbers[3][0].Visible)
                                        alarmNumbers[3][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].P_blow_Pvout.MathRound();
                                    if (alarmNumbers[4][0].Visible)
                                        alarmNumbers[4][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].Close_pos_Pvout.MathRound();
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (alarmNumbers[5 + i][0].Visible)
                                            alarmNumbers[5 + i][0].Text = GlobalVar.plcData.hmi_alarmout_D[nowEditAlarm - 1].P_vac_Pvout[i].MathRound();
                                    }
                                    break;
                                case "C":
                                    if (alarmNumbers[0][0].Visible)
                                        alarmNumbers[0][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].Vactime_Pvout.MathRound();
                                    if (alarmNumbers[1][0].Visible)
                                        alarmNumbers[1][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].P_vac_Pvout.MathRound();
                                    if (alarmNumbers[2][0].Visible)
                                        alarmNumbers[2][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].P_blow1_Pvout.MathRound();
                                    if (alarmNumbers[3][0].Visible)
                                        alarmNumbers[3][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].P_blow2_Pvout.MathRound();
                                    if (alarmNumbers[4][0].Visible)
                                        alarmNumbers[4][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].P_blow3_Pvout.MathRound();
                                    if (alarmNumbers[5][0].Visible)
                                        alarmNumbers[5][0].Text = GlobalVar.plcData.hmi_alarmout_C[nowEditAlarm - 1].P_CheckPoint_Pvout.MathRound();
                                    break;
                                case "E":
                                    if (alarmNumbers[0][0].Visible)
                                        alarmNumbers[0][0].Text = GlobalVar.plcData.hmi_alarmout_E[nowEditAlarm - 1].P_vac_Pvout.MathRound();
                                    break;
                                default:
                                    break;
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        Work.frm_main.ShowErrorMessage(ex.Message);

                    }
                    finally
                    {
                        alarmRealbusy = false;
                        int millisecondsTimeout = 500 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }

                }
            });
        }

        delegate void SetVisibleCallBack(Control con, bool flag);
        private void SetVisible(Control con, bool flag)
        {
            if (con.InvokeRequired)
            {
                SetVisibleCallBack stcb = new SetVisibleCallBack(SetVisible);
                this.Invoke(stcb, new object[] { con, flag });
            }
            else
            {
                con.Visible = flag;
            }
        }
    }
}
