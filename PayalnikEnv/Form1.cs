using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayalnikEnv
{
    public partial class Form1 : Form
    {
        int i = 1;
        double napryshzenie = 50;
        bool button_is_active = false;
        public Form1()
        {
            InitializeComponent();
        }
        System.Diagnostics.Stopwatch sw = new Stopwatch();
        private void button1_Click(object sender, EventArgs e)
        {
            timer4.Start();
            sw.Start();
            button_is_active = !button_is_active;
            timer1.Enabled = !timer1.Enabled;
            timer2.Start();
            group1.Enabled = button_is_active;
            group2.Enabled = button_is_active;
            group3.Enabled = button_is_active;
            if (button_is_active)
                labelNapr.Text = napryshzenie.ToString() + " V";
            else
                labelNapr.Text = "0";
            naprTimer1.Enabled = button_is_active;
            naprTimer2.Enabled = button_is_active;
            naprTimer3.Enabled = button_is_active;
        }

        private void StartCooling()
        {
            c1 = 0.025 + coolingTimer1.Interval * (68 - 22) / 1400.0;
            c2 = coolingTimer2.Interval * 68.0 / 600.0;
            timer2.Stop();
            tempPaya = 90;
            isCooling = true;
            napryshzenie = 22;
            coolingTimer1_Tick(this, null);
            coolingTimer1.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Image img = new Bitmap(payalnikBox.Width, payalnikBox.Height);
            Rectangle payalnik = new Rectangle(0, payalnikBox.Height / 4, payalnikBox.Width * 3 / 5, payalnikBox.Height / 2);
            payalnikBox.Image = DrawPayalnik(payalnik, img, 10);
        }

        private Image DrawPayalnik(Rectangle payalnik, Image img, int heatLevel)
        {
            Graphics g = Graphics.FromImage(img);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            SolidBrush lightGrayBrush = new SolidBrush(Color.LightGray);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            g.FillRectangle(blueBrush, payalnik);
            g.FillEllipse(redBrush, payalnik.Right / 2 - 40, payalnik.Y + payalnik.Height / 2 - 10, 20, 20);
            g.FillEllipse(redBrush, payalnik.Right / 2 + 20, payalnik.Y + payalnik.Height / 2 - 10, 20, 20);
            Rectangle miniPayalnik = new Rectangle(
                    payalnik.Right,
                    payalnik.Y + payalnik.Height / 4,
                    payalnik.Width / 3,
                    payalnik.Height / 2
                );
            g.FillRectangle(grayBrush, miniPayalnik);
            Rectangle superMiniPayalnik = new Rectangle(
                    miniPayalnik.Right,
                    miniPayalnik.Y + miniPayalnik.Height / 4,
                    miniPayalnik.Width / 2,
                    miniPayalnik.Height / 2
                );
            g.FillRectangle(lightGrayBrush, superMiniPayalnik);
            Color payalnilEndColor = Color.FromArgb(heatLevel, Color.Red);
            lightGrayBrush.Color = payalnilEndColor;
            g.FillRectangle(lightGrayBrush, superMiniPayalnik);
            return img;
        }
        Random random = new Random(Guid.NewGuid().GetHashCode());
        static bool forward = true;
        static int tempPaya = -2;
        static Datchick dat1 = new Datchick();
        static Datchick dat2 = new Datchick();
        static Datchick dat3 = new Datchick();
        static Datchick dat4 = new Datchick();
        static Datchick dat5 = new Datchick();
        bool isCooling = false;
        double heatingTime = 0;
        int j = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //tempPaya++;
            heatingTime += timer1.Interval;
            Image img = payalnikBox.Image;
            Rectangle payalnik = new Rectangle(0, payalnikBox.Height / 4, payalnikBox.Width * 3 / 5, payalnikBox.Height / 2);
            payalnikBox.Image = DrawPayalnik(payalnik, img, tempPaya+3);
            dat1.NextState(tempPaya);
            label1.Text = dat1.tempiratura.ToString();
            dat2.NextState(tempPaya);
            label2.Text = dat2.tempiratura.ToString();

            dat3.NextState(tempPaya);
            label3.Text = dat3.tempiratura.ToString();

            dat4.NextState(tempPaya);
            label4.Text = dat4.tempiratura.ToString();

            dat5.NextState(tempPaya);
            label5.Text = dat5.tempiratura.ToString();

            
        }

        private void group1_Tick(object sender, EventArgs e)
        {
            label13.Text = dat2.tempiratura.ToString();
            if (dat2.tempiratura >= 90)
            {
                group1.Enabled = false;
                group2.Enabled = false;
                group3.Enabled = false;
                heatingTime = 0;
                sw.Restart();
                timer3.Start();
                timer4.Stop();
                StartCooling();
            }
            group1.Interval = 240;
        }

        private void naprTimer1_Tick(object sender, EventArgs e)
        {
            napryshzenie = 60;
            naprTimer1.Enabled = false;
            labelNapr.Text = napryshzenie.ToString() + " V";
        }

        private void naprTimer2_Tick(object sender, EventArgs e)
        {
            napryshzenie = 65;
            naprTimer2.Enabled = false;
            labelNapr.Text = napryshzenie.ToString() + " V";
        }

        private void naprTimer3_Tick(object sender, EventArgs e)
        {
            napryshzenie = 68;
            naprTimer3.Enabled = false;
            labelNapr.Text = napryshzenie.ToString() + " V";
        }

        private void group2_Tick(object sender, EventArgs e)
        {
            label14.Text = ((dat1.tempiratura + dat3.tempiratura) / 2).ToString();
            if ((dat1.tempiratura + dat3.tempiratura) / 2.0 >= 90)
            {
                group1.Enabled = false;
                group2.Enabled = false;
                group3.Enabled = false;
                heatingTime = 0;
                sw.Restart();
                timer3.Start();
                timer4.Stop();
                StartCooling();
            }
            else
                group2.Interval = 240;
        }

        private void group3_Tick(object sender, EventArgs e)
        {
            label15.Text = ((dat2.tempiratura + dat4.tempiratura + dat5.tempiratura) / 3).ToString();
            if ((dat2.tempiratura + dat4.tempiratura + dat5.tempiratura) / 3.0 >= 90)
            {
                group1.Enabled = false;
                group2.Enabled = false;
                group3.Enabled = false;
                heatingTime = 0;
                sw.Restart();
                timer3.Start();
                timer4.Stop();
                StartCooling();
            }
            group3.Interval = 240;
        }

        double c1 = 0;
        double c2 = 0;
        private void coolingTimer1_Tick(object sender, EventArgs e)
        {
            napryshzenie += c1;
            labelNapr.Text = Convert.ToInt32(napryshzenie).ToString() + " V";
            if (napryshzenie >= 68)
            {
                coolingTimer1.Stop();
                coolingTimer2_Tick(this, null);
                coolingTimer2.Start();
            }
        }
        
        private void coolingTimer2_Tick(object sender, EventArgs e)
        {
            napryshzenie -= c2;
            labelNapr.Text = Convert.ToInt32(napryshzenie).ToString() + " V";
            if (napryshzenie < c2)
            {
                coolingTimer2.Stop();
                j--;
                isCooling = false;
            }
            if (j != 0 && !isCooling)
                StartCooling();
            if (j == 0)
            {
                sw.Stop();
                timer1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tempPaya++;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label8.Visible = true;
            label9.Visible = true;
            label9.Text = (sw.Elapsed.TotalSeconds).ToString("F1") + "c";
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            labelHeatingTime.Text = (sw.Elapsed.TotalSeconds).ToString("F1") + "c";
        }
    }
    public class Datchick
    {
        public int tempiratura;
        private Random random;
        public Datchick()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
            tempiratura = 5;
        }
        public void NextState(int tempIst)
        {
            tempiratura = tempIst - random.Next(5);
            if (tempiratura < 5)
                tempiratura = 5;
        }
    }
}
