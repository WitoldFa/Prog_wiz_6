using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prog_wiz_6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        public static class UstawieniaGry
        {
            public static int wysokosc { get; set; } = 3;
            public static int szerokosc { get; set; } = 3;
            public static int dydelf { get; set; } = 1;
            public static int krokodyl { get; set; } = 0;
            public static int szop { get; set; } = 3;
            public static int czasgry { get; set; } = 10;
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UstawieniaGry.wysokosc= (int)numericUpDown1.Value;
            UstawieniaGry.szerokosc= (int)numericUpDown2.Value;
            UstawieniaGry.dydelf= (int)numericUpDown3.Value;
            UstawieniaGry.krokodyl= (int)numericUpDown4.Value;
            UstawieniaGry.szop = (int)numericUpDown5.Value;
            UstawieniaGry.czasgry = (int)numericUpDown6.Value;
             
            this.Close();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
