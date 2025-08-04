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
    public partial class WXitongshedingControl : UserControl
    {
        public bool loadflag = false;
        private BottomPageSwitch bSwitch;
        public WXitongshedingControl()
        {
            InitializeComponent();
            IniAllControlName();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.系统设定);
            RealTimeDataTask();
            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP9001")
            {
                xt_left_label_18.Visible = false;
                xt_left_label_19.Visible = false;
                xt_left_label_20.Visible = false;
                xt_text_12.Visible = false;
                xt_text_13.Visible = false;
                xt_btn_5.Visible = false;
            }

            // 20250124 Anders 
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
            xt_text_1.MouseDown += txtBox_MouseDown;
            xt_text_2.MouseDown += txtBox_MouseDown;
            xt_text_3.MouseDown += txtBox_MouseDown;
            xt_text_4.MouseDown += txtBox_MouseDown;
            xt_text_5.MouseDown += txtBox_MouseDown;
            xt_text_6.MouseDown += txtBox_MouseDown;
            xt_text_7.MouseDown += txtBox_MouseDown;
            xt_text_8.MouseDown += txtBox_MouseDown;
            xt_text_9.MouseDown += txtBox_MouseDown;
            xt_text_10.MouseDown += txtBox_MouseDown;
            xt_text_11.MouseDown += txtBox_MouseDown;
            xt_text_12.MouseDown += txtBox_MouseDown;
            xt_text_13.MouseDown += txtBox_MouseDown;
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
                LanguageSet.SetLanguageByData(ctrl, "SystemSet");

            }
            var u = LanguageSet.SetL("VacConfig", "btn_use");
            foreach(var c in this.Controls)
            {
                if (c is UIButton)
                {
                    var b= (UIButton)c;
                    if (b.Text == "")
                        b.Text = u;
                }
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.系统设定)
                        {
                            CommonTaskRead.ReadUPS();
                            //SetAllData();
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
            xt_text_1.Text = GlobalVar.plcData.UPS.DCMcloseTest_hi.ToString();
            xt_text_2.Text = GlobalVar.plcData.UPS.DCMcloseTest_lo.ToString();
            xt_text_3.Text = GlobalVar.plcData.UPS.ProbeTime.ToString();
            xt_text_4.Text = GlobalVar.plcData.UPS.testTime.ToString();
            xt_text_5.Text = GlobalVar.plcData.UPS.SpoolTest_hihi.ToString();
            xt_text_6.Text = GlobalVar.plcData.UPS.SpoolTest_hi.ToString();
            xt_text_7.Text = GlobalVar.plcData.UPS.Spool_ProbeTime.ToString();
            xt_text_8.Text = GlobalVar.plcData.UPS.Spool_testTime.ToString();
            xt_text_9.Text = GlobalVar.plcData.UPS.VacLatency.ToString();
            xt_text_10.Text = GlobalVar.plcData.UPS.Set_tankHI.ToString();
            xt_text_11.Text = GlobalVar.plcData.UPS.Set_tankLO.ToString();
            xt_text_12.Text = GlobalVar.plcData.UPS.Set_tank1HI.ToString();
            xt_text_13.Text = GlobalVar.plcData.UPS.Set_tank1LO.ToString();
        }
        private void SetAllDataNoInput()
        {
            try
            {
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_1, GlobalVar.plcData.UPS.use_DCMcloseTest);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_2, GlobalVar.plcData.UPS.use_SpoolTest);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_3, GlobalVar.plcData.UPS.use_openRise);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_4, GlobalVar.plcData.UPS.use_tankSET);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_5, GlobalVar.plcData.UPS.setUse_standbyValve);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_6, GlobalVar.plcData.UPS.use_P);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_7, GlobalVar.plcData.UPS.Use_MBlow);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_8, GlobalVar.plcData.UPS.use_autoEndS);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_9, GlobalVar.plcData.UPS.select_gauging_M);
                CommonAdsUi.SetBtnColorGrayAndGreen(xt_btn_10, GlobalVar.plcData.UPS.use_TakePos);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_fangshi1, GlobalVar.plcData.UPS.use_Vacsign == 0);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_fangshi2, GlobalVar.plcData.UPS.use_Vacsign == 1);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_fangshi3, GlobalVar.plcData.UPS.use_Vacsign == 2);
                //CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_time1, GlobalVar.plcData.UPS.use_VacTime == 0);
                //CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_time2, GlobalVar.plcData.UPS.use_VacTime == 1);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_time1, GlobalVar.plcData.UPS.use_VacTime == 1);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_zhenkong_time2, GlobalVar.plcData.UPS.use_VacTime == 0);
            }
            catch (Exception ex)
            {
                throw new Exception("System Setting SetAllData::" + ex.Message);
            }
        }

        private void xt_btn_1_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 257, !GlobalVar.plcData.UPS.use_DCMcloseTest);
        }

        private void xt_btn_2_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 258, !GlobalVar.plcData.UPS.use_SpoolTest);
        }

        private void xt_btn_3_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 259, !GlobalVar.plcData.UPS.use_openRise);
        }

        private void btn_zhenkong_fangshi1_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name,286, 0);
        }

        private void btn_zhenkong_fangshi2_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 286, 1);
        }

        private void btn_zhenkong_fangshi3_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 286, 2);
        }

        private void btn_zhenkong_time1_Click(object sender, EventArgs e)
        {
            //GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 288, 0);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 288, 1);

        }

        private void btn_zhenkong_time2_Click(object sender, EventArgs e)
        {
            //GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 288, 1);
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 288, 0);

        }

        private void xt_btn_4_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 308, !GlobalVar.plcData.UPS.use_tankSET);
        }

        private void xt_btn_5_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 299, !GlobalVar.plcData.UPS.setUse_standbyValve);
        }

        private void xt_btn_6_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 294, !GlobalVar.plcData.UPS.use_P);
        }

        private void xt_btn_7_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 295, !GlobalVar.plcData.UPS.Use_MBlow);
        }

        private void xt_btn_8_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 296, !GlobalVar.plcData.UPS.use_autoEndS);
        }

        private void xt_btn_9_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 297, !GlobalVar.plcData.UPS.select_gauging_M);
        }

        private void xt_btn_10_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 298, !GlobalVar.plcData.UPS.use_TakePos);
        }
        private void SetDataFloat(UITextBox uITextBox, int beforecount)
        {
            if (loadflag) return;
            var value = CommonFunction.GetFloatFromUIText(uITextBox);
            if (value == null)
            {
                SetAllDataInput();
                return;
            }
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, beforecount, value.Value);

        }
        private void SetDataShort(UITextBox uITextBox, int beforecount)
        {
            if (loadflag) return;
            var value = CommonFunction.GetShortFromUIText(uITextBox);
            if (value == null)
            {
                SetAllDataInput();
                return;
            }
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, beforecount, value.Value);

        }

        private void xt_text_1_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 260);
        }

        private void xt_text_2_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 264);
        }

        private void xt_text_3_TextChanged(object sender, EventArgs e)
        {
            SetDataShort((UITextBox)sender, 280);
        }

        private void xt_text_4_TextChanged(object sender, EventArgs e)
        {
            SetDataShort((UITextBox)sender, 282);
        }

        private void xt_text_5_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 268);
        }

        private void xt_text_6_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 272);
        }

        private void xt_text_7_TextChanged(object sender, EventArgs e)
        {
            SetDataShort((UITextBox)sender, 276);
        }

        private void xt_text_8_TextChanged(object sender, EventArgs e)
        {
            SetDataShort((UITextBox)sender, 278);
        }

        private void xt_text_9_TextChanged(object sender, EventArgs e)
        {
            SetDataShort((UITextBox)sender, 284);
        }

        private void xt_text_10_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 309);
        }

        private void xt_text_11_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 313);
        }

        private void xt_text_12_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 317);
        }

        private void xt_text_13_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 321);
        }
    }
}
