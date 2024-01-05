namespace WaveRecMic
{
    partial class StartForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            recButton = new Button();
            stopButton = new Button();
            label1 = new Label();
            label2 = new Label();
            deviceCheckButton = new Button();
            listBox1 = new ListBox();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(352, 38);
            richTextBox1.Margin = new Padding(3, 2, 3, 2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(316, 184);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.Visible = false;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // recButton
            // 
            recButton.Location = new Point(287, 254);
            recButton.Margin = new Padding(3, 2, 3, 2);
            recButton.Name = "recButton";
            recButton.Size = new Size(151, 22);
            recButton.TabIndex = 1;
            recButton.Text = "一括録音";
            recButton.UseVisualStyleBackColor = true;
            recButton.Visible = false;
            recButton.Click += recButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(460, 254);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(165, 23);
            stopButton.TabIndex = 2;
            stopButton.Text = "一括録音停止";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Visible = false;
            stopButton.Click += stopButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(336, 230);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 3;
            label1.Text = "録音待機中";
            label1.Visible = false;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(287, 230);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 4;
            label2.Text = "状態：";
            label2.Visible = false;
            // 
            // deviceCheckButton
            // 
            deviceCheckButton.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            deviceCheckButton.Location = new Point(12, 254);
            deviceCheckButton.Name = "deviceCheckButton";
            deviceCheckButton.Size = new Size(269, 59);
            deviceCheckButton.TabIndex = 5;
            deviceCheckButton.Text = "KIRAKU接続確認";
            deviceCheckButton.UseVisualStyleBackColor = true;
            deviceCheckButton.Click += deviceCheckButton_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 30;
            listBox1.Location = new Point(11, 65);
            listBox1.Margin = new Padding(3, 2, 3, 2);
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiSimple;
            listBox1.Size = new Size(324, 184);
            listBox1.TabIndex = 6;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(11, 16);
            label3.Name = "label3";
            label3.Size = new Size(472, 45);
            label3.TabIndex = 7;
            label3.Text = "音声入力デバイスを選択してください";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(350, 11);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 8;
            label4.Text = "選択中のデバイス";
            label4.Visible = false;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 438);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(listBox1);
            Controls.Add(deviceCheckButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(stopButton);
            Controls.Add(recButton);
            Controls.Add(richTextBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "StartForm";
            Text = "音声入力デバイスを選択してください";
            WindowState = FormWindowState.Maximized;
            Shown += StartForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button recButton;
        private Button stopButton;
        private Label label1;
        private Label label2;
        private Button deviceCheckButton;
        private ListBox listBox1;
        private Label label3;
        private Label label4;
    }
}