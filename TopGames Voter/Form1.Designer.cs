namespace TopGames_Voter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            tbServerUrl = new TextBox();
            niTrayIcon = new NotifyIcon(components);
            btnCloseToTray = new Button();
            timerVote = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 0;
            label1.Text = "Server URL:";
            // 
            // tbServerUrl
            // 
            tbServerUrl.Location = new Point(12, 27);
            tbServerUrl.Name = "tbServerUrl";
            tbServerUrl.Size = new Size(342, 23);
            tbServerUrl.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            niTrayIcon.Text = "notifyIcon1";
            niTrayIcon.Visible = true;
            // 
            // btnCloseToTray
            // 
            btnCloseToTray.Location = new Point(12, 120);
            btnCloseToTray.Name = "btnCloseToTray";
            btnCloseToTray.Size = new Size(342, 23);
            btnCloseToTray.TabIndex = 5;
            btnCloseToTray.Text = "Close to tray";
            btnCloseToTray.UseVisualStyleBackColor = true;
            btnCloseToTray.Click += btnCloseToTray_Click;
            // 
            // timer1
            // 
            timerVote.Interval = 1000;
            timerVote.Tick += timerVote_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(295, 30);
            label2.TabIndex = 6;
            label2.Text = "If you want to run it autoatically, every time you log in,\r\nput a link to this file into";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(12, 84);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(342, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "%appdata%/Microsoft/Windows/Start Menu/Programs/Startup";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 100);
            label3.Name = "label3";
            label3.Size = new Size(299, 15);
            label3.TabIndex = 8;
            label3.Text = "Remember to delete the link, if you delete the program!";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(366, 153);
            ControlBox = false;
            Controls.Add(label3);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(btnCloseToTray);
            Controls.Add(tbServerUrl);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Top-Games Voter Settings";
            Load += Form1_Load;
            Shown += Form1_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbServerUrl;
        private NotifyIcon niTrayIcon;
        private Button btnCloseToTray;
        private System.Windows.Forms.Timer timerVote;
        private Label label2;
        private LinkLabel linkLabel1;
        private Label label3;
    }
}