using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.IO;
namespace visual_cryptography
{
    public partial class frmLogin : Form
    {
        public static int log_flg = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var firebase = new FirebaseClient(FBConfig.baseUrl);

            try
            {
                

                var fbdata = await firebase.Child("User").OnceAsync<User>();

                int flg = 0;
                foreach (var data in fbdata)
                {
                    //Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
                    User p = new User();
                    p.Name = data.Object.Name;
                    p.Contact = data.Object.Contact;
                    p.Email = data.Object.Email;
                    p.Password = data.Object.Password;
                    if (p.Name == txtuser.Text && p.Password == txtpwd.Text)
                    {
                        flg = 1;
                        Form1.password = txtpwd.Text;
                        frnDecryption.password = txtpwd.Text;
                        break;
                    }
                  
                }
                if (flg == 1)
                {
                    MessageBox.Show("Login successfull");
                    this.Close();
                    log_flg = 1;
                    frmMain frm = new frmMain();
                    frm.Show();

                }
                else
                {
                    log_flg = 0;
                    MessageBox.Show("Invalid user");

                }

                // dataGridView1.DataSource = policePojos;

            }
            catch (Exception ex)
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtuser.Text = "";
            txtpwd.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            frmReg frm1 = new frmReg();
            frm1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpwd.PasswordChar = '\0';
            }
            else
            {
                txtpwd.PasswordChar = '*';
            }
        }
    }
}
