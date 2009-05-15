using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Just25
{
    public partial class Form1 : Form
    {
        DateTime start;
        Controller controller;
        string default_icon = "Just25.Resources.clock.ico";

        public Form1()
        {
            InitializeComponent();
            if (Process.GetProcessesByName("ITunes").Length>0)
                controller = new Controller();            
            notifyIcon1.Icon = resource_icon(default_icon);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var total = new TimeSpan(0, 25,0);
            TimeSpan span = total - (DateTime.Now - start);
            if (span.Minutes > 0)
            {
                label1.Text = span.Minutes.ToString();
                notifyIcon1.Icon = resource_icon("Just25.Resources." + ((span.Minutes+1).ToString("00")) + ".ico");
                notifyIcon1.Text = (span.Minutes+1) + " minutes left";
            }
            else
            {
                label1.Text = span.Seconds.ToString();
                if (span.Seconds <= 25)
                {
                    notifyIcon1.Icon = resource_icon("Just25.Resources." + (span.Seconds.ToString("00")) + ".ico");
                    label1.Text = span.Seconds.ToString("00");
                    notifyIcon1.Text = span.Seconds.ToString("00") + " seconds left";
                }
                else
                {
                    notifyIcon1.Text = span.Seconds.ToString("00") + " seconds left";
                    notifyIcon1.Icon = resource_icon("Just25.Resources.01.ico");
                }
                if (span.Seconds == 0)
                {
                    timer1.Stop();
                    start_these(); 
                    MessageBox.Show("Times up!");                    
                    notifyIcon1.Icon = resource_icon(default_icon);
                }
            }
        }
        
        private static Icon resource_icon(string location)
        {
            return new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream(location));
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            start = DateTime.Now;
            timer1.Start();
            kill_these();
        }

        ProcessStartInfo start_info;

        private void kill_these()
        {
            string processName = "TweetDeck";
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                start_info = process.StartInfo;
                start_info.FileName = process.MainModule.FileName;
                process.Close();
            }
            if(controller != null)
                controller.Play();
        }

        private void start_these()
        {
            if (start_info != null)
            {
                Process p = new Process();
                p.StartInfo = start_info;
                p.EnableRaisingEvents = true;
                p.Start();
            }
            if(controller != null)
                controller.Pause();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();

        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start_or_pause();
        }

        private void start_or_pause(){
            if (false)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Enabled = true;
                timer1.Interval = 1000;
                start = DateTime.Now;
                timer1.Start();
                kill_these();
            }
        }
    }
}