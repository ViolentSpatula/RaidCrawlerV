using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using RaidCrawler.Structures;
using Newtonsoft.Json;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using PKHeX.Core;
using NLog.Targets;

namespace CustomControls.RJControls
{
    public class RJButton : Button
    {
        private static readonly string configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        private Config c = new();
        //Fields
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color text = Color.Orange;
        private Color textEnabled = Color.Purple;
        private Color textDisabled = Color.Red;
        private Color background = Color.Green;
        private Color borderColor = Color.GreenYellow;

        //Properties
        [Category("RJ Code Advance")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("RJ Code Advance")]
        public Color TextEnabledColor
        {
            get { return textEnabled; }
            set { textEnabled = value; }
        }

        [Category("RJ Code Advance")]
        public Color TextDisabledColor
        {
            get { return textDisabled; }
            set { textDisabled = value; }
        }

        [Category("RJ Code Advance")]
        public Color back
        {
            get { return background; }
            set { background = value; this.Invalidate(); }
            
        }

        //Constructor
        public RJButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.Green;
            this.Resize += new EventHandler(Button_Resize);
            if (File.Exists(configpath))
            {
                c = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configpath)) ?? new Config();
            }
            else
            {
                c = new Config();
            }
            background = this.Enabled ? c.buttonBackgroundColor : c.buttonDisabledBackgroundColor;
        }

        //Methods
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            int diminisher = 0;

            path.StartFigure();
            path.AddArc(rect.X , rect.Y , curveSize, curveSize, 180, 90);
            //path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.X + rect.Width - curveSize - diminisher, rect.Y, curveSize, curveSize, 270, 90);
            //path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X + rect.Width - curveSize - diminisher, rect.Y + rect.Height - curveSize - diminisher, curveSize, curveSize, 0, 90);
            //path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - curveSize - diminisher, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            
            //this.BackColor = Color.Green;

            text = this.Enabled ? textEnabled : textDisabled;
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Brush backBrush = new SolidBrush(background))
                using (Pen penSurface = new Pen(Color.Transparent, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //pevent.Graphics.FillPath(backBrush, pathBorder);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penBorder, pathSurface);
                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                this.BackColor = this.Enabled ? c.buttonBackgroundColor : c.buttonDisabledBackgroundColor;
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, text,
                    Color.Transparent);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        /*protected override void OnEnabledChanged(EventArgs e)
        {
            if (this.Enabled) { text = textEnabled; }
            else { text = textDisabled; }
        }*/
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
    }
}
