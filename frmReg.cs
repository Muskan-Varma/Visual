using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.IO;

namespace visual_cryptography
{
    public partial class frmReg : Form
    {
        public frmReg()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtcontact.Text == "")
            {
                MessageBox.Show("Enter contact no");
                return;
            }
            if (txtcontact.Text.Length != 10)
            {
                MessageBox.Show("Enter valid contact no");
                return;
            }
            if (txtname.Text == "")
            {
                MessageBox.Show("Enter name");
                return;
            }
            if (txtpwd.Text == "")
            {
                MessageBox.Show("Enter password");
                return;
            }
            if (txtemail.Text == "")
            {
                MessageBox.Show("Enter Mail id");
                return;
            }
            if (txtemail.Text.Contains("@") == false || txtemail.Text.Contains(".") == false)
            {
                MessageBox.Show("Enter valid email");
                return;
            }
            User p = new User();
            p.Name = txtname.Text;
            p.Contact = txtcontact.Text;
            p.Email = txtemail.Text;
            p.Password = txtpwd.Text;




            var firebase = new FirebaseClient(FBConfig.baseUrl);

            try
            {
                
              await firebase.Child("User").PostAsync(p);

            }
            catch (Exception ex)
            {

            }
            MessageBox.Show("Register successfully");
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtname.Text = "";
            txtemail.Text = "";
            txtcontact.Text = "";
            txtpwd.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
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
