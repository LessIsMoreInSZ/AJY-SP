using SPWindowsForms.AdsConnect;
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
    public partial class WYsgControl : UserControl
    {
        public bool loadflag = false;
        private BottomPageSwitch bSwitch;
        public WYsgControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.压射杆);
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
            txt_pos.MouseDown += txtBox_MouseDown;
            txt_offset.MouseDown += txtBox_MouseDown;
            txt_safe_vel.MouseDown += txtBox_MouseDown;
            txt_safe_pos.MouseDown += txtBox_MouseDown;
            txt_safe_time.MouseDown += txtBox_MouseDown;
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
                LanguageSet.SetLanguageByData(ctrl, "YSG");
            }
        }
        public void SetAllData()
        {
            try
            {
                txt_pos.Text = GlobalVar.plcData.UPS.K_pos.ToString();
                txt_offset.Text = GlobalVar.plcData.UPS.offset_pos.ToString();
                txt_safe_vel.Text = GlobalVar.plcData.UPS.SafetyVel/*Safety_value_pos*/.ToString();
                txt_safe_pos.Text = GlobalVar.plcData.UPS.Safety_value_pos.ToString();
                txt_safe_time.Text = GlobalVar.plcData.UPS.Safety_time.ToString();


            }
            catch (Exception ex)
            {
                throw new Exception("YSG SetAllData::" + ex.Message);
            }
        }
        private void SetOnlyBtnData()
        {
            try
            {
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_save, GlobalVar.plcData.hmi_butt_save);
            }
            catch (Exception ex)
            {
                throw new Exception("YSG SetOnlyBtnData::" + ex.Message);
            }
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.压射杆)
                        {
                            CommonTaskRead.ReadUPS();
                            if (loadflag)
                            {
                                SetAllData();
                                loadflag = false;
                            }
                            SetOnlyBtnData();

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

        private void btn_save_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(GlobalVar.commonAdsControl.hmi_butt_save_name, true);
        }

        private void btn_save_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(GlobalVar.commonAdsControl.hmi_butt_save_name, false);
        }

        private void txt_pos_TextChanged(object sender, EventArgs e)
        {
            var value = txt_pos.Text;
            if (value == "")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (value == "-")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllData();
                return;
            }
            var _set = Convert.ToSingle(value);
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 67, _set);
        }

        private void txt_offset_TextChanged(object sender, EventArgs e)
        {
            var value = txt_offset.Text;
            if (value == "")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (value == "-")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, true))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllData();
                return;
            }
            var _set = Convert.ToSingle(value);
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 67 + 4, _set);
        }

        private void txt_safe_pos_TextChanged(object sender, EventArgs e)
        {
            var value = txt_safe_pos.Text;
            if (value == "")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (value == "-")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, false))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllData();
                return;
            }
            var _set = Convert.ToInt16(value);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 67 + 4 + 4, _set);
        }

        private void txt_safe_time_TextChanged(object sender, EventArgs e)
        {
            var value = txt_safe_time.Text;
            if (value == "")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (value == "-")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, false))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllData();
                return;
            }
            var _set = Convert.ToInt16(value);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 67 + 10, _set);
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowIO();
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            // Work.frm_main.ShowPfLog();
        }

        private void txt_safe_vel_TextChanged(object sender, EventArgs e)
        {
            var value = txt_safe_vel.Text;
            if (value == "")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (value == "-")
            {
                //sysTextBoxs[chn - 1][order - 1].Text = "";
                //return true;
                value = "0";
            }
            if (!CommonFunction.CheckStrNumType(value, false))
            {
                Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                SetAllData();
                return;
            }
            var _set = Convert.ToInt16(value);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 290, _set);
        }
    }
}
