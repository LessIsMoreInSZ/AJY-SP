using SPWindowsForms.AdsConnect;
using SPWindowsForms.ExcelHelper;
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
    public partial class WIoControl : UserControl
    {
        private List<IOLightControl> iOLightControls;
        private BottomPageSwitch bSwitch;
        // private List<IOValueControl> iOValueControls;
        public WIoControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            iOLightControls = new List<IOLightControl>();
           // iOValueControls = new List<IOValueControl>();
            IOExcelRead();
            bSwitch=CommonFunction.AddBottomSwitchButton(uiFlowLayoutPanel_bottom, NowUi.IO);
            RealTimeDataTask();
        }
        public void HideFactoryBtn()
        {
            bSwitch.HideFactoryBtn();
        }
        public void IOExcelRead()
        {
            try
            {
                var _language = GlobalVar.ini.iniSystem.ReadString("DATA", "language", "");
                var excelReader = new ExcelReaderClass(Work.GetIOExcelLanguageName(_language));
                var spIOs = new List<IOReportModel>();
                if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
                {
                    spIOs = excelReader.GetIOReportModels("P1-SP9001");
                    if (spIOs.Count != 96) throw new Exception("excel row count incorrect");
                    uiFlowLayoutPanel1.Controls.Add(new IOTitleControl("SP9001"));

                }
                else
                {
                    spIOs = excelReader.GetIOReportModels("P1-SP6001");
                    if (spIOs.Count != 76) throw new Exception("excel row count incorrect");
                    uiFlowLayoutPanel1.Controls.Add(new IOTitleControl("SP6001"));
                }
                for (int i = 1; i <= spIOs.Count; i++)
                {
                    var _spIO = spIOs[i - 1];
                    if (_spIO.IoName.Contains("AI"))
                    {
                    //    var _IOVlaue = new IOValueControl(_spIO.IoName, _spIO.IoText);
                     //   uiFlowLayoutPanel1.Controls.Add(_IOVlaue);
                     //   iOValueControls.Add(_IOVlaue);
                    }
                    else
                    {
                        var _IOLight = new IOLightControl(_spIO.IoName, _spIO.IoText);
                        uiFlowLayoutPanel1.Controls.Add(_IOLight);
                        iOLightControls.Add(_IOLight);
                    }
                }
                var dces = excelReader.GetIOReportModels("D-C-E", 3, 2, true);
                if (dces.Count != 48) throw new Exception("excel row count incorrect");
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    uiFlowLayoutPanel1.Controls.Add(new IOTitleControl($"D{i + 1}"));
                    for (int j = 25; j <= 48; j++)
                    {
                        var _io = dces[j - 1];
                        if (_io.IoName.Contains("AI"))
                        {
                          //  var _IOVlaue = new IOValueControl(_io.IoName, _io.IoText);
                           // uiFlowLayoutPanel1.Controls.Add(_IOVlaue);
                           // iOValueControls.Add(_IOVlaue);
                        }
                        else
                        {
                            var _IOLight = new IOLightControl(_io.IoName, _io.IoText);
                            uiFlowLayoutPanel1.Controls.Add(_IOLight);
                            iOLightControls.Add(_IOLight);
                        }
                    }
                }
                for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                {
                    uiFlowLayoutPanel1.Controls.Add(new IOTitleControl($"C{i + 1}"));
                    for (int j = 9; j <= 24; j++)
                    {
                        var _io = dces[j - 1];
                        if (_io.IoName.Contains("AI"))
                        {
                         //   var _IOVlaue = new IOValueControl(_io.IoName, _io.IoText);
                         //   uiFlowLayoutPanel1.Controls.Add(_IOVlaue);
                          //  iOValueControls.Add(_IOVlaue);
                        }
                        else
                        {
                            var _IOLight = new IOLightControl(_io.IoName, _io.IoText);
                            uiFlowLayoutPanel1.Controls.Add(_IOLight);
                            iOLightControls.Add(_IOLight);
                        }
                    }
                }
                for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                {
                    uiFlowLayoutPanel1.Controls.Add(new IOTitleControl($"E{i + 1}"));
                    for (int j = 1; j <= 8; j++)
                    {
                        var _io = dces[j - 1];
                        if (_io.IoName.Contains("AI"))
                        {
                           // var _IOVlaue = new IOValueControl(_io.IoName, _io.IoText);
                            //uiFlowLayoutPanel1.Controls.Add(_IOVlaue);
                            //iOValueControls.Add(_IOVlaue);
                        }
                        else
                        {
                            var _IOLight = new IOLightControl(_io.IoName, _io.IoText);
                            uiFlowLayoutPanel1.Controls.Add(_IOLight);
                            iOLightControls.Add(_IOLight);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Work.frm_main.ShowErrorMessage("IOExcelRead::" + ex.Message);
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
                        if (GlobalVar.NowUiDisplay == (int)NowUi.IO)
                        {
                            CommonTaskRead.ReadIO();
                            SetAllData();

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
        private void SetAllData()
        {
            try
            {
                int lcount = 0;
                int vcount = 0;
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI1[i]);
                    lcount++;
                }
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI2[i]);
                    lcount++;
                }
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI3[i]);
                    lcount++;
                }
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI4[i]);
                    lcount++;
                }
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI5[i]);
                    lcount++;
                }
                if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
                {
                    for (int i = 0; i < 8; i++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI6[i]);
                        lcount++;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DI7[i]);
                        lcount++;
                    }
                }
                //for (int i = 0; i < 8; i++)
                //{
                //    iOValueControls[vcount].SetIOText(GlobalVar.plcData.IO.hmi_P1_AI1[i]);
                //    vcount++;
                //}
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO1[i]);
                    lcount++;
                }
                for (int i = 0; i < 8; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO2[i]);
                    lcount++;
                }
                for (int i = 0; i < 4; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO3[i]);
                    lcount++;
                }
                for (int i = 0; i < 4; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO4[i]);
                    lcount++;
                }
                for (int i = 0; i < 4; i++)
                {
                    iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO5[i]);
                    lcount++;
                }
                if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_P1_DO6[i]);
                        lcount++;
                    }
                }
                for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_D[i].DI1[j]);
                        lcount++;
                    }
                    //for (int j = 0; j < 4; j++)
                    //{
                    //    iOValueControls[vcount].SetIOText(GlobalVar.plcData.IO.hmi_D[i].AI1[j]);
                    //    vcount++;
                    //}
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_D[i].DO1[j]);
                        lcount++;
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_D[i].DO2[j]);
                        lcount++;
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_D[i].DO3[j]);
                        lcount++;
                    }
                }
                for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
                {
                   
                    //for (int j = 0; j < 4; j++)
                    //{
                    //    iOValueControls[vcount].SetIOText(GlobalVar.plcData.IO.hmi_C[i].AI1[j]);
                    //    vcount++;
                    //}
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_C[i].DO1[j]);
                        lcount++;
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_C[i].DO2[j]);
                        lcount++;
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_D[i].DO3[j]);
                        lcount++;
                    }
                }
                for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
                {
                    //for (int j = 0; j < 4; j++)
                    //{
                    //    iOValueControls[vcount].SetIOText(GlobalVar.plcData.IO.hmi_E[i].AI1[j]);
                    //    vcount++;
                    //}
                    for (int j = 0; j < 4; j++)
                    {
                        iOLightControls[lcount].SetIOOnOff(GlobalVar.plcData.IO.hmi_E[i].DO1[j]);
                        lcount++;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetAllData::" + ex.Message);
            }
        }

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowBengzu();
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            Work.frm_main.ShowYSG();
        }
    }
}
