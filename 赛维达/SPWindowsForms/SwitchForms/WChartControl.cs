using EF.Models.EF.Entities;
using SPWindowsForms.AdsConnect;
using SPWindowsForms.DbService;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace SPWindowsForms.SwitchForms
{
    public partial class WChartControl : UserControl
    {
        private bool isRefreshHistoryTable = false;
        private bool isRefreshChart = false;
        private bool isRefreshRealData = false;
        private bool isRefreshAutoLine = false;
        private bool isUpdating = false;
        private bool realTimeFlag = true;
        private List<UISymbolButton> visiblebtns = new List<UISymbolButton>();
        private List<UILineSeries> lineSeries = new List<UILineSeries>();
        private List<Color> colors = new List<Color> { Color.Blue,
    Color.Green,
    Color.Purple,
    Color.Orange,
    Color.Brown,
    Color.Yellow,
    Color.Pink,
    Color.Cyan,
    Color.Magenta,
    Color.Lime,
    Color.SkyBlue,
    Color.Olive,
    Color.Maroon,
    Color.Navy,
    Color.ForestGreen
        };
        private int visiblebtnSpace = 1;
        private int visiblebtnWidth = 70;
        private int visiblebtnHeight = 25;
        private int visiblebtnStartX = 3;
        private int visiblebtnStartY = 710;
        private int nowPoint = 0;
        private bool nowClickPointFlag = false;
        public int realChartType = 1;
        private ConvertSampleTable searchSample = null;
        private List<ConvertSampleTable> lastSamples = new List<ConvertSampleTable>();
        private List<ConvertSampleTable> reallastSamples = new List<ConvertSampleTable>();
        private List<ProcessControl> processControls_D = new List<ProcessControl>();
        private List<ProcessControl> processControls_C = new List<ProcessControl>();
        private List<ProcessControl> processControls_E = new List<ProcessControl>();
        //new Font("Arial", 9)
        public WChartControl()
        {
            InitializeComponent();
            reallastSamples = CommonFunction.DeepCopyConvertSampleTables(SampleDBCommon.GetAllLastSamples());
            CheckForIllegalCrossThreadCalls = false;
            IniAllControlName();
            NewAllVisibleButtons();
            IniChart();
            IniRealTable1Head();
            IniRealTableRows();
            IniHistoryTable1Head();

            ShowHideView();
            IniSearchBox();
            IniTablePanel();
            if (GlobalVar.systemSetting.machine_config.ToUpper() != "SP9001")
            {
                total_label_4.Visible = false;
                total_label_6.Visible = false;
                total_label_value_4.Visible = false;
                total_label_value_6.Visible = false;
            }
            RealTimeChartTask();
        }

        private void IniTablePanel()
        {
            uiFlowLayoutPanel1.Visible = false;
            uiPanelTotal.Visible = true;
            uiFlowLayoutPanel1.Size = new Size(900, 600);
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                var processControl = new ProcessControl("D", i + 1);
                uiFlowLayoutPanel1.Controls.Add(processControl);
                processControls_D.Add(processControl);
            }

            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                var processControl = new ProcessControl("C", i + 1);
                uiFlowLayoutPanel1.Controls.Add(processControl);
                processControls_C.Add(processControl);
            }
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                var processControl = new ProcessControl("E", i + 1);
                uiFlowLayoutPanel1.Controls.Add(processControl);
                processControls_E.Add(processControl);
            }
        }
        private void IniAllControlName()
        {
            foreach (var ctrl in this.Controls)
            {
                LanguageSet.SetLanguageByData(ctrl, "chart");
                if (ctrl is UIGroupBox)
                {
                    var _panel = (UIGroupBox)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "chart");
                    }
                }
                if (ctrl is UIPanel)
                {
                    var _panel = (UIPanel)ctrl;
                    foreach (var panelctrl in _panel.Controls)
                    {
                        LanguageSet.SetLanguageByData(panelctrl, "chart");
                    }
                }
            }
        }
        #region 初始化图表和按钮
        private void NewAllVisibleButtons()
        {
            int _count = 1;
            int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
            for (int i = 1; i <= totalCount; i++)
            {
                var _type = "D";
                if (i > GlobalVar.systemSetting.d_chn_count)
                {
                    _type = "C";
                }
                var _btn = NewUISymbolButton(i, $"{_type}{_count}", visiblebtnStartX + (visiblebtnWidth + visiblebtnSpace) * (i - 1), visiblebtnStartY, visiblebtnWidth, visiblebtnHeight);
                this.Controls.Add(_btn);
                visiblebtns.Add(_btn);
                if (i == GlobalVar.systemSetting.d_chn_count)
                    _count = 1;
                else
                    _count++;

            }
            var _sbtn = NewUISymbolButton(totalCount + 1, "Velocity", visiblebtnStartX + (visiblebtnWidth + visiblebtnSpace) * totalCount, visiblebtnStartY, visiblebtnWidth, visiblebtnHeight);
            this.Controls.Add(_sbtn);
            visiblebtns.Add(_sbtn);

        }

        private UISymbolButton NewUISymbolButton(int order, string text, int x, int y, int width, int height, string baseName = "button_visible_chn")
        {
            var btn = new UISymbolButton();
            btn.Size = new Size(width, height);
            btn.Location = new Point(x, y);
            btn.Name = $"{baseName}{order}";
            btn.Text = text;
            btn.Font = new Font("Arial", 9);
            btn.Tag = order;
            btn.RectColor = Color.White;
            btn.FillColor = Color.White;
            btn.FillColor2 = Color.White;
            btn.ForeColor = Color.Black;
            btn.Symbol = 61517;
            btn.SymbolSize = 24;
            int colorCount = (order - 1) % colors.Count;
            btn.SymbolColor = colors[colorCount];
            btn.Click += ShowHideChnHandler;
            return btn;
        }
        private void ShowHideChnHandler(object sender, EventArgs e)
        {
            UISymbolButton clickedButton = (UISymbolButton)sender;
            int i = Convert.ToInt32(clickedButton.Tag);
            var option = LineChart.Option;
            if (lineSeries[i - 1].Visible)
            {
                lineSeries[i - 1].Visible = false;
                clickedButton.ForeColor = Color.Gray;
                clickedButton.SymbolColor = Color.Gray;
            }
            else
            {
                int colorCount = (i - 1) % colors.Count;
                lineSeries[i - 1].Visible = true;
                clickedButton.ForeColor = Color.Black;
                clickedButton.SymbolColor = colors[colorCount];
            }
            LineChart.SetOption(option);
        }

        /// <summary>
        /// anders 20250527 刷新所有曲线ui
        /// </summary>
        /// <param name="option"></param>
        private void NewAllUILineSeries(UILineOption option)
        {
            int _count = 1;
            int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
            for (int i = 1; i <= totalCount; i++)
            {
                var _type = "D";
                if (i > GlobalVar.systemSetting.d_chn_count)
                {
                    _type = "C";
                }
                var _s = NewUILineSeries(option, i, $"{_type}{_count}");

                lineSeries.Add(_s);
                if (i == GlobalVar.systemSetting.d_chn_count)
                    _count = 1;
                else
                    _count++;

            }
            var _ss = NewUILineSeries(option, totalCount + 1, "Velocity", true);
            lineSeries.Add(_ss);

        }
        public UILineSeries NewUILineSeries(UILineOption option, int order, string name, bool y2flag = false)
        {
            int colorCount = (order - 1) % colors.Count;
            var series = option.AddSeries(new UILineSeries(name, y2flag));
            series.CustomColor = true;
            series.Color = colors[colorCount];
            series.Symbol = UILinePointSymbol.Circle;
            series.SymbolSize = 0;
            series.SymbolLineWidth = 1;

            series.SymbolColor = colors[colorCount];
            series.Smooth = true;
            return series;
        }
        public void IniChart()
        {
            UILineOption option = new UILineOption();
            option.Title.Text = "";
            option.ToolTip.Visible = false;
            //option.Title = new UITitle();
            //option.Title.Text = "SunnyUI";
            //option.Title.SubText = "LineChart";
            NewAllUILineSeries(option);

            option.XAxis.Name = $"{LanguageSet.SetL("chart", "pos")} mm";
            option.XAxis.ShowGridLine = false;
            option.XAxis.MinAuto = false;
            option.XAxis.MaxAuto = false;
            option.XAxis.Min = 1;
            option.XAxis.Max = GlobalVar.systemSetting.sample_pointcount;

            option.YAxis.Name = $"{LanguageSet.SetL("chart", "pg")} mbar";
            option.YAxis.ShowGridLine = false;
            option.YAxis.MinAuto = false;
            option.YAxis.MaxAuto = false;
            option.YAxis.Min = GlobalVar.systemSetting.pg_min;
            option.YAxis.Max = GlobalVar.systemSetting.pg_max;
            option.Y2Axis.Name = $"{LanguageSet.SetL("chart", "vel")} mm/s";
            option.Y2Axis.ShowGridLine = false;
            option.Y2Axis.MinAuto = false;
            option.Y2Axis.MaxAuto = false;
            option.Y2Axis.Min = GlobalVar.systemSetting.vel_min;
            option.Y2Axis.Max = GlobalVar.systemSetting.vel_max;

            //设置坐标轴为自定义标签
            //     option.XAxis.CustomLabels = new CustomLabels(1, 1, GlobalVar.systemSetting.sample_pointcount - 1);
            // option.XAxis.CustomLabels.ClearLabels();
            //   var poses = GetCommonPos();
            //for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
            //{
            //    float x = 0;
            //    if (poses.Count > i)
            //        x = poses[i];
            //    if (x % 500 == 0 || x == 1)
            //        option.XAxis.CustomLabels.AddLabel(x.ToString());
            //    else
            //        option.XAxis.CustomLabels.AddLabel(" ");
            //}
            LineChart.SetOption(option);
        }
        #endregion
        private int minMouseMoveX = 60;
        private int maxMouseMoveX = 826;
        private string GetStringByIndex(int index)
        {
            var option = LineChart.Option;
            var result = $"[Pos {index + 1}] ";

            //if (option.XAxis.CustomLabels.Labels.Count > index)
            //    result += $"pos:{option.XAxis.CustomLabels.Labels[index]};";
            //else result += $"pos:-;";
            for (int i = 0; i < lineSeries.Count; i++)
            {
                var _line = lineSeries[i];
                if (_line.Visible)
                {
                    if (_line.DataCount > index)
                    {
                        result += $"{_line.Name}:{_line.GetDataPoint(index).Y.MathRound(0)};";
                    }
                    else
                    {
                        result += $"{_line.Name}:-;";
                    }
                }
            }
            return result;

        }
        private void DrawAutoLine(int point, int drawFlag = 0)
        {
            if (isRefreshRealData || isRefreshAutoLine) return;
            try
            {
                isRefreshAutoLine = true;
                var option = LineChart.Option;
                option.XAxisScaleLines.Clear();
                var _name = $"Pos {point + 1}";
                //if (option.XAxis.CustomLabels.Labels.Count > point)
                //    _name = option.XAxis.CustomLabels.Labels[point];
                if (point == 0)
                    option.XAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = _name, Value = point + 2 });
                else if (point == GlobalVar.systemSetting.sample_pointcount - 1)
                    option.XAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = _name, Value = point });
                else
                    option.XAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = _name, Value = point + 1 });
                LineChart.SetOption(option);
                var str = GetStringByIndex(point);
                uiLabel_chartmessage.Text = str;
                nowPoint = point;
                if (drawFlag == 0)
                    nowClickPointFlag = false;
                else if (drawFlag == 1)
                {
                    nowClickPointFlag = !nowClickPointFlag;
                }
                else if (drawFlag == 2)
                {
                    nowClickPointFlag = true;
                }

            }
            catch (Exception ex)
            {
                Work.frm_main.ShowErrorMessage("DrawAutoLine()::" + ex.Message);
            }
            finally
            {
                isRefreshAutoLine = false;
            }
        }
        private int GetPointByMouse(MouseEventArgs e)
        {
            if (e.X >= minMouseMoveX && e.X <= maxMouseMoveX)
            {
                double _point = (double)(e.X - minMouseMoveX) * ((double)GlobalVar.systemSetting.sample_pointcount / (double)(maxMouseMoveX - minMouseMoveX));
                int point = Convert.ToInt32(_point);
                if (point < 0) point = 0;
                else if (point >= GlobalVar.systemSetting.sample_pointcount) point = GlobalVar.systemSetting.sample_pointcount - 1;
                //  Console.WriteLine($"{e.X},{e.Y}\r\n");
                return point;
            }
            else
                return -1;
        }
        private void LineChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (nowClickPointFlag) return;
            int point = GetPointByMouse(e);
            if (point >= 0) DrawAutoLine(point, 0);
        }

        private void LineChart_MouseClick(object sender, MouseEventArgs e)
        {
            int point = GetPointByMouse(e);
            if (point >= 0) DrawAutoLine(point, 1);
        }
        private void uiButton1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            var option = LineChart.Option;
            lineSeries.ForEach(l => l.Clear());
            option.XAxis.CustomLabels.ClearLabels();
            for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
            {
                int x = 4000 + i;

                option.XAxis.CustomLabels.AddLabel(x.ToString());
                for (int j = 0; j < lineSeries.Count; j++)
                {
                    int y1;
                    y1 = 5000 + j * 100 + random.Next(1, 100);
                    lineSeries[j].Add(i, y1);
                }
            }
            option.XAxis.MinAuto = true;
            option.XAxis.MaxAuto = true;

            option.YAxis.MinAuto = true;
            option.YAxis.MaxAuto = true;

            option.Y2Axis.MinAuto = true;
            option.Y2Axis.MaxAuto = true;
            LineChart.SetOption(option);
        }
        private void EmptyChart()
        {
            var option = LineChart.Option;
            option.Title.Text = "No Data";
            lineSeries.ForEach(l => l.Clear());
            //   option.XAxis.CustomLabels.ClearLabels();
            LineChart.SetOption(option);
        }
        private List<float> GetCommonPos()
        {
            var result = new List<float>();
            for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
            {
                result.Add(i + 1);
            }
            return result;

        }

        private void IniChartVisbleAndButtons(List<bool> uschs, UILineOption option)
        {
            for (int i = 0; i < option.Series.Count - 1; i++)
            {
                var flag = GetListBoolFlag(uschs, i);
                if (!flag)
                {
                    lineSeries[i].Visible = false;
                    visiblebtns[i].ForeColor = Color.Gray;
                    visiblebtns[i].SymbolColor = Color.Gray;
                }
                else
                {
                    int colorCount = i % colors.Count;
                    lineSeries[i].Visible = true;
                    visiblebtns[i].ForeColor = Color.Black;
                    visiblebtns[i].SymbolColor = colors[colorCount];
                }
            }
        }
        private string GetPfname()
        {
            if (GlobalVar.usePf != null)
            {
                return GlobalVar.usePf.pfname ?? "";
            }
            else
                return "";
        }
        private void DrawChart(ConvertSampleTable _sampleTable)
        {
            if (isRefreshChart) return;
            try
            {
                isRefreshChart = true;
                var sampleTable = new ConvertSampleTable();
                sampleTable = (ConvertSampleTable)CommonFunction.DeepCopy(_sampleTable);
                var uschs = (sampleTable.usechanle ?? "").Split(',').Select(m => m == "True").ToList();
                var option = LineChart.Option;
                option.Title.Text = $"{ LanguageSet.SetL("chart", "peifang")}: {sampleTable.pfname}; { LanguageSet.SetL("chart", "timeId")}: {sampleTable.product_id}; {LanguageSet.SetL("chart", "yasheId")}: {sampleTable.DCM_id}";
                lineSeries.ForEach(l => l.Clear());
                //option.XAxis.CustomLabels.ClearLabels();
                //var poses = GetCommonPos();
                //for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                //{
                //    float x = 0;
                //    if (poses.Count > i)
                //        x = poses[i];
                //    if (x % 500 == 0 || x == 1)
                //        option.XAxis.CustomLabels.AddLabel(x.ToString());
                //    else
                //        option.XAxis.CustomLabels.AddLabel(" ");
                //}
                LineChart.SetOption(option);
                var pgs = new List<List<float>>();
                pgs.Add((sampleTable.pg1 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg2 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg3 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg4 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg5 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg6 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg7 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg8 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg9 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                pgs.Add((sampleTable.pg10 ?? "").Split(',').Select(m => m.StringToFloat()).ToList());
                //   var poses = (sampleTable.pos ?? "").Split(',').Select(m => m.StringToFloat()).ToList();

                var vels = (sampleTable.vel ?? "").Split(',').Select(m => m.StringToFloat()).ToList();
                for (int i = 0; i < GlobalVar.systemSetting.sample_pointcount; i++)
                {
                    //   float x = 0;
                    //  if (poses.Count > i)
                    //     x = poses[i];
                    //     if (i % 500 == 0)
                    //    option.XAxis.CustomLabels.AddLabel(x.ToString()+"-");
                    //    else
                    //       option.XAxis.CustomLabels.AddLabel("");
                    for (int j = 0; j < lineSeries.Count - 1; j++)
                    {
                        float y1 = 0;
                        if (pgs.Count > j)
                        {
                            var pg = pgs[j];
                            if (pg.Count > i)
                                y1 = pg[i];
                        }
                        lineSeries[j].Add(i, y1);
                    }
                    float y2 = 0;
                    if (vels.Count > i)
                    {
                        y2 = vels[i];
                    }
                    lineSeries[lineSeries.Count - 1].Add(i, y2);
                }
                IniChartVisbleAndButtons(uschs, option);
                LineChart.SetOption(option);
            }
            catch (Exception ex)
            {

                throw new Exception("DrawChart::" + ex.Message);
            }
            finally
            {
                isRefreshChart = false;
            }
        }
        private void RealTimeChartTask()
        {
            Task.Run(delegate
            {

                while (true)
                {
                    DateTime now = DateTime.Now;
                    try
                    {
                        var readflag = GlobalVar.commonAdsControl.Readenablereading();
                        if (readflag && !isRefreshAutoLine)
                        {
                            isRefreshRealData = true;
                            GlobalVar.plcData.sample = GlobalVar.commonAdsControl.Readst_sample(GetPfname());
                            // if (reallastSamples.Count == 0)
                            //   reallastSamples = SampleDBCommon.GetAllLastSamples();
                            var _nowSample = SampleDBCommon.NewSampleTable(GlobalVar.plcData.sample, GlobalVar.usePfDetails);
                            GlobalVar.commonAdsControl.Writeenablereading();
                            GlobalVar.nowSample = CommonFunction.DeepCopyConvertSampleTable(_nowSample);

                            reallastSamples.Insert(0, GlobalVar.nowSample);
                            if (reallastSamples.Count > 10)
                            {
                                reallastSamples = reallastSamples.GetRange(0, 10).ToList();
                            }

                            if (realChartType != 3)
                            {
                                SetRealTable1Data(GlobalVar.nowSample);
                                DrawChart(GlobalVar.nowSample);
                                SetAllHistoryCell(reallastSamples);
                            }
                         
                        }
                        if (GlobalVar.NowUiDisplay == (int)NowUi.图表 && realChartType == 2)
                        {
                            CommonTaskRead.ReadAuto();
                            ShowProcessData();
                        }


                   
                    }
                    catch (Exception ex)
                    {
                        Work.frm_main.ShowErrorMessage("RealTimeChartTask::" + ex.Message);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:RealTimeChartTask-NG:{ex.Message}\r\n");
                    }
                    finally
                    {
                        int millisecondsTimeout = 500 - ((int)DateTime.Now.Subtract(now).TotalMilliseconds);
                        if (millisecondsTimeout > 0)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                        isRefreshRealData = false;
                    }
                }
            });
        }
        public void ShowProcessData()
        {
            for (int i = 0; i < GlobalVar.systemSetting.d_chn_count; i++)
            {
                processControls_D[i].ShowData(GlobalVar.plcData.hmi_auto.led_D[i]);
            }
            for (int i = 0; i < GlobalVar.systemSetting.c_chn_count; i++)
            {
                processControls_C[i].ShowData(GlobalVar.plcData.hmi_auto.led_C[i]);
            }
            for (int i = 0; i < GlobalVar.systemSetting.e_chn_count; i++)
            {
                processControls_E[i].ShowDataE(GlobalVar.plcData.hmi_auto.led_E[i]);
            }
        }
        #region 操作表格1
        private void IniRealTable1Head()
        {
            uiDataGridViewreal.RowHeadersVisible = false;
            uiDataGridViewreal.AllowUserToAddRows = false;
            uiDataGridViewreal.AllowUserToDeleteRows = false;
            uiDataGridViewreal.AllowUserToResizeRows = false;
            uiDataGridViewreal.AllowUserToResizeColumns = false;
            uiDataGridViewreal.AllowUserToOrderColumns = false;
            uiDataGridViewreal.ReadOnly = true;
            CommonFunction.AddCol(uiDataGridViewreal, "firstcol", "", DataGridViewAutoSizeColumnMode.AllCells);
            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                CommonFunction.AddCol(uiDataGridViewreal, $"cold{i}", $"D{i}");
            }
            for (int i = 1; i <= GlobalVar.systemSetting.c_chn_count; i++)
            {
                CommonFunction.AddCol(uiDataGridViewreal, $"colc{i}", $"C{i}");
            }
            //for (int i = 1; i <= GlobalVar.systemSetting.e_chn_count; i++)
            //{
            //    CommonFunction.AddCol(uiDataGridViewreal, $"cole{i}", $"E{i}");
            //}
        }
        private void IniRealTableRows()
        {
            this.uiDataGridViewreal.Rows.Clear();
            #region 第一列
            this.uiDataGridViewreal.Rows.Add(14);
            this.uiDataGridViewreal.Rows[0].Height = 42;
            for (int i = 0; i < 14; i++)
            {
                this.uiDataGridViewreal.Rows[i].Cells[0].Value = LanguageSet.SetL("chart", $"tableRow{i + 1}");
            }
            #endregion
        }
        private string GetFaLeixing(bool select_paiqi, bool select_yeya, bool select_jixie)
        {
            if (select_yeya) return LanguageSet.SetL("chart", "yeya");
            if (select_paiqi) return LanguageSet.SetL("chart", "paiqi");
            if (select_jixie) return LanguageSet.SetL("chart", "jixie");
            else return "-";
        }
        private bool GetListBoolFlag(List<bool> bools, int index)
        {
            if (bools.Count <= index) return false;
            else return bools[index];
        }
        private string GetListFloatFlag(List<float?> floats, int index)
        {
            if (floats.Count <= index) return "";
            else
            {
                //if (floats[index] == null)
                //    return "";
                //else
                return floats[index].MathRound();
            }
        }
        private void SetCellDisable(int roworder, int colorder)
        {
            this.uiDataGridViewreal.Rows[roworder].Cells[colorder].Value = "";
            this.uiDataGridViewreal.Rows[roworder].Cells[colorder].Style.BackColor = Color.LightGray;
        }
        private void SetCellEmpty(int roworder, int colorder)
        {
            this.uiDataGridViewreal.Rows[roworder].Cells[colorder].Value = "";
            this.uiDataGridViewreal.Rows[roworder].Cells[colorder].Style.BackColor = Color.White;
        }
        private void SetAllRealCellDisable(int colorder)
        {
            for (int j = 0; j <= 13; j++)
            {
                SetCellDisable(j, colorder);
            }
        }
        private void SetAllRealCellEmpty(int colorder)
        {
            for (int j = 0; j <= 13; j++)
            {
                SetCellEmpty(j, colorder);
            }
        }
        private void SetRealTable1Data(ConvertSampleTable _sampleTable)
        {
            try
            {
                var sampleTable = new ConvertSampleTable();
                sampleTable = (ConvertSampleTable)CommonFunction.DeepCopy(_sampleTable);
                ClearRealTable1Data();
                var uschs = (sampleTable.usechanle ?? "").Split(',').Select(m => m == "True").ToList();
                var select_paiqis = (sampleTable.select_paiqi ?? "").Split(',').Select(m => m == "True").ToList();
                var select_yeyas = (sampleTable.select_yeya ?? "").Split(',').Select(m => m == "True").ToList();
                var select_jixies = (sampleTable.select_jixie ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance1_hmienables = (sampleTable.duoduanjiance1_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance2_hmienables = (sampleTable.duoduanjiance2_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance3_hmienables = (sampleTable.duoduanjiance3_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance4_hmienables = (sampleTable.duoduanjiance4_hmienable ?? "").Split(',').Select(m => m == "True").ToList();
                var duoduanjiance5_hmienables = (sampleTable.duoduanjiance5_hmienable ?? "").Split(',').Select(m => m == "True").ToList();


                var startpoints = (sampleTable.startpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var endpoints = (sampleTable.endpoint ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var chuisaoyalis = (sampleTable.chuisaoyali ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance1s = (sampleTable.duoduanjiance1 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance2s = (sampleTable.duoduanjiance2 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance3s = (sampleTable.duoduanjiance3 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance4s = (sampleTable.duoduanjiance4 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var duoduanjiance5s = (sampleTable.duoduanjiance5 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var tongfengs = (sampleTable.tongfeng ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var fanyings = (sampleTable.fanying ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var fengbis = (sampleTable.fengbi ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var start_yeya_PGs = (sampleTable.start_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var stop_yeya_PGs = (sampleTable.stop_yeya_PG ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var start_PG_tanks = (sampleTable.start_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var end_PG_tanks = (sampleTable.end_PG_tank ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                var total_zongchouqi = sampleTable.zongchouqi != null ? sampleTable.zongchouqi.ToString():"";
                var total_CA_PG = sampleTable.CA_PG != null ? sampleTable.CA_PG.ToString() : "";
                //var count = 0;
                int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
                var bools = new bool[totalCount];
                for (int i = 0; i < totalCount; i++)
                {
                    if (GetListBoolFlag(uschs, i))
                    {
                        this.uiDataGridViewreal.Rows[0].Cells[i + 1].Value = GetFaLeixing(GetListBoolFlag(select_paiqis, i), GetListBoolFlag(select_yeyas, i), GetListBoolFlag(select_jixies, i));
                        this.uiDataGridViewreal.Rows[1].Cells[i + 1].Value = GetListFloatFlag(startpoints, i);
                        this.uiDataGridViewreal.Rows[2].Cells[i + 1].Value = GetListFloatFlag(endpoints, i);
                        this.uiDataGridViewreal.Rows[3].Cells[i + 1].Value = GetListFloatFlag(chuisaoyalis, i);
                        if (GetListBoolFlag(duoduanjiance1_hmienables, i))
                            this.uiDataGridViewreal.Rows[4].Cells[i + 1].Value = GetListFloatFlag(duoduanjiance1s, i);
                        else
                            SetCellDisable(4, i + 1);

                        if (GetListBoolFlag(duoduanjiance2_hmienables, i))
                            this.uiDataGridViewreal.Rows[5].Cells[i + 1].Value = GetListFloatFlag(duoduanjiance2s, i);
                        else
                            SetCellDisable(5, i + 1);

                        if (GetListBoolFlag(duoduanjiance3_hmienables, i))
                            this.uiDataGridViewreal.Rows[6].Cells[i + 1].Value = GetListFloatFlag(duoduanjiance3s, i);
                        else
                            SetCellDisable(6, i + 1);

                        if (GetListBoolFlag(duoduanjiance4_hmienables, i))
                            this.uiDataGridViewreal.Rows[7].Cells[i + 1].Value = GetListFloatFlag(duoduanjiance4s, i);
                        else
                            SetCellDisable(7, i + 1);

                        if (GetListBoolFlag(duoduanjiance5_hmienables, i))
                            this.uiDataGridViewreal.Rows[8].Cells[i + 1].Value = GetListFloatFlag(duoduanjiance5s, i);
                        else
                            SetCellDisable(8, i + 1);
                        this.uiDataGridViewreal.Rows[9].Cells[i + 1].Value = GetListFloatFlag(tongfengs, i);

                        if (GetListBoolFlag(select_yeyas, i))
                        {
                            this.uiDataGridViewreal.Rows[10].Cells[i + 1].Value = GetListFloatFlag(fanyings, i);
                            this.uiDataGridViewreal.Rows[11].Cells[i + 1].Value = GetListFloatFlag(fengbis, i);
                            this.uiDataGridViewreal.Rows[12].Cells[i + 1].Value = GetListFloatFlag(start_yeya_PGs, i);
                            this.uiDataGridViewreal.Rows[13].Cells[i + 1].Value = GetListFloatFlag(stop_yeya_PGs, i);
                        }
                        else
                        {
                            for (int j = 10; j <= 13; j++)
                            {
                                SetCellDisable(j, i + 1);
                            }
                        }
                    }
                    else
                    {
                        SetAllRealCellDisable(i + 1);
                    }
                }

                total_label_value_1.Text = total_CA_PG;
                total_label_value_2.Text = total_zongchouqi;
                total_label_value_3.Text = GetListFloatFlag(start_PG_tanks, 0);
                total_label_value_5.Text = GetListFloatFlag(end_PG_tanks, 0);
                if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
                {
                    total_label_value_4.Text = GetListFloatFlag(start_PG_tanks, 1);
                    total_label_value_6.Text = GetListFloatFlag(end_PG_tanks, 1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetRealTable1Data::" + ex.Message);
            }
        }
        private void ClearRealTable1Data()
        {

            int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
            for (int i = 0; i < totalCount; i++)
            {
                SetAllRealCellEmpty(i + 1);
            }
            total_label_value_1.Text = "";
            total_label_value_2.Text = "";
            total_label_value_3.Text = "";
            total_label_value_5.Text = "";
            if (GlobalVar.systemSetting.machine_config.ToUpper() == "SP9001")
            {
                total_label_value_4.Text = "";
                total_label_value_6.Text = "";
            }
        }
        #endregion
        #region 操作表格2
        private void IniHistoryTable1Head()
        {
            uiDataGridViewhistory.RowHeadersVisible = false;
            //     uiDataGridViewhistory.ColumnHeadersVisible = false;
            uiDataGridViewhistory.AllowUserToAddRows = false;
            uiDataGridViewhistory.AllowUserToDeleteRows = false;
            uiDataGridViewhistory.AllowUserToResizeRows = false;
            uiDataGridViewhistory.AllowUserToResizeColumns = false;
            uiDataGridViewhistory.ReadOnly = true;

            CommonFunction.AddCol(uiDataGridViewhistory, "yscol", LanguageSet.SetL("chart", "yasheId"), DataGridViewAutoSizeColumnMode.NotSet/*, DataGridViewAutoSizeColumnMode.AllCells*/);
            CommonFunction.AddCol(uiDataGridViewhistory, "timeidcol", LanguageSet.SetL("chart", "timeId"), DataGridViewAutoSizeColumnMode.NotSet/*, DataGridViewAutoSizeColumnMode.AllCells*/);

            for (int i = 1; i <= GlobalVar.systemSetting.d_chn_count; i++)
            {
                CommonFunction.AddCol(uiDataGridViewhistory, $"cold{i}_zk", $"D{i}\r\n" + LanguageSet.SetL("chart", "pg"), DataGridViewAutoSizeColumnMode.NotSet, 60/*, DataGridViewAutoSizeColumnMode.AllCells*/);
                CommonFunction.AddCol(uiDataGridViewhistory, $"cold{i}_cs", $"D{i}\r\n" + LanguageSet.SetL("chart", "chuisao"), DataGridViewAutoSizeColumnMode.NotSet, 60/*, DataGridViewAutoSizeColumnMode.AllCells*/);
            }
            for (int i = 1; i <= GlobalVar.systemSetting.c_chn_count; i++)
            {
                CommonFunction.AddCol(uiDataGridViewhistory, $"colc{i}_zk", $"C{i}\r\n" + LanguageSet.SetL("chart", "pg"), DataGridViewAutoSizeColumnMode.NotSet, 60/*, DataGridViewAutoSizeColumnMode.AllCells*/);
                CommonFunction.AddCol(uiDataGridViewhistory, $"colc{i}_cs", $"C{i}\r\n" + LanguageSet.SetL("chart", "chuisao"), DataGridViewAutoSizeColumnMode.NotSet, 60/*, DataGridViewAutoSizeColumnMode.AllCells*/);
            }

            uiDataGridViewhistory.Columns[0].Frozen = true;
            uiDataGridViewhistory.Columns[1].Frozen = true;
            uiDataGridViewhistory.AutoResizeColumnHeadersHeight();
            this.uiDataGridViewhistory.Rows.Add(10);
        }
        private void SetHistoryCellDisable(int roworder, int colorder)
        {
            this.uiDataGridViewhistory.Rows[roworder].Cells[colorder].Value = "";
            this.uiDataGridViewhistory.Rows[roworder].Cells[colorder].Style.BackColor = Color.LightGray;
        }
        private void SetAllHistoryCellEmpty()
        {
            //uiDataGridViewhistory.Rows.Clear();
            //return;
            for (int i = 0; i < uiDataGridViewhistory.ColumnCount; i++)
            {
                for (int j = 0; j < uiDataGridViewhistory.RowCount; j++)
                {
                    this.uiDataGridViewhistory.Rows[j].Cells[i].Value = "";
                    this.uiDataGridViewhistory.Rows[j].Cells[i].Style.BackColor = Color.White;
                }
            }
        }
        private void SetAllHistoryCell(List<ConvertSampleTable> _sampleTables)
        {
            //  return;
            if (isRefreshHistoryTable)
                return;
            try
            {
                var sampleTables = new List<ConvertSampleTable>();
                _sampleTables.ForEach(s => sampleTables.Add((ConvertSampleTable)CommonFunction.DeepCopy(s)));
                isRefreshHistoryTable = true;
                SetAllHistoryCellEmpty();
                //  Thread.Sleep(100);
                // return;
                //  if (sampleTables.Count!=10)
                //   {
                //    MessageBox.Show("!=10");
                //}
                //this.uiDataGridViewhistory.Rows.Add(sampleTables.Count);
                for (int k = 0; k < sampleTables.Count; k++)
                {
                    if (k >= this.uiDataGridViewhistory.Rows.Count) continue;
                    var sampleTable = sampleTables[k];
                    var uschs = (sampleTable.usechanle ?? "").Split(',').Select(m => m == "True").ToList();
                    var chuisaoyalis = (sampleTable.chuisaoyali ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var tongfengs = (sampleTable.tongfeng ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    var duoduanjiance1 = (sampleTable.duoduanjiance1 ?? "").Split(',').Select(m => m.StringToFloatAllowNull()).ToList();
                    int totalCount = GlobalVar.systemSetting.d_chn_count + GlobalVar.systemSetting.c_chn_count;
                    this.uiDataGridViewhistory.Rows[k].Tag = sampleTable.id;
                    this.uiDataGridViewhistory.Rows[k].Cells[0].Value = sampleTable.DCM_id;
                    this.uiDataGridViewhistory.Rows[k].Cells[1].Value = sampleTable.product_id;
                    for (int i = 0; i < totalCount; i++)
                    {
                        if (GetListBoolFlag(uschs, i))
                        {
                            this.uiDataGridViewhistory.Rows[k].Cells[2 + 2 * i].Value = GetListFloatFlag(duoduanjiance1, i);
                            this.uiDataGridViewhistory.Rows[k].Cells[2 + 2 * i + 1].Value = GetListFloatFlag(chuisaoyalis, i);
                        }
                        else
                        {
                            SetHistoryCellDisable(k, 2 + 2 * i);
                            SetHistoryCellDisable(k, 2 + 2 * i + 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetAllHistoryCell::" + ex.Message);
            }
            finally
            {
                isRefreshHistoryTable = false;
            }
        }
        #endregion

        private void uiSymbolButton_left_Click(object sender, EventArgs e)
        {
            int point = nowPoint - 1;
            if (point < 0) point = 0;
            DrawAutoLine(point, 2);
        }

        private void uiSymbolButton_right_Click(object sender, EventArgs e)
        {
            int point = nowPoint + 1;
            if (point > GlobalVar.systemSetting.sample_pointcount - 1) point = GlobalVar.systemSetting.sample_pointcount - 1;
            DrawAutoLine(point, 2);
        }
        private void ShowReal()
        {
            if (GlobalVar.nowSample != null)
            {
                DrawChart(GlobalVar.nowSample);
                SetRealTable1Data(GlobalVar.nowSample);
                SetAllHistoryCell(reallastSamples);
            }
            else
            {
                EmptyChart();
                ClearRealTable1Data();
            }
        }
        private void ShowHistory()
        {
            if (searchSample != null)
            {
                DrawChart(searchSample);
                SetRealTable1Data(searchSample);
                SetAllHistoryCell(lastSamples);
            }
            else
            {
                EmptyChart();
                ClearRealTable1Data();
                SetAllHistoryCellEmpty();
            }
        }
        public void ShowHideView()
        {
            if (realChartType == 1)
            {
                uiGroupBox_search.Visible = false;
                uiDataGridViewhistory.Visible = true;
                uiDataGridViewreal.Visible = true;
                uiButton_realtime.FillColor = Color.IndianRed;
                uiButton_realtime2.FillColor = Color.DarkGray;
                uiButton_history.FillColor = Color.DarkGray;
                uiFlowLayoutPanel1.Visible = false;
                uiPanelTotal.Visible = true;
                ShowReal();
            }
            else if (realChartType == 3)
            {
                uiGroupBox_search.Visible = true;
                uiDataGridViewhistory.Visible = true;
                uiDataGridViewreal.Visible = true;
                uiButton_realtime.FillColor = Color.DarkGray;
                uiButton_realtime2.FillColor = Color.DarkGray;
                uiButton_history.FillColor = Color.IndianRed;
                uiFlowLayoutPanel1.Visible = false;
                uiPanelTotal.Visible = true;
                ShowHistory();
            }
            else
            {
                uiGroupBox_search.Visible = false;
                uiFlowLayoutPanel1.Visible = true;
                uiPanelTotal.Visible = false;
                uiDataGridViewhistory.Visible = false;
                uiDataGridViewreal.Visible = false;
                uiButton_realtime.FillColor = Color.DarkGray;
                uiButton_realtime2.FillColor = Color.IndianRed;
                uiButton_history.FillColor = Color.DarkGray;
                ShowReal();
            }
            DrawAutoLine(nowPoint, 3);
        }
        private void uiButton_realtime_Click(object sender, EventArgs e)
        {
            realChartType = 1;
            ShowHideView();
        }

        private void uiButton_history_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }
            realChartType = 3;
            ShowHideView();
        }
        #region Search
        private void IniSearchBox()
        {
            DateTime now = DateTime.Now.AddDays(1); // 获取当前日期和时间  
            DateTime endDateToday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime startDateToday = endDateToday.AddDays(-7);
            uiDatetimePicker_start.Text = startDateToday.ToString("yyyy-MM-dd HH:mm:ss");
            uiDatetimePicker_stop.Text = endDateToday.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void btn_clearstartdate_Click(object sender, EventArgs e)
        {
            uiDatetimePicker_start.Text = "";

        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            uiDatetimePicker_stop.Text = "";
        }

        #endregion

        private async void uiSymbolButton_search_Click(object sender, EventArgs e)
        {
            if (!CommonFunction.CheckAccessGongyi())
            {
                Work.ShowErrorMessage(LanguageSet.SetL("Others", "NotAccsess"));
                return;
            }

            if (isUpdating) return;
            isUpdating = true;
            try
            {
                var s_dateTimeStart = uiDatetimePicker_start.Text.Trim().StringToDateTimeAllowNull();
                var s_dateTimeEnd = uiDatetimePicker_stop.Text.Trim().StringToDateTimeAllowNull();
                if (s_dateTimeStart != null && s_dateTimeEnd != null)
                {
                    if (s_dateTimeEnd <= s_dateTimeStart)
                        throw new Exception(LanguageSet.SetL("chart", "checksearch"));
                }
                var s_pfname = uiTextBox_pf.Text.Trim();
                var s_ys = uiTextBox_yashe.Text.Trim();
                if (s_pfname == "" && s_ys == "" && s_dateTimeStart == null && s_dateTimeEnd == null)
                    throw new Exception(LanguageSet.SetL("chart", "checksearch2"));
                var searchs = await SampleDBCommon.SearchSampleTable(s_dateTimeStart, s_dateTimeEnd, s_pfname, s_ys);
                uiComboBox_result.Clear();
                uiComboBox_result.DataSource = searchs;
                uiComboBox_result.ValueMember = "id";
                uiComboBox_result.DisplayMember = "product_id";
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage("uiSymbolButton_search_Click()" + ex.Message);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void uiComboBox_result_SelectedValueChanged(object sender, EventArgs e)
        {
            var result = uiComboBox_result.SelectedValue;


            if (result is int)
            {
                var model = SampleDBCommon.GetSampleTableById((int)result);
                searchSample = CommonFunction.DeepCopyConvertSampleTable(model.sample);
                lastSamples = CommonFunction.DeepCopyConvertSampleTables(model.last_samples);

            }

            else
            {
                searchSample = null;
                lastSamples = new List<ConvertSampleTable>();
            }

            ShowHistory();
        }

        private void uiButton_realtime2_Click(object sender, EventArgs e)
        {
            realChartType = 2;
            ShowHideView();
        }

        private void uiDataGridViewhistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = uiDataGridViewhistory.Rows[e.RowIndex];
                var id = row.Tag as int?; // 假设你的ID是int类型，使用as进行安全转换  
                if (id.HasValue)
                {
                    var model = SampleDBCommon.GetSampleTableById((int)id, false);
                    DrawChart(CommonFunction.DeepCopyConvertSampleTable(model.sample));
                }


            }
        }

        private void uiSymbolButton_search_previous_Click(object sender, EventArgs e)
        {
            if (uiComboBox_result.Items.Count > 0)
            {
                int currentIndex = uiComboBox_result.SelectedIndex;
                if (currentIndex > 0)
                {
                    uiComboBox_result.SelectedIndex = currentIndex - 1;
                }
            }
        }

        private void uiSymbolButton_search_next_Click(object sender, EventArgs e)
        {
            if (uiComboBox_result.Items.Count > 0)
            {
                int currentIndex = uiComboBox_result.SelectedIndex;

                if (currentIndex < uiComboBox_result.Items.Count - 1)
                {
                    uiComboBox_result.SelectedIndex = currentIndex + 1;
                }
            }
        }


    }


}
