using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPWindowsForms
{
    public static class LanguageSet
    {
        public static void SetLanguageByData(object ctrl, string sectionName)
        {
            if (!GlobalVar.ini.iniLanguage.allData.ContainsKey(sectionName)) return;
            var valueDic = GlobalVar.ini.iniLanguage.allData[sectionName];
            if (ctrl is Label)
            {
                var _ctrl = (Label)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UILabel)
            {
                var _ctrl = (UILabel)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UIMarkLabel)
            {
                var _ctrl = (UIMarkLabel)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is Button)
            {
                var _ctrl = (Button)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UIButton)
            {
                var _ctrl = (UIButton)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UISymbolButton)
            {
                var _ctrl = (UISymbolButton)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UISymbolLabel)
            {
                var _ctrl = (UISymbolLabel)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UIGroupBox)
            {
                var _ctrl = (UIGroupBox)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
            else if (ctrl is UICheckBox)
            {
                var _ctrl = (UICheckBox)ctrl;
                if (valueDic.ContainsKey(_ctrl.Name))
                    _ctrl.Text = valueDic[_ctrl.Name];
            }
        }
        public static string SetL( string sectionName, string keyname)
        {
            if (!GlobalVar.ini.iniLanguage.allData.ContainsKey(sectionName)) return "Language Error";
            var valueDic = GlobalVar.ini.iniLanguage.allData[sectionName];

            if (valueDic.ContainsKey(keyname))
                return valueDic[keyname];
            else
                return "Language Error";
        }
    }

}
