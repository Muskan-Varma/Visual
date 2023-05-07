using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Security.Cryptography;
namespace visual_cryptography
{
 
    public partial class frnDecryption : Form
    {
        public static string password = "12345";

        int imgW = 0;
        int imgH = 0;
        int recons, n, k;

        int[, , ,] IMG_SHARE;
        int[] RAND;
        int[] IMG_CONS;
        int[] ORG;
        //int[, ,] ENV;
        Bitmap[] envbt;
        Bitmap[] btshare;
        Bitmap[] envolope_image;
        public class MYENV
        {
            public int[, ,] ENV;
        }
        public frnDecryption()
        {
            InitializeComponent();
            n = k = recons = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtNshare.Text == "")
            {
                MessageBox.Show("Enter N share");
                return;
            }
            if (txtKshare.Text == "")
            {
                MessageBox.Show("Enter K share");
                return;
            }
            n = int.Parse(txtNshare.Text);
            k = int.Parse(txtKshare.Text);
            if (k > n)
            {
                MessageBox.Show("k must be less than or equal to n");
            }
            OpenFileDialog ofd = new OpenFileDialog();

            envolope_image = new Bitmap[k];
            flowLayoutPanel1.Controls.Clear();
            int env_no;
            try
            {
                for (int i = 0; i < k; i++)
                {
                    ofd.ShowDialog();
                    Image img = Bitmap.FromFile(ofd.FileName);
                    envolope_image[i] = new Bitmap(img);
                    string[] ss = Path.GetFileNameWithoutExtension(ofd.FileName).Split('_');
                    imgW = int.Parse(ss[1]);
                    imgH = int.Parse(ss[2]);

                    PictureBox pic2 = new PictureBox();
                    pic2.Size = new System.Drawing.Size(100, 100);
                    pic2.Image = envolope_image[i] ;
                     pic2.SizeMode = PictureBoxSizeMode.StretchImage;
                    flowLayoutPanel1.Controls.Add(pic2);

                 

                }
              
                    
                

            }

            catch
            {
            }
            Cursor.Current = Cursors.Default;
        }

        private void frnDecryption_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
         

            string PIX_ST = "";
            int[, , ,] STORE = new int[k, imgW, imgH, 32];

            for (int i = 0; i < k; i++)
            {
                int r = 0;
                int c = 0;
                int bit = 0;
                for (int y = 0; y < envolope_image[i].Height; y++)
                {
                    for (int x = 0; x < envolope_image[i].Width; x++)
                    {
                        string value = "";

                        if (r < imgH)
                        {
                            if (c < imgW)
                            {
                                int argb = envolope_image[i].GetPixel(x, y).ToArgb();
                                PIX_ST = Convert.ToString(argb, 2).ToString();


                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[6] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[7] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[14] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[15] + "");

                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[22] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[23] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[30] + "");
                                STORE[i, c, r, bit++] = int.Parse(PIX_ST[31] + "");
                                if (bit == 32)
                                {
                                    bit = 0;
                                    c++;
                                }
                            }
                            if(c==imgW)
                            {
                                r++;
                                c = 0;
                            }
                        }
                    }
                }
            }
            //----------

            string[] pix = new string[k];
            int[, ,] FINAL = new int[imgW, imgH, 32];
            for (int y = 0; y < imgH; y++)
            {
                for (int x = 0; x < imgW; x++)
                {


                    string value = "";
                    for (int k3 = 0; k3 < 32; k3++)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            value = "" + STORE[i, x, y, k3];
                            int v = FINAL[x, y, k3] | int.Parse("" + value);
                            FINAL[x, y, k3] = v;
                        }
                    }


                }
            }
            Bitmap bt = new Bitmap(imgW, imgH);
            for (int y = 0; y < imgH; y++)
            {
                for (int x = 0; x < imgW; x++)
                {
                    string value = "";
                    for (int k3 = 0; k3 < 32; k3++)
                    {

                        value += FINAL[x, y, k3];
                    }
                    string str = value.Substring(0, 8);
                    int alpha = Convert.ToInt16(str, 2);
                    str = value.Substring(8, 8);
                    int red = Convert.ToInt16(str, 2);
                    str = value.Substring(16, 8);

                    int green = Convert.ToInt16(str, 2);
                    str = value.Substring(24, 8);

                    int blue = Convert.ToInt16(str, 2);

                    bt.SetPixel(x, y, Color.FromArgb(alpha, red, green, blue));
                }
            }
            pictureBox2.Image = bt;
            Cursor.Current = Cursors.Default;
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           string data= decrypt(textBox1.Text, password).ToString();
            data = data.Replace("\0", "");
            String[] kn = data.Split(',');
            txtKshare.Text = kn[0];
            txtNshare.Text = kn[1];
        }
        public string decrypt(string data, string key)
        {
            try
            {
                byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                int BlockSize = 128;

                byte[] bytes = Convert.FromBase64String(data);
                SymmetricAlgorithm crypt = Aes.Create();
                HashAlgorithm hash = MD5.Create();
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
                crypt.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[bytes.Length];
                        cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                        string dec = Encoding.Unicode.GetString(decryptedBytes);
                        return dec;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("invalid key for this user");
            }
            return "0,0";
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Bitmap bt = new Bitmap(pictureBox2.Image);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "jpg|*.jpg";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK) ;
            {
                bt.Save(sfd.FileName);
            }
        }
    }
}
