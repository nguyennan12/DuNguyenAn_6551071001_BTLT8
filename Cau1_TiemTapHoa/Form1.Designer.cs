namespace Cau1_TiemTapHoa;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpThongTin = new GroupBox();
        chkConKinhDoanh = new CheckBox();
        dtpNgayNhap = new DateTimePicker();
        lblNgayNhap = new Label();
        nudSoLuongTon = new NumericUpDown();
        lblSoLuongTon = new Label();
        nudGiaBan = new NumericUpDown();
        lblGiaBan = new Label();
        cboDonVi = new ComboBox();
        lblDonVi = new Label();
        cboLoaiHang = new ComboBox();
        lblLoaiHang = new Label();
        txtTenHang = new TextBox();
        lblTenHang = new Label();
        
        grpTimKiem = new GroupBox();
        chkLocConKinhDoanh = new CheckBox();
        cboLocLoaiHang = new ComboBox();
        txtTimKiem = new TextBox();

        dgvHangHoa = new DataGridView();
        colMa = new DataGridViewTextBoxColumn();
        colTen = new DataGridViewTextBoxColumn();
        colLoai = new DataGridViewTextBoxColumn();
        colDonVi = new DataGridViewTextBoxColumn();
        colGia = new DataGridViewTextBoxColumn();
        colTon = new DataGridViewTextBoxColumn();
        colNgayNhap = new DataGridViewTextBoxColumn();
        colKinhDoanh = new DataGridViewCheckBoxColumn();

        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        btnXemBangGia = new Button();
        btnXuatPdf = new Button();
        btnXuatExcelLuoi = new Button();
        btnXuatExcelTonKho = new Button();

        lblTrangThai = new Label();
        tmrDebounce = new System.Windows.Forms.Timer();

        grpThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
        grpTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
        SuspendLayout();

        // 
        // grpThongTin
        // 
        grpThongTin.Controls.Add(chkConKinhDoanh);
        grpThongTin.Controls.Add(dtpNgayNhap);
        grpThongTin.Controls.Add(lblNgayNhap);
        grpThongTin.Controls.Add(nudSoLuongTon);
        grpThongTin.Controls.Add(lblSoLuongTon);
        grpThongTin.Controls.Add(nudGiaBan);
        grpThongTin.Controls.Add(lblGiaBan);
        grpThongTin.Controls.Add(cboDonVi);
        grpThongTin.Controls.Add(lblDonVi);
        grpThongTin.Controls.Add(cboLoaiHang);
        grpThongTin.Controls.Add(lblLoaiHang);
        grpThongTin.Controls.Add(txtTenHang);
        grpThongTin.Controls.Add(lblTenHang);
        grpThongTin.Location = new Point(14, 12);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Size = new Size(540, 150);
        grpThongTin.TabIndex = 0;
        grpThongTin.TabStop = false;
        grpThongTin.Text = "Thông tin hàng hóa";

        // lblTenHang
        lblTenHang.AutoSize = true;
        lblTenHang.Location = new Point(12, 25);
        lblTenHang.Name = "lblTenHang";
        lblTenHang.Size = new Size(59, 15);
        lblTenHang.TabIndex = 0;
        lblTenHang.Text = "Tên hàng:";

        // txtTenHang
        txtTenHang.Location = new Point(80, 22);
        txtTenHang.Name = "txtTenHang";
        txtTenHang.Size = new Size(180, 23);
        txtTenHang.TabIndex = 1;

        // lblLoaiHang
        lblLoaiHang.AutoSize = true;
        lblLoaiHang.Location = new Point(12, 55);
        lblLoaiHang.Name = "lblLoaiHang";
        lblLoaiHang.Size = new Size(62, 15);
        lblLoaiHang.TabIndex = 2;
        lblLoaiHang.Text = "Loại hàng:";

        // cboLoaiHang
        cboLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiHang.FormattingEnabled = true;
        cboLoaiHang.Location = new Point(80, 52);
        cboLoaiHang.Name = "cboLoaiHang";
        cboLoaiHang.Size = new Size(180, 23);
        cboLoaiHang.TabIndex = 3;

        // lblDonVi
        lblDonVi.AutoSize = true;
        lblDonVi.Location = new Point(12, 85);
        lblDonVi.Name = "lblDonVi";
        lblDonVi.Size = new Size(44, 15);
        lblDonVi.TabIndex = 4;
        lblDonVi.Text = "Đơn vị:";

        // cboDonVi
        cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDonVi.FormattingEnabled = true;
        cboDonVi.Location = new Point(80, 82);
        cboDonVi.Name = "cboDonVi";
        cboDonVi.Size = new Size(180, 23);
        cboDonVi.TabIndex = 5;

        // lblGiaBan
        lblGiaBan.AutoSize = true;
        lblGiaBan.Location = new Point(12, 115);
        lblGiaBan.Name = "lblGiaBan";
        lblGiaBan.Size = new Size(50, 15);
        lblGiaBan.TabIndex = 6;
        lblGiaBan.Text = "Giá bán:";

        // nudGiaBan
        nudGiaBan.DecimalPlaces = 0;
        nudGiaBan.Increment = new decimal(new int[] { 500, 0, 0, 0 });
        nudGiaBan.Location = new Point(80, 112);
        nudGiaBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudGiaBan.Name = "nudGiaBan";
        nudGiaBan.Size = new Size(180, 23);
        nudGiaBan.TabIndex = 7;
        nudGiaBan.ThousandsSeparator = true;

        // lblSoLuongTon
        lblSoLuongTon.AutoSize = true;
        lblSoLuongTon.Location = new Point(275, 55);
        lblSoLuongTon.Name = "lblSoLuongTon";
        lblSoLuongTon.Size = new Size(78, 15);
        lblSoLuongTon.TabIndex = 8;
        lblSoLuongTon.Text = "Số lượng tồn:";

        // nudSoLuongTon
        nudSoLuongTon.Location = new Point(360, 52);
        nudSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudSoLuongTon.Name = "nudSoLuongTon";
        nudSoLuongTon.Size = new Size(165, 23);
        nudSoLuongTon.TabIndex = 9;

        // lblNgayNhap
        lblNgayNhap.AutoSize = true;
        lblNgayNhap.Location = new Point(275, 85);
        lblNgayNhap.Name = "lblNgayNhap";
        lblNgayNhap.Size = new Size(68, 15);
        lblNgayNhap.TabIndex = 10;
        lblNgayNhap.Text = "Ngày nhập:";

        // dtpNgayNhap
        dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
        dtpNgayNhap.Format = DateTimePickerFormat.Custom;
        dtpNgayNhap.Location = new Point(360, 82);
        dtpNgayNhap.Name = "dtpNgayNhap";
        dtpNgayNhap.Size = new Size(165, 23);
        dtpNgayNhap.TabIndex = 11;

        // chkConKinhDoanh
        chkConKinhDoanh.AutoSize = true;
        chkConKinhDoanh.Checked = true;
        chkConKinhDoanh.CheckState = CheckState.Checked;
        chkConKinhDoanh.Location = new Point(360, 114);
        chkConKinhDoanh.Name = "chkConKinhDoanh";
        chkConKinhDoanh.Size = new Size(111, 19);
        chkConKinhDoanh.TabIndex = 12;
        chkConKinhDoanh.Text = "Còn kinh doanh";
        chkConKinhDoanh.UseVisualStyleBackColor = true;

        // 
        // grpTimKiem
        // 
        grpTimKiem.Controls.Add(chkLocConKinhDoanh);
        grpTimKiem.Controls.Add(cboLocLoaiHang);
        grpTimKiem.Controls.Add(txtTimKiem);
        grpTimKiem.Location = new Point(568, 12);
        grpTimKiem.Name = "grpTimKiem";
        grpTimKiem.Size = new Size(300, 150);
        grpTimKiem.TabIndex = 1;
        grpTimKiem.TabStop = false;
        grpTimKiem.Text = "Tìm kiếm";

        // txtTimKiem
        txtTimKiem.Location = new Point(15, 22);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập tên hàng...";
        txtTimKiem.Size = new Size(270, 23);
        txtTimKiem.TabIndex = 0;

        // cboLocLoaiHang
        cboLocLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocLoaiHang.FormattingEnabled = true;
        cboLocLoaiHang.Location = new Point(15, 52);
        cboLocLoaiHang.Name = "cboLocLoaiHang";
        cboLocLoaiHang.Size = new Size(270, 23);
        cboLocLoaiHang.TabIndex = 1;

        // chkLocConKinhDoanh
        chkLocConKinhDoanh.AutoSize = true;
        chkLocConKinhDoanh.Location = new Point(15, 85);
        chkLocConKinhDoanh.Name = "chkLocConKinhDoanh";
        chkLocConKinhDoanh.Size = new Size(174, 19);
        chkLocConKinhDoanh.TabIndex = 2;
        chkLocConKinhDoanh.Text = "Chỉ hiện hàng còn kinh doanh";
        chkLocConKinhDoanh.UseVisualStyleBackColor = true;

        // 
        // dgvHangHoa
        // 
        dgvHangHoa.AllowUserToAddRows = false;
        dgvHangHoa.AllowUserToDeleteRows = false;
        dgvHangHoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHangHoa.BackgroundColor = Color.White;
        dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] {
            colMa,
            colTen,
            colLoai,
            colDonVi,
            colGia,
            colTon,
            colNgayNhap,
            colKinhDoanh
        });
        dgvHangHoa.Location = new Point(14, 175);
        dgvHangHoa.MultiSelect = false;
        dgvHangHoa.Name = "dgvHangHoa";
        dgvHangHoa.ReadOnly = true;
        dgvHangHoa.RowHeadersWidth = 30;
        dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHangHoa.Size = new Size(854, 255);
        dgvHangHoa.TabIndex = 2;

        // colMa
        colMa.DataPropertyName = "MaHang";
        colMa.FillWeight = 40F;
        colMa.HeaderText = "Mã";
        colMa.Name = "colMa";
        colMa.ReadOnly = true;

        // colTen
        colTen.DataPropertyName = "TenHang";
        colTen.FillWeight = 160F;
        colTen.HeaderText = "Tên hàng";
        colTen.Name = "colTen";
        colTen.ReadOnly = true;

        // colLoai
        colLoai.DataPropertyName = "LoaiHang";
        colLoai.FillWeight = 85F;
        colLoai.HeaderText = "Loại hàng";
        colLoai.Name = "colLoai";
        colLoai.ReadOnly = true;

        // colDonVi
        colDonVi.DataPropertyName = "DonVi";
        colDonVi.FillWeight = 60F;
        colDonVi.HeaderText = "Đơn vị";
        colDonVi.Name = "colDonVi";
        colDonVi.ReadOnly = true;

        // colGia
        colGia.DataPropertyName = "GiaBan";
        colGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colGia.DefaultCellStyle.Format = "N0";
        colGia.FillWeight = 75F;
        colGia.HeaderText = "Giá bán";
        colGia.Name = "colGia";
        colGia.ReadOnly = true;

        // colTon
        colTon.DataPropertyName = "SoLuongTon";
        colTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colTon.FillWeight = 60F;
        colTon.HeaderText = "Tồn";
        colTon.Name = "colTon";
        colTon.ReadOnly = true;

        // colNgayNhap
        colNgayNhap.DataPropertyName = "NgayNhap";
        colNgayNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        colNgayNhap.DefaultCellStyle.Format = "dd/MM/yyyy";
        colNgayNhap.FillWeight = 80F;
        colNgayNhap.HeaderText = "Ngày nhập";
        colNgayNhap.Name = "colNgayNhap";
        colNgayNhap.ReadOnly = true;

        // colKinhDoanh
        colKinhDoanh.DataPropertyName = "ConKinhDoanh";
        colKinhDoanh.FillWeight = 65F;
        colKinhDoanh.HeaderText = "Kinh doanh";
        colKinhDoanh.Name = "colKinhDoanh";
        colKinhDoanh.ReadOnly = true;

        // 
        // btnThem
        // 
        btnThem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnThem.Location = new Point(14, 442);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(72, 30);
        btnThem.TabIndex = 3;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;

        // btnSua
        btnSua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSua.Location = new Point(92, 442);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(72, 30);
        btnSua.TabIndex = 4;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;

        // btnXoa
        btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnXoa.Location = new Point(170, 442);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(72, 30);
        btnXoa.TabIndex = 5;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;

        // btnLamMoi
        btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLamMoi.Location = new Point(248, 442);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(75, 30);
        btnLamMoi.TabIndex = 6;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;

        // btnXemBangGia
        btnXemBangGia.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnXemBangGia.Location = new Point(340, 442);
        btnXemBangGia.Name = "btnXemBangGia";
        btnXemBangGia.Size = new Size(100, 30);
        btnXemBangGia.TabIndex = 7;
        btnXemBangGia.Text = "Xem bảng giá";
        btnXemBangGia.UseVisualStyleBackColor = true;

        // btnXuatPdf
        btnXuatPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnXuatPdf.Location = new Point(446, 442);
        btnXuatPdf.Name = "btnXuatPdf";
        btnXuatPdf.Size = new Size(85, 30);
        btnXuatPdf.TabIndex = 8;
        btnXuatPdf.Text = "Xuất PDF";
        btnXuatPdf.UseVisualStyleBackColor = true;

        // btnXuatExcelLuoi
        btnXuatExcelLuoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnXuatExcelLuoi.Location = new Point(537, 442);
        btnXuatExcelLuoi.Name = "btnXuatExcelLuoi";
        btnXuatExcelLuoi.Size = new Size(115, 30);
        btnXuatExcelLuoi.TabIndex = 9;
        btnXuatExcelLuoi.Text = "Xuất Excel (lưới)";
        btnXuatExcelLuoi.UseVisualStyleBackColor = true;

        // btnXuatExcelTonKho
        btnXuatExcelTonKho.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnXuatExcelTonKho.Location = new Point(658, 442);
        btnXuatExcelTonKho.Name = "btnXuatExcelTonKho";
        btnXuatExcelTonKho.Size = new Size(125, 30);
        btnXuatExcelTonKho.TabIndex = 10;
        btnXuatExcelTonKho.Text = "Xuất Excel tồn kho";
        btnXuatExcelTonKho.UseVisualStyleBackColor = true;

        // 
        // lblTrangThai
        // 
        lblTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTrangThai.AutoSize = true;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblTrangThai.Location = new Point(14, 485);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(122, 15);
        lblTrangThai.TabIndex = 11;
        lblTrangThai.Text = "Tìm thấy 6 mặt hàng";

        // 
        // tmrDebounce
        // 
        tmrDebounce.Interval = 350;

        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(884, 511);
        Controls.Add(lblTrangThai);
        Controls.Add(btnXuatExcelTonKho);
        Controls.Add(btnXuatExcelLuoi);
        Controls.Add(btnXuatPdf);
        Controls.Add(btnXemBangGia);
        Controls.Add(btnLamMoi);
        Controls.Add(btnXoa);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(dgvHangHoa);
        Controls.Add(grpTimKiem);
        Controls.Add(grpThongTin);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý hàng hóa - Tạp hóa UTC2";
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
        grpTimKiem.ResumeLayout(false);
        grpTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private GroupBox grpThongTin;
    private Label lblTenHang;
    private TextBox txtTenHang;
    private Label lblLoaiHang;
    private ComboBox cboLoaiHang;
    private Label lblDonVi;
    private ComboBox cboDonVi;
    private Label lblGiaBan;
    private NumericUpDown nudGiaBan;
    private Label lblSoLuongTon;
    private NumericUpDown nudSoLuongTon;
    private Label lblNgayNhap;
    private DateTimePicker dtpNgayNhap;
    private CheckBox chkConKinhDoanh;

    private GroupBox grpTimKiem;
    private TextBox txtTimKiem;
    private ComboBox cboLocLoaiHang;
    private CheckBox chkLocConKinhDoanh;

    private DataGridView dgvHangHoa;
    private DataGridViewTextBoxColumn colMa;
    private DataGridViewTextBoxColumn colTen;
    private DataGridViewTextBoxColumn colLoai;
    private DataGridViewTextBoxColumn colDonVi;
    private DataGridViewTextBoxColumn colGia;
    private DataGridViewTextBoxColumn colTon;
    private DataGridViewTextBoxColumn colNgayNhap;
    private DataGridViewCheckBoxColumn colKinhDoanh;

    private Button btnThem;
    private Button btnSua;
    private Button btnXoa;
    private Button btnLamMoi;
    private Button btnXemBangGia;
    private Button btnXuatPdf;
    private Button btnXuatExcelLuoi;
    private Button btnXuatExcelTonKho;

    private Label lblTrangThai;
    private System.Windows.Forms.Timer tmrDebounce;
}
