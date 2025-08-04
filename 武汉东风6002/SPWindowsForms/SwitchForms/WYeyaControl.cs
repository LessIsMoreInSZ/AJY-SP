using SPWindowsForms.AdsConnect;
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

namespace SPWindowsForms.SwitchForms
{
    public partial class WYeyaControl : UserControl
    {
        public bool loadflag = false;
        public List<UIButton> uIButtons = new List<UIButton>();
        private BottomPageSwitch bSwitch;
        public WYeyaControl()
        {
            InitializeComponent();
            IniAllControlName();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.液压站);
            uIButtons.Add(butt_start_hyd1);
            uIButtons.Add(butt_start_hyd2);
            uIButtons.Add(butt_stop_hyd1);
            uIButtons.Add(butt_stop_hyd2);
            uIButtons.Add(open_Valve1);
            uIButtons.Add(open_Valve2);
            uIButtons.Add(butt_CL_HYDpump1);
            uIButtons.Add(butt_CL_HYDpump2);
            uIButtons.ForEach(b =>
            {
                b.MouseDown += KMouse_Down;
                b.MouseUp += KMouse_Up;
            });
            RealTimeDataTask();
            AddTxtMouseDownEvent();
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

        private void AddTxtMouseDownEvent()
        {
            set_input3.MouseDown += txtBox_MouseDown;
            set_input4.MouseDown += txtBox_MouseDown;
            set_input5.MouseDown += txtBox_MouseDown;
            set_input6.MouseDown += txtBox_MouseDown;
            
        }

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

        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();
        }

        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "YeYaZhan");
                if (ctrl is UIGroupBox)
                {
                    var _c = (UIGroupBox)ctrl;
                    foreach (var gctrl in _c.Controls)
                    {
                        LanguageSet.SetLanguageByData(gctrl, "YeYaZhan");
                    }
                }
            }
            btn_use1.Text = LanguageSet.SetL("YeYaZhan", "btn_use");
            btn_use3.Text = LanguageSet.SetL("YeYaZhan", "btn_use");//20250219 Anders

            btn_use2.Text = LanguageSet.SetL("YeYaZhan", "btn_use");
            var _open = LanguageSet.SetL("YeYaZhan", "btn_open");
            var _close = LanguageSet.SetL("YeYaZhan", "btn_close");
            butt_start_hyd1.Text = _open;
            butt_start_hyd2.Text = _open;
            butt_stop_hyd1.Text = _close;
            butt_stop_hyd2.Text = _close;
            open_Valve1.Text = _close;
            open_Valve2.Text = _close;
        }
        private void RealTimeDataTask()
        {
            Task.Run(delegate
            {
                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.液压站)
                        {
                            CommonTaskRead.ReadKsystem();
                            CommonTaskRead.ReadUPS();
                            // SetAllData();
                            SetAllDataNoInput();
                            if (loadflag)
                            {
                                SetAllDataInput();
                                loadflag = false;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Work.frm_main.ShowErrorMessage(ex.Message);

                    }
                    finally
                    {
                        int millisecondsTimeout = 500 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }
                }

            });
        }
        private void SetAllDataInput()
        {
            set_input3.Text = GlobalVar.plcData.UPS.set_HYD_time.ToString();
            set_input4.Text = GlobalVar.plcData.UPS.set_Hyd_hi.ToString();
            set_input5.Text = GlobalVar.plcData.UPS.set_Hyd_lo.ToString();
            set_input6.Text = GlobalVar.plcData.UPS.set_Upkeep_HYD.ToString();
        }
        private void SetAllDataNoInput()
        {
            try
            {
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use1, GlobalVar.plcData.UPS.Use_HYD1);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use3, GlobalVar.plcData.UPS.Use_HYD2);

                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use2, GlobalVar.plcData.UPS.Ust_timeControl_HYD);

                CommonAdsUi.SetLabelColorGrayAndOrange(label_led_remote_1, GlobalVar.plcData.hmi_K.led_remote[0]);
                CommonAdsUi.SetLabelColorGrayAndOrange(label_led_remote_2, GlobalVar.plcData.hmi_K.led_remote[1]);
                label_Pt_Hyd_1.Text = GlobalVar.plcData.hmi_K.Pt_Hyd[0].ToString();
                label_Pt_Hyd_2.Text = GlobalVar.plcData.hmi_K.Pt_Hyd[1].ToString();
                label_T_Hyd_1.Text = GlobalVar.plcData.hmi_K.T_Hyd[0].ToString();
                label_T_Hyd_2.Text = GlobalVar.plcData.hmi_K.T_Hyd[1].ToString();
                label_HYDpump_timing1.Text = GlobalVar.plcData.hmi_K.HYDpump_timing[0].ToString();
                label_HYDpump_timing2.Text = GlobalVar.plcData.hmi_K.HYDpump_timing[1].ToString();
                label_countdown_HYDpump1.Text = GlobalVar.plcData.hmi_K.countdown_HYDpump[0].ToString();
                label_countdown_HYDpump2.Text = GlobalVar.plcData.hmi_K.countdown_HYDpump[1].ToString();
                label_HYDpump_runtime1.Text = GlobalVar.plcData.hmi_K.HYDpump_runtime[0].ToString();
                label_HYDpump_runtime2.Text = GlobalVar.plcData.hmi_K.HYDpump_runtime[1].ToString();
                CommonAdsUi.SetLightOnOff(Light_Alarm_Hyd_1, GlobalVar.plcData.hmi_K.Alarm_Hyd[0]);
                CommonAdsUi.SetLightOnOff(Light_Alarm_Hyd_2, GlobalVar.plcData.hmi_K.Alarm_Hyd[1]);
                CommonAdsUi.SetLightOnOff(Light_Led_hydRuning1, GlobalVar.plcData.hmi_K.Led_hydRuning[0]);
                CommonAdsUi.SetLightOnOff(Light_Led_hydRuning2, GlobalVar.plcData.hmi_K.Led_hydRuning[1]);
                CommonAdsUi.SetLightOnOff(Light_Led_Valve1, GlobalVar.plcData.hmi_K.Led_valve[0]);
                CommonAdsUi.SetLightOnOff(Light_Led_Valve2, GlobalVar.plcData.hmi_K.Led_valve[1]);

                CommonAdsUi.SetBtnColorGrayAndGreen(butt_start_hyd1, GlobalVar.plcData.hmi_K.Led_hydRuning[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(butt_start_hyd2, GlobalVar.plcData.hmi_K.Led_hydRuning[1]);

                CommonAdsUi.SetBtnColorGrayAndGreen(butt_stop_hyd1, GlobalVar.plcData.hmi_K.butt_stop_hyd[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(butt_stop_hyd2, GlobalVar.plcData.hmi_K.butt_stop_hyd[1]);

                CommonAdsUi.SetBtnColorGrayAndGreen(open_Valve1, GlobalVar.plcData.hmi_K.Led_valve[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(open_Valve2, GlobalVar.plcData.hmi_K.Led_valve[1]);

                CommonAdsUi.SetBtnColorGrayAndGreen(butt_CL_HYDpump1, GlobalVar.plcData.hmi_K.butt_CL_HYDpump[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(butt_CL_HYDpump2, GlobalVar.plcData.hmi_K.butt_CL_HYDpump[1]);
                if (CommonFunction.CheckAccessShebei())
                {
                    btn_use1.Enabled = true;
                    btn_use3.Enabled = true;
                    btn_use2.Enabled = true;
                    set_input3.Enabled = true;
                    set_input4.Enabled = true;
                    set_input5.Enabled = true;
                    set_input6.Enabled = true;
                    butt_start_hyd1.Enabled = true;
                    butt_stop_hyd1.Enabled = true;
                    open_Valve1.Enabled = true;
                    butt_CL_HYDpump1.Enabled = true;
                    butt_start_hyd2.Enabled = true;
                    butt_stop_hyd2.Enabled = true;
                    open_Valve2.Enabled = true;
                    butt_CL_HYDpump2.Enabled = true;
                }
                else if (CommonFunction.CheckAccessGongyi())
                {
                    btn_use1.Enabled = true;
                    btn_use3.Enabled = true;
                    btn_use2.Enabled = true;
                    set_input3.Enabled = true;
                    set_input4.Enabled = true;
                    set_input5.Enabled = true;
                    set_input6.Enabled = false;
                    butt_start_hyd1.Enabled = true;
                    butt_stop_hyd1.Enabled = true;
                    open_Valve1.Enabled = true;
                    butt_CL_HYDpump1.Enabled = false;
                    butt_start_hyd2.Enabled = true;
                    butt_stop_hyd2.Enabled = true;
                    open_Valve2.Enabled = true;
                    butt_CL_HYDpump2.Enabled = false;
                }
                else if (CommonFunction.CheckAccessJishu())
                {
                    btn_use1.Enabled = false;
                    btn_use3.Enabled = false;
                    btn_use2.Enabled = false;
                    set_input3.Enabled = false;
                    set_input4.Enabled = false;
                    set_input5.Enabled = false;
                    set_input6.Enabled = false;
                    butt_start_hyd1.Enabled = true;
                    butt_stop_hyd1.Enabled = true;
                    open_Valve1.Enabled = true;
                    butt_CL_HYDpump1.Enabled = false;
                    butt_start_hyd2.Enabled = true;
                    butt_stop_hyd2.Enabled = true;
                    open_Valve2.Enabled = true;
                    butt_CL_HYDpump2.Enabled = false;
                }
                else
                {
                    btn_use1.Enabled = false;
                    btn_use3.Enabled = false;
                    btn_use2.Enabled = false;
                    set_input3.Enabled = false;
                    set_input4.Enabled = false;
                    set_input5.Enabled = false;
                    set_input6.Enabled = false;
                    butt_start_hyd1.Enabled = false;
                    butt_stop_hyd1.Enabled = false;
                    open_Valve1.Enabled = false;
                    butt_CL_HYDpump1.Enabled = false;
                    butt_start_hyd2.Enabled = false;
                    butt_stop_hyd2.Enabled = false;
                    open_Valve2.Enabled = false;
                    butt_CL_HYDpump2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" SetAllData::" + ex.Message);
            }
        }

        private void set_input3_TextChanged(object sender, EventArgs e)
        {
            var value = set_input3.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, false))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllDataInput();
                return;
            }
            var _set = Convert.ToInt16(value);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 243, _set);
        }

        private void set_input4_TextChanged(object sender, EventArgs e)
        {
            var value = set_input4.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllDataInput();
                return;
            }
            var _set = Convert.ToSingle(value);
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 245, _set);
        }

        private void set_input5_TextChanged(object sender, EventArgs e)
        {
            var value = set_input5.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllDataInput();
                return;
            }
            var _set = Convert.ToSingle(value);
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 249, _set);
        }

        private void btn_use1_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 241, !GlobalVar.plcData.UPS.Use_HYD1);
        }

        private void btn_use2_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 242, !GlobalVar.plcData.UPS.Ust_timeControl_HYD);
        }
        private void KMouse_Down(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, _tag, true);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }
        private void KMouse_Up(object sender, EventArgs e)
        {
            try
            {
                if (sender is UIButton btn)
                {
                    var _tag = Convert.ToInt32(btn.Tag);
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, _tag, false);

                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
            }
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowBaoYang();
        }

        private void set_input6_TextChanged(object sender, EventArgs e)
        {
            var value = set_input6.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllDataInput();
                return;
            }
            var _set = Convert.ToSingle(value);
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 253, _set);
        }

        private void btn_use3_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessJishu())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 339, !GlobalVar.plcData.UPS.Use_HYD2);
        }
    }
}
