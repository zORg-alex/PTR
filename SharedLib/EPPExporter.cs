using OfficeOpenXml;
using OfficeOpenXml.Style;
using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public class EPPExporter {
		public ExcelPackage Ep = new ExcelPackage();

		public ExcelWorksheet ExportIFunction(IFunction iFunction) {
			Ep.Workbook.Worksheets.Add(iFunction.NameTrimmed);
			ExcelWorksheet ws = Ep.Workbook.Worksheets.LastOrDefault();
			ws.Name = iFunction.NameTrimmed; //Setting Sheet's name
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

			ws.Column(1).Width = 2.5f;
			ws.Column(2).Width = 15f;
			ws.Column(3).Width = 70f;

			ws.Cells[2, 2].Value = iFunction.NameTrimmed;

			int lines = 0;
			foreach (var ifa in iFunction.IFunctionAccesses) {
				lines++;
				ws.Cells[2 + lines, 2].Value = ifa.ISDAVSUser.TrimmedLogin;
				ws.Cells[2 + lines, 3].Value = ifa.ISDAVSUser.FullName;
				if (ifa.To != null) ws.Cells[2 + lines, 2, 2 + lines, 3].Style.Font.Color.SetColor(Color.DarkGray);
				ws.Cells[2 + lines, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
				ws.Cells[2 + lines, 2].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
				ws.Cells[2 + lines, 2].Style.Border.Bottom.Style = ws.Cells[2 + lines, 2].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
				foreach (var qa in ifa.IQuestAccesses) {
					lines++;
					ws.Cells[2 + lines, 3].Value = qa.IQuest.Name;
				}
			}

			DrawTableInCells(ws.Cells[2, 2, 2 + lines, 3]);

			return ws;
		}

		public ExcelWorksheet ExportADFolder(ADFolder adFolder, bool Expand, bool AllowHistoricalData) {
			return ExportADFolder(adFolder, Expand, AllowHistoricalData, null);
		}

		public ExcelWorksheet ExportADFolder(ADFolder adFolder, bool Expand, bool AllowHistoricalData, ExcelRange LinkBack) {
			try {
				Ep.Workbook.Worksheets.Add(adFolder.Name);
			} catch {
				Ep.Workbook.Worksheets.Add(Guid.NewGuid().ToString());
			}
			ExcelWorksheet ws = Ep.Workbook.Worksheets.LastOrDefault();
			//ws.Name = adFolder.FullPath; //Setting Sheet's name
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

			//ws.Column(1).Width = 2.5f;
			//ws.Column(2).Width = 20f;
			//ws.Column(3).Width = 40f;
			SetColumnWidths(ws, new float[] { 2.5f, 20f, 40f });
			int lines = 2;

			if (LinkBack != null) AddHyperLink(ws.Cells[1, 2], LinkBack, "Back");

			ws.Cells[lines, 2].Value = adFolder.FullPath;
			DrawTableInCells(ws.Cells[2, 2, 2, 3]);

			lines++;
			int foldersLength = -1;
			if (Expand) {
				lines++;
				ws.Cells[lines, 2].Value = "Sub Folders";
				foldersLength = WriteColumn(ws, lines + 1, (f, wss, line) => {
					if (f.Status || AllowHistoricalData) {
						wss.Cells[line, 2].Value = f.Name;
						if (Expand) {
							AddHyperLink(wss.Cells[line, 2], ExportADFolder(f, Expand, AllowHistoricalData, wss.Cells[line, 2]).Cells[2, 2], f.Name);
						}
						if (!f.Status) wss.Cells[line, 2].Style.Font.Color.SetColor(Color.DarkGray);
					}
				}, adFolder.SubFolders);
				DrawTableInCells(ws.Cells[lines, 2, lines + foldersLength + 1, 3]);
				lines += 1 + foldersLength;
			}

			//foreach (var fa in adFolder.ADFolderAccesses) {
			//	lines++;
			//	ws.Cells[2 + lines, 2].Value = fa.ADUser.Login;
			//	ws.Cells[2 + lines, 3].Value = fa.FileSystemRights;
			//	if (fa.To != null) ws.Cells[2 + lines, 2, 2 + lines, 3].Style.Font.Color.SetColor(Color.DarkGray);
			//}

			lines += 2;
			ws.Cells[lines, 2].Value = "Folder Accesses";

			int faLength = WriteColumn(ws, lines + 1, (fa, wss, line) => {
				wss.Cells[line, 2].Value = fa.ADUser.Login;
				wss.Cells[line, 3].Value = fa.FileSystemRights;
				if (fa.To != null) wss.Cells[line, 2, line, 3].Style.Font.Color.SetColor(Color.DarkGray);
			}, adFolder.ADFolderAccesses);

			DrawTableInCells(ws.Cells[lines, 2, lines + faLength + 1, 3]);

			return ws;
		}

		public enum ExportType { ISDAVS, AD }

		public void ExportPVStructure(ExportType ExportType, PVStructural pvStructural, bool AllowHistoricalData) {
			Ep.Workbook.Worksheets.Add(pvStructural.Name);
			ExcelWorksheet ws = Ep.Workbook.Worksheets.LastOrDefault();
			ws.Name = pvStructural.Name; //Setting Sheet's name
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

			ws.Column(1).Width = 2.5f;
			for (int i = 2; i < 4; i++) {
				ws.Column(i).Width = 30f;
			}
			ws.Column(5).Width = 2.5f;

			ws.Cells[2, 2].Value = "PVStructural";
			ws.Cells[2, 3].Value = pvStructural.Name;

			int lines = 0;
			foreach (var pvu in pvStructural.PVEmploeesInPart) {
				lines++;
				//UUser = pvu.UUser;
				var wws = ExportUUser(ExportType, pvu.UUser, ws.Cells[2 + lines, 2], AllowHistoricalData);
				//ws.Cells[2 + lines, 2].Value = pvu.FullName;
				//ws.Cells[2 + lines, 2].Hyperlink = new Uri(wws.Cells[2,2].FullAddress);
				AddHyperLink(ws.Cells[2 + lines, 2], wws.Cells[2, 2], pvu.FullName);
				if (pvu.Status == false) ws.Cells[2 + lines, 2, 2 + lines, 3].Style.Font.Color.SetColor(Color.DarkGray);
			}
			DrawTableInCells(ws.Cells[2, 2, 2 + lines, 3]);
		}

		private static void AddHyperLink(ExcelRange source, ExcelRange destination, string displayText) {
			source.Formula = "HYPERLINK(\"[\"&MID(CELL(\"filename\"),SEARCH(\"[\",CELL(\"filename\"))+1, SEARCH(\"]\",CELL(\"filename\"))-SEARCH(\"[\",CELL(\"filename\"))-1)&\"]" + destination.FullAddress + "\",\"" + displayText + "\")";
		}

		public ExcelWorksheet ExportUUser(ExportType ExportType, UUser uUser, bool AllowHistoricalData) {
			return ExportUUser(ExportType, uUser, null, AllowHistoricalData);
		}

		public ExcelWorksheet ExportUUser(ExportType ExportType, UUser uUser, ExcelRange LinkBack, bool AllowHistoricalData) {
			Ep.Workbook.Worksheets.Add(uUser.FullName);
			ExcelWorksheet ws = Ep.Workbook.Worksheets.LastOrDefault();
			ws.Name = uUser.FullName; //Setting Sheet's name
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

			ws.Column(1).Width = 2.5f;
			for (int i = 2; i < 5; i++) {
				ws.Column(i).Width = 20f;
			}
			ws.Column(5).Width = 2.5f;

			if (LinkBack != null) AddHyperLink(ws.Cells[1, 4], LinkBack, "Home");

			//PVEmploeetable Style
			DrawTableInCells(ws.Cells[2, 2, 6, 4]);
			//Filling PVEmploee table
			var pve = uUser.PVEmploees.FirstOrDefault();
			ws.Cells[2, 2].Value = "PVEmploee";
			int lines = 0;
			ws.Cells[5, 2, 5, 4].Merge = true;
			ws.Cells[6, 2, 6, 4].Merge = true;
			if (pve != null) {
				ws.Cells[3, 2].Value = pve.TrimmedName;
				ws.Cells[3, 3].Value = pve.TrimmedSurname;
				ws.Cells[3, 4].Value = pve.Status;
				ws.Cells[4, 2].Value = pve.TrimmedLocalPhone;
				ws.Cells[4, 3].Value = pve.TrimmedPhone;
				ws.Cells[4, 4].Value = pve.TrimmedEmail;
				ws.Cells[5, 2].Value = pve.DepartmentS;
				ws.Cells[6, 2].Value = pve.PartS;

				if (ExportType == ExportType.ISDAVS) {
					var iu = uUser.ISDAVSUsers.FirstOrDefault();
					if (iu != null) {
						ws.Cells[8, 2].Value = "ISDAVS User";
						foreach (var fa in iu.IFunctionAccesses) {
							if (fa.To != null || AllowHistoricalData) {
								lines++;
								ws.Cells[8 + lines, 2].Value = fa.IFunction.Name;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Merge = true;
								if (fa.To != null)
									ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Font.Color.SetColor(Color.DarkGray);
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
								foreach (var qa in fa.IQuestAccesses) {
									lines++;
									ws.Cells[8 + lines, 2].Value = qa.IQuest.Name;
									ws.Cells[8 + lines, 2, 8 + lines, 4].Merge = true;
								}
							}
						}
					} else {
						lines++;
						ws.Cells[8 + lines, 2].Value = "NULL";
					}
				} else if (ExportType == ExportType.AD) {
					var adu = uUser.ADUsers.FirstOrDefault();
                    if (adu == null) return ws;
					if (adu.Status || AllowHistoricalData)
						if (adu != null) {
							ws.Cells[8, 2].Value = "AD User";
							lines++;
							ws.Cells[8 + lines, 2].Value = adu.Login;
							ws.Cells[8 + lines, 4].Value = adu.Status;
							if (adu.Status == false)
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Font.Color.SetColor(Color.DarkGray);
							foreach (var pbd in adu.PermissionsByDrive) {
								lines++;
								ws.Cells[8 + lines, 2].Value = pbd.Drive.Name;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Merge = true;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
								ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
								foreach (var ra in pbd.RootAccesses) {
									lines = WriteFolderAccess(ra, ws.Cells, lines);
									ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
									ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
									ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
									ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
									if (ra.To != null)
										ws.Cells[8 + lines, 2, 8 + lines, 4].Style.Font.Color.SetColor(Color.DarkGray);
								}
							}
						}
				}
			}
			DrawTableInCells(ws.Cells[8, 2, 8 + lines, 4]);
			return ws;
		}

		private int WriteFolderAccess(ADFolderAccess fa, ExcelRange cells, int lines) {
			lines++;
			cells[8 + lines, 2].Value = fa.ADFolder.FullPath;
			cells[8 + lines, 4].Value = fa.FileSystemRights;
			if (fa.To != null)
				cells[8 + lines, 2, 8 + lines, 4].Style.Font.Color.SetColor(Color.DarkGray);
			foreach (var sfa in fa.SubFolderAccesses) {
				lines = WriteFolderAccess(sfa, cells, lines);
			}
			return lines;
		}

		void DrawTableInCells(ExcelRange Cells) {
			int sc = Cells.Start.Column;
			int sr = Cells.Start.Row;
			int ec = Cells.End.Column;
			int er = Cells.End.Row;
			for (int r = sr; r <= er; r++) {
				Cells[r, sc].Style.Border.Left.Style = Cells[r, ec].Style.Border.Right.Style = ExcelBorderStyle.Thin;
			}
			for (int c = sc; c <= ec; c++) {
				Cells[sr, c].Style.Border.Top.Style = Cells[sr, c].Style.Border.Bottom.Style = Cells[er, c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				var headerFill = Cells[sr, c].Style.Fill;
				headerFill.PatternType = ExcelFillStyle.Solid;
				headerFill.BackgroundColor.SetColor(Color.LightGray);
			}
			Cells[sr, sc, sr, ec].Merge = true;
		}

		void SetColumnWidths(ExcelWorksheet ws, float[] Widths) {
			for (int i = 0; i < Widths.Length; i++) {
				ws.Column(i + 1).Width = Widths[i];
			}
		}

		int WriteColumn<T>(ExcelWorksheet ws, int RowStart, Action<T, ExcelWorksheet, int> Write, ICollection<T> List) {
			int lines = -1;
			foreach (var o in List) {
				lines += 1;
				Write(o, ws, RowStart + lines);
			}
			return lines;
		}
	}
}
