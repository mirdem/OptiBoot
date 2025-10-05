using System;
using System.Drawing;
using System.Windows.Forms;

namespace OptiBoot
{
    public class ColorProgressBar : ProgressBar
    {
        public ColorProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Arka plan
            using (SolidBrush backBrush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(backBrush, this.ClientRectangle);
            }

            // Kullanım oranı
            double percent = (double)Value / Maximum;
            Rectangle rect = new Rectangle(0, 0, (int)(this.Width * percent), this.Height);

            // Renk seçimi
            Color barColor = Color.Green;
            if (percent >= 0.9)
                barColor = Color.Red;
            else if (percent >= 0.7)
                barColor = Color.Orange;

            // Dolu kısmı çiz
            using (SolidBrush brush = new SolidBrush(barColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            // Kenarlık
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, this.Width - 1, this.Height - 1);

            // Yüzde metni
            string text = $"{percent * 100:0.00}%";
            SizeF textSize = e.Graphics.MeasureString(text, this.Font);
            float x = (this.Width - textSize.Width) / 2;
            float y = (this.Height - textSize.Height) / 2;

            using (SolidBrush textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(text, this.Font, textBrush, x, y);
            }
        }
    }
}
