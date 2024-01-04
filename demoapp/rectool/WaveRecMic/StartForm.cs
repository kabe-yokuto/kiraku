/*
 * WAV同時録音ツール
 */
using NAudio.Wave;
using NAudio.Codecs;
using NAudio.CoreAudioApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.WindowsForms;
using NAudio.Gui;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System.Windows.Controls;

namespace WaveRecMic
{

    public partial class StartForm : Form
    {
        // 録音状態
        const int STATUS_NONE = 0;
        const int STATUS_REC = 1;
        const int STATUS_MAX = 2;

        // 波形表示エリアの高さ
        const int WAVE_RECT_H = 100;
        const int WAVE_MARGIN = 10;

        // とりあえずデバイスは最大３２個としておく
        public const int DEVICE_MAX = 32;

        WaveInCapabilities[] deviceCaps = new WaveInCapabilities[DEVICE_MAX];
        int[] iCareIndexs = new int[DEVICE_MAX];
        int indexCount = 0;
        string[] statusStr = new string[STATUS_MAX] { "待機中", "録音中" };
        int status = STATUS_NONE;


        List<RecordForm> deviceFormList = new List<RecordForm>();
        List<string> deviceNameList = new List<string>();

        public bool recFlag = false;

        public StartForm()
        {
            InitializeComponent();
            CheckDevice();
            StatusChange(STATUS_NONE);

            recButtonControl(false);

        }


        public void　StartForm_Shown(object sender, EventArgs e)
        {

            int width = this.Size.Width;
            int height = this.Size.Height;

            deviceCheckButton.Left = Width / 2 - deviceCheckButton.Size.Width / 2;
            deviceCheckButton.Top = height - deviceCheckButton.Size.Height - 64;


            listBox1.Left = Width / 2 - width * 3 / 4 / 2;
            listBox1.Size = new Size(width*3/4, height * 3 / 4);

            //listBox1.Size.Width = width * 3 / 4;
            //listBox1.Size.Height = height * 3 / 4;

            this.Refresh();

        }
        public void recButtonControl(bool flag)
        {
            recFlag = flag;
            recButton.Enabled = !flag;
            stopButton.Enabled = flag;
        }

        // 子のボタン状況をチェックして全停止していたら一括録音可能にする
        public void recButtonCheck()
        {
            bool flag = false;

            foreach( RecordForm f in deviceFormList) {
                if (f.recFlag) flag = true;
            }
            recFlag = flag;
            recButtonControl(flag);
        }

        public void Form1_Shown(object sender, EventArgs e)
        {



        }

        // 状態を変更する
        private void StatusChange(int st)
        {
            status = st;
            label1.Text = statusStr[st];
        }

        private void CheckDevice()
        {
            closeAllDevice();

            indexCount = 0;

            listBox1.Items.Clear();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                WaveInCapabilities caps = WaveIn.GetCapabilities(i);

                System.Diagnostics.Debug.WriteLine(i + ":" + caps.ProductName);
                listBox1.Items.Add(i + ":" + caps.ProductName);
                deviceCaps[indexCount] = caps;
                //iCareIndexs[indexCount] = i;
                indexCount++;

            }
        }







        // 録音開始
        private void recButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                recButtonControl(true);

                StatusChange(STATUS_REC);

                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                {
                    string str = listBox1.SelectedItems[i].ToString();
                    //string[] words = str.Split(":");
                    //int no = int.Parse(words[0]);
                    deviceFormList[i].RecStart(str);
                }
            }
        }

        // 録音終了
        private void stopButton_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedItems.Count > 0)
            {
                recButtonControl(false);

                StatusChange(STATUS_NONE);

                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                {
                    string str = listBox1.SelectedItems[i].ToString();
                    //string[] words = str.Split(":");
                    //int no = int.Parse(words[0]);
                    int no = deviceNameList.IndexOf(str);
                    deviceFormList[no].RecStop();
                }
            }
        }

        // iCare接続確認
        private void deviceCheckButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            //richTextBox1.Text += AsioOut.GetDriverNames() + "\n";
            CheckDevice();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            StatusChange(STATUS_NONE);
            richTextBox1.Text = "";
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                richTextBox1.Text += listBox1.SelectedItems[i].ToString() + "\n";

                deviceOpen(listBox1.SelectedItems[i].ToString());



            }

        }


        private void deviceOpen(string name)
        {
            if (deviceNameList.IndexOf(name) == -1)
            {
                this.Location = new Point(0, 0);

                RecordForm f = new RecordForm();

                int no = deviceNameList.Count;
                deviceNameList.Add(name);
                deviceFormList.Add(f);
                f.SetTitle(name);
                f.Owner = this;
                f.Show();
                f.Left = 0 + (no % 4) * f.Width;
                f.Top = this.Height + (no / 4) * f.Height;
            }
        }

        public void deviceClose(string name)
        {
            int no = deviceNameList.IndexOf(name);
            if (no != -1)
            {
                RecordForm f = deviceFormList[no];

                deviceNameList.Remove(name);
                deviceFormList.Remove(f);
                f.SetTitle(name);
                f.Show();
                f.Left = 0 + (no % 4) * f.Width;
                f.Top = this.Height + (no / 4) * f.Height;
            }
        }

        private void closeAllDevice()
        {
            foreach (RecordForm f in deviceFormList)
            {
                f.Close();
            }
            deviceFormList.Clear();
            deviceNameList.Clear();
        }
        private void plotView1_Click(object sender, EventArgs e)
        {

        }
    }
}