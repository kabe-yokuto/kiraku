namespace WaveRecMic
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            recButton = new Button();
            stopButton = new Button();
            SuspendLayout();
            // 
            // plotView1
            // 
            plotView1.BackColor = SystemColors.ActiveCaptionText;
            plotView1.Font = new Font("Yu Gothic UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            plotView1.ForeColor = SystemColors.ActiveCaptionText;
            plotView1.Location = new Point(5, 2);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(372, 203);
            plotView1.TabIndex = 10;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            plotView1.Click += plotView1_Click;
            // 
            // recButton
            // 
            recButton.Location = new Point(29, 216);
            recButton.Name = "recButton";
            recButton.Size = new Size(121, 29);
            recButton.TabIndex = 11;
            recButton.Text = "録音開始";
            recButton.UseVisualStyleBackColor = true;
            recButton.Click += button1_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(240, 216);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(121, 29);
            stopButton.TabIndex = 12;
            stopButton.Text = "録音停止";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 253);
            Controls.Add(stopButton);
            Controls.Add(recButton);
            Controls.Add(plotView1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView1;
        private Button recButton;
        private Button stopButton;
    }
}