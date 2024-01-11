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

        bool draw = false;

        public void Draw()
        {

            // 表示位置調整
            if (!draw)
            {
                int width = this.Size.Width;
                int height = this.Size.Height;

                line_image.Left = 0;
                line_image.Size = new Size(width, 1);

                int w = pictureBox2.Size.Width;
                int h = pictureBox2.Size.Height;

                pictureBox2.Left = width / 2 - w / 2;
                pictureBox2.Top = height / 2 - h / 2;

                w = pictureBox1.Size.Width;
                h = pictureBox1.Size.Height;

                pictureBox1.Left = width / 2 - w / 2;
                pictureBox1.Top = pictureBox2.Top+pictureBox2.Height+h/2;

                label1.Left = width / 2 - label1.Size.Width / 2;
                label1.Top = 117 + 16;

                draw = true;
            }
            //this.Refresh();
            pictureBox1.Refresh();


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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
