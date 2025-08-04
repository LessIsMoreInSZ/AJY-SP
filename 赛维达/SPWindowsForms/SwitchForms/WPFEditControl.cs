using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace SPWindowsForms.SwitchForms
{
    public partial class WPFEditControl : UserControl
    {
        private List<UIButton> chButtons = new List<UIButton>();
        private List<UICheckBox> useCheckBoxes = new List<UICheckBox>();
        private List<UICheckBox> sysCheckBoxes = new List<UICheckBox>();
        private List<UILabel> sysLabels = new List<UILabel>();
        private List<UIDoubleUpDown> systemNumbers = new List<UIDoubleUpDown>();
        private List<UILabel> alarmLeftLabels = new List<UILabel>();
        private List<UICheckBox> alarmCheckBoxes = new List<UICheckBox>();
        private List<List<UIDoubleUpDown>> alarmNumbers = new List<List<UIDoubleUpDown>>();
        private PfPageConfigModel D_PfPageConfigModel = new PfPageConfigModel();
        private PfPageConfigModel C_PfPageConfigModel = new PfPageConfigModel();
        private int buttonSpace = 10;


        public WPFEditControl()
        {
            InitializeComponent();
            IniAllControlName();
            ListAllControls();
            BuildAllPageModels();
            int newWidth = GetNewButtonWidth();
            NewAllButtons(newWidth);
            NewCheckBoxs(newWidth);
            SwitchChnClickHandler(button_switch_CH1, null);
            //button_switch_CH1.Tag = "1";
            //button_switch_CH1.Click += ButtonClickHandler;
        }
        #region Auto New 
        private void SetAllBtnColor()
        {
            chButtons.ForEach(c => c.FillColor = Color.DodgerBlue);
        }
        private void SwitchChnClickHandler(object sender, EventArgs e)
        {
            UIButton clickedButton = (UIButton)sender;
            int i = Convert.ToInt32(clickedButton.Tag);
            SetAllBtnColor();
            chButtons[i - 1].FillColor = Color.MidnightBlue;
            if (i <= GlobalVar.systemSetting.d_chn_count)
                ChangeChnModel(D_PfPageConfigModel);
            else if (i - GlobalVar.systemSetting.d_chn_count == 1)
                ChangeChnModel(C_PfPageConfigModel);
            //else
            //    btnName = "E1";
        }
        private int GetNewButtonWidth()
        {
            int width = (this.Size.Width - buttonSpace) / (GlobalVar.systemSetting.d_chn_count + 2) - buttonSpace;
            return width;
        }
        private void NewAllButtons(int newWidth)
        {
         //   int newWidth = GetNewButtonWidth();
            if (GlobalVar.systemSetting.d_chn_count > 1)
            {
                for (int i = 2; i <= GlobalVar.systemSetting.d_chn_count + 2; i++)
                {
                    var btnName = "";
                    if (i <= GlobalVar.systemSetting.d_chn_count)
                        btnName = $"D{i}";
                    else if (i - GlobalVar.systemSetting.d_chn_count == 1)
                        btnName = "C1";
                    else
                        btnName = "E1";

                    var newBtn = NewUIButton(i, btnName, buttonSpace + (i - 1) * (buttonSpace + newWidth), button_switch_CH1.Location.Y, button_switch_CH1.Font);
                    this.Controls.Add(newBtn);
                    chButtons.Add(newBtn);
                }
            }
            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count + 2; i++)
            {
                chButtons[i - 1].Tag = i;
                chButtons[i - 1].Size = new Size(newWidth, button_switch_CH1.Size.Height);
                chButtons[i - 1].RectColor = Color.White;
                chButtons[i - 1].FillColor = Color.DodgerBlue;
                chButtons[i - 1].Click += SwitchChnClickHandler;
            }
        }
        private UIButton NewUIButton(int order, string text, int x, int y, /*int width, int height,*/ Font font, string baseName = "button_switch_CH")
        {
            var btn = new UIButton();
            //btn.Size = new Size(width, height);
            btn.Location = new Point(x, y);
            btn.Name = $"{baseName}{order}";
            btn.Text = text;
            btn.Font = font;
            return btn;

        }
        private void NewCheckBoxs(int newWidth)
        {
          //  int newWidth = GetNewButtonWidth();
            if (GlobalVar.systemSetting.d_chn_count > 1)
            {
                for (int i = 2; i <= GlobalVar.systemSetting.d_chn_count + 2; i++)
                {
                    var newC = NewUICheckBox(i, buttonSpace + (i - 1) * (buttonSpace + newWidth), use_check_1.Location.Y, use_check_1.Size.Width, use_check_1.Size.Height, use_check_1.CheckBoxSize);
                    this.Controls.Add(newC);
                    useCheckBoxes.Add(newC);
                }
            }
        }
        private UICheckBox NewUICheckBox(int order, int x, int y,int width,int height, int checkBoxSize, string baseName = "use_check_")
        {
            var c = new UICheckBox();
            c.Size = new Size(width, height);
            c.Location = new Point(x, y);
            c.Name = $"{baseName}{order}";
            c.CheckBoxSize = checkBoxSize;
            return c;

        }
        #endregion
        private void BuildAllPageModels()
        {
            D_PfPageConfigModel = BuildDSystemPageData();
            C_PfPageConfigModel= BuildCSystemPageData();
        }
        private void HideAllEles()
        {
            sysCheckBoxes.ForEach(s => s.Visible = false);
            sysLabels.ForEach(s => s.Visible = false);
            systemNumbers.ForEach(s => s.Visible = false);
            alarmLeftLabels.ForEach(a => a.Visible = false);
            alarmCheckBoxes.ForEach(a => a.Visible = false);
            alarmNumbers.ForEach(a => a.ForEach(n => n.Visible = false));
        }
        private void ChangeChnModel(PfPageConfigModel model)
        {
            HideAllEles();
            for (int i = 0; i < model.systemModel.checks.Count; i++)
            {
                var c = model.systemModel.checks[i];
                sysCheckBoxes[i].Visible = true;
                sysCheckBoxes[i].Text = LanguageSet.SetL("pf", c.parName);
            }
            for (int i = 0; i < model.systemModel.inputs.Count; i++)
            {
                var input = model.systemModel.inputs[i];
                sysLabels[i].Visible = true;
                systemNumbers[i].Visible = true;
                sysLabels[i].Text = LanguageSet.SetL("pf", input.parName);
                if (input.numType)
                    systemNumbers[i].Step = 0.1;
                else
                    systemNumbers[i].Step = 1;
            }
            for (int i = 0; i < model.alarmModel.alarms.Count; i++)
            {
                var a = model.alarmModel.alarms[i];
                alarmLeftLabels[i].Visible = true;
                alarmLeftLabels[i].Text = LanguageSet.SetL("pf", a.parName);
                for(int j= 0; j < a.numUse.Count; j++)
                {
                    var _use = a.numUse[j];
                    if (j == 0)
                    {
                        if (_use)
                            alarmCheckBoxes[i].Visible = true;
                    }
                    else
                    {
                        if (_use)
                            alarmNumbers[i][j - 1].Visible = true;
                    }
                }
                
            }
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "pf");
                if (ctrl is UIGroupBox)
                {
                    var _panel = (UIGroupBox)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "pf");
                    }
                }
            }
        }
        private void ListAllControls()
        {
            chButtons.Add(button_switch_CH1);
            useCheckBoxes.Add(use_check_1);
            #region 系统CheckBox
            sysCheckBoxes.Add(uiCheckBox_System1);
            sysCheckBoxes.Add(uiCheckBox_System2);
            sysCheckBoxes.Add(uiCheckBox_System3);
            sysCheckBoxes.Add(uiCheckBox_System4);
            sysCheckBoxes.Add(uiCheckBox_System5);
            sysCheckBoxes.Add(uiCheckBox_System6);
            sysCheckBoxes.Add(uiCheckBox_System7);
            sysCheckBoxes.Add(uiCheckBox_System8);
            sysCheckBoxes.Add(uiCheckBox_System9);
            sysCheckBoxes.Add(uiCheckBox_System10);
            #endregion
            #region 系统Lables
            sysLabels.Add(uiLabel_System1);
            sysLabels.Add(uiLabel_System2);
            sysLabels.Add(uiLabel_System3);
            sysLabels.Add(uiLabel_System4);
            sysLabels.Add(uiLabel_System5);
            sysLabels.Add(uiLabel_System6);
            sysLabels.Add(uiLabel_System7);
            sysLabels.Add(uiLabel_System8);
            sysLabels.Add(uiLabel_System9);
            sysLabels.Add(uiLabel_System10);
            sysLabels.Add(uiLabel_System11);
            sysLabels.Add(uiLabel_System12);
            sysLabels.Add(uiLabel_System13);
            sysLabels.Add(uiLabel_System14);
            sysLabels.Add(uiLabel_System15);
            sysLabels.Add(uiLabel_System16);
            #endregion
            #region systemNumbers
            systemNumbers.Add(uiDoubleUpDown_System1);
            systemNumbers.Add(uiDoubleUpDown_System2);
            systemNumbers.Add(uiDoubleUpDown_System3);
            systemNumbers.Add(uiDoubleUpDown_System4);
            systemNumbers.Add(uiDoubleUpDown_System5);
            systemNumbers.Add(uiDoubleUpDown_System6);
            systemNumbers.Add(uiDoubleUpDown_System7);
            systemNumbers.Add(uiDoubleUpDown_System8);
            systemNumbers.Add(uiDoubleUpDown_System9);
            systemNumbers.Add(uiDoubleUpDown_System10);
            systemNumbers.Add(uiDoubleUpDown_System11);
            systemNumbers.Add(uiDoubleUpDown_System12);
            systemNumbers.Add(uiDoubleUpDown_System13);
            systemNumbers.Add(uiDoubleUpDown_System14);
            systemNumbers.Add(uiDoubleUpDown_System15);
            systemNumbers.Add(uiDoubleUpDown_System16);
            #endregion
            #region alarmLeftLabels
            alarmLeftLabels.Add(uiLabel_alarmleft1);
            alarmLeftLabels.Add(uiLabel_alarmleft2);
            alarmLeftLabels.Add(uiLabel_alarmleft3);
            alarmLeftLabels.Add(uiLabel_alarmleft4);
            alarmLeftLabels.Add(uiLabel_alarmleft5);
            alarmLeftLabels.Add(uiLabel_alarmleft6);
            alarmLeftLabels.Add(uiLabel_alarmleft7);
            alarmLeftLabels.Add(uiLabel_alarmleft8);
            alarmLeftLabels.Add(uiLabel_alarmleft9);
            alarmLeftLabels.Add(uiLabel_alarmleft10);
            #endregion
            #region alarmCheckBoxes
            alarmCheckBoxes.Add(uiCheckBox_alarm1);
            alarmCheckBoxes.Add(uiCheckBox_alarm2);
            alarmCheckBoxes.Add(uiCheckBox_alarm3);
            alarmCheckBoxes.Add(uiCheckBox_alarm4);
            alarmCheckBoxes.Add(uiCheckBox_alarm5);
            alarmCheckBoxes.Add(uiCheckBox_alarm6);
            alarmCheckBoxes.Add(uiCheckBox_alarm7);
            alarmCheckBoxes.Add(uiCheckBox_alarm8);
            alarmCheckBoxes.Add(uiCheckBox_alarm9);
            alarmCheckBoxes.Add(uiCheckBox_alarm10);
            #endregion
            #region alarmNumbers
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_1_1, uiDoubleUpDown_alarm_1_2, uiDoubleUpDown_alarm_1_3, uiDoubleUpDown_alarm_1_4, uiDoubleUpDown_alarm_1_5, uiDoubleUpDown_alarm_1_6 });

            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_2_1, uiDoubleUpDown_alarm_2_2, uiDoubleUpDown_alarm_2_3, uiDoubleUpDown_alarm_2_4, uiDoubleUpDown_alarm_2_5, uiDoubleUpDown_alarm_2_6 });

            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_3_1, uiDoubleUpDown_alarm_3_2, uiDoubleUpDown_alarm_3_3, uiDoubleUpDown_alarm_3_4,
uiDoubleUpDown_alarm_3_5, uiDoubleUpDown_alarm_3_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_4_1, uiDoubleUpDown_alarm_4_2, uiDoubleUpDown_alarm_4_3, uiDoubleUpDown_alarm_4_4,
uiDoubleUpDown_alarm_4_5, uiDoubleUpDown_alarm_4_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_5_1, uiDoubleUpDown_alarm_5_2, uiDoubleUpDown_alarm_5_3, uiDoubleUpDown_alarm_5_4,
uiDoubleUpDown_alarm_5_5, uiDoubleUpDown_alarm_5_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_6_1, uiDoubleUpDown_alarm_6_2, uiDoubleUpDown_alarm_6_3, uiDoubleUpDown_alarm_6_4,
uiDoubleUpDown_alarm_6_5, uiDoubleUpDown_alarm_6_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_7_1, uiDoubleUpDown_alarm_7_2, uiDoubleUpDown_alarm_7_3, uiDoubleUpDown_alarm_7_4,
uiDoubleUpDown_alarm_7_5, uiDoubleUpDown_alarm_7_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_8_1, uiDoubleUpDown_alarm_8_2, uiDoubleUpDown_alarm_8_3, uiDoubleUpDown_alarm_8_4,
uiDoubleUpDown_alarm_8_5, uiDoubleUpDown_alarm_8_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_9_1, uiDoubleUpDown_alarm_9_2, uiDoubleUpDown_alarm_9_3, uiDoubleUpDown_alarm_9_4,
uiDoubleUpDown_alarm_9_5, uiDoubleUpDown_alarm_9_6 });
            alarmNumbers.Add(new List<UIDoubleUpDown> { uiDoubleUpDown_alarm_10_1, uiDoubleUpDown_alarm_10_2, uiDoubleUpDown_alarm_10_3, uiDoubleUpDown_alarm_10_4, uiDoubleUpDown_alarm_10_5, uiDoubleUpDown_alarm_10_6 });
            #endregion
        }


        private PfPageConfigModel BuildDSystemPageData()
        {
            var model = new PfPageConfigModel();
            #region systemModel input

            for (int i = 1; i <= 7; i++)
            {
                model.systemModel.inputs.Add(new PfPageConfigSystemSingleModel
                {
                    parName = $"D_System{i}",
                    numType = true
                });
            }
            model.systemModel.inputs[0].numType = true;
            model.systemModel.inputs[1].numType = true;
            #endregion
            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= 7; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"D_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 10; i++)
            {
                model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                {
                    parName = $"D_Alarm{i}"
                });
            }
            #endregion
            return model;
        }

        private PfPageConfigModel BuildCSystemPageData()
        {
            var model = new PfPageConfigModel();
            #region systemModel input

            for (int i = 1; i <= 13; i++)
            {
                model.systemModel.inputs.Add(new PfPageConfigSystemSingleModel
                {
                    parName = $"C_System{i}",
                    numType = true
                });
            }
            model.systemModel.inputs[0].numType = true;
            model.systemModel.inputs[1].numType = true;
            model.systemModel.inputs[2].numType = true;
            model.systemModel.inputs[3].numType = true;
            model.systemModel.inputs[4].numType = true;
            model.systemModel.inputs[5].numType = true;

            #endregion
            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= 2; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"C_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 8; i++)
            {
                model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                {
                    parName = $"C_Alarm{i}"
                });
            }
            #endregion
            return model;
        }
        private PfPageConfigModel BuildESystemPageData()
        {
            var model = new PfPageConfigModel();
     
            #region systemModel check
            //  model.systemModel.checkCount = 5;
            model.systemModel.checks = new List<PfPageConfigSystemCheckModel>();
            for (int i = 1; i <= 2; i++)
            {
                model.systemModel.checks.Add(new PfPageConfigSystemCheckModel
                {
                    parName = $"C_Check{i}"
                });
            }
            #endregion

            #region alarmModel check
            for (int i = 1; i <= 8; i++)
            {
                model.alarmModel.alarms.Add(new PfPageConfigAlarmSingleModel
                {
                    parName = $"C_Alarm{i}"
                });
            }
            #endregion
            return model;
        }
    }
}
