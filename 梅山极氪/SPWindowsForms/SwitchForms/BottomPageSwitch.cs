using Sunny.UI;
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
    public partial class BottomPageSwitch: UserControl
    {
        public BottomPageSwitch(int page)
        {
            InitializeComponent();
            IniAllControlName();
            IniAllPageBtn();
            SetPageBtnActive(page);
        }
        public void HideFactoryBtn()
        {
            if (!CommonFunction.CheckAccessFactory())
            {
              //  btn_switch_ysg.Visible = false;
                btn_switch_chuanganqi.Visible = false;
                btn_switch_zhuji.Visible = false;
            }
            else
            {
               // btn_switch_ysg.Visible = true;
                btn_switch_chuanganqi.Visible = true;
                btn_switch_zhuji.Visible = true;
            }
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "BottomSwitch");
            }
        }
        private void IniAllPageBtn()
        {
            foreach (var ctrl in this.Controls)
            {
                if (ctrl is UIButton)
                {
                    var _ctrl = (UIButton)ctrl;
                    CommonAdsUi.SetBtnColorGrayAndBlue(_ctrl, false);
                }
            }
        }
        private void SetPageBtnActive(int page)
        {
            switch (page)
            {
                case (int)NowUi.设备管理:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_shebeiguanli, true);
                    break;
                case (int)NowUi.设备维护:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_weihu, true);
                    break;
                case (int)NowUi.泵组维护:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_bengzu, true);
                    break;
                case (int)NowUi.IO:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_IO, true);
                    break;
                case (int)NowUi.压射杆:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_ysg, true);
                    break;
                case (int)NowUi.配方Log:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_pflog, true);
                    break;
                case (int)NowUi.液压站:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_yeya, true);
                    break;
                case (int)NowUi.设备保养:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_baoyang, true);
                    break;
                case (int)NowUi.传感器校准:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_chuanganqi, true);
                    break;
                case (int)NowUi.主机信号模拟:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_zhuji, true);
                    break;
                case (int)NowUi.系统设定:
                    CommonAdsUi.SetBtnColorGrayAndBlue(btn_switch_sheding, true);
                    break;
                default:
                    break;
            }
        }

        private void btn_switch_shebeiguanli_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
               // if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowSheBeiGuanLi();
            GlobalVar.BottomUiDisplay = (int)NowUi.设备管理;
        }

        private void btn_switch_weihu_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowWeihu();
            GlobalVar.BottomUiDisplay = (int)NowUi.设备维护;
        }

        private void btn_switch_bengzu_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowBengzu();
            GlobalVar.BottomUiDisplay = (int)NowUi.泵组维护;
        }

        private void btn_switch_IO_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowIO();
            GlobalVar.BottomUiDisplay = (int)NowUi.IO;
        }

        private void btn_switch_ysg_Click(object sender, EventArgs e)
        {
            //if (!CommonFunction.CheckAccessFactory())
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowYSG();
            GlobalVar.BottomUiDisplay = (int)NowUi.压射杆;
        }

        private async void btn_switch_pflog_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            /* await */
            Work.frm_main.ShowPfLog();
            GlobalVar.BottomUiDisplay = (int)NowUi.配方Log;
          
   
        }

        private void btn_switch_baoyang_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowBaoYang();
            GlobalVar.BottomUiDisplay = (int)NowUi.设备保养;
        }

        private void btn_switch_yeya_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowYeYang();
            GlobalVar.BottomUiDisplay = (int)NowUi.液压站;
        }

        private void btn_switch_chuanganqi_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessFactory())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowChuanganqi();
            GlobalVar.BottomUiDisplay = (int)NowUi.传感器校准;
        }

        private void btn_switch_zhuji_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessFactory())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowZhuji();
            GlobalVar.BottomUiDisplay = (int)NowUi.主机信号模拟;
        }

        private void btn_switch_sheding_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            //  if (!CommonFunction.CheckAccessShebei())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            Work.frm_main.ShowXitongsheding();
            GlobalVar.BottomUiDisplay = (int)NowUi.系统设定;
        }
    }
}
