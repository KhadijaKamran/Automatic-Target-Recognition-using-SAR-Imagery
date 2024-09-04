using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace terrasar
{
    public partial class TerraSAR : Form
    {
        public TerraSAR()
        {
            InitializeComponent();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerX.Start();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelSlide_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TimerX_Tick(object sender, EventArgs e)
        {
            panelSlide.Width += 3;
            if (panelSlide.Width >= 1166)
            {
                timerX.Stop();
                Form2 f2 = new Form2();
                this.Hide();
                f2.Show();
            }
        }
    }
}
