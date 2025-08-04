using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms.SwitchForms
{
    public partial class AirFilterControl : UserControl
    {
        private int air_use_start = 82;//
        private int air_alarm_start =181;//
        private int air_ql_start =12;//

        private int iron_use_start_D = 95;//
        private int iron_alarm_start_D = 213;//
        private int iron_ql_start_D = 25;//

        private int iron_use_start_C = 106;//
        private int iron_alarm_start_C = 235;//
        private int iron_ql_start_C = 36;//

        private int iron_use_start_E = 108;//
        private int iron_alarm_start_E = 239;//
        private int iron_ql_start_E = 38;//

        private int paper_use_start_D = 85;//
        private int paper_alarm_start_D = 193;//
        private int paper_ql_start_D = 15;//

        private int paper_use_start_C = 105;//
        private int paper_alarm_start_C = 233;//
        private int paper_ql_start_C = 35;//

        private int paper_use_start_E = 107;//
        private int paper_alarm_start_E = 237;//
        private int paper_ql_start_E = 37;//
        private int order;
        private int type;
        private string chn;
        public AirFilterControl(int _order, int _type, string _chn)
        {
            InitializeComponent();
            order = _order;
            type = _type;
            chn = _chn;
            if (type == 1)
                left_label.Text = $"{order}#{LanguageSet.SetL("SheBeiBaoYang", "air_filter_label")}";
            else if (type == 2)
                left_label.Text = $"{chn}{order}{LanguageSet.SetL("SheBeiBaoYang", "iron_filter_label")}";
            else
                left_label.Text = $"{chn}{order}{LanguageSet.SetL("SheBeiBaoYang", "paper_filter_label")}";
            btn_use.Text = LanguageSet.SetL("SheBeiBaoYang", "btn_use");
        }
        public void SetAllDataNoInput()
        {
            if (type == 1)
            {
                label_yunxing.Text = GlobalVar.plcData.UPS.Time_AirFilter_pump[order - 1].ToString();
                label_leiji.Text= GlobalVar.plcData.HMI_Timing_AirFilter_pump[order - 1].ToString();
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_AirFilter_pump[order - 1]);
                CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_AirFilter_pump[order - 1]);
            }
            else if (type == 2)
            {
                if (chn == "D")
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_IronFilter_D[order - 1].ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_IronFilter_D[order - 1].ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_IronFilter_D[order - 1]);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_IronFilter_D[order - 1]);
                }
                else if (chn == "C")
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_IronFilter_C.ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_IronFilter_C.ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_IronFilter_C);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_IronFilter_C);
                }
                else
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_IronFilter_E.ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_IronFilter_E.ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_IronFilter_E);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_IronFilter_E);
                }
            }
            else
            {
                if (chn == "D")
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_PaperFilter_D[order - 1].ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_PaperFilter_D[order - 1].ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_PaperFilter_D[order - 1]);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_PaperFilter_D[order - 1]);
                }
                else if (chn == "C")
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_PaperFilter_C.ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_PaperFilter_C.ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_PaperFilter_C);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_PaperFilter_C);
                }
                else
                {
                    label_yunxing.Text = GlobalVar.plcData.UPS.Time_PaperFilter_E.ToString();
                    label_leiji.Text = GlobalVar.plcData.HMI_Timing_PaperFilter_E.ToString();
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_ql, GlobalVar.plcData.hmi_K.ButtCL_PaperFilter_E);
                    CommonAdsUi.SetBtnColorGrayAndGreen(btn_use, GlobalVar.plcData.UPS.Use_PaperFilter_E);
                }
            }

        }
        public void SetAllDataInput()
        {
            if (type == 1)
                txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_AirFilter_pump[order - 1].ToString();
            else if (type == 2)
            {
                if (chn == "D")
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_IronFilter_D[order - 1].ToString();
                }
                else if (chn == "C")
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_IronFilter_C.ToString();
                }
                else
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_IronFilter_E.ToString();
                }
            }
            else
            {
                if (chn == "D")
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_PaperFilter_D[order - 1].ToString();
                }
                else if (chn == "C")
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_PaperFilter_C.ToString();
                }
                else
                {
                    txt_alarm.Text = GlobalVar.plcData.UPS.Upkeep_PaperFilter_E.ToString();
                }
            }
        }

        private void btn_use_Click(object sender, EventArgs e)
        {
            if (type == 1)
                GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, air_use_start + order - 1, !GlobalVar.plcData.UPS.Use_AirFilter_pump[order - 1]);
            else if (type == 2)
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_use_start_D + order - 1, !GlobalVar.plcData.UPS.Use_IronFilter_D[order - 1]);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_use_start_C, !GlobalVar.plcData.UPS.Use_IronFilter_C);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_use_start_E, !GlobalVar.plcData.UPS.Use_IronFilter_E);
                }
            }
            else
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_use_start_D + order - 1, !GlobalVar.plcData.UPS.Use_PaperFilter_D[order - 1]);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_use_start_C, !GlobalVar.plcData.UPS.Use_PaperFilter_C);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_use_start_E, !GlobalVar.plcData.UPS.Use_PaperFilter_E);
                }
            }
          
        }

        private void txt_alarm_TextChanged(object sender, EventArgs e)
        {
            var value = txt_alarm.Text;
            if (value == "")
            {
                value = "0";
            }
            if (value == "-")
            {
                value = "0";
            }
            if (type == 1)
            {
                if (!CommonFunction.CheckStrNumType(value, true))
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                    SetAllDataInput();
                    return;
                }
            }
            else
            {
                if (!CommonFunction.CheckStrNumType(value, false))
                {
                    Work.ShowErrorMessage(LanguageSet.SetL("pf", "alarm_convert"));
                    SetAllDataInput();
                    return;
                }
            }

            if (type == 1)
            {
                var _set = Convert.ToSingle(value);
                GlobalVar.commonAdsControl.WriteCommonFloatByBefore(GlobalVar.commonAdsControl.st_ups_name, air_alarm_start + (order - 1) * 4, _set);
            }
            else {
                var _set = Convert.ToUInt16(value);
                if (type == 2)
                {
                    if (chn == "D")
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_alarm_start_D + (order - 1) * 2, _set);
                    }
                    else if (chn == "C")
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_alarm_start_C, _set);
                    }
                    else
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, iron_alarm_start_E, _set);
                    }
                }
                else
                {
                    if (chn == "D")
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_alarm_start_D + (order - 1) * 2, _set);
                    }
                    else if (chn == "C")
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_alarm_start_C, _set);
                    }
                    else
                    {
                        GlobalVar.commonAdsControl.WriteCommonUShortByBefore(GlobalVar.commonAdsControl.st_ups_name, paper_alarm_start_E, _set);
                    }
                }
            }
        }

        private void btn_ql_MouseDown(object sender, MouseEventArgs e)
        {
            if (type == 1)
                GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, air_ql_start + (order - 1), true);
            else if (type == 2)
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_D + (order - 1), true);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_C, true);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_E, true);
                }
            }
            else
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_D + (order - 1), true);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_C, true);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_E, true);
                }
            }
        }

        private void btn_ql_MouseUp(object sender, MouseEventArgs e)
        {
            if (type == 1)
                GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, air_ql_start + (order - 1), false);
            else if (type == 2)
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_D + (order - 1), false);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_C, false);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, iron_ql_start_E, false);
                }
            }
            else
            {
                if (chn == "D")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_D + (order - 1), false);
                }
                else if (chn == "C")
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_C, false);
                }
                else
                {
                    GlobalVar.commonAdsControl.WriteCommonBoolByBefore(GlobalVar.commonAdsControl.hmi_K_name, paper_ql_start_E, false);
                }
            }
        }
    }
}
