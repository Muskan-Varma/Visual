using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace visual_cryptography
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
           // f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //f.Dock = DockStyle.Fill;
            //f.TopMost = true;
           // f.TopLevel = false;

           //panel2.Controls.Clear();
           // panel2.Controls.Add(f);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frnDecryption f = new frnDecryption();
            //f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
           // f.Dock = DockStyle.Fill;
            //f.TopMost = true;
            //f.TopLevel = false;

            //panel2.Controls.Clear();
            //panel2.Controls.Add(f);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string filename = "D:\\Help.pdf";
            System.Diagnostics.Process.Start(filename);
        }
    }
}
