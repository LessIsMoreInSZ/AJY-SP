using SPWindowsForms.AdsConnect;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.SwitchForms
{
    public partial class VacPicture6002Control : UserControl
    {
        public bool loadflag = false;
        public VacPicture6002Control()
        {
            InitializeComponent();
            IniAllControlName();
            AutoRealTimeDataTask();
        }

        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "Vac");
            }
        }
        private void AutoRealTimeDataTask()
        {
            Task.Run(delegate
            {

                while (true)
                {

                    DateTime now = DateTime.Now;
                    try
                    {
                        if (GlobalVar.NowUiDisplay == (int)NowUi.真空源图片)
                        {
                            CommonTaskRead.ReadVacsourc();
                            CommonTaskRead.ReadUPS();
                            SetAllData();
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
            b1.Text = $"{GlobalVar.plcData.UPS.set_Amps[0]}";
            b2.Text = $"{GlobalVar.plcData.UPS.set_Amps[1]}";
            b3.Text = $"{GlobalVar.plcData.UPS.set_Amps[2]}";
        }
        private void SetAllData()
        {
            try
            {
                time1.Text = $"P1 {GlobalVar.plcData.UPS.runtime_pump[0]} H";
                time2.Text = $"P2 {GlobalVar.plcData.UPS.runtime_pump[1]} H";
                time3.Text = $"P3 {GlobalVar.plcData.UPS.runtime_pump[2]} H";
                p1_A.Text = $"{Math.Round(GlobalVar.plcData.hmi_Vacsource.current_pump[0], 2)}";
                p2_A.Text = $"{Math.Round(GlobalVar.plcData.hmi_Vacsource.current_pump[1], 2)}";
                p3_A.Text = $"{Math.Round(GlobalVar.plcData.hmi_Vacsource.current_pump[2], 2)}";

                h1.Text = $"{GlobalVar.plcData.pump_timing[0]}";
                h2.Text = $"{GlobalVar.plcData.pump_timing[1]}";
                h3.Text = $"{GlobalVar.plcData.pump_timing[2]}";
                //CommonAdsUi.SetVacCircleBtn(p1_circle, GlobalVar.plcData.hmi_Vacsource.status_pump[0]);
                //CommonAdsUi.SetVacCircleBtn(p2_circle, GlobalVar.plcData.hmi_Vacsource.status_pump[1]);
                //CommonAdsUi.SetVacCircleBtn(p3_circle, GlobalVar.plcData.hmi_Vacsource.status_pump[2]);

                CommonAdsUi.SetIconOnOff(pump1, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[0]);
                CommonAdsUi.SetIconOnOff(pump2, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[1]); 
                CommonAdsUi.SetIconOnOff(pump3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[2]);

                CommonAdsUi.SetBtnColorGrayAndGreen(btn_start, GlobalVar.plcData.hmi_Vacsource.status_runing);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_stop, GlobalVar.plcData.hmi_Vacsource.butt_pump_stop);
                //label_tank1PG.Text = $"TANK1.PG {GlobalVar.plcData.hmi_Vacsource.PG_tank[0]} mbar";


                //CommonAdsUi.SetIconOnOff(ps_v1_1, ps_v1_2, ps_v1_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[0]);
                //CommonAdsUi.SetIconOnOff(ps_v2_1, ps_v2_2, ps_v2_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[1]);
                //CommonAdsUi.SetIconOnOff(ps_v3_1, ps_v3_2, ps_v3_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[2]);
                //CommonAdsUi.SetIconOnOff(ps_v4_1, ps_v4_2, ps_v4_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[3]);
                //CommonAdsUi.SetIconOnOff(ps_v5_1, ps_v5_2, ps_v5_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[4]);
                //CommonAdsUi.SetIconOnOff(ps_v6_1, ps_v6_2, ps_v6_3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[5]);

                CommonAdsUi.SetIconOnOff(pumpV1, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[0]);
                CommonAdsUi.SetIconOnOff(pumpV2, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[1]);
                CommonAdsUi.SetIconOnOff(pumpV3, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[2]);
                CommonAdsUi.SetIconOnOff(pumpV4, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[3]);
                CommonAdsUi.SetIconOnOff(pumpV5, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[4]);
                CommonAdsUi.SetIconOnOff(pumpV6, GlobalVar.plcData.hmi_Vacsource.led_pumps_v[5]);

                //CommonAdsUi.SetIconOnOff(tank1V1, GlobalVar.plcData.hmi_Vacsource.led_ExTank_v[0]);
                //CommonAdsUi.SetIconOnOff(tank1V2, GlobalVar.plcData.hmi_Vacsource.led_ExTank_v[1]);
                //CommonAdsUi.SetIconOnOff(tank1V3, GlobalVar.plcData.hmi_Vacsource.led_ExTank_v[2]);
                //CommonAdsUi.SetIconOnOff(tank1V4, GlobalVar.plcData.hmi_Vacsource.led_ExTank_v[3]);
                //CommonAdsUi.SetIconOnOff(tank1V5, GlobalVar.plcData.hmi_Vacsource.led_ExTank_v[4]);
                if (GlobalVar.plcData.hmi_St_Layer.Status_remote_pump)
                {
                    label_btn.Visible = true;
                    btn_start.Visible = true;
                    btn_stop.Visible = true;
                }
                else
                {
                    label_btn.Visible = false;
                    btn_start.Visible = false;
                    btn_stop.Visible = false;
                }
                if (CommonFunction.CheckAccessShebei())
                {
                    b1.Enabled = true;
                    b2.Enabled = true;
                    b3.Enabled = true;
                }
                else
                {
                    b1.Enabled = false;
                    b2.Enabled = false;
                    b3.Enabled = false;
                }
                CommonAdsUi.SetBtnColorGrayAndGreen(mode_1, GlobalVar.plcData.UPS.pump_mode1);
                CommonAdsUi.SetBtnColorGrayAndGreen(mode_2, GlobalVar.plcData.UPS.pump_mode2);
                CommonAdsUi.SetBtnColorGrayAndGreen(mode_3, GlobalVar.plcData.UPS.pump_mode3);

            }
            catch (Exception ex)
            {
                throw new Exception("6001 SetAllData::" + ex.Message);
            }
        }

        private void mode_1_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.Write6001Mode((int)Set6001Mode.Mode1, !GlobalVar.plcData.UPS.pump_mode1);
        }

        private void mode_2_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.Write6001Mode((int)Set6001Mode.Mode2, !GlobalVar.plcData.UPS.pump_mode2);
        }

        private void mode_3_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            CommonTaskRead.Write6001Mode((int)Set6001Mode.Mode3, !GlobalVar.plcData.UPS.pump_mode3);
        }

        private void btn_start_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteVacBtnBool((int)SetVacButtonOrder.自动启动, true);
        }

        private void btn_stop_MouseDown(object sender, MouseEventArgs e)
        {
            if (GlobalVar.IsAuto)
            {
                MessageBox.Show("Error");
                return;
            }
            CommonTaskRead.WriteVacBtnBool((int)SetVacButtonOrder.自动停止, true);
        }

        private void btn_stop_MouseUp(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteVacBtnBool((int)SetVacButtonOrder.自动停止, false);
        }

        private void btn_start_MouseDown(object sender, MouseEventArgs e)
        {
            CommonTaskRead.WriteVacBtnBool((int)SetVacButtonOrder.自动启动, false);
        }

        private void b1_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 325);
        }

        private void b2_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 329);
        }

        private void b3_TextChanged(object sender, EventArgs e)
        {
            SetDataFloat((UITextBox)sender, 333);
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

        private void mode_info1_MouseHover(object sender, EventArgs e)
        {
            mode_label1.Visible = true;
        }

        private void mode_info1_MouseLeave(object sender, EventArgs e)
        {
            mode_label1.Visible = false;
        }

        private void mode_info2_MouseHover(object sender, EventArgs e)
        {
            mode_label2.Visible = true;
        }

        private void mode_info2_MouseLeave(object sender, EventArgs e)
        {
            mode_label2.Visible = false;
        }

        private void mode_info3_MouseHover(object sender, EventArgs e)
        {
            mode_label3.Visible = true;
        }

        private void mode_info3_MouseLeave(object sender, EventArgs e)
        {
            mode_label3.Visible = false;
        }

        private void btnESource_MouseDown(object sender, MouseEventArgs e)
        {
            WriteSourceBtnBool(0, true);
        }

        private void btnESource_MouseUp(object sender, MouseEventArgs e)
        {
            WriteSourceBtnBool(0, false);
        }

        private void btnCSource_MouseDown(object sender, MouseEventArgs e)
        {
            WriteSourceBtnBool(1, true);
        }

        private void btnCSource_MouseUp(object sender, MouseEventArgs e)
        {
            WriteSourceBtnBool(1, false);
        }

        public static void WriteSourceBtnBool(int length, bool value)
        {
            int beforeLength = 348;
            string varName = GlobalVar.commonAdsControl.st_ups_name;// st_ups
            GlobalVar.commonAdsControl.WriteCommonBoolByBefore(varName, beforeLength, value);
        }
    }

    
}
