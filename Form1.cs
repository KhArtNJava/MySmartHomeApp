using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Onvif;
using System.Threading;
using Timer = System.Timers.Timer;
using System.Timers;

namespace MySmartHomeApp
{
    public partial class Form1 : Form
    {
        public LibVLC _libVLC;
        public MediaPlayer _mp;

        public Form1()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }

            InitializeComponent();

            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mp;
            Load += Form1_Load;

            _mp.EnableMouseInput = false;

        }

        private void videoView1_Click(object sender, EventArgs e)
        {

            OnvifCls.testPzt(serviceUrl);
            Console.WriteLine();
        }

        string serviceUrl = "";

        public int runMe(string rtspUrl, string serviceUrl )
        {
            //Console.WriteLine(rtspUrl);

            this.serviceUrl = serviceUrl;

            //_mp.Play(new Media(_libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));
            //_mp.Play(new Media(_libVLC, "rtsp://admin:q1234567@192.168.31.12:554/onvif1", FromType.FromLocation));
            _mp.Play(new Media(_libVLC, rtspUrl, FromType.FromLocation));

            _mp.EnableMouseInput = false;

            return 0;
        }

        bool alreadyLoaded = false;
        private static Timer loopTimer = new Timer();


        private void Form1_Load(object sender, EventArgs e)
        {
            if (!alreadyLoaded)
            {
                OnvifCls o = new OnvifCls(runMe);
                o.getRtspUrl();
                alreadyLoaded = true;
            }


            loopTimer.Interval = 500; //interval in milliseconds
            loopTimer.Enabled = false;
            loopTimer.Elapsed += loopTimerEvent;
            loopTimer.AutoReset = true;

        }

        private static void loopTimerEvent(Object source, ElapsedEventArgs e)
        {
            //this does whatever you want to happen while clicking on the button
        }

        private void videoView1_MouseDown(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = true;
        }

        private void videoView1_MouseUp(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = false;
        }
    }
}
