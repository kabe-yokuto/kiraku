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
            okButton = new Button();
            judgeLabel = new Label();
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
            judgeLabel.Font = new Font("Yu Gothic UI", 48F, FontStyle.Regular, GraphicsUnit.Point);
            judgeLabel.Location = new Point(157, 106);
            judgeLabel.Name = "judgeLabel";
            judgeLabel.Size = new Size(293, 86);
            judgeLabel.TabIndex = 1;
            judgeLabel.Text = "判定結果";
            judgeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // JudgeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(630, 416);
            Controls.Add(judgeLabel);
            Controls.Add(okButton);
            Margin = new Padding(3, 2, 3, 2);
            Name = "JudgeForm";
            Text = "判定結果";
            WindowState = FormWindowState.Maximized;
            Load += JudgeForm_Load;
            Shown += JudgeForm_Shown;

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Label judgeLabel;
    }
}