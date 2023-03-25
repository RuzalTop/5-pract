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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private ProgressBar progressBar2;
        private Button buttonValve1, buttonValve2;
        private TextBox textBoxLevel1, textBoxLevel2;
        private bool valve1Open = false;
        private bool valve2Open = false;
        private const double F1 = 60;
        private const double F2 = 50;
        private const double F3 = 50;
        private const double F4 = 75;
        private const int T1 = 11;
        private const int T2 = 15;

        private int mixer1TimeRemaining = 100;
        private int mixer2TimeRemaining = 100;
        private DateTime startTime1 = DateTime.MinValue;
        private DateTime startTime2 = DateTime.MinValue;


        private void timer1_Tick(object sender, EventArgs e)
        {
            // Реализация логики обновления состояния и визуализации
            if (valve1Open)
            {
                // Обновление progressBar1 и textBoxLevel1 на основе текущего состояния клапана 1
                int newLevel1 = progressBar1.Value + (int)(F1 / 10);
                progressBar1.Value = newLevel1 > progressBar1.Maximum ? progressBar1.Maximum : newLevel1;
                textBoxLevel1.Text = $"{progressBar1.Value}%";

                // Обновление времени работы мешалки 1
                if (mixer1TimeRemaining > 0)
                {
                    if (startTime1 == DateTime.MinValue)
                    {
                        // Если это начало процесса, сохраните текущее время
                        startTime1 = DateTime.Now;
                    }
                    mixer1TimeRemaining--;
                    if (mixer1TimeRemaining == 0)
                    {
                        
                    }
                }
                else
                {
                    // Если процесс закончился, сбросьте время начала
                    startTime1 = DateTime.MinValue;
                }
            }

            if (valve2Open)
            {
                // Обновление progressBar2 и textBoxLevel2 на основе текущего состояния клапана 2
                int newLevel2 = progressBar2.Value + (int)(F2 / 10);
                progressBar2.Value = newLevel2 > progressBar2.Maximum ? progressBar2.Maximum : newLevel2;
                textBoxLevel2.Text = $"{progressBar2.Value}%";

                // Обновление времени работы мешалки 2
                if (mixer2TimeRemaining > 0)
                {
                    if (startTime2 == DateTime.MinValue)
                    {
                        // Если это начало процесса, сохраните текущее время
                        startTime2 = DateTime.Now;
                    }
                    mixer2TimeRemaining--;
                    if (mixer2TimeRemaining == 0)
                    {
                        
                    }
                }
                else
                {
                    // Если процесс закончился, сбросьте время начала
                    startTime2 = DateTime.MinValue;
                }
            }

            // Остановить таймер, когда progressBar заполнен
            if (progressBar1.Value == progressBar1.Maximum && progressBar2.Value == progressBar2.Maximum)
            {
                timer1.Enabled = false;
                Debug.WriteLine("Timer stopped.");
            }

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            // Реализация логики для клапана 1
            valve1Open = !valve1Open;
            button1.BackColor = valve1Open ? Color.Green : Color.Red;
            if (!valve1Open)
            {
                // Сбросьте время начала, когда клапан закрывается
                startTime1 = DateTime.MinValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Реализация логики для клапана 1
            valve2Open = !valve2Open;
            button2.BackColor = valve1Open ? Color.Green : Color.Red;
            if (!valve2Open)
            {
                // Сбросьте время начала, когда клапан закрывается
                startTime2 = DateTime.MinValue;
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Определить время работы каждой мешалки
            TimeSpan mixer1Time = mixer1TimeRemaining > 0 ? DateTime.Now - startTime1 : TimeSpan.Zero;
            TimeSpan mixer2Time = mixer2TimeRemaining > 0 ? DateTime.Now - startTime2 : TimeSpan.Zero;

            // Отобразить время работы каждой мешалки в новом окне сообщения
            string message = $"Mixer 1 time: {mixer1Time}\nMixer 2 time: {mixer2Time}";
            MessageBox.Show(message, "Work Time");
            // Начать процесс
            mixer1TimeRemaining = 60;
            mixer2TimeRemaining = 120;
            valve1Open = true;
            valve2Open = true;
            startTime1 = DateTime.MinValue;
            startTime2 = DateTime.MinValue;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            textBoxLevel1.Text = "0%";
            textBoxLevel2.Text = "0%";
            button1.BackColor = Color.Green;
            button2.BackColor = Color.Green;
            button3.Enabled = false;

            // Запустить таймер
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void InitializeCustomComponents()
        {
            // Panel
            Panel panel = new Panel
            {
                Size = new Size(600, 400),
                Location = new Point(10, 10),
                BackColor = Color.LightGray
            };
            Controls.Add(panel);

            // ProgressBar
            progressBar1 = new ProgressBar
            {
                Location = new Point(200, 100),
                Height = 150,
                 
                Minimum = 0,
                Maximum = 100
            };
            panel.Controls.Add(progressBar1);

            progressBar2 = new ProgressBar
            {
                Location = new Point(400, 100),
                Height = 150,
                
                Minimum = 0,
                Maximum = 100
            };
            panel.Controls.Add(progressBar2);

            // Button
            buttonValve1 = new Button
            {
                Text = "Клапан 1",
                Location = new Point(100, 300)
            };
            buttonValve1.Click += button1_Click;
            panel.Controls.Add(buttonValve1);

            buttonValve2 = new Button
            {
                Text = "Клапан 2",
                Location = new Point(300, 300)
            };
            buttonValve2.Click += button2_Click;
            panel.Controls.Add(buttonValve2);

            // TextBox
            textBoxLevel1 = new TextBox
            {
                Location = new Point(100, 275),
                Size = new Size(100, 20),
                ReadOnly = true
            };
            panel.Controls.Add(textBoxLevel1);

            textBoxLevel2 = new TextBox
            {
                Location = new Point(300, 275),
                Size = new Size(100, 20),
                ReadOnly = true
            };
            panel.Controls.Add(textBoxLevel2);

            // Label
            Label labelLevel1 = new Label
            {
                Text = "Уровень 1",
                Location = new Point(200, 250)
            };
            panel.Controls.Add(labelLevel1);

            Label labelLevel2 = new Label
            {
                Text = "Уровень 2",
                Location = new Point(300, 250)
            };
            panel.Controls.Add(labelLevel2);

            // Label F1
            Label labelF1 = new Label
            {
                Text = $"Расход 1: {F1} л/сек",
                Location = new Point(10, 100)
            };
            panel.Controls.Add(labelF1);

            // Label F2
            Label labelF2 = new Label
            {
                Text = $"Расход 2: {F2} л/сек",
                Location = new Point(10, 130)
            };
            panel.Controls.Add(labelF2);

            // Label F3
            Label labelF3 = new Label
            {
                Text = $"Расход 3: {F3} л/сек",
                Location = new Point(10, 160)
            };
            panel.Controls.Add(labelF3);

            // Label F4
            Label labelF4 = new Label
            {
                Text = $"Расход 4: {F4} л/сек",
                Location = new Point(10, 190)
            };
            panel.Controls.Add(labelF4);

            // Label F4
            Label labelT1 = new Label
            {
                Text = $"Время 1: {T1} сек",
                Location = new Point(10, 220)
            };
            panel.Controls.Add(labelT1);

            // Label Mixer2 Time
            Label labelMixer2Time = new Label
            {
                Text = $"Время 1: {T2} сек",
                Location = new Point(10, 250)
            };
            panel.Controls.Add(labelMixer2Time);



            // Timer
            timer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            timer.Tick += timer1_Tick;
    }
    }
}
