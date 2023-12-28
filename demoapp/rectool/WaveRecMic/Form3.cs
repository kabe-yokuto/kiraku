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

        public void setResult(string str)
        {
            resultString = str;
            judgeLabel.Text = resultString;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
