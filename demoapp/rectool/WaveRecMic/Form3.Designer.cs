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
            okButton.Location = new Point(214, 196);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // judgeLabel
            // 
            judgeLabel.AutoSize = true;
            judgeLabel.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            judgeLabel.Location = new Point(194, 70);
            judgeLabel.Name = "judgeLabel";
            judgeLabel.Size = new Size(130, 54);
            judgeLabel.TabIndex = 1;
            judgeLabel.Text = "label1";
            judgeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // JudgeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 238);
            Controls.Add(judgeLabel);
            Controls.Add(okButton);
            Name = "JudgeForm";
            Text = "判定結果";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Label judgeLabel;
    }
}