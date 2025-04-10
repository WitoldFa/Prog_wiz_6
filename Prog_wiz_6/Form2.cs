using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Prog_wiz_6.Form3;

namespace Prog_wiz_6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private int[,] plansza; // 0 = puste, 1 = Dydelf, 2 = Krokodyl, 3 = Szop
        private Button[,] przyciski;
        private int pozostaloDydelfow;
        private int czasPozostaly;
        private Timer timer = new Timer();


        private void Form2_Load(object sender, EventArgs e)
        {
            
            int w = UstawieniaGry.wysokosc;
            int k = UstawieniaGry.szerokosc;
            plansza = new int[w, k];
            przyciski = new Button[w, k];
            pozostaloDydelfow = UstawieniaGry.dydelf;
            czasPozostaly = UstawieniaGry.czasgry;

            GenerujZwierzatka(w, k);
            GenerujPlansze(w, k);
            UruchomTimer();
        }
        private void GenerujZwierzatka(int w, int k)
        {
            Random rand = new Random();
            

            void Losuj(int ile, int kod)
            {
                int count = 0;
                while (count < ile)
                {
                    int x = rand.Next(w);
                    int y = rand.Next(k);

                    if (plansza[x, y] == 0)
                    {
                        plansza[x, y] = kod;
                        count++;
                    }
                }
            }

            Losuj(UstawieniaGry.dydelf, 1);   // Dydelf
            Losuj(UstawieniaGry.krokodyl, 2);  // Krokodyl
            Losuj(UstawieniaGry.szop, 3);     // Szop
        }
        private void GenerujPlansze(int w, int k)
        {
            tlpPlansza.RowCount = w;
            tlpPlansza.ColumnCount = k;
            tlpPlansza.Controls.Clear();
            tlpPlansza.RowStyles.Clear();
            tlpPlansza.ColumnStyles.Clear();

            for (int i = 0; i < w; i++)
                tlpPlansza.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / w));
            for (int j = 0; j < k; j++)
                tlpPlansza.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / k));

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.Tag = new Point(i, j);
                    btn.Text = ""; // można dać np. „?”
                    btn.BackColor = Color.Gray;
                    btn.Click += Pole_Click;
                    przyciski[i, j] = btn;
                    tlpPlansza.Controls.Add(btn, j, i);
                }
            }
        }
        private void Pole_Click(object sender, EventArgs e)
        {
            Button klikniety = sender as Button;
            Point p = (Point)klikniety.Tag;
            int x = p.X;
            int y = p.Y;

            switch (plansza[x, y])
            {
                case 0: // puste
                    klikniety.BackColor = Color.White;
                    klikniety.Enabled = false;
                    break;

                case 1: // Dydelf
                    klikniety.BackColor = Color.Green;
                    klikniety.Text = "D";
                    plansza[x, y] = -1;
                    pozostaloDydelfow--;
                    if (pozostaloDydelfow == 0)
                        Wygrana();
                    break;

                case 2: // Krokodyl
                    klikniety.BackColor = Color.Red;
                    klikniety.Text = "K";
                    plansza[x, y] = -2;
                    timer.Stop();
                    MessageBox.Show("Trafiłeś na Krokodyla! Przegrałeś!");
                    this.Close();
                    break;

                case 3: // Szop
                    klikniety.BackColor = Color.Blue;
                    klikniety.Text = "S";
                    ZakonczSzopa(x, y);
                    break;
            }

            klikniety.Enabled = false;
        }
        private async void ZakonczSzopa(int x, int y)
        {
            await Task.Delay(2000);
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < plansza.GetLength(0) && ny >= 0 && ny < plansza.GetLength(1))
                    {
                        przyciski[nx, ny].Enabled = true;
                        przyciski[nx, ny].BackColor = Color.Gray;
                        przyciski[nx, ny].Text = ""; 
                    }
                }
            }
        }
        private void UruchomTimer()
        {
            lblTimer.Text = $"Pozostały czas: {czasPozostaly}s";
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            czasPozostaly--;
            lblTimer.Text = $"Pozostały czas: {czasPozostaly}s";

            if (czasPozostaly <= 0)
            {
                timer.Stop();
                MessageBox.Show("Czas minął! Przegrałeś.");
                this.Close();
            }
        }
        private void Wygrana()
        {
            timer.Stop();
            MessageBox.Show("Gratulacje! Znalazłeś wszystkie Dydelfy!");
            this.Close();
        }


    }
}
