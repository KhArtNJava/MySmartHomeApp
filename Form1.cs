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

            //_mp.Play(new Media(_libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));




        }

        string serviceUrl = "";

        public int runMe(string rtspUrl, string serviceUrl)
        {
            //Console.WriteLine(rtspUrl);

            this.serviceUrl = serviceUrl;

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


            loopTimer.Interval = 100; //interval in milliseconds
            loopTimer.Enabled = false;
            loopTimer.Elapsed += loopTimerEvent;
            loopTimer.AutoReset = true;

        }

        void loopTimerEvent(Object source, ElapsedEventArgs e)
        {
            if (mouseDown)
            {
                float x = 0;
                float y = 0;
                if (clickX < senderMiddleX)
                {
                    x = -2f;
                }
                else if (clickX > senderMiddleX * 2)
                {
                    x = 2f;
                }

                if (clickY < senderMiddleY)
                {
                    y = 2f;
                }
                else if (clickY > senderMiddleY * 2)
                {
                    y = -2f;
                }

                OnvifCls.sendPtzMove(serviceUrl, x, y);
            }

        }

        int senderX;
        int senderY;
        int senderMiddleX;
        int senderMiddleY;

        int clickX;
        int clickY;

        bool mouseDown = false;

        void videoView1_MouseDown(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = true;

            mouseDown = true;

            senderX = ((MyControl)sender).Width;
            senderY = ((MyControl)sender).Height;
            senderMiddleX = senderX / 3;
            senderMiddleY = senderY / 3;

            clickX = e.X;
            clickY = e.Y;

        }

        private void videoView1_MouseUp(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = false;
            mouseDown = false;
        }

    }
}
