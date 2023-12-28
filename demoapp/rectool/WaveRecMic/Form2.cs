﻿using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Transactions;

using System.Diagnostics;
using System.Windows.Shapes;
using System.Diagnostics.Tracing;
using System.Xml.Linq;

using FFmpeg.AutoGen;

namespace WaveRecMic
{
    public partial class Form2 : Form
    {
        WaveInEvent waveIn; // = new WaveInEvent;
        WaveFileWriter waveWriter;  // = new WaveFileWriter;

        string formName;

        string fileName;

        public bool recFlag = false;

        public string judge_string = "";

        string folderName1 = "icare";
        string folderName2;
        string startTime;
        string endTime;
        string path;

        int deviceNo;

        public Form2()
        {
            InitializeComponent();

            InitPlot();

            recButtonControl(false);

        }

        public void recButtonControl(bool flag)
        {
            recFlag = flag;
            recButton.Enabled = !flag;
            stopButton.Enabled = flag;
        }

        public void SetTitle(string titleText)
        {
            formName = titleText;
            this.Text = titleText;
        }

        // 録音の開始
        public void RecStart(string name)
        {
            if (recFlag) return;

            recButtonControl(true);

            string[] words = name.Split(":");
            int no = int.Parse(words[0]);

            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = no;
            waveIn.WaveFormat = new WaveFormat(16000, 1);

            deviceNo = no;

            DateTime dt = DateTime.Now;
            folderName1 = "icare";
            folderName2 = dt.ToString("yyyyMMdd");
            startTime = dt.ToString("HHmmss");
            endTime = dt.ToString("HHmmss");

            // icareフォルダが無かったら作成する
            path = @"c:\" + folderName1;
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
            }
            path += @"\" + folderName2;
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
            }

            fileName = path + @"\" + startTime + "_" + endTime + "_" + deviceNo + ".wav";
            waveWriter = new WaveFileWriter(fileName, waveIn.WaveFormat);

            waveIn.DataAvailable += (_, ee) =>
            {
                waveWriter.Write(ee.Buffer, 0, ee.BytesRecorded);
                waveWriter.Flush();



                // 32bitで最大値1.0fにする
                for (int index = 0; index < ee.BytesRecorded; index += 2)
                {
                    short sample = (short)((ee.Buffer[index + 1] << 8) | ee.Buffer[index + 0]);

                    float sample32 = sample / 32768f;
                    ProcessSample(sample32);
                }

            };
            waveIn.RecordingStopped += (_, __) =>
            {
                //waveWriter.Flush();
            };

            try
            {
                waveIn.StartRecording();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }


        // 録音の停止
        public void RecStop()
        {
            if (!recFlag) return;

            recButtonControl(false);
            ((Form1)this.Owner).recButtonCheck();


            waveIn.StopRecording();
            waveIn.Dispose();
            //waveIn[i] = null;

            waveWriter.Close();
            //waveWriter[i] = null;

            DateTime dt = DateTime.Now;
            endTime = dt.ToString("HHmmss");

            string oldFileName = fileName;
            fileName = path + @"\" + startTime + "_" + endTime + "_" + deviceNo + ".wav";
            File.Move(oldFileName, fileName);

        }

        List<float> _recorded = new List<float>(); // 音声データ

        private LineSeries _lineSeries = new LineSeries();
        // OxyPlotのためのモデルとコントローラー
        public PlotModel oxyModel { get; } = new PlotModel();
        // 軸の設定
        public OxyPlot.Axes.LinearAxis AxisX { get; } = new OxyPlot.Axes.LinearAxis();
        public OxyPlot.Axes.LinearAxis AxisY { get; } = new OxyPlot.Axes.LinearAxis();

        private void ProcessSample(float sample)
        {
            _recorded.Add(sample);

            if (_recorded.Count == 1024)
            {
                var points = _recorded.Select((v, index) =>
                        new DataPoint((double)index, v)
                    ).ToList();

                _lineSeries.Points.Clear();
                _lineSeries.Points.AddRange(points);

                plotView1.InvalidatePlot(true);

                _recorded.Clear();
            }

        }
        void InitPlot()
        {
            var model = new PlotModel();

            model.Axes.Add(new LinearAxis { Minimum = -0.2, Maximum = 0.2, Position = AxisPosition.Left, });
            model.Series.Add(_lineSeries);
            plotView1.Model = model;
        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecStart(formName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecStop();
            judge();
        }

        // 判定する
        //private void judge_Click(object sender, EventArgs e)
        private void judge()
        {
            /*
            //Pythonプログラムを実行し、戻ってきた結果をテキストボックスに出力
            foreach (string line in PythonCall("biosonoColabTest.py", fileName))
            {
                Console.WriteLine(line);
            }
            */

            /*
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "python.exe";
            psInfo.Arguments = "biosonoColabTest.py";

            System.Diagnostics.Process.Start(psInfo);
            */

            // Pythonの実行ファイル名
            string PythonExe = "python.exe";

            // Pythonのアプリ―ケーション
            // C#の実行ファイルと同じフォルダかフルパスで指定します。
            string PythonApp = "biosonoColabTest.py";
            //string PythonApp = "hello.py";

            // ファイルネーム　トレーニングするか？(-Tでトレーニング)
            PythonApp += " " + fileName + " -N";

            // python.exeの実行結果を読み込む変数
            var result = string.Empty;

            // python.exeのプロセスを設定します。
            using (var process = new Process
            {
                // Process.Startメソッドに渡すプロパティを設定します。
                StartInfo = new ProcessStartInfo(PythonExe)
                {
                    // OSのシェルを使用しません。
                    UseShellExecute = false,
                    // pythonのテキスト出力をStandardOutputストリームに出力します。
                    RedirectStandardOutput = true,
                    // python.exeのコマンドライン引数
                    Arguments = PythonApp
                }
            })
            {
                // python.exeのプロセスを起動します。
                bool p = process.Start();

                Form4 loadingdialog = new Form4();
                loadingdialog.TopMost = true;
                loadingdialog.Show();

                System.Diagnostics.Debug.WriteLine("Python start....");

                // python.exeのプロセスの終了を待ちます。
                //process.WaitForExit(60 * 10 * 1000);　//とりあえず適当に１０分でタイムアウトとしておく
                int t = 0;
                while( process.WaitForExit(100) == false)
                {
                    loadingdialog.Draw();
                    //System.Diagnostics.Debug.WriteLine("wait" + (t++));
                }
                System.Diagnostics.Debug.WriteLine("......Python end");

                loadingdialog.Close();

                result = process.StandardOutput.ReadToEnd();

            }

            // python.exeの実行結果を表示します。
            //System.Diagnostics.Debug.WriteLine(result);

            string[] output_lines = result.Split("\r\n");

            // 判定結果
            for (int i = 0; i < output_lines.Length; i++)
            {
                if (output_lines[i].IndexOf("result:") > -1)
                {
                    string[] words = output_lines[i].Split(":");
                    judge_string = words[1];
                    break;
                }
            }

            JudgeForm f = new JudgeForm();

            f.setResult(judge_string);
            f.Owner = this;
            f.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }

}
