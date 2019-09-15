using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace CustomControl.Controls
{
    public class AnimationButton : Control
    {
        #region BUTTION PROPERTIES
        [Description("Steps count for ripple animation changes")]
        public int rippleStepCount { get; set; } = 15;
        [Description("Ripple animation changes FPS")]
        public int rippleAnimationFPS { get; set; } = 60;
        #endregion

        #region VARIABLES      

        private StringFormat _stringFormat = new StringFormat();
        private bool _mouseEntered = false;
        private bool _mousePressed = false;
    
        private Point _clickLocationPoint = new Point();

        private Thread _threadRipple;
        private float _rippleValue;
        private float _rippleStartValue;
        private float _rippleTargetValue;

        private enum RippleStatus
        {
            Request, Active, Completed
        }
        private RippleStatus _rippleStatus;


        #endregion


        public AnimationButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint,
                    true);

            DoubleBuffered = true;

            Size = new Size(100, 30);
            BackColor = Color.Green;
            ForeColor = Color.White;

            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            graphics.Clear(Parent.BackColor);

            Rectangle rectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rippleRectangle = new Rectangle(
                _clickLocationPoint.X - (int)_rippleValue / 2,
                _clickLocationPoint.Y - (int)_rippleValue / 2,
                (int)_rippleValue,
                (int)_rippleValue
                );

            //Background
            graphics.DrawRectangle(new Pen(BackColor), rectangle);
            graphics.FillRectangle(new SolidBrush(BackColor), rectangle);
            
            //Mouse Enter
            if (_mouseEntered)
            {
                graphics.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rectangle);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rectangle);
            }
            //Mouse pressed
            if (_mousePressed)
            {
                graphics.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rectangle);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rectangle);
            }
            else
            {
                //Ripple Effect draw                   
                graphics.DrawEllipse(new Pen(Color.FromArgb(30, Color.Black)), rippleRectangle);
                graphics.FillEllipse(new SolidBrush(Color.FromArgb(30, Color.Black)), rippleRectangle); 
            }
                       
            
            graphics.DrawString(Text, Font, new SolidBrush(ForeColor), rectangle, _stringFormat);
        }

        private void ButtonRippleEffectAction()
        {
            _rippleStatus = RippleStatus.Request;
            _rippleValue = 0;
            _rippleStartValue = 0;
            _rippleTargetValue = Width * 2;

            _threadRipple = new Thread(RippleEffectUpdate)
            {
                IsBackground = true,
                Name = "Ripple Effect"
            };
            _threadRipple.Start();
        }

        private void RippleEffectUpdate()
        {
            while (_rippleStatus != RippleStatus.Completed)
            {
                _rippleStatus = RippleStatus.Active;
                if (_rippleValue <= _rippleTargetValue)
                {
                    _rippleValue += Step();
                }
                else
                {
                    _rippleValue = 0;
                    _rippleStatus = RippleStatus.Completed;                   
                }

                double timeMs = 1000.0 / rippleAnimationFPS;              
                Thread.Sleep(Convert.ToInt32(timeMs));
                Invalidate();            
            }
           
        }

        private float Step()
        {
            return Math.Abs(_rippleTargetValue - _rippleStartValue) / rippleStepCount;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _mouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseEntered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mousePressed = true;
            Invalidate();

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _mousePressed = false;
            _clickLocationPoint = e.Location;
            ButtonRippleEffectAction();
        }
      
    }
}
