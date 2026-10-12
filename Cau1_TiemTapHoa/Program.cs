using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cau1_TiemTapHoa;

static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (args.Length > 0 && args[0] == "--screenshot")
        {
            var form = new Form1();
            form.Shown += async (s, e) =>
            {
                try
                {
                    await Task.Delay(300);
                    string screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Screenshots");
                    Directory.CreateDirectory(screenshotDir);

                    // 1. Initial screen with all items
                    using (var bmp = new Bitmap(form.Width, form.Height))
                    {
                        form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));
                        bmp.Save(Path.Combine(screenshotDir, "Hinh1.1_QuanLyHangHoa_MacDinh.png"), System.Drawing.Imaging.ImageFormat.Png);
                    }

                    // 2. Filter "nước" matches Hình 1.1 in PDF
                    await form.SetSearchKeywordAsync("nước");
                    await Task.Delay(200);
                    using (var bmp = new Bitmap(form.Width, form.Height))
                    {
                        form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));
                        bmp.Save(Path.Combine(screenshotDir, "Hinh1.1_QuanLyHangHoa_TimKiemNuoc.png"), System.Drawing.Imaging.ImageFormat.Png);
                    }

                    // 3. Export report image matches Hình 1.2 in PDF
                    await form.CaptureReportScreenshotAsync(Path.Combine(screenshotDir, "Hinh1.2_ReportBangGia.png"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in screenshot: " + ex);
                }
                finally
                {
                    Application.Exit();
                }
            };
            Application.Run(form);
            return;
        }

        Application.Run(new Form1());
    }
}