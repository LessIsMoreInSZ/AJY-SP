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
    public partial class WSheBeiGuanLiControl : UserControl
    {
        public bool loadflag = false;
        private BottomPageSwitch bSwitch;
        public WSheBeiGuanLiControl()
        {
            InitializeComponent();
            IniAllControlName();
            bSwitch = CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom,NowUi.设备管理);
            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP6001")
            {
                left_label_13.Visible = false;
                text_luocibeng.Visible = false;
            }
            if (GlobalVar.systemSetting.tiaojiefa_kaidu_flag == 0)
            {
                left_label_14.Visible = false;
                label_kaidu.Visible = false;
                text_kaidu.Visible = false;
            }
            
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
            txt_alarm_1.MouseDown += txtBox_MouseDown;
            txt_alarm_2.MouseDown += txtBox_MouseDown;
            txt_alarm_3.MouseDown += txtBox_MouseDown;
            txt_alarm_4.MouseDown += txtBox_MouseDown;
            text_ylsd.MouseDown += txtBox_MouseDown;
            text_cqg_jiance.MouseDown += txtBox_MouseDown;
            text_luocibeng.MouseDown += txtBox_MouseDown;
            text_kaidu.MouseDown += txtBox_MouseDown;
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
                LanguageSet.SetLanguageByData(ctrl, "SheBeiGuanLi");
              
            }
            var _use = LanguageSet.SetL("SheBeiGuanLi", "btn_use");
            btn_use_1.Text = _use;
            btn_use_2.Text = _use;
            btn_use_3.Text = _use;
            btn_use_4.Text = _use;
            btn_use_5.Text = _use;
            btn_use_6.Text = _use;
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.设备管理)
                        {
                            CommonTaskRead.ReadKsystem();
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
            txt_alarm_1.Text = GlobalVar.plcData.UPS.set_pump_upkeepTime[0].ToString();
            txt_alarm_2.Text = GlobalVar.plcData.UPS.set_pump_upkeepTime[1].ToString();
            txt_alarm_3.Text = GlobalVar.plcData.UPS.set_pump_upkeepTime[2].ToString();
            txt_alarm_4.Text = GlobalVar.plcData.UPS.set_CycleTime_value.ToString();
            text_ylsd.Text= GlobalVar.plcData.UPS.set_CompressedAir_lo.ToString();
            text_cqg_jiance.Text = GlobalVar.plcData.UPS.Set_tank_StartPG.ToString();
            text_luocibeng.Text = GlobalVar.plcData.UPS.Set_RootsPump_StartPG.ToString();
            text_kaidu.Text = GlobalVar.plcData.UPS.set_kaidu.ToString();
        }
        private void SetAllDataNoInput()
        {
            try
            {
                CommonAdsUi.SetBtnColorGrayAndRed(btn_dis_1, GlobalVar.plcData.UPS.butt_i_dis[0]);
                CommonAdsUi.SetBtnColorGrayAndRed(btn_dis_2, GlobalVar.plcData.UPS.butt_i_dis[1]);
                CommonAdsUi.SetBtnColorGrayAndRed(btn_dis_3, GlobalVar.plcData.UPS.butt_i_dis[2]);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql_1, GlobalVar.plcData.hmi_K.ButtCL_PumpUpkeep[0]);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql_2, GlobalVar.plcData.hmi_K.ButtCL_PumpUpkeep[1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql_3, GlobalVar.plcData.hmi_K.ButtCL_PumpUpkeep[2]);

                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_1, GlobalVar.plcData.UPS.use_CycleTime);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_2, GlobalVar.plcData.UPS.use_atandby_vavle);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_3, GlobalVar.plcData.UPS.use_TestCoolingWather);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_4, GlobalVar.plcData.UPS.use_TestCompressedAir);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_5, GlobalVar.plcData.UPS.autoUse_PumpRuningCondition);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use_6, GlobalVar.plcData.UPS.hmi_Butt_BOV);
                label_yunxing_1.Text = GlobalVar.plcData.UPS.runtime_pump[0].ToString();
                label_yunxing_2.Text = GlobalVar.plcData.UPS.runtime_pump[1].ToString();
                label_yunxing_3.Text = GlobalVar.plcData.UPS.runtime_pump[2].ToString();
                label_baoyang_1.Text = GlobalVar.plcData.UPS.Countdown_pump[0].ToString();
                label_baoyang_2.Text = GlobalVar.plcData.UPS.Countdown_pump[1].ToString();
                label_baoyang_3.Text = GlobalVar.plcData.UPS.Countdown_pump[2].ToString();
                label_baoyang_4.Text = GlobalVar.plcData.hmi_K.auto_CycleTime.ToString();
                label_ylsd.Text = GlobalVar.plcData.hmi_K.P_CompressedAir.ToString();
                label_kaidu.Text = GlobalVar.plcData.hmi_feedback_kaidu.ToString();
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_save, GlobalVar.plcData.hmi_butt_save);
            }
            catch (Exception ex)
            {
                throw new Exception("ShebeiGuanli SetAllData::" + ex.Message);
            }
        }

        private void btn_dis_1_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 38, !GlobalVar.plcData.UPS.butt_i_dis[0]);
        }

        private void btn_dis_2_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 39, !GlobalVar.plcData.UPS.butt_i_dis[1]);
        }

        private void btn_dis_3_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 40, !GlobalVar.plcData.UPS.butt_i_dis[2]);
        }

        private void btn_use_1_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 53, !GlobalVar.plcData.UPS.use_CycleTime);
        }

        private void btn_use_2_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 64, !GlobalVar.plcData.UPS.use_atandby_vavle);
        }

        private void btn_use_3_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 58, !GlobalVar.plcData.UPS.use_TestCoolingWather);
        }

        private void btn_use_4_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 59, !GlobalVar.plcData.UPS.use_TestCompressedAir);
        }

        private void btn_use_5_Click(object sender, EventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 65, !GlobalVar.plcData.UPS.autoUse_PumpRuningCondition);
        }

        private void btn_use_6_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_ql_1_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, 9, true);
        }

        private void btn_ql_1_MouseMove(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, 9, false);
        }

        private void btn_ql_2_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, 10, true);
        }

        private void btn_ql_2_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, 10, false);
        }

        private void btn_ql_3_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name,11, true);
        }

        private void btn_ql_3_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, 11, false);
        }
     

        private void txt_alarm_1_TextChanged(object sender, EventArgs e)
        {
            var value = txt_alarm_1.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 41, _set);
        }

        private void txt_alarm_2_TextChanged(object sender, EventArgs e)
        {
            var value = txt_alarm_2.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 45, _set);
        }

        private void txt_alarm_3_TextChanged(object sender, EventArgs e)
        {
            var value = txt_alarm_3.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 49, _set);
        }

        private void txt_alarm_4_TextChanged(object sender, EventArgs e)
        {
            var value = txt_alarm_4.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 54, _set);
        }

        private void text_ylsd_TextChanged(object sender, EventArgs e)
        {
            var value = text_ylsd.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 60, _set);
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowWeihu();
        }

        private void btn_use_6_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 66, true/*!GlobalVar.plcData.UPS.hmi_Butt_BOV*/);
        }

        private void btn_use_6_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, 66, false/*!GlobalVar.plcData.UPS.hmi_Butt_BOV*/);
        }

        private void btn_save_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(GlobalVar.commonAdsControl.hmi_butt_save_name, true);
        }

        private void btn_save_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalVar.commonAdsControl.WriteCommonBool2(GlobalVar.commonAdsControl.hmi_butt_save_name, false);
        }

        private void text_cqg_jiance_TextChanged(object sender, EventArgs e)
        {
            var value = text_cqg_jiance.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 300, _set);
        }

        private void text_luocibeng_TextChanged(object sender, EventArgs e)
        {
            var value = text_luocibeng.Text;
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
            GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, 304, _set);
        }

        private void text_kaidu_TextChanged(object sender, EventArgs e)
        {
            var value = text_kaidu.Text;
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
            GlobalVar.commonAdsControl.WriteCommonShortByBefore(GlobalVar.commonAdsControl.st_ups_name, 337, _set);
        }
    }
}
