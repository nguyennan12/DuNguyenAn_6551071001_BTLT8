USE master;
GO

IF DB_ID('QuanLyDiem') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyDiem SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyDiem;
END
GO

CREATE DATABASE QuanLyDiem;
GO

USE QuanLyDiem;
GO

CREATE TABLE Lop (
    MaLop VARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE SinhVien (
    MaSV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATETIME NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    MaLop VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Lop(MaLop)
);
GO

CREATE TABLE DiemMon (
    MaDiem INT IDENTITY(1,1) PRIMARY KEY,
    MaSV VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES SinhVien(MaSV) ON DELETE CASCADE,
    TenMon NVARCHAR(100) NOT NULL,
    SoTinChi INT NOT NULL CHECK (SoTinChi >= 1 AND SoTinChi <= 6),
    Diem DECIMAL(4,2) NOT NULL CHECK (Diem >= 0 AND Diem <= 10)
);
GO

INSERT INTO Lop (MaLop, TenLop) VALUES
('CNTT01', N'Công nghệ thông tin K01'),
('KT01', N'Kế toán K01');
GO

INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaLop) VALUES
('SV001', N'Nguyễn Minh Anh', '2005-03-15', N'Nữ', 'CNTT01'),
('SV002', N'Trần Quốc Bảo', '2005-07-22', N'Nam', 'CNTT01'),
('SV003', N'Lê Thị Cẩm Tú', '2005-11-09', N'Nữ', 'KT01');
GO

INSERT INTO DiemMon (MaSV, TenMon, SoTinChi, Diem) VALUES
('SV001', N'Lập trình trực quan', 3, 8.5),
('SV001', N'Cơ sở dữ liệu', 3, 7.5),
('SV001', N'Tiếng Anh chuyên ngành', 2, 9.0),
('SV001', N'Toán rời rạc', 3, 6.5),
('SV002', N'Lập trình trực quan', 3, 9.0),
('SV002', N'Cơ sở dữ liệu', 3, 8.0),
('SV002', N'Tiếng Anh chuyên ngành', 2, 8.5);
GO
