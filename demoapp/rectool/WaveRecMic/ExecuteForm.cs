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
    public partial class ExecuteForm : Form
    {
        public ExecuteForm()
        {
            InitializeComponent();


        }

        public void Draw()
        {
            pictureBox1.Refresh();

            // 表示位置調整

            int width = this.Size.Width;
            int height = this.Size.Height;

            int w = pictureBox1.Size.Width;
            int h = pictureBox1.Size.Height;

            pictureBox1.Left = width / 2 - w / 2;
            pictureBox1.Top = height / 2 - h / 2;

            label1.Left = width/2-label1.Size.Width/2;

            this.Refresh();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
