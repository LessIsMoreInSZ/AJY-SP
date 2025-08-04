
using DocumentFormat.OpenXml.Drawing.Diagrams;
using EF.Models.EF.DLL;
using SPWindowsForms.AdsConnect;
using SPWindowsForms.AppForms;
using SPWindowsForms.DbService;
using SPWindowsForms.SwitchForms;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public partial class FormMain : Form
    {
        private bool isAlarmUpdating = false;
        private bool isDragging = false;
        private System.Drawing.Point dragCursorPoint;
        private System.Drawing.Point dragFormPoint;
        private List<UILedBulb> uILedChnBulbDs = new List<UILedBulb>();
        private List<UILedBulb> uILedChnBulbCs = new List<UILedBulb>();
        private List<UILedBulb> uILedChnBulbEs = new List<UILedBulb>();
        private List<Label> uILedChnBulbDs_label = new List<Label>();
        private List<Label> uILedChnBulbCs_label = new List<Label>();
        private List<Label> uILedChnBulbEs_label = new List<Label>();
        public WSystemMainControl wSystemMainControl;
        public WPFControl wPFControl;// 工艺管理
        public WChartControl wChartControl;// 曲线显示
        public WLoginControl wLoginControl;
        public MachinePfLogControl machinePfLogControl;
        public WAlarmTableControl wAlarmTableControl;
        public WTongdaoControl wTongdaoControl;
        public WManualControl wManualControl;// 手动测试
        public WAutoControl wAutoControl;
        public VacPicture6001Control vacPicture6001Control;
        public VacPicture6002Control vacPicture6002Control;
        public VacPicture9001Control vacPicture9001Control;
        public VacSetControl vacSetControl;
        public WWeihuControl wWeihuControl;
        public WYsgControl wYsgControl;
        public WBenzuControl wBenzuControl;//泵组维护
        public WSheBeiGuanLiControl wSheBeiGuanLiControl;// 设备管理
        public WIoControl wIoControl;
        public WBaoYangControl wBaoYangControl;// 设备保养
        public WYeyaControl wYeyaControl;// 液压站
        public WChuanganqiControl wChuanganqiControl;
        public WZhujiControl wZhujiControl;// 主机信号模拟
        public DownloadTableControl wDownloadTableControl;
        public WXitongshedingControl wXitongshedingControl;
        private Process kbpr;
        private List<UserControl> mousemoveAdd = new List<UserControl>();
        private bool firstStartFlag = true;
        // 申明要使用的dll和api
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        // public WPFEditControl wPFEditControl;
        public FormMain()
        {
            InitializeComponent();
        }

        #region IniUI
        private void IniFormMain()
        {
            try
            {
                this.DoubleBuffered = true; // 启用双缓冲，减少拖动时的闪烁  
                //this.FormBorderStyle = FormBorderStyle.None; // 无边框
                IniTopPanel();
                IniAllControlName();
                IniAlarmTableHeader();
                //  labelVersion.Text = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
             this.Text=LanguageSet.SetL("Main", "label_Title")+  $" version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

                GlobalVar.commonAdsControl = new CommonAdsControl(GlobalVar.systemSetting.netID, GlobalVar.systemSetting.port);
                GlobalVar.commonAdsControl.CheckAdsConnection();
                GlobalVar.NowAlarms = AlarmLogCommon.GetNowAlarm();
                var needoffs = GlobalVar.NowAlarms.Where(m => !GlobalVar.PlcAlarms.allAlarmReports.Select(a => a.AlarmCode).Contains(m.alarmCode)).ToList();
                if (needoffs.Count > 0)
                {
                    AlarmLogCommon.OffAlarms(needoffs.Select(m=>m.id).ToList());
                    GlobalVar.NowAlarms = AlarmLogCommon.GetNowAlarm();
                }
                if (GlobalVar.NowAlarms.Count > 0)
                    firstStartFlag = false;
                SetUserLable();
                IniAllUi();
                button_menu10_Click(null, null);
                HideTank2();
                LogOutTask();
                UITask();
                panelRight.MouseMove += MainPanel_MouseMove;
                panelTop.MouseMove += MainPanel_MouseMove;
                this.WindowState = FormWindowState.Maximized;
               // this.TopMost = true;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        public void HideTank2()
        {
            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP9001")
            {
                label_unit4.Visible = false;
                label_topmiddle4.Visible = false;
                label_topmiddle_value4.Visible = false;
            }
        }
        public void SetUserLable()
        {
            if (String.IsNullOrEmpty(GlobalVar.UserNumber))
            {
                uiLabel_user.ForeColor = Color.Salmon;
                uiLabel_user.Text = /*"未登录"*/LanguageSet.SetL("Login", "notlogin");
            }
            else
            {
                uiLabel_user.ForeColor = Color.Black;
                uiLabel_user.Text = $"{GlobalVar.UserNumber}";
            }
        }
        private void IniAllUi()
        {

            wLoginControl = new WLoginControl();
            wPFControl = new WPFControl();
            wChartControl = new WChartControl();
            wSystemMainControl = new WSystemMainControl();
            machinePfLogControl = new MachinePfLogControl();
            wAlarmTableControl = new WAlarmTableControl();
            wTongdaoControl = new WTongdaoControl();
            wManualControl = new WManualControl();
            wAutoControl = new WAutoControl();
            wWeihuControl = new WWeihuControl();
            wYsgControl = new WYsgControl();
            wBenzuControl = new WBenzuControl();
            wSheBeiGuanLiControl = new WSheBeiGuanLiControl();
            wBaoYangControl = new WBaoYangControl();
            wYeyaControl = new WYeyaControl();
            wIoControl = new WIoControl();
            wChuanganqiControl = new WChuanganqiControl();
            wZhujiControl = new WZhujiControl();
            wDownloadTableControl = new DownloadTableControl();
            wXitongshedingControl = new WXitongshedingControl();
            if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
            {
                vacPicture9001Control = new VacPicture9001Control();
                vacSetControl = new VacSetControl();
            }
            else if(GlobalVar.systemSetting.machine_config.ToUpper() == "SP6002")
                vacPicture6002Control = new VacPicture6002Control();
            else
                vacPicture6001Control = new VacPicture6001Control();


            // wPFEditControl = new WPFEditControl();
        }
        private void AddSpaceButton(UISymbolButton btn)
        {
            var space = "      ";
            btn.Text = space + btn.Text;
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Main");
                if (ctrl is Panel)
                {
                    var _panel = (Panel)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "Main");
                    }
                }
            }
            AddSpaceButton(button_menu1);
            AddSpaceButton(button_menu2);
            AddSpaceButton(button_menu3);
            AddSpaceButton(button_menu4);
            AddSpaceButton(button_menu5);
            AddSpaceButton(button_menu6);
            AddSpaceButton(button_menu7);
            AddSpaceButton(button_menu8);
            AddSpaceButton(button_menu9);
            AddSpaceButton(button_menu10);
            AddSpaceButton(button_menu11);
            AddSpaceButton(button_menu12);
            AddSpaceButton(button_menu13);
            AddSpaceButton(button_menu14);
        }
        #endregion

        #region 右边栏的控制
        private void ShowUserControl(UserControl w)
        {
            //if (w != wZhujiControl) {
            //    CommonTaskRead.WirteZhujiAllfalse();
            //}
            //if (w == wBenzuControl || w == wWeihuControl || w == machinePfLogControl || w == wSheBeiGuanLiControl || w == wIoControl || w == wBaoYangControl || w == wYeyaControl || w == wYsgControl||w== wChuanganqiControl)
            //{
            //    if (!CommonFunction.CheckAccessShebei())
            //    {
            //        Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
            //        return;
            //    }
            //}

            // 20250120 Anders 增加非空判断
            //if (w == null) return;

            if (w != wBenzuControl)
            {
                CommonTaskRead.WriteEnable_ManAction_name(false);
            }
            w.Show();
            groupBox_main.Controls.Clear();
            groupBox_main.Controls.Add(w);
            if (!mousemoveAdd.Contains(w))
            {
                mousemoveAdd.Add(w);
                w.MouseMove += MainPanel_MouseMove;
            }

        }


        private void SetAllBtnBorderNone()
        {
            button_menu1.RectColor = Color.FromArgb(64, 64, 64);
            button_menu2.RectColor = Color.FromArgb(64, 64, 64);
            button_menu3.RectColor = Color.FromArgb(64, 64, 64);
            button_menu4.RectColor = Color.FromArgb(64, 64, 64);
            button_menu5.RectColor = Color.FromArgb(64, 64, 64);
            button_menu6.RectColor = Color.FromArgb(64, 64, 64);
            button_menu7.RectColor = Color.FromArgb(64, 64, 64);
            button_menu8.RectColor = Color.FromArgb(64, 64, 64);
            button_menu9.RectColor = Color.FromArgb(64, 64, 64);
            button_menu10.RectColor = Color.FromArgb(64, 64, 64);
            button_menu11.RectColor = Color.FromArgb(64, 64, 64);
            button_menu12.RectColor = Color.FromArgb(64, 64, 64);
            button_menu13.RectColor = Color.FromArgb(64, 64, 64);
            button_menu14.RectColor = Color.FromArgb(64, 64, 64);
        }
        private void SetBtnBorder(UISymbolButton btn)
        {
            SetAllBtnBorderNone();
            btn.RectColor = Color.FromArgb(80, 160, 255);
        }
        #endregion

        private void IniTopPanel()
        {
            panelTop.MouseDown += DragPanel_MouseDown;
            panelTop.MouseMove += DragPanel_MouseMove;
            panelTop.MouseUp += DragPanel_MouseUp;
            this.MouseMove += MyForm_MouseMove;
            this.MouseUp += MyForm_MouseUp;
            uILedChnBulbDs.Add(led_ch1);
            uILedChnBulbDs.Add(led_ch2);
            uILedChnBulbDs.Add(led_ch3);
            uILedChnBulbDs.Add(led_ch4);
            uILedChnBulbDs.Add(led_ch5);
            uILedChnBulbDs.Add(led_ch6);
            uILedChnBulbDs.Add(led_ch7);
            uILedChnBulbDs.Add(led_ch8);
            uILedChnBulbCs.Add(led_ch9);
            uILedChnBulbEs.Add(led_ch10);
            uILedChnBulbDs_label.Add(label_D1);
            uILedChnBulbDs_label.Add(label_D2);
            uILedChnBulbDs_label.Add(label_D3);
            uILedChnBulbDs_label.Add(label_D4);
            uILedChnBulbDs_label.Add(label_D5);
            uILedChnBulbDs_label.Add(label_D6);
            uILedChnBulbDs_label.Add(label_D7);
            uILedChnBulbDs_label.Add(label_D8);
            uILedChnBulbCs_label.Add(label_C1);
            uILedChnBulbEs_label.Add(label_E1);
            for(int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                if (uILedChnBulbDs.Count > i) uILedChnBulbDs[i].Visible = true;
                if (uILedChnBulbDs_label.Count > i) uILedChnBulbDs_label[i].Visible = true;
            }
            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                if (uILedChnBulbCs.Count > i) uILedChnBulbCs[i].Visible = true;
                if (uILedChnBulbCs_label.Count > i) uILedChnBulbCs_label[i].Visible = true;
            }
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                if (uILedChnBulbEs.Count > i) uILedChnBulbEs[i].Visible = true;
                if (uILedChnBulbEs_label.Count > i) uILedChnBulbEs_label[i].Visible = true;
            }
        }

        private void DragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // 计算鼠标移动了多少  
                int dx = Cursor.Position.X - dragCursorPoint.X;
                int dy = Cursor.Position.Y - dragCursorPoint.Y;

                // 更新窗体的位置  
                this.Location = new System.Drawing.Point(dragFormPoint.X + dx, dragFormPoint.Y + dy);
            }
        }

        private void DragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void MyForm_MouseMove(object sender, MouseEventArgs e)
        {
            // 如果不在拖动面板上且正在拖动，则停止拖动  
            if (!panelTop.ClientRectangle.Contains(e.Location) && isDragging)
            {
                isDragging = false;
            }
        }

        private void MyForm_MouseUp(object sender, MouseEventArgs e)
        {
            // 确保在鼠标释放时停止拖动，即使鼠标不在拖动面板上  
            isDragging = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            Work.MyAppStart();
            IniFormMain();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Work.frm_main.ShowErrorMessage("heh");
            try
            {
                //GlobalVar.commonAdsControl.Readst_sample();
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }

        }

        private void btnTopErrorClose_Click(object sender, EventArgs e)
        {
            btnTopErrorClose.Visible = false;
            labelTopError.Visible = false;
        }
        public void ShowErrorMessage(string message)
        {
            SetVisible(btnTopErrorClose, true);
            SetVisible(labelTopError, true);
            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("[yyyy-MM-dd HH:mm:ss]");
            SetText(labelTopError, $"{timeString} {message}");
            Work._logHelper.WriteLog(message);
        }

        #region Page Btn
        private void button_menu1_Click(object sender, EventArgs e)
        {

            ShowUserControl(wSystemMainControl);
            SetBtnBorder(button_menu1);
            GlobalVar.NowUiDisplay = (int)NowUi.主页;
        }
        private void button_menu3_Click(object sender, EventArgs e)
        {

            ShowUserControl(wTongdaoControl);
            SetBtnBorder(button_menu3);
            GlobalVar.NowUiDisplay = (int)NowUi.通道概览;

        }

        /// <summary>
        /// 工艺管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu4_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            wPFControl.HideAdminButtons();
            ShowUserControl(wPFControl);
            SetBtnBorder(button_menu4);

            GlobalVar.NowUiDisplay = (int)NowUi.配方;
        }

        private void button_menu9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }

        /// <summary>
        /// 曲线显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu7_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi() && wChartControl.realChartType == 3)
            {
                wChartControl.realChartType = 1;
                wChartControl.ShowHideView();
            }
            ShowUserControl(wChartControl);
            SetBtnBorder(button_menu7);
            GlobalVar.NowUiDisplay = (int)NowUi.图表;
        }

        private void button_menu10_Click(object sender, EventArgs e)
        {

            ShowUserControl(wLoginControl);
            wLoginControl.ShowUserEdit();
            SetBtnBorder(button_menu10);
            GlobalVar.NowUiDisplay = (int)NowUi.登录;
        }
        private void button_menu11_Click(object sender, EventArgs e)
        {
            try
            {
                kbpr = Process.Start(@"C:\Windows\System32\osk.exe");
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
                int posY = (iActulaHeight - 300);


                //设定键盘显示位置
                MoveWindow(intptr, posX, posY, 1000, 300, true);


                //设置软键盘到前端显示
                SetForegroundWindow(intptr);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// 报警管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private /*async*/ void button_menu5_Click(object sender, EventArgs e)
        {

            ShowUserControl(wAlarmTableControl);
            SetBtnBorder(button_menu5);

            GlobalVar.NowUiDisplay = (int)NowUi.报警log;
          //  await wAlarmTableControl.DoSearch();
        }

        /// <summary>
        /// 设备管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu8_Click(object sender, EventArgs e)
        {
            SetBtnBorder(button_menu8);
            ShowBottomUi();
            //ShowUserControl(wWeihuControl/*machinePfLogControl*/);

            //machinePfLogControl.DoSearch();
            //GlobalVar.NowUiDisplay = (int)NowUi.配方Log;
            // GlobalVar.NowUiDisplay = (int)NowUi.设备维护;
        }

        private void ShowBottomUi()
        {
            if(!CommonFunction.CheckAccessShebei()&& GlobalVar.BottomUiDisplay> (int)NowUi.液压站)
            {
              //  ShowYeYang();
                GlobalVar.BottomUiDisplay = (int)NowUi.液压站;
               // return;
            }
            if (!CommonFunction.CheckAccessFactory() && GlobalVar.BottomUiDisplay >= (int)NowUi.传感器校准/*压射杆*/)
            {
               // ShowYeYang();
                GlobalVar.BottomUiDisplay = (int)NowUi.液压站;
               // return;
            }
            switch (GlobalVar.BottomUiDisplay)
            {
                case (int)NowUi.设备维护:
                    ShowWeihu();
                    break;
                case (int)NowUi.泵组维护:
                    ShowBengzu();
                    break;
                case (int)NowUi.IO:
                    ShowIO();
                    break;
                case (int)NowUi.压射杆:
                    ShowYSG();
                    break;
                case (int)NowUi.配方Log:
                    ShowPfLog();
                    break;
                case (int)NowUi.设备保养:
                    ShowBaoYang();
                    break;
                case (int)NowUi.液压站:
                    ShowYeYang();
                    break;
                case (int)NowUi.主机信号模拟:
                    ShowZhuji();
                    break;
                case (int)NowUi.传感器校准:
                    ShowChuanganqi();
                    break;
                case (int)NowUi.设备管理:
                    ShowSheBeiGuanLi();
                    break;
                case (int)NowUi.系统设定:
                    ShowXitongsheding();
                    break;
                default:
                    ShowYeYang();
                    GlobalVar.BottomUiDisplay = (int)NowUi.液压站;
                    break;
            }
        }

        /// <summary>
        /// 手动测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu6_Click(object sender, EventArgs e)
        {
            ShowUserControl(wManualControl);
            SetBtnBorder(button_menu6);
            wManualControl.alreadyStart = false;
            GlobalVar.NowUiDisplay = (int)NowUi.手动测试;
        }

        /// <summary>
        /// 自动操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu12_Click(object sender, EventArgs e)
        {
            ShowUserControl(wAutoControl);
            SetBtnBorder(button_menu12);
            GlobalVar.NowUiDisplay = (int)NowUi.自动;
        }

        /// <summary>
        /// 真空源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu2_Click(object sender, EventArgs e)
        {
            if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
            {
                vacPicture9001Control.loadflag = true;
                ShowUserControl(vacPicture9001Control);
            }
            else if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP6002")
            {
                vacPicture6002Control.loadflag = true;
                ShowUserControl(vacPicture6002Control);
            }
            else
            {
                vacPicture6001Control.loadflag = true;
                ShowUserControl(vacPicture6001Control);
            }
            SetBtnBorder(button_menu2);
            GlobalVar.NowUiDisplay = (int)NowUi.真空源图片;
        }

        public void Show9001Pic()
        {
            ShowUserControl(vacPicture9001Control);
            GlobalVar.NowUiDisplay = (int)NowUi.真空源图片;
        }
        public void Show9001Vac()
        {
            ShowUserControl(vacSetControl);
            GlobalVar.NowUiDisplay = (int)NowUi.真空源设置;
        }

        public void/*async Task*/ ShowPfLog()
        {
            ShowUserControl(machinePfLogControl);
            machinePfLogControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.配方Log;
         //   await machinePfLogControl.DoSearch();
        }
        public void ShowWeihu()
        {
            ShowUserControl(wWeihuControl);
            wWeihuControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.设备维护;
        }
        public void ShowYSG()
        {
            ShowUserControl(wYsgControl);
            wYsgControl.loadflag = true;
            wYsgControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.压射杆;
        }
        public void ShowBengzu()
        {
            ShowUserControl(wBenzuControl);
            wBenzuControl.SetCheckBox();
            wBenzuControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.泵组维护;
        }
        public void ShowSheBeiGuanLi()
        {
            ShowUserControl(wSheBeiGuanLiControl);
            wSheBeiGuanLiControl.loadflag = true;
            wSheBeiGuanLiControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.设备管理;
        }
        public void ShowBaoYang()
        {
            ShowUserControl(wBaoYangControl);
            wBaoYangControl.loadflag = true;
            wBaoYangControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.设备保养;
        }
        public void ShowYeYang()
        {
            ShowUserControl(wYeyaControl);
            wYeyaControl.loadflag = true;
            wYeyaControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.液压站;
        }
        public void ShowIO()
        {
            ShowUserControl(wIoControl);
            wIoControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.IO;
        }
        public void ShowXitongsheding()
        {
            ShowUserControl(wXitongshedingControl);
            wXitongshedingControl.loadflag = true; wXitongshedingControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.系统设定;
        }
        public void ShowChuanganqi()
        {
            ShowUserControl(wChuanganqiControl);
            wChuanganqiControl.loadflag = true;
            wChuanganqiControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.传感器校准;
        }
        public void ShowZhuji()
        {
            ShowUserControl(wZhujiControl);
            wZhujiControl.HideFactoryBtn();
            GlobalVar.NowUiDisplay = (int)NowUi.主机信号模拟;
        }
        #endregion

        #region tasks
        private void LogOutTask()
        {
            Task.Run(delegate
            {

                while (true)
                {
                    DateTime now = DateTime.Now;
                    try
                    {
                        if (!String.IsNullOrEmpty(GlobalVar.UserNumber))
                        {
                            if ((int)now.Subtract(GlobalVar.loginTime).TotalMilliseconds > (10 * 60 * 1000) /*&& !CommonFunction.StayUser()*/)
                            {
                                GlobalVar.UserNumber = "";
                                GlobalVar.UserRole = 0;
                                SetUserLable();
                                Thread.Sleep(1000);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("AuotLogOut::" + ex.Message);
                    }
                    finally
                    {
                        int millisecondsTimeout = 10000 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }
                }
            });
        }
        private void UITask()
        {
            Task.Run(/*delegate*/()=>
            {

                while (true)
                {
                    DateTime now = DateTime.Now;
                    try
                    {
                        var changeAlarmFlag =CommonTaskRead.RefreshAlarms();
                        if (changeAlarmFlag||!firstStartFlag)
                        {
                            firstStartFlag = true;
                            this.Invoke((MethodInvoker)delegate
                            {
                                RefreshAlarmUi();
                            });
                           
                        }

                        CommonTaskRead.ReadTopStatus();
                        for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                        {
                            CommonAdsUi.SetLedColor(uILedChnBulbDs[i], GlobalVar.plcData.hmi_St_Layer.Use_ch_D[i]);
                        }
                        for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                        {
                            CommonAdsUi.SetLedColor(uILedChnBulbCs[i], GlobalVar.plcData.hmi_St_Layer.Use_ch_C[i]);
                        }
                        for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                        {
                            CommonAdsUi.SetLedColor(uILedChnBulbEs[i], GlobalVar.plcData.hmi_St_Layer.Use_ch_E[i]);
                        }
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_zhuangtai1, GlobalVar.plcData.hmi_St_Layer.Status_auto);
                        GlobalVar.IsAuto = GlobalVar.plcData.hmi_St_Layer.Status_auto;
                        CommonAdsUi.SetLabelColorGrayAndRed(label_zhuangtai2, GlobalVar.plcData.hmi_St_Layer.Status_err);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_zhuangtai3, GlobalVar.plcData.hmi_St_Layer.Status_ready);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_zhuangtai4, GlobalVar.plcData.hmi_St_Layer.Status_pumpruning);
                        label_topmiddle_value1.Text = GlobalVar.plcData.hmi_St_Layer.DcmPos.ToString();
                        label_topmiddle_value2.Text = GlobalVar.plcData.hmi_St_Layer.DcmVel.ToString();
                        label_topmiddle_value3.Text = GlobalVar.plcData.hmi_St_Layer.P_Tank[0].ToString();
                        label_topmiddle_value4.Text = GlobalVar.plcData.hmi_St_Layer.P_Tank[1].ToString();
                        label_topmiddle_value6.Text = GlobalVar.plcData.hmi_St_Layer.DcmPosCoding.ToString();
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright1, GlobalVar.plcData.hmi_St_Layer.DcmClose);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright2, GlobalVar.plcData.hmi_St_Layer.DcmOpen);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright3, GlobalVar.plcData.hmi_St_Layer.DcmBlow);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright4, GlobalVar.plcData.hmi_St_Layer.DcmPosZero);
                        CommonAdsUi.SetLabelColorGrayAndRed(label_topright5, GlobalVar.plcData.hmi_St_Layer.Es_Controls);

                        //???慢压射
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright6, GlobalVar.plcData.hmi_St_Layer.DcmHotMould);

                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright7, GlobalVar.plcData.hmi_St_Layer.DcmAuto);
                        label_topright8.Text = GlobalVar.plcData.hmi_St_Layer.Status_remote_pump ? LanguageSet.SetL("Main", "yuankong") : LanguageSet.SetL("Main", "benkong");
                        // CommonAdsUi.SetLabelColorGrayAndGreen(label_topright8, GlobalVar.plcData.hmi_St_Layer.Es_Controls);
                        CommonAdsUi.SetLabelColorGrayAndGreen(label_topright9, GlobalVar.plcData.hmi_St_Layer.Zero_Calibration);
                        CommonAdsUi.SetLabelColorGrayAndRed(label_topright10, GlobalVar.plcData.hmi_St_Layer.Es_pump);
                        if (GlobalVar.NowAlarms.Exists(m => m.alarmCode == "A000"))
                        {
                            var oklist = new List<string> { "A000" };
                            var ids = GlobalVar.NowAlarms.Where(m => oklist.Contains(m.alarmCode)).Select(m => m.id).ToList();
                            GlobalVar.NowAlarms = GlobalVar.NowAlarms.Where(m => !oklist.Contains(m.alarmCode)).ToList();
                            AlarmLogCommon.OffAlarms(ids);
                            this.Invoke((MethodInvoker)delegate
                            {
                                RefreshAlarmUi();
                            });
                        }
                        //CommonAdsUi.SetColor(label_topright6, GlobalVar.plcData.hmi_St_Layer.DcmOpen);
                        // CommonAdsUi.SetColor(label_topright7, GlobalVar.plcData.hmi_St_Layer.DcmBlow);
                        //   CommonAdsUi.SetColor(label_topright8, GlobalVar.plcData.hmi_St_Layer.DcmPosZero);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex.Message);
                        try
                        {
                            GlobalVar.commonAdsControl.Readenablereading();
                        }
                        catch
                        {
                            try
                            {
                                if (!GlobalVar.NowAlarms.Exists(m => m.alarmCode == "A000"))
                                {
                                    var list = AlarmLogCommon.NewAlarmLogTables(new List<string> { "A000" });
                                    GlobalVar.NowAlarms.AddRange(list);
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        RefreshAlarmUi();
                                    });
                                }
                            }
                            catch (Exception ex2)
                            {
                                ShowErrorMessage(ex2.Message);
                            }
                        }
                    }
                    finally
                    {
                        int millisecondsTimeout = 200 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }
                }
            });
        }


        #endregion

        #region 委托
        delegate void SetTextCallBack(Control con, string text);
        private void SetText(Control con, string text)
        {
            if (con.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                this.Invoke(stcb, new object[] { con, text });
            }
            else
            {
                con.Text = text;
            }
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
        #endregion

        #region alarm table
        private void IniAlarmTableHeader()
        {
            dataGridView_error.Columns[0].HeaderText = LanguageSet.SetL("AlarmTable", "Header1");
            dataGridView_error.Columns[1].HeaderText = LanguageSet.SetL("AlarmTable", "Header2");
            dataGridView_error.Columns[2].HeaderText = LanguageSet.SetL("AlarmTable", "Header3");
            dataGridView_error.Columns[3].HeaderText = LanguageSet.SetL("AlarmTable", "Header4");
            dataGridView_error.Columns[4].HeaderText = LanguageSet.SetL("AlarmTable", "Header5");
        }

        private void RefreshAlarmUi()
        {
            dataGridView_error.Rows.Clear();
            if (GlobalVar.NowAlarms.Count > 0)
            {
                dataGridView_error.Rows.Add(GlobalVar.NowAlarms.Count);
            }
            GlobalVar.NowAlarms = GlobalVar.NowAlarms.OrderByDescending(m => m.create_time).ToList();
            for (int i = 0; i < GlobalVar.NowAlarms.Count; i++)
            {
                var a = GlobalVar.NowAlarms[i];
                dataGridView_error.Rows[i].DefaultCellStyle.SelectionBackColor = a.alarmFlag ? Color.Red : Color.White;
                dataGridView_error.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                dataGridView_error.Rows[i].DefaultCellStyle.BackColor = a.alarmFlag ? Color.Red : Color.White;
                dataGridView_error.Rows[i].Cells[0].Value = a.alarmFlag ? LanguageSet.SetL("AlarmTable", "AlarmOn") : LanguageSet.SetL("AlarmTable", "AlarmOff");
                dataGridView_error.Rows[i].Cells[1].Value = a.create_time.ToString("MM/dd/yyyy HH:mm:ss");
                dataGridView_error.Rows[i].Cells[2].Value = a.alarmCode;
                dataGridView_error.Rows[i].Cells[3].Value = CommonTaskRead.GetAlarmInfoByCode(a.alarmCode);
                dataGridView_error.Rows[i].Cells[4].Value = CommonTaskRead.GetAlarmLevelByCode(a.alarmCode);

            }

        }




        #endregion


        private void button_menu13_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            using (var _form = new LanuageSelectForm())
            {
                var _result = _form.ShowDialog();
                if (_result == DialogResult.OK)
                {
                    if (MessageBox.Show("Changing the language will restart the program. Are you sure about the change?", "Warning:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var _language = _form.resultLanuage;
                        GlobalVar.ini.iniSystem.WriteString("DATA", "language", _language);
                        Process process = Process.GetCurrentProcess();
                        process.Close();
                        Application.Restart();
                    }

                }

            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_menu14_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            ShowUserControl(wDownloadTableControl);
            SetBtnBorder(button_menu14);

            GlobalVar.NowUiDisplay = (int)NowUi.数据下载;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            //GlobalVar.loginTime = DateTime.Now;
            //Console.WriteLine(GlobalVar.loginTime.ToString("yyyy MM-dd-HH:mm:ss"));
        }
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            GlobalVar.loginTime = DateTime.Now;
           // Console.WriteLine(GlobalVar.loginTime.ToString("yyyy MM-dd-HH:mm:ss"));
        }
    }
}
