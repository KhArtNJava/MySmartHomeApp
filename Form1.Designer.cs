namespace MySmartHomeApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.SuspendLayout();

            // myControl
            // 
            this.myControl = new MyControl();
            this.myControl.BackColor = System.Drawing.Color.Transparent;
            this.myControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myControl.Location = new System.Drawing.Point(0, 0);
            this.myControl.Name = "myControl";
            this.myControl.Size = new System.Drawing.Size(800, 450);
            this.myControl.TabIndex = 1;
            this.Controls.Add(this.myControl);
            this.myControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.videoView1_MouseDown);
            this.myControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.videoView1_MouseUp);

            // 
            // videoView1
            // 
            this.videoView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(2, 3);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(797, 449);
            this.videoView1.TabIndex = 0;
            this.videoView1.Text = "videoView1";
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.videoView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.ResumeLayout(false);

          

        }

        #endregion

        private LibVLCSharp.WinForms.VideoView videoView1;
        private MyControl myControl;
    }
}

