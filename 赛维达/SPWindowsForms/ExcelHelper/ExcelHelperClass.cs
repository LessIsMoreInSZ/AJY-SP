using ClosedXML.Excel;
using EF.Models.EF.Entities;
using SPWindowsForms.DbService;
using SPWindowsForms.SwitchForms;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.ExcelHelper
{
    public class ExcelHelperClass
    {
        public XLWorkbook workbook;
        //private List<PfMainTable> pfMainTables;
        public ExcelHelperClass(/*List<PfMainTable> _pfMainTables*/)
        {
            workbook = new XLWorkbook();
            //pfMainTables = _pfMainTables;
        }
        #region PF Table
        public XLWorkbook WriteAllpfMainTables(List<PfMainTable> pfMainTables)
        {
            pfMainTables.ForEach(p => WritePfTable(p));
            return workbook;
        }
        public void WritePfTable(PfMainTable pfMain)
        {
            try
            {
                var worksheet = workbook.AddWorksheet(pfMain.pfname);
                worksheet.Column(1).Width = 30;
                var _details = PFDetailDBCommon.GetPfDetailTable(pfMain.id);
                var _details_D = _details.Where(m => m.chnType == "D").OrderBy(m => m.chnorder).ToList();
                var _details_C = _details.Where(m => m.chnType == "C").OrderBy(m => m.chnorder).ToList();
                var _details_E = _details.Where(m => m.chnType == "E").OrderBy(m => m.chnorder).ToList();
                int startRow = 1;
                if (_details_D.Count > 0)
                {
                    startRow = SetSystemRowsD(worksheet, startRow, _details_D);
                    _details_D.ForEach(d =>
                    {
                        startRow = NewAllAlarms(worksheet, GlobalVar.D_PfPageConfigModel, d, startRow, "D");
                    });
                }
                if (_details_C.Count > 0)
                {
                    startRow = SetSystemRowsC(worksheet, startRow, _details_C);
                    _details_C.ForEach(d =>
                    {
                        startRow = NewAllAlarms(worksheet, GlobalVar.C_PfPageConfigModel, d, startRow, "C");
                    });
                }
                if (_details_E.Count > 0)
                {
                    startRow = SetSystemRowsE(worksheet, startRow, _details_E);
                    _details_E.ForEach(d =>
                    {
                        startRow = NewAllAlarms(worksheet, GlobalVar.E_PfPageConfigModel, d, startRow, "E");
                    });
                }

            }
            catch (Exception ex)
            {
                throw new Exception("WritePfTable::" + ex.Message);
            }
        }
        private string GetAlarmInput(string topType, int order, int colOrder, PfDetailTable _edit)
        {
            try
            {
                if (_edit == null) return "0";
                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.Opentime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Opentime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 2:
                                if (colOrder == 2)
                                    return (_edit.Closetime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Closetime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 3:
                                if (colOrder == 2)
                                    return (_edit.Vactime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Vactime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 4:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 5:
                                if (colOrder == 2)
                                    return (_edit.Close_pos_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Close_pos_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 6:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos ?? 0).ToString();
                                else
                                    return "0";
                            case 7:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi2 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo2 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos2 ?? 0).ToString();
                                else
                                    return "0";
                            case 8:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi3 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo3 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos3 ?? 0).ToString();
                                else
                                    return "0";
                            case 9:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi4 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo4 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos4 ?? 0).ToString();
                                else
                                    return "0";
                            case 10:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi5 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo5 ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.P_vac_pos5 ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }

                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.Vactime_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.Vactime_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 2:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 3:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo ?? 0).ToString();
                                else
                                    return "0";
                            case 4:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi2 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo2 ?? 0).ToString();
                                else
                                    return "0";
                            case 5:
                                if (colOrder == 2)
                                    return (_edit.P_blow_hi3 ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_blow_lo3 ?? 0).ToString();
                                else
                                    return "0";
                            case 6:
                                if (colOrder == 2)
                                    return (_edit.set_CheckPointHi_C ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.set_CheckPointLo_C ?? 0).ToString();
                                else if (colOrder == 4)
                                    return (_edit.set_CheckPoint_C ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                if (colOrder == 2)
                                    return (_edit.P_vac_hi ?? 0).ToString();
                                else if (colOrder == 3)
                                    return (_edit.P_vac_lo ?? 0).ToString();
                                else
                                    return "0";
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return "0";
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return "0";
            }
        }
        private bool GetAlarmCheckBox(string topType, int order, PfDetailTable _edit)
        {
            try
            {
                if (_edit == null) return false;

                switch (topType)
                {
                    case "D":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_Opentime ?? false;
                            case 2:
                                return _edit.enable_Closetime ?? false;
                            case 3:
                                return _edit.enable_Vactime ?? false;
                            case 4:
                                return _edit.enable_P_blow ?? false;
                            case 5:
                                return _edit.enable_Close_pos ?? false;
                            case 6:
                                return _edit.enable_P_vac ?? false;
                            case 7:
                                return _edit.enable_P_vac2 ?? false;
                            case 8:
                                return _edit.enable_P_vac3 ?? false;
                            case 9:
                                return _edit.enable_P_vac4 ?? false;
                            case 10:
                                return _edit.enable_P_vac5 ?? false;
                            default:
                                break;
                        }
                        break;
                    case "C":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_Vactime ?? false;
                            case 2:
                                return _edit.enable_P_vac ?? false;
                            case 3:
                                return _edit.enable_P_blow ?? false;
                            case 4:
                                return _edit.enable_P_blow2 ?? false;
                            case 5:
                                return _edit.enable_P_blow3 ?? false;
                            case 6:
                                return _edit.use_checkPoint_C ?? false;
                            default:
                                break;
                        }
                        break;
                    case "E":
                        switch (order)
                        {
                            case 1:
                                return _edit.enable_P_vac ?? false;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage(ex.Message);
                return false;
            }
        }
        private int NewAllAlarms(IXLWorksheet worksheet, PfPageConfigModel model, PfDetailTable pfDetail, int row, string nowTopType)
        {
            int _row = row;
            SetCellValue(worksheet, _row, 1, $"[{pfDetail.chnName}] Alarm", XLColor.LightPink);
            SetCellValue(worksheet, _row + 1, 1, "", XLColor.LightYellow);
            SetCellValue(worksheet, _row + 1, 2, LanguageSet.SetL("pf", "uiLabel_alarmtop1"), XLColor.LightYellow);
            SetCellValue(worksheet, _row + 1, 3, LanguageSet.SetL("pf", "uiLabel_alarmtop3"), XLColor.LightYellow);
            SetCellValue(worksheet, _row + 1, 4, LanguageSet.SetL("pf", "uiLabel_alarmtop4"), XLColor.LightYellow);
            SetCellValue(worksheet, _row + 1, 5, LanguageSet.SetL("pf", "uiLabel_alarmtop5"), XLColor.LightYellow);
            for (int i = 0; i < model.alarmModel.alarms.Count; i++)
            {
                var a = model.alarmModel.alarms[i];
                SetCellValue(worksheet, _row + 2 + i, 1, LanguageSet.SetL("pf", a.parName), XLColor.LightGray);
                var _checked = GetAlarmCheckBox(nowTopType, i + 1, pfDetail);
                SetCellValue(worksheet, _row + 2 + i, 2, _checked == true ? "Yes" : "No");
                for (int j = 0; j < a.numUse.Count; j++)
                {
                    var _use = a.numUse[j];
                    if (_use)
                    {
                        var _text = GetAlarmInput(nowTopType, i + 1, j + 2, pfDetail);
                        SetCellValue(worksheet, _row + 2 + i, 3 + j, _text);
                    }
                }

            }

            return _row + 1 + model.alarmModel.alarms.Count + 2;
        }

        public int SetSystemRowsD(IXLWorksheet worksheet, int startRow, List<PfDetailTable> pfDetails)
        {
            NewAllSysLabels(worksheet, GlobalVar.D_PfPageConfigModel, startRow + 1);

            int col = 2;
            pfDetails.ForEach(d =>
            {

                SetCellValue(worksheet, startRow, col, d.chnName, XLColor.LightBlue);
                SetCellValue(worksheet, startRow + 1, col, d.use_ch == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 2, col, d.select_paiqi == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 3, col, d.select_yeya == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 4, col, d.select_jixie == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 5, col, d.use_chuisao_M == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 6, col, d.select_gauging_M == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 7, col, d.select_auto_S == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 8, col, d.use_VAC_time == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 9, col, (d.auto_startpoint ?? 0).ToString());
                SetCellValue(worksheet, startRow + 10, col, (d.auto_endpoint ?? 0).ToString());
                SetCellValue(worksheet, startRow + 11, col, (d.filter_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 12, col, (d.Blow_Delay_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 13, col, (d.Blow_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 14, col, (d.VAC_time ?? 0).ToString());
                col++;
            });
            return startRow + 1 + GlobalVar.D_PfPageConfigModel.systemModel.checks.Count + GlobalVar.D_PfPageConfigModel.systemModel.inputs.Count + 1;
        }
        public int SetSystemRowsC(IXLWorksheet worksheet, int startRow, List<PfDetailTable> pfDetails)
        {
            NewAllSysLabels(worksheet, GlobalVar.C_PfPageConfigModel, startRow + 1);
            int col = 2;
            pfDetails.ForEach(d =>
            {
                SetCellValue(worksheet, startRow, col, d.chnName, XLColor.LightBlue);
                SetCellValue(worksheet, startRow + 1, col, d.use_ch == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 2, col, d.use_blow == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 3, col, d.use_VAC_time == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 4, col, d.enable_zu1 == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 5, col, d.enable_zu2 == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 6, col, d.enable_zu3 == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 7, col, (d.auto_startpoint ?? 0).ToString());
                SetCellValue(worksheet, startRow + 8, col, (d.auto_startpoint2 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 9, col, (d.auto_startpoint3 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 10, col, (d.auto_endpoint ?? 0).ToString());
                SetCellValue(worksheet, startRow + 11, col, (d.auto_endpoint2 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 12, col, (d.auto_endpoint3 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 13, col, (d.VAC_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 14, col, (d.VAC_time2 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 15, col, (d.VAC_time3 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 16, col, (d.Blow_Delay_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 17, col, (d.Blow_INR_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 18, col, (d.Blow_time ?? 0).ToString());
                SetCellValue(worksheet, startRow + 19, col, (d.Blow_time2 ?? 0).ToString());
                SetCellValue(worksheet, startRow + 20, col, (d.Blow_time3 ?? 0).ToString());
                col++;
            });
            return startRow + 1 + GlobalVar.C_PfPageConfigModel.systemModel.checks.Count + GlobalVar.C_PfPageConfigModel.systemModel.inputs.Count + 1;
        }
        public int SetSystemRowsE(IXLWorksheet worksheet, int startRow, List<PfDetailTable> pfDetails)
        {
            NewAllSysLabels(worksheet, GlobalVar.E_PfPageConfigModel, startRow + 1);
            int col = 2;
            pfDetails.ForEach(d =>
            {
                SetCellValue(worksheet, startRow, col, d.chnName, XLColor.LightBlue);
                SetCellValue(worksheet, startRow + 1, col, d.use_ch == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 2, col, d.use_VAC_hemu == true ? "Yes" : "No");
                SetCellValue(worksheet, startRow + 3, col, d.use_VAC_stop == true ? "Yes" : "No");
                col++;
            });
            return startRow + 1 + GlobalVar.E_PfPageConfigModel.systemModel.checks.Count + GlobalVar.E_PfPageConfigModel.systemModel.inputs.Count + 1;
        }
        private void NewAllSysLabels(IXLWorksheet worksheet, PfPageConfigModel model, int row)
        {
            int _row = row;
            SetCellValue(worksheet, _row - 1, 1, "", XLColor.LightBlue);
            for (int i = 1; i <= model.systemModel.checks.Count; i++)
            {
                var c = model.systemModel.checks[i - 1];
                var lblName = LanguageSet.SetL("pf", c.parName);
                SetCellValue(worksheet, _row, 1, lblName, XLColor.LightGray);
                _row++;
            }
            for (int i = 1; i <= model.systemModel.inputs.Count; i++)
            {
                var c = model.systemModel.inputs[i - 1];
                var lblName = LanguageSet.SetL("pf", c.parName);
                SetCellValue(worksheet, _row, 1, lblName, XLColor.LightGray);
                _row++;
            }

        }
        #endregion

        #region Write GridView Excel
        public XLWorkbook WriteGridViewExcel(UIDataGridView gridView, string sheetName = "Sheet1")
        {
            WriteByGridView(gridView, sheetName);
            return workbook;
        }
        private void WriteByGridView(UIDataGridView gridView, string sheetName = "Sheet1")
        {
            try
            {
                var worksheet = workbook.AddWorksheet(sheetName);
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    var c = gridView.Columns[i];
                    SetCellValue(worksheet, 1, i + 1, c.HeaderText, XLColor.LightBlue);
                }
                int startrow = 2;
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    var r = gridView.Rows[i];
                    for (int j = 0; j < r.Cells.Count; j++)
                    {
                        var cell = r.Cells[j];
                        SetCellValue(worksheet, startrow, j + 1, (cell.Value??"").ToString());
                    }
                    startrow++;
                }
            }
            catch (Exception ex)
            {
                Work.ShowErrorMessage("WriteGridViewExcel()::" + ex.Message);
            }
        }
        #endregion

        public void SetCellValue(IXLWorksheet worksheet, int row, int col, string value, XLColor backcolor = null)
        {
            var cell = CommonFunction.GetExcelCell(row, col);
            worksheet.Cell(cell).Value = value;
            if (backcolor != null)
                worksheet.Cell(cell).Style.Fill.BackgroundColor = backcolor;
        }
    }
}
