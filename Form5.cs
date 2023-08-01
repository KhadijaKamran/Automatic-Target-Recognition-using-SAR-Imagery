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

namespace terrasar
{
    public partial class Form5 : Form
    {
        public string fileName { get; set; }
        public Form5()
        {
            InitializeComponent();
            fileName = "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

        }
        private void Vid_Enter(object sender, EventArgs e)
        {

        }

        private void Vid_Enter_1(object sender, EventArgs e)
        {
        }

        private void Vid_Enter_2(object sender, EventArgs e)
        {

        }

        private void AxWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3();
            f.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4();
            f.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)| *.mp3; *.wav; *.mp4; *.mov; *.wmv; *.mpg | all files | *.* ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                vid.URL = openFileDialog1.FileName;
                fileName = openFileDialog1.FileName;
                vid.Ctlcontrols.play();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            convertVideo(fileName);
            vid.URL = (fileName.Substring(0, fileName.Length - 4) + "_detected.jpg");
        }

        private void convertVideo(string f)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            string a = "E:\\FYP\\terraSAR_vid.py";
            string b = f;
            start.Arguments = string.Format("{0} {1}", a, b);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            Process process = Process.Start(start);
            
        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


