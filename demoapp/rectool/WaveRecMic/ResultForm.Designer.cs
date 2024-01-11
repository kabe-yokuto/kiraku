namespace WaveRecMic
{
    partial class JudgeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JudgeForm));
            okButton = new Button();
            judgeLabel = new Label();
            cautionImage = new PictureBox();
            okImage = new PictureBox();
            pictureBox3 = new PictureBox();
            line_image = new PictureBox();
            judgeLabel2 = new Label();
            ((System.ComponentModel.ISupportInitialize)cautionImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)okImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)line_image).BeginInit();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            okButton.Location = new Point(157, 321);
            okButton.Margin = new Padding(3, 2, 3, 2);
            okButton.Name = "okButton";
            okButton.Size = new Size(310, 84);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // judgeLabel
            // 
            judgeLabel.AutoSize = true;
            judgeLabel.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            judgeLabel.Location = new Point(157, 274);
            judgeLabel.Name = "judgeLabel";
            judgeLabel.Size = new Size(354, 45);
            judgeLabel.TabIndex = 1;
            judgeLabel.Text = "Suspected of aspiration";
            judgeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // cautionImage
            // 
            cautionImage.Image = (Image)resources.GetObject("cautionImage.Image");
            cautionImage.Location = new Point(175, 35);
            cautionImage.Name = "cautionImage";
            cautionImage.Size = new Size(256, 256);
            cautionImage.SizeMode = PictureBoxSizeMode.StretchImage;
            cautionImage.TabIndex = 2;
            cautionImage.TabStop = false;
            // 
            // okImage
            // 
            okImage.Image = (Image)resources.GetObject("okImage.Image");
            okImage.Location = new Point(246, 35);
            okImage.Name = "okImage";
            okImage.Size = new Size(256, 256);
            okImage.SizeMode = PictureBoxSizeMode.StretchImage;
            okImage.TabIndex = 3;
            okImage.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(10, 10);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(230, 100);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            // 
            // line_image
            // 
            line_image.Image = (Image)resources.GetObject("line_image.Image");
            line_image.Location = new Point(15, 117);
            line_image.Name = "line_image";
            line_image.Size = new Size(600, 1);
            line_image.SizeMode = PictureBoxSizeMode.StretchImage;
            line_image.TabIndex = 11;
            line_image.TabStop = false;
            // 
            // judgeLabel2
            // 
            judgeLabel2.AutoSize = true;
            judgeLabel2.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point);
            judgeLabel2.Location = new Point(206, 282);
            judgeLabel2.Name = "judgeLabel2";
            judgeLabel2.Size = new Size(234, 37);
            judgeLabel2.TabIndex = 12;
            judgeLabel2.Text = "No abnormality";
            // 
            // JudgeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HighlightText;
            ClientSize = new Size(630, 416);
            Controls.Add(judgeLabel2);
            Controls.Add(line_image);
            Controls.Add(pictureBox3);
            Controls.Add(okImage);
            Controls.Add(cautionImage);
            Controls.Add(judgeLabel);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "JudgeForm";
            Text = "判定結果";
            WindowState = FormWindowState.Maximized;
            Load += JudgeForm_Load;
            Shown += JudgeForm_Shown;
            ((System.ComponentModel.ISupportInitialize)cautionImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)okImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)line_image).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Label judgeLabel;
        private PictureBox cautionImage;
        private PictureBox okImage;
        private PictureBox pictureBox3;
        private PictureBox line_image;
        private Label judgeLabel2;
    }
}