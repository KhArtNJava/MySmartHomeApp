﻿using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void videoView1_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //_mp.Play(new Media(_libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));
            _mp.Play(new Media(_libVLC, "rtsp://admin:q1234567@192.168.31.12:554/onvif1", FromType.FromLocation));
        }
    }
}
