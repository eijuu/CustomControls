using CustomControl.Controls;

namespace CustomControl
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.animationButton1 = new CustomControl.Controls.AnimationButton();
            this.SuspendLayout();
            // 
            // animationButton1
            // 
            this.animationButton1.BackColor = System.Drawing.Color.Green;
            this.animationButton1.ForeColor = System.Drawing.Color.White;
            this.animationButton1.Location = new System.Drawing.Point(251, 74);
            this.animationButton1.Name = "animationButton1";
            this.animationButton1.rippleAnimationFPS = 65;
            this.animationButton1.rippleStepCount = 15;
            this.animationButton1.Size = new System.Drawing.Size(99, 41);
            this.animationButton1.TabIndex = 2;
            this.animationButton1.Text = "animationButton1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.animationButton1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AnimationButton animationButton1;
    }

    internal class CustomButton : Controls.AnimationButton
    {
    }
}

