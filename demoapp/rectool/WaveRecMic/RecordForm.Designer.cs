namespace WaveRecMic
{
    partial class RecordForm
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
            normalPlayButton = new Button();
            abnormalPlayButton = new Button();
            SuspendLayout();
            // 
            // plotView1
            // 
            plotView1.BackColor = SystemColors.ActiveCaptionText;
            plotView1.Font = new Font("Yu Gothic UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            plotView1.ForeColor = SystemColors.ActiveCaptionText;
            plotView1.Location = new Point(4, 2);
            plotView1.Margin = new Padding(3, 2, 3, 2);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(500, 200);
            plotView1.TabIndex = 10;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            plotView1.Click += plotView1_Click;
            // 
            // recButton
            // 
            recButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            recButton.Location = new Point(12, 245);
            recButton.Margin = new Padding(3, 2, 3, 2);
            recButton.Name = "recButton";
            recButton.Size = new Size(305, 94);
            recButton.TabIndex = 11;
            recButton.Text = "録音開始";
            recButton.UseVisualStyleBackColor = true;
            recButton.Click += button1_Click;
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            stopButton.Location = new Point(351, 245);
            stopButton.Margin = new Padding(3, 2, 3, 2);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(305, 94);
            stopButton.TabIndex = 12;
            stopButton.Text = "録音停止";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += button2_Click;
            // 
            // normalPlayButton
            // 
            normalPlayButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            normalPlayButton.Location = new Point(12, 352);
            normalPlayButton.Margin = new Padding(3, 2, 3, 2);
            normalPlayButton.Name = "normalPlayButton";
            normalPlayButton.Size = new Size(305, 94);
            normalPlayButton.TabIndex = 13;
            normalPlayButton.Text = "正常音再生";
            normalPlayButton.UseVisualStyleBackColor = true;
            normalPlayButton.Click += normalPlayButton_Click;
            // 
            // abnormalPlayButton
            // 
            abnormalPlayButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            abnormalPlayButton.Location = new Point(351, 352);
            abnormalPlayButton.Margin = new Padding(3, 2, 3, 2);
            abnormalPlayButton.Name = "abnormalPlayButton";
            abnormalPlayButton.Size = new Size(305, 94);
            abnormalPlayButton.TabIndex = 14;
            abnormalPlayButton.Text = "異常音再生";
            abnormalPlayButton.UseVisualStyleBackColor = true;
            abnormalPlayButton.Click += abnormalPlayButton_Click;
            // 
            // RecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(685, 489);
            Controls.Add(abnormalPlayButton);
            Controls.Add(normalPlayButton);
            Controls.Add(stopButton);
            Controls.Add(recButton);
            Controls.Add(plotView1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "RecordForm";
            Text = "Form2";
            WindowState = FormWindowState.Maximized;
            Load += Form2_Load;
            Shown += Form2_Shown;
            ResumeLayout(false);
        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView1;
        private Button recButton;
        private Button stopButton;
        private Button normalPlayButton;
        private Button abnormalPlayButton;
    }
}