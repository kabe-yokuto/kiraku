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
//using NAudio.CoreAudio;
using NAudio.Wave;
using System.Transactions;

using System.Diagnostics;
using System.Windows.Shapes;
using System.Diagnostics.Tracing;
using System.Xml.Linq;

using FFmpeg.AutoGen;

namespace WaveRecMic
{
    public partial class RecordForm : Form
    {
        WaveInEvent waveIn; // = new WaveInEvent;
        WaveFileWriter waveWriter;  // = new WaveFileWriter;
        
        WaveOutEvent waveOut;

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

        int rate = 16000;

        public RecordForm()
        {
            InitializeComponent();

            InitPlot();

            recButtonControl(false);

        }

        public void Form2_Shown(object sender, EventArgs e)
        {

            int width = this.Size.Width;
            int height = this.Size.Height;

            plotView1.Size = new Size(width - 32, height * 3 / 4);
            plotView1.Left = 8;


            recButton.Left = Width / 5 - recButton.Size.Width / 2;
            recButton.Top = height - recButton.Size.Height - 64;
            stopButton.Left = Width * 2 / 5 - stopButton.Size.Width / 2;
            stopButton.Top = height - stopButton.Size.Height - 64;
            normalPlayButton.Left = Width * 3 / 5 - normalPlayButton.Size.Width / 2;
            normalPlayButton.Top = height - normalPlayButton.Size.Height - 64;
            abnormalPlayButton.Left = Width * 4 / 5 - abnormalPlayButton.Size.Width / 2;
            abnormalPlayButton.Top = height - abnormalPlayButton.Size.Height - 64;


            this.Refresh();

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
            waveIn.WaveFormat = new WaveFormat(rate, 1);

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
            ((StartForm)this.Owner).recButtonCheck();


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

            if (_recorded.Count == rate / 10)
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
        /*
        private void ProcessSample(float sample)
        {
            _recorded.Add(sample);

            if (_recorded.Count % 100 == 0)
            {
                var points = _recorded.Select((v, index) =>
                        new DataPoint((double)index, v)
                    ).ToList();

                _lineSeries.Points.Clear();
                _lineSeries.Points.AddRange(points);

                plotView1.InvalidatePlot(true);

                //_recorded.Clear();
            }

            if( _recorded.Count>=rate*2)
            {
                _recorded.RemoveRange(0, 100);
            }
        }
        */

        void InitPlot()
        {
            var model = new PlotModel();

            model.Axes.Add(new LinearAxis { Minimum = -1.5, Maximum = 1.5, Position = AxisPosition.Left, });
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

                ExecuteForm loadingdialog = new ExecuteForm();
                loadingdialog.TopMost = true;
                loadingdialog.Show();

                System.Diagnostics.Debug.WriteLine("Python start....");

                // python.exeのプロセスの終了を待ちます。
                //process.WaitForExit(60 * 10 * 1000);　//とりあえず適当に１０分でタイムアウトとしておく
                int t = 0;
                while (process.WaitForExit(100) == false)
                {
                    loadingdialog.Draw();
                    //System.Diagnostics.Debug.WriteLine("wait" + (t++));
                }
                System.Diagnostics.Debug.WriteLine("....Python end");

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

        private void PlayMemoryStream(string filename)
        {
            /*
            using (WaveOutEvent waveOut = new WaveOutEvent())
            {
                bool play = true;
                using (AudioFileReader reader = new AudioFileReader(filename))
                {
                    Debug.WriteLine(filename+ " Length=" + reader.Length);

                    using (WaveChannel32 pcm = new WaveChannel32(reader))
                    {
                        waveOut.Init(pcm);
                        waveOut.Play();

                        waveOut.PlaybackStopped += (s, args) =>
                        {
                            // ここで何か処理を行うこともできます
                            Debug.WriteLine("Playback stopped");
                           play = false;
                        };

       
                    }
                }
                //while (waveOut.PlaybackState == PlaybackState.Playing)
                while (play)
                {
                    Thread.Sleep(100);
                }
                waveOut.Dispose();
            }
            */

            /*            
            // WAVファイルをメモリに読み込む
            AudioFileReader reader = new AudioFileReader(fileName);

            // 再生ストリームを作成
            AudioStream stream = reader.CreateStream();

            // 再生する
            stream.Start();

            // 波形表示用のコントロールに反映する処理
            void UpdatePlot()
            {
                // 再生ストリームからデータを取得
                byte[] data = stream.GetData(0, 1024);

                // 波形表示用のコントロールに反映
                PlotModel model = plotView1.Model;
                model.Series.Clear();
                model.Series.Add(new LineSeries(data));
                plotView1.Model = model;
            }

            // 再生の終了を判定する処理
            void CheckPlaybackEnd()
            {
                if (!stream.IsPlaying)
                {
                    // 再生が終了した
                    stream.Dispose();
                    stream = null;
                    timer.Stop();
                }
            }

            // 一定間隔でUpdatePlot()とCheckPlaybackEnd()を呼び出す
            Timer timer = new Timer(1000 / 16000, UpdatePlot);
            timer.Tick += CheckPlaybackEnd;
            timer.Start();
            */

            // WAVファイルの読み込み
            WaveStream waveStream = new WaveFileReader(filename);

            // メモリに読み込む
            MemoryStream memoryStream = new MemoryStream();
            WaveFileWriter.WriteWavFileToStream(memoryStream, waveStream);

            // 再生
            PlayWavFromMemory(memoryStream);

            // 波形表示
            PlotWaveform(memoryStream);
        }

        void PlayWavFromMemory(MemoryStream memoryStream)
        {
            using (WaveStream waveStream = new WaveFileReader(new MemoryStream(memoryStream.ToArray())))
            using (WaveOutEvent waveOut = new WaveOutEvent())
            {
                waveOut.Init(waveStream);
                waveOut.Play();
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

       void PlotWaveform(MemoryStream memoryStream)
        {
            // OxyPlotの初期化
            var plotModel = new PlotModel();
            var lineSeries = new LineSeries();

            // バイトデータを取得
            byte[] waveData = memoryStream.GetBuffer();

            // 波形をOxyPlotのLineSeriesに追加
            for (int i = 0; i < waveData.Length; i += 2)
            {
                short sample = BitConverter.ToInt16(waveData, i);
                lineSeries.Points.Add(new DataPoint(i / 2, sample));
            }

            // プロットにLineSeriesを追加
            plotModel.Series.Add(lineSeries);

            // グラフを表示
        
            plotView1.Model = plotModel;
      
        }

        private void normalPlayButton_Click(object sender, EventArgs e)
        {
            PlayMemoryStream("normal.wav");
        }

        private void abnormalPlayButton_Click(object sender, EventArgs e)
        {
            PlayMemoryStream("abnormal.wav");
        }
    }

}
