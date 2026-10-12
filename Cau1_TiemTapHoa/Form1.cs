using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;
using Cau1_TiemTapHoa.Models;

namespace Cau1_TiemTapHoa;

public partial class Form1 : Form
{
    private int _selectedMaHang = 0;
    private bool _isBinding = false;

    private readonly string[] _loaiHangList = new[]
    {
        "Đồ uống",
        "Thực phẩm khô",
        "Gia vị",
        "Đồ gia dụng"
    };

    private readonly string[] _donViList = new[]
    {
        "Chai",
        "Gói",
        "Hộp",
        "Túi"
    };

    public Form1()
    {
        InitializeComponent();
        InitControls();
        RegisterEvents();
    }

    private void InitControls()
    {
        cboLoaiHang.Items.AddRange(_loaiHangList);
        if (cboLoaiHang.Items.Count > 0) cboLoaiHang.SelectedIndex = 0;

        cboDonVi.Items.AddRange(_donViList);
        if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;

        cboLocLoaiHang.Items.Add("Tất cả các loại");
        cboLocLoaiHang.Items.AddRange(_loaiHangList);
        cboLocLoaiHang.SelectedIndex = 0;

        dgvHangHoa.AutoGenerateColumns = false;
    }

    private void RegisterEvents()
    {
        Load += async (s, e) => await LocHangHoaAsync();
        dgvHangHoa.SelectionChanged += DgvHangHoa_SelectionChanged;

        btnThem.Click += async (s, e) => await ThemHangHoaAsync();
        btnSua.Click += async (s, e) => await SuaHangHoaAsync();
        btnXoa.Click += async (s, e) => await XoaHangHoaAsync();
        btnLamMoi.Click += (s, e) => LamMoi();

        txtTimKiem.TextChanged += (s, e) =>
        {
            tmrDebounce.Stop();
            tmrDebounce.Start();
        };

        tmrDebounce.Tick += async (s, e) =>
        {
            tmrDebounce.Stop();
            await LocHangHoaAsync();
        };

        cboLocLoaiHang.SelectedIndexChanged += async (s, e) => await LocHangHoaAsync();
        chkLocConKinhDoanh.CheckedChanged += async (s, e) => await LocHangHoaAsync();

        btnXemBangGia.Click += async (s, e) => await XemBangGiaAsync();
        btnXuatPdf.Click += async (s, e) => await XuatPdfAsync();
        btnXuatExcelLuoi.Click += (s, e) => XuatExcelLuoi();
        btnXuatExcelTonKho.Click += async (s, e) => await XuatExcelTonKhoAsync();
    }

    public async Task LocHangHoaAsync()
    {
        try
        {
            string keyword = txtTimKiem.Text.Trim();
            string selectedLoai = cboLocLoaiHang.SelectedItem?.ToString() ?? "Tất cả các loại";
            bool onlyKinhDoanh = chkLocConKinhDoanh.Checked;

            using var context = new TiemTapHoaDbContext();
            var query = context.HangHoas.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(h => EF.Functions.Like(h.TenHang, $"%{keyword}%"));
            }

            if (selectedLoai != "Tất cả các loại")
            {
                query = query.Where(h => h.LoaiHang == selectedLoai);
            }

            if (onlyKinhDoanh)
            {
                query = query.Where(h => h.ConKinhDoanh);
            }

            var list = await query.OrderBy(h => h.MaHang).ToListAsync();

            _isBinding = true;
            dgvHangHoa.DataSource = list;
            _isBinding = false;

            lblTrangThai.Text = $"Tìm thấy {list.Count} mặt hàng";

            if (list.Count > 0)
            {
                dgvHangHoa.ClearSelection();
                dgvHangHoa.Rows[0].Selected = true;
                BindRowToControls(dgvHangHoa.Rows[0]);
            }
            else
            {
                LamMoi();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public async Task SetSearchKeywordAsync(string keyword, bool onlyKinhDoanh = false)
    {
        txtTimKiem.Text = keyword;
        chkLocConKinhDoanh.Checked = onlyKinhDoanh;
        await LocHangHoaAsync();
    }

    private void DgvHangHoa_SelectionChanged(object? sender, EventArgs e)
    {
        if (_isBinding || dgvHangHoa.SelectedRows.Count == 0) return;
        var row = dgvHangHoa.SelectedRows[0];
        BindRowToControls(row);
    }

    private void BindRowToControls(DataGridViewRow row)
    {
        if (row.DataBoundItem is HangHoa h)
        {
            _selectedMaHang = h.MaHang;
            txtTenHang.Text = h.TenHang;
            cboLoaiHang.SelectedItem = h.LoaiHang;
            cboDonVi.SelectedItem = h.DonVi;
            nudGiaBan.Value = Math.Max(nudGiaBan.Minimum, Math.Min(nudGiaBan.Maximum, h.GiaBan));
            nudSoLuongTon.Value = Math.Max(nudSoLuongTon.Minimum, Math.Min(nudSoLuongTon.Maximum, h.SoLuongTon));
            dtpNgayNhap.Value = h.NgayNhap;
            chkConKinhDoanh.Checked = h.ConKinhDoanh;
        }
    }

    private void LamMoi()
    {
        _selectedMaHang = 0;
        txtTenHang.Clear();
        if (cboLoaiHang.Items.Count > 0) cboLoaiHang.SelectedIndex = 0;
        if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
        nudGiaBan.Value = 0;
        nudSoLuongTon.Value = 0;
        dtpNgayNhap.Value = DateTime.Now;
        chkConKinhDoanh.Checked = true;
        txtTenHang.Focus();
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtTenHang.Text))
        {
            MessageBox.Show("Tên hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenHang.Focus();
            return false;
        }

        if (nudGiaBan.Value <= 0)
        {
            MessageBox.Show("Giá bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudGiaBan.Focus();
            return false;
        }

        return true;
    }

    private async Task ThemHangHoaAsync()
    {
        if (!ValidateInput()) return;

        try
        {
            using var context = new TiemTapHoaDbContext();
            var hangHoa = new HangHoa
            {
                TenHang = txtTenHang.Text.Trim(),
                LoaiHang = cboLoaiHang.SelectedItem?.ToString() ?? "Đồ uống",
                DonVi = cboDonVi.SelectedItem?.ToString() ?? "Chai",
                GiaBan = nudGiaBan.Value,
                SoLuongTon = (int)nudSoLuongTon.Value,
                NgayNhap = dtpNgayNhap.Value,
                ConKinhDoanh = chkConKinhDoanh.Checked
            };

            await context.HangHoas.AddAsync(hangHoa);
            await context.SaveChangesAsync();

            MessageBox.Show("Thêm hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LocHangHoaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi thêm hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task SuaHangHoaAsync()
    {
        if (_selectedMaHang == 0)
        {
            MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!ValidateInput()) return;

        try
        {
            using var context = new TiemTapHoaDbContext();
            var hangHoa = await context.HangHoas.FindAsync(_selectedMaHang);
            if (hangHoa == null)
            {
                MessageBox.Show("Không tìm thấy hàng hóa cần sửa trong CSDL!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hangHoa.TenHang = txtTenHang.Text.Trim();
            hangHoa.LoaiHang = cboLoaiHang.SelectedItem?.ToString() ?? hangHoa.LoaiHang;
            hangHoa.DonVi = cboDonVi.SelectedItem?.ToString() ?? hangHoa.DonVi;
            hangHoa.GiaBan = nudGiaBan.Value;
            hangHoa.SoLuongTon = (int)nudSoLuongTon.Value;
            hangHoa.NgayNhap = dtpNgayNhap.Value;
            hangHoa.ConKinhDoanh = chkConKinhDoanh.Checked;

            await context.SaveChangesAsync();
            MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LocHangHoaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi cập nhật hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task XoaHangHoaAsync()
    {
        if (_selectedMaHang == 0)
        {
            MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mặt hàng '{txtTenHang.Text}' (Mã: {_selectedMaHang}) không?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            using var context = new TiemTapHoaDbContext();
            var hangHoa = await context.HangHoas.FindAsync(_selectedMaHang);
            if (hangHoa != null)
            {
                context.HangHoas.Remove(hangHoa);
                await context.SaveChangesAsync();
                MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LocHangHoaAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi xóa hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string GetReportPath()
    {
        string path = Path.Combine(Application.StartupPath, "Reports", "BangGia.frx");
        if (!File.Exists(path))
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "BangGia.frx");
        }
        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BangGia.frx");
        }
        return path;
    }

    private async Task<Report?> PrepareBangGiaReportAsync()
    {
        try
        {
            using var context = new TiemTapHoaDbContext();
            var list = await context.HangHoas
                .AsNoTracking()
                .Where(h => h.ConKinhDoanh)
                .OrderBy(h => h.MaHang)
                .ToListAsync();

            DataTable dt = new DataTable("HangHoa");
            dt.Columns.Add("MaHang", typeof(int));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("LoaiHang", typeof(string));
            dt.Columns.Add("DonVi", typeof(string));
            dt.Columns.Add("GiaBan", typeof(decimal));
            dt.Columns.Add("SoLuongTon", typeof(int));

            foreach (var h in list)
            {
                dt.Rows.Add(h.MaHang, h.TenHang, h.LoaiHang, h.DonVi, h.GiaBan, h.SoLuongTon);
            }

            string reportFile = GetReportPath();
            if (!File.Exists(reportFile))
            {
                MessageBox.Show($"Không tìm thấy file mẫu report tại: {reportFile}", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            Report report = new Report();
            report.Load(reportFile);
            report.RegisterData(dt, "HangHoa");
            var ds = report.GetDataSource("HangHoa");
            if (ds != null) ds.Enabled = true;

            report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
            return report;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khởi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    private async Task XemBangGiaAsync()
    {
        try
        {
            using var report = await PrepareBangGiaReportAsync();
            if (report != null)
            {
                report.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xem trước báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task XuatPdfAsync()
    {
        try
        {
            using var report = await PrepareBangGiaReportAsync();
            if (report == null) return;

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = "BangGia_UTC2.pdf",
                Title = "Chọn nơi lưu file PDF"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                report.Prepare();
                using var pdfExport = new PDFExport();
                report.Export(pdfExport, sfd.FileName);

                var ask = MessageBox.Show("Xuất file PDF thành công! Bạn có muốn mở file vừa xuất không?",
                    "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void XuatExcelLuoi()
    {
        try
        {
            if (dgvHangHoa.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trên lưới để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = "DanhSachHangHoa_Luoi.xlsx",
                Title = "Xuất danh sách hàng hóa từ lưới"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("HangHoa");

            string[] headers = { "STT", "Mã hàng", "Tên hàng", "Loại hàng", "Đơn vị", "Giá bán", "Số lượng tồn", "Ngày nhập", "Kinh doanh" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            int rowIdx = 2;
            int stt = 1;
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.DataBoundItem is HangHoa h)
                {
                    ws.Cell(rowIdx, 1).Value = stt++;
                    ws.Cell(rowIdx, 2).Value = h.MaHang;
                    ws.Cell(rowIdx, 3).Value = h.TenHang;
                    ws.Cell(rowIdx, 4).Value = h.LoaiHang;
                    ws.Cell(rowIdx, 5).Value = h.DonVi;

                    var cellGia = ws.Cell(rowIdx, 6);
                    cellGia.Value = h.GiaBan;
                    cellGia.Style.NumberFormat.Format = "#,##0";
                    cellGia.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    var cellTon = ws.Cell(rowIdx, 7);
                    cellTon.Value = h.SoLuongTon;
                    cellTon.Style.NumberFormat.Format = "#,##0";
                    cellTon.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    var cellNgay = ws.Cell(rowIdx, 8);
                    cellNgay.Value = h.NgayNhap.ToString("dd/MM/yyyy");
                    cellNgay.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    var cellKd = ws.Cell(rowIdx, 9);
                    cellKd.Value = h.ConKinhDoanh ? "Còn bán" : "Ngừng bán";
                    cellKd.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    rowIdx++;
                }
            }

            ws.Range(1, 1, rowIdx - 1, headers.Length).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(1, 1, rowIdx - 1, headers.Length).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            workbook.SaveAs(sfd.FileName);

            var ask = MessageBox.Show("Xuất Excel từ lưới thành công! Bạn có muốn mở file không?",
                "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ask == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
            }
        }
        catch (IOException)
        {
            MessageBox.Show("File đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!",
                "Cảnh báo trùng file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel từ lưới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task XuatExcelTonKhoAsync()
    {
        try
        {
            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = "BaoCaoTonKho.xlsx",
                Title = "Xuất báo cáo tồn kho"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var context = new TiemTapHoaDbContext();
            var list = await context.HangHoas
                .AsNoTracking()
                .Where(h => h.ConKinhDoanh)
                .OrderBy(h => h.MaHang)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("TonKho");

            ws.Cell("A1").Value = "BÁO CÁO TỒN KHO - TẠP HÓA NHÀ TRƯỜNG UTC2";
            ws.Cell("A1").Style.Font.Bold = true;
            ws.Cell("A1").Style.Font.FontSize = 14;
            ws.Range("A1:G1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell("A2").Value = $"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}";
            ws.Cell("A2").Style.Font.Italic = true;
            ws.Range("A2:G2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int headerRow = 4;
            string[] headers = { "STT", "Mã hàng", "Tên hàng", "Đơn vị", "Giá bán", "Số lượng tồn", "Giá trị tồn" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            int currRow = headerRow + 1;
            int stt = 1;
            int startDataRow = currRow;

            foreach (var h in list)
            {
                ws.Cell(currRow, 1).Value = stt++;
                ws.Cell(currRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(currRow, 2).Value = h.MaHang;
                ws.Cell(currRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(currRow, 3).Value = h.TenHang;

                ws.Cell(currRow, 4).Value = h.DonVi;
                ws.Cell(currRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var cellGia = ws.Cell(currRow, 5);
                cellGia.Value = h.GiaBan;
                cellGia.Style.NumberFormat.Format = "#,##0";
                cellGia.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var cellTon = ws.Cell(currRow, 6);
                cellTon.Value = h.SoLuongTon;
                cellTon.Style.NumberFormat.Format = "#,##0";
                cellTon.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var cellGiaTri = ws.Cell(currRow, 7);
                cellGiaTri.FormulaA1 = $"=E{currRow}*F{currRow}";
                cellGiaTri.Style.NumberFormat.Format = "#,##0";
                cellGiaTri.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                currRow++;
            }

            int endDataRow = currRow - 1;

            ws.Cell(currRow, 1).Value = "Tổng";
            ws.Cell(currRow, 1).Style.Font.Bold = true;
            ws.Range(currRow, 1, currRow, 6).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Range(currRow, 1, currRow, 6).Style.Font.Bold = true;

            var cellTongGiaTri = ws.Cell(currRow, 7);
            cellTongGiaTri.FormulaA1 = $"=SUM(G{startDataRow}:G{endDataRow})";
            cellTongGiaTri.Style.NumberFormat.Format = "#,##0";
            cellTongGiaTri.Style.Font.Bold = true;
            cellTongGiaTri.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            cellTongGiaTri.Style.Fill.BackgroundColor = XLColor.Yellow;

            ws.Range(headerRow, 1, endDataRow, headers.Length).SetAutoFilter();
            ws.SheetView.FreezeRows(headerRow);
            ws.Range(headerRow, 1, currRow, headers.Length).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(headerRow, 1, currRow, headers.Length).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Columns().AdjustToContents();

            workbook.SaveAs(sfd.FileName);

            var ask = MessageBox.Show("Xuất file Excel tồn kho thành công! Bạn có muốn mở file không?",
                "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ask == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
            }
        }
        catch (IOException)
        {
            MessageBox.Show("File đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!",
                "Cảnh báo trùng file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public async Task CaptureReportScreenshotAsync(string outputPath)
    {
        try
        {
            using var report = await PrepareBangGiaReportAsync();
            if (report != null)
            {
                report.Prepare();
                using var imgExport = new FastReport.Export.Image.ImageExport();
                imgExport.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png;
                imgExport.Resolution = 96;
                imgExport.HasMultipleFiles = false;
                report.Export(imgExport, outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing report: {ex.Message}");
        }
    }
}
