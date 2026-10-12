USE master;
GO

IF DB_ID('TiemTapHoa') IS NOT NULL
BEGIN
    ALTER DATABASE TiemTapHoa SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TiemTapHoa;
END
GO

CREATE DATABASE TiemTapHoa;
GO

USE TiemTapHoa;
GO

CREATE TABLE HangHoa (
    MaHang INT IDENTITY(1,1) PRIMARY KEY,
    TenHang NVARCHAR(100) NOT NULL,
    LoaiHang NVARCHAR(50) NOT NULL,
    DonVi NVARCHAR(20) NOT NULL,
    GiaBan DECIMAL(18,0) NOT NULL CHECK (GiaBan > 0),
    SoLuongTon INT NOT NULL CHECK (SoLuongTon >= 0),
    NgayNhap DATETIME NOT NULL,
    ConKinhDoanh BIT NOT NULL DEFAULT 1
);
GO

INSERT INTO HangHoa (TenHang, LoaiHang, DonVi, GiaBan, SoLuongTon, NgayNhap, ConKinhDoanh) VALUES
(N'Nước suối Aquafina 500ml', N'Đồ uống', N'Chai', 6000, 120, '2026-10-02', 1),
(N'Mì Hảo Hảo tôm chua cay', N'Thực phẩm khô', N'Gói', 4500, 200, '2026-10-02', 1),
(N'Dầu ăn Tường An 1L', N'Gia vị', N'Chai', 48000, 35, '2026-10-05', 1),
(N'Bột giặt OMO 800g', N'Đồ gia dụng', N'Túi', 52000, 40, '2026-10-05', 1),
(N'Sữa tươi Vinamilk 1L', N'Đồ uống', N'Hộp', 32000, 60, '2026-10-08', 1),
(N'Nước mắm Nam Ngư 500ml', N'Gia vị', N'Chai', 38000, 25, '2026-10-01', 0);
GO
