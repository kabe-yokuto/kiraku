using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveRecMic
{
    public partial class JudgeForm : Form
    {
        public string resultString = "";

        public JudgeForm()
        {
            InitializeComponent();
        }

        public void JudgeForm_Shown(object sender, EventArgs e)
        {
            // 表示位置調整

            int width = this.Size.Width;
            int height = this.Size.Height;

            line_image.Left = 0;
            line_image.Size = new Size(width, 1);


            int w = judgeLabel.Size.Width;
            int h = judgeLabel.Size.Height;

            judgeLabel.Left = width / 2 - w / 2;
            judgeLabel.Top = height / 2 + 128;

            w = judgeLabel2.Size.Width;
            h = judgeLabel2.Size.Height;

            judgeLabel2.Left = width / 2 - w / 2;
            judgeLabel2.Top = height / 2 + 128;

            cautionImage.Left = width / 2 - cautionImage.Width / 2;
            cautionImage.Top = height / 2 - cautionImage.Height / 2;

            okImage.Left = width / 2 - okImage.Width / 2;
            okImage.Top = height / 2 - okImage.Height / 2;


            okButton.Left = Width / 2 - okButton.Size.Width / 2;
            okButton.Top = height - okButton.Size.Height - 64;
            
            if( resultString=="Normal")
            {
                cautionImage.Visible = false;
                okImage.Visible = true;

                judgeLabel.Visible = false;
                judgeLabel2.Visible = true;
            }
            else
            {
                cautionImage.Visible = true;
                okImage.Visible = false;

                judgeLabel.Visible = true;
                judgeLabel2.Visible = false;
            }

            this.Refresh();
        }

        public void setResult(string str)
        {
            resultString = str;
            //judgeLabel.Text = resultString;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void JudgeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
