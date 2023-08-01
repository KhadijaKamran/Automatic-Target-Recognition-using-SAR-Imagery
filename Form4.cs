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
using System.Runtime.InteropServices;


namespace terrasar
{
    public partial class Form4 : Form
    {
        public string fileName { get; set; }

        public Form4()
        {
            InitializeComponent();
            fileName = "";
            d1.Hide(); c1.Hide(); t1.Hide();
            d2.Hide(); c2.Hide(); t2.Hide();
            d3.Hide(); c3.Hide(); t3.Hide();
            d4.Hide(); c4.Hide(); t4.Hide();
            d5.Hide(); c5.Hide(); t5.Hide();
            d6.Hide(); c6.Hide(); t6.Hide();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if(opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
                fileName = (opf.FileName);

            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            d1.Hide(); c1.Hide(); t1.Hide();
            d2.Hide(); c2.Hide(); t2.Hide();
            d3.Hide(); c3.Hide(); t3.Hide();
            d4.Hide(); c4.Hide(); t4.Hide();
            d5.Hide(); c5.Hide(); t5.Hide();
            d6.Hide(); c6.Hide(); t6.Hide();
            string result = convertImage(fileName);

            //textBox1.Text = result;
            pictureBox1.Image = Image.FromFile(fileName.Substring(0,fileName.Length - 4)+"_detected.jpg");
            string[] targets = result.Split('\n');
            for (int i = 0; i < targets.Length-1; i = i+2)
            {
                string target = targets[i].Substring(0, targets[i].Length - 1);
                string img_path = "E:\\FYP\\images\\" + targets[i].Substring(0, targets[i].Length - 1) +"LG.JPG";
                string conf = "Confidence: " + targets[i + 1].Substring(2, 2) + "%";
                

                if (i == 0)
                {
                    d1.Show(); c1.Show(); t1.Show();
                    d1.Image = Image.FromFile(img_path);
                    t1.Text = target;
                    c1.Text = conf ;
                }
                if (i == 2)
                {
                    d2.Show(); c2.Show(); t2.Show();
                    d2.Image = Image.FromFile(img_path);
                    t2.Text = target;
                    c2.Text = conf;
                }
                if (i == 4)
                {
                    d3.Show(); c3.Show(); t3.Show();
                    d3.Image = Image.FromFile(img_path);
                    t3.Text = target;
                    c3.Text = conf;
                }
                if (i == 6)
                {
                    d4.Show(); c4.Show(); t4.Show();
                    d4.Image = Image.FromFile(img_path);
                    t4.Text = target;
                    c4.Text = conf;
                }
                if (i == 8)
                {
                    d5.Show(); c5.Show(); t5.Show();
                    d5.Image = Image.FromFile(img_path);
                    t5.Text = target;
                    c5.Text = conf;
                }
                if (i == 10)
                {
                    d6.Show(); c6.Show(); t6.Show();
                    d6.Image = Image.FromFile(img_path);
                    t6.Text = target;
                    c6.Text = conf;
                }


            }


        }

        private string convertImage(string f)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            string a = "E:\\FYP\\terraSAR.py";
            string b = f;
            start.Arguments = string.Format("{0} {1}", a, b);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            Process process = Process.Start(start);
            System.IO.StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            

            return result;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3();
            f.Show();
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f = new Form5();
            f.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private void T6_Click(object sender, EventArgs e)
        {

        }
    }
}
