using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidCrawler.Structures;
using Newtonsoft.Json;

namespace RaidCrawler.Structures
{
    public class TransTextBox : TextBox
    {
        public TransTextBox()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            this.Font = new Font("Segoe UI", 9);
            BackColor = Color.FromArgb(100, 240, 240, 240);
            TextChanged += UserControl2_OnTextChanged;
    }

        protected override void OnPaint(PaintEventArgs e)
        {
            var backgroundBrush = new SolidBrush(BackColor);
            Graphics g = e.Graphics;
            g.FillRectangle(backgroundBrush, 0, 0, this.Width, this.Height);
            g.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), 0, 0);
        }

        public void UserControl2_OnTextChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }

    /*public partial class TransparentBackgroundTextBox : TextBox
    {
        public TransparentBackgroundTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                             //ControlStyles.OptimizedDoubleBuffer | //comment this flag out
                             ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.ResizeRedraw |
                             ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }

        private void redrawText()
        {
            using (Graphics graphics = this.CreateGraphics())
            using (SolidBrush brush = new SolidBrush(this.ForeColor))
                graphics.DrawString(this.Text, this.Font, brush, 1, 1); //play around with how you draw string more to suit your original
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            redrawText();
        }
    }*/

    public class MyPanel : Panel
    {
        public Color back { get; set; }
        public MyPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color back = Color.FromArgb(255, 220, 220, 220);
            Pen pen = new Pen(back);
            e.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }

    }

    public class RoundLabel : Label
    {
        private static readonly string configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        private Config c = new();

        [Browsable(true)]
        public Color back { get; set; }

        public RoundLabel()
        {
            this.DoubleBuffered = true;
            if (File.Exists(configpath))
            {
                c = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configpath)) ?? new Config();
            }
            else
            {
                c = new Config();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color backColor = Color.FromArgb(125, back.R, back.G, back.B);
            base.OnPaint(e);
            using (var graphicsPath = _getRoundRectangle(this.ClientRectangle))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(backColor))
                    e.Graphics.FillPath(brush, graphicsPath);
                using (var pen = new Pen(back, 1.0f))
                    e.Graphics.DrawPath(pen, graphicsPath);
                TextRenderer.DrawText(e.Graphics, Text, this.Font, this.ClientRectangle, this.ForeColor);
            }
        }

        private GraphicsPath _getRoundRectangle(Rectangle rectangle)
        {
            int cornerRadius = 13; // change this value according to your needs
            int diminisher = 1;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rectangle.X + rectangle.Width - cornerRadius - diminisher, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(rectangle.X + rectangle.Width - cornerRadius - diminisher, rectangle.Y + rectangle.Height - cornerRadius - diminisher, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius - diminisher, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }

    public class BetterButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (!this.DesignMode)
            {
                this.BackColor = this.Enabled ? Color.White : Color.Maroon;

                base.OnPaint(pevent);

                if (!this.Enabled)
                {
                    TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor,
                        this.BackColor);
                }
            }
            else
                base.OnPaint(pevent);
        }
    }
}
