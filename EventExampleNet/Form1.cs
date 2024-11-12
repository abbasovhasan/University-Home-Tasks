using EventExampleNet.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventExampleNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Vehicle vehicle = new Vehicle();
        Timer timer = new Timer();
        bool start = false;
        int speed = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            vehicle.Marka = "Audi";
            vehicle.Model = "A4";
            vehicle.Hiz = 1;
            vehicle.SpeedEvent += Vehicle_SpeedEvent;

            timer.Interval = 50;
            timer.Tick += Timer_Tick;
             
        }

        private void Vehicle_SpeedEvent(int vites, System.Drawing.Color color)
        {
            circularProgressBar1.SuperscriptText = vites.ToString();
            circularProgressBar1.ProgressColor = color;
            this.Text = $"Hız: {speed} - Vites: {vites}";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (start)
            {
                speed++;
            }
            else
            {
                speed--;
            }


            if (speed > 0 && speed <= 300)
            {
                vehicle.Hiz = speed;
                circularProgressBar1.Value = speed;
                circularProgressBar1.Text = speed.ToString();
            }

            if (speed == 0)
            {
                vehicle.Start = false;
                speed = 1;
                circularProgressBar1.Value = 0;
                circularProgressBar1.Text = "0";
                circularProgressBar1.SuperscriptText = "0";
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            // yukarı ok tuşundan parmağınızı kaldırdığınızda çalışır ( sol işaret parmağı olmalıdır)
            if (e.KeyCode == Keys.Up)
            {
                start = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                start = true;
                timer.Start();

                if (!vehicle.Start)
                {
                    try
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer($@"{Environment.CurrentDirectory}\..\..\sound\Engine_Rev_Continuous.wav");
                        player.Play(); // Use Play instead of PlaySync for non-blocking sound playback
                        vehicle.Start = true;
                        circularProgressBar1.ProgressColor = Color.Pink;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error playing sound: {ex.Message}", "Sound Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


    }
}
