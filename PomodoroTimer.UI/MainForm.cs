using System;
using System.Drawing;
using System.Windows.Forms;

namespace PomodoroTimer.UI
{
    public class MainForm : Form
    {
        private Label lblTimer;
        private Button btnStartPause;
        private Button btnReset;
        private System.Windows.Forms.Timer timer;

        private int totalSeconds = 25 * 60;
        private bool isRunning = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Pomodoro Timer";
            ClientSize = new Size(300, 180);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            lblTimer = new Label
            {
                Text = FormatTime(totalSeconds),
                Font = new Font("Segoe UI", 24F),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };
            Controls.Add(lblTimer);

            btnStartPause = new Button
            {
                Text = "Start",
                Dock = DockStyle.Left,
                Width = 100
            };
            btnStartPause.Click += BtnStartPause_Click;
            Controls.Add(btnStartPause);

            btnReset = new Button
            {
                Text = "Reset",
                Dock = DockStyle.Right,
                Width = 100
            };
            btnReset.Click += BtnReset_Click;
            Controls.Add(btnReset);

            
            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
        }

        private void BtnStartPause_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                timer.Start();
                btnStartPause.Text = "Pause";
            }
            else
            {
                timer.Stop();
                btnStartPause.Text = "Start";
            }
            isRunning = !isRunning;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            timer.Stop();
            totalSeconds = 25 * 60;
            lblTimer.Text = FormatTime(totalSeconds);
            btnStartPause.Text = "Start";
            isRunning = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (totalSeconds > 0)
            {
                totalSeconds--;
                lblTimer.Text = FormatTime(totalSeconds);
            }
            else
            {
                timer.Stop();
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private static string FormatTime(int secs)
        {
            int m = secs / 60;
            int s = secs % 60;
            return $"{m:D2}:{s:D2}";
        }
    }
}
