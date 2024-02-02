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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordForm));
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            recButton = new Button();
            stopButton = new Button();
            pictureBox1 = new PictureBox();
            line_image = new PictureBox();
            closeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)line_image).BeginInit();
            SuspendLayout();
            // 
            // plotView1
            // 
            plotView1.BackColor = SystemColors.ActiveCaptionText;
            plotView1.Font = new Font("Yu Gothic UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            plotView1.ForeColor = SystemColors.ActiveCaptionText;
            plotView1.Location = new Point(12, 160);
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
            recButton.Location = new Point(12, 384);
            recButton.Margin = new Padding(3, 2, 3, 2);
            recButton.Name = "recButton";
            recButton.Size = new Size(305, 94);
            recButton.TabIndex = 11;
            recButton.Text = "Start recording";
            recButton.UseVisualStyleBackColor = true;
            recButton.Click += button1_Click;
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            stopButton.Location = new Point(351, 384);
            stopButton.Margin = new Padding(3, 2, 3, 2);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(305, 94);
            stopButton.TabIndex = 12;
            stopButton.Text = "Stop recording";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(230, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // line_image
            // 
            line_image.Image = (Image)resources.GetObject("line_image.Image");
            line_image.Location = new Point(0, 117);
            line_image.Name = "line_image";
            line_image.Size = new Size(600, 1);
            line_image.SizeMode = PictureBoxSizeMode.StretchImage;
            line_image.TabIndex = 14;
            line_image.TabStop = false;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(645, 8);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(28, 23);
            closeButton.TabIndex = 15;
            closeButton.Text = "×";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // RecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(685, 489);
            Controls.Add(closeButton);
            Controls.Add(line_image);
            Controls.Add(pictureBox1);
            Controls.Add(stopButton);
            Controls.Add(recButton);
            Controls.Add(plotView1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "RecordForm";
            Text = "Form2";
            WindowState = FormWindowState.Maximized;
            Load += Form2_Load;
            Shown += Form2_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)line_image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView1;
        private Button recButton;
        private Button stopButton;
        private PictureBox pictureBox1;
        private PictureBox line_image;
        private Button closeButton;
    }
}