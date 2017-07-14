using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FXDebugger
{
    public partial class MainWindow : Window
    {
        bool isPause = false;
        delegate void OutputDataReceive(string s);
        event OutputDataReceive OutputDataReceivedEvent;
        //Process process = new Process();
        //StreamWriter StandardInput;
        public MainWindow()
        {
            OutputDataReceivedEvent += OutputDataReceived;

            Task.Run(() =>
            {
                Thread.Sleep(300);
                while (true)
                {
                    string s = Console.ReadLine();
                    if (String.IsNullOrEmpty(s)) return;
                    OutputDataReceivedEvent.Invoke(s);
                }
            });
            #region 奇怪的注释
            //DispatcherTimer timer = new DispatcherTimer()
            //{
            //    Interval = TimeSpan.FromSeconds(0.1)
            //};
            //timer.Tick += GetMessage;
            //timer.Start();
            ////System.Threading.Thread.Sleep(1000);
            ////Console.WriteLine("?????");
            //int i = Convert.ToInt32(Console.ReadLine());
            ////Console.WriteLine("?????");
            //Console.WriteLine(i);
            //process = Process.GetProcessById(i);
            ////Console.WriteLine("?????");
            //process.StartInfo.RedirectStandardOutput = true;
            //process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.RedirectStandardInput = true;
            ////Console.WriteLine("?????");
            ////StandardInput = process.StandardInput;
            //Console.WriteLine("?????");
            //process.OutputDataReceived += OutputDataReceived;
            //Console.WriteLine("?????");
            //process.ErrorDataReceived += ErrorDataReceived;
            //Console.WriteLine("?????");
            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();
            //Console.WriteLine("?????");
            #endregion
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("continue");
            Print("continue");
            isPause = false;
        }

        void OutputDataReceived(string s)
        {
            if (s == "pause")
            {
                isPause = true;
            }
            Print(s);
        }

        void Print(string s)
        {
            Output.Dispatcher.BeginInvoke(new Action(() =>
            {
                Output.Text = Output.Text + s + Environment.NewLine;
                Output.ScrollToEnd();
            }));
            
        }

        void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {

        }



        #region Asixa的奇怪的东西

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Asixa.About window = new Asixa.About();
            window.Show();
        }

        #endregion
    }
}
