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

            int w = judgeLabel.Size.Width;
            int h = judgeLabel.Size.Height;

            judgeLabel.Left = width / 2 - w / 2;
            judgeLabel.Top = height / 2 - h / 2;


            okButton.Left = Width / 2 - okButton.Size.Width / 2;
            okButton.Top = height - okButton.Size.Height - 64;
            

            this.Refresh();
        }

        public void setResult(string str)
        {
            resultString = str;
            judgeLabel.Text = resultString;
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
