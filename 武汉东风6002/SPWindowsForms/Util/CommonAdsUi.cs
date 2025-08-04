using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public static class CommonAdsUi
    {
        public static void SetLedColor(UILedBulb led, bool flag)
        {
            led.Color = flag ? GlobalVar.cutomerColors.ActiveColor : GlobalVar.cutomerColors.NotActiveColor;
        }
        public static void SetLabelColorGrayAndGreen(UILabel label, bool flag)
        {
            label.BackColor = flag ? GlobalVar.cutomerColors.ActiveColor : GlobalVar.cutomerColors.NotActiveColor;
        }
        public static void SetLabelColorGrayAndRed(UILabel label, bool flag)
        {
            if (flag)
            {
                label.BackColor = GlobalVar.cutomerColors.RedColor;
                label.ForeColor = Color.White;
            }
            else
            {
                label.BackColor = GlobalVar.cutomerColors.NotActiveColor;
                label.ForeColor = Color.Black;
            }
        }

        public static void SetLabelColorGrayAndOrange(UILabel label, bool flag)
        {
            if (flag)
            {
                label.BackColor = Color.Orange;
                label.ForeColor = Color.White;
            }
            else
            {
                label.BackColor = GlobalVar.cutomerColors.NotActiveColor;
                label.ForeColor = Color.Black;
            }
        }
        public static void SetBtnColorGrayAndBlue(UIButton btn, bool flag)
        {
            if (flag)
            {
                btn.FillColor = Color.SteelBlue;
                btn.FillHoverColor = Color.SteelBlue;
                btn.FillPressColor = Color.SteelBlue;
                btn.FillSelectedColor = Color.SteelBlue;
            }
            else
            {
                btn.FillColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillHoverColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillPressColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillSelectedColor = GlobalVar.cutomerColors.NotActiveColor;
            }
        }
        public static void SetBtnColorGrayAndGreen(UIButton btn, bool flag)
        {
            if (flag)
            {
                btn.FillColor = GlobalVar.cutomerColors.ActiveColor;
                btn.FillHoverColor = GlobalVar.cutomerColors.ActiveColor;
                btn.FillPressColor = GlobalVar.cutomerColors.ActiveColor;
                btn.FillSelectedColor = GlobalVar.cutomerColors.ActiveColor;
            }
            else
            {
                btn.FillColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillHoverColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillPressColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillSelectedColor = GlobalVar.cutomerColors.NotActiveColor;
            }
        }
        public static void SetBtnColorGrayAndRed(UIButton btn, bool flag)
        {
            if (flag)
            {
                btn.FillColor = GlobalVar.cutomerColors.RedColor;
                btn.FillHoverColor = GlobalVar.cutomerColors.RedColor;
                btn.FillPressColor = GlobalVar.cutomerColors.RedColor;
                btn.FillSelectedColor = GlobalVar.cutomerColors.RedColor;
            }
            else
            {
                btn.FillColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillHoverColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillPressColor = GlobalVar.cutomerColors.NotActiveColor;
                btn.FillSelectedColor = GlobalVar.cutomerColors.NotActiveColor;
            }
        }
        public static void SetLightOnOff(UILight light, bool flag)
        {
            if (flag)
            {
                light.State = UILightState.On;
            }
            else
            {
                light.State = UILightState.Off;
            }
        }
        public static void SetSLabelColorGrayAndGreen(UISymbolLabel lbl, bool flag)
        {
            if (flag)
            {
                lbl.BackColor = Color.DarkGreen;
            }
            else
            {
                lbl.BackColor = Color.DarkGray;
            }
        }
        public static void SetSLabelColorGrayAndGreen(UILabel lbl, bool flag)
        {
            if (flag)
            {
                lbl.BackColor = Color.DarkGreen;
            }
            else
            {
                lbl.BackColor = Color.DarkGray;
            }
        }
        public static void SetVacCircleBtn(UIButton btn, bool flag)
        {
            if (flag)
            {
                btn.FillDisableColor = Color.DarkGreen;
            }
            else
            {
                btn.FillDisableColor = Color.DarkGray;
            }
        }
        public static void SetIconOnOff(UISymbolLabel label1, UISymbolLabel label2, UISymbolLabel label3, bool flag)
        {
            if (flag)
            {
                label1.SymbolColor = Color.Green;
                label2.SymbolColor = Color.Green;
                label3.SymbolColor = Color.Green;
            }
            else
            {
                label1.SymbolColor = Color.Red;
                label2.SymbolColor = Color.Red;
                label3.SymbolColor = Color.Red;
            }
        }

        public static void SetIconOnOff(UILabel label, bool flag)
        {
            if (flag)
            {
                label.ForeColor = Color.Green;
            }
            else
            {
                label.ForeColor = Color.Red;
            }
        }

        public static void SetIconBackOnOff(UILabel label, bool flag)
        {
            if (flag)
            {
                label.ForeColor = Color.Black;
                label.BackColor = GlobalVar.cutomerColors.ActiveColor;
            }
            else
            {
                label.ForeColor = Color.Red;
                label.BackColor = Color.White;
            }
        }

        public static void SetUiButtonByInt(List<UIButton> btns, short value)
        {
            for (int i = 0; i < btns.Count; i++)
            {
                SetBtnColorGrayAndGreen(btns[i], Math.Pow(2, i) == value);
            }
        }
        public static void SetUiButtonVisible(List<UIButton> btns, bool flag)
        {
            for (int i = 0; i < btns.Count; i++)
            {
                btns[i].Visible = flag;
            }
        }
    }
}
