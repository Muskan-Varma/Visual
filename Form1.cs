using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
namespace visual_cryptography
{
    public partial class Form1 : Form
    {
        int imgW=0;
        int imgH=0;
        int recons, n, k;
        
        int[, , ,] IMG_SHARE;
        int[] RAND;
        int[] IMG_CONS;
        int[] ORG;
      //int[, ,] ENV;
        Bitmap []envbt;
        Bitmap[] btshare;
        Bitmap[] envolope_image;
        public static string password = "12345";
       public class MYENV
        {
           public int[, ,] ENV;
        }

       MYENV []en;
       public Form1()
        {
            InitializeComponent();
            n = k = recons = 0;
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            try
            {
                pictureBox1.Image = Bitmap.FromFile(ofd.FileName);
            }
            catch { }
        }
        public void Random_Place(int n1, int recons1)
        {
            for (int i = 0;i < recons1; i++)
            {
                Random rd = new Random();

                int rand_int = rd.Next(n1);


                if (RAND.Contains(rand_int) == false)
                {

                    RAND[i] = rand_int;
                }
            }
             
           
        }
        private void button2_Click(object sender, EventArgs e)
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
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Bitmap ipimg = new Bitmap(pictureBox1.Image);

            //Step I: Take an image IMG as input and calculate its width (w) and height (h).
            imgW = ipimg.Width;
            imgH = ipimg.Height;
            //step 2
          

            //step3
            try
            {
                IMG_SHARE = new int[n, imgW, imgH, 32];
            }
            catch
            {
                MessageBox.Show("Image size too large");
                return;
            }
            recons = (n - k) + 1;
            string PIX_ST = "";
            RAND = new int[recons];
            IMG_CONS = new int[n];
            
            for (int y = 0; y <imgH; y++)
            {

                for (int x = 0; x < imgW; x++)
                {
                    int argb = ipimg.GetPixel(x, y).ToArgb();
                    PIX_ST = Convert.ToString(argb, 2).ToString();

                    //int aa = ipimg.GetPixel(x, y).A;
                    //int rr = ipimg.GetPixel(x, y).R;
                    //    int gg= ipimg.GetPixel(x, y).G;
                    //int bb= ipimg.GetPixel(x, y).B;
                   
                    for (int j = 0; j < 32; j++)

                    {
                       
                        if (PIX_ST[j] == '1')
                        {
                            Random_Place(n, recons);
                            for (int k1 = 0; k1 < recons; k1++)
                            {
                               
                                IMG_SHARE[RAND[k1], x, y, j] = int.Parse(PIX_ST[j] + "");  // image share creation
                            }

                            
                        }
                        
                    }
                }
            }
            //-------------

            //step 4
            flowLayoutPanel1.Controls.Clear();
            int[,] img_arr = new int[imgW, imgH];
            btshare=new Bitmap[n];

            for (int k1 = 0; k1 < n; k1++)
            {
                btshare[k1] = new Bitmap(imgW, imgH);
                for (int y = 0; y < imgH; y++)
                {
                    for (int x = 0; x < imgW; x++)
                    {
                        string value = "";
                        int k2 = y + x;
                        for (int k3 = 0; k3 < 32; k3++)
                        {
                          //  value += IMG_SHARE[k1, k2, k3];
                            value += IMG_SHARE[k1,x, y, k3];
                        }
                        string str = value.Substring(0, 8);
                        int alpha = Convert.ToInt16(str, 2);
                        str = value.Substring(8, 8);
                        int red = Convert.ToInt16(str, 2);
                        str = value.Substring(16, 8);

                        int green = Convert.ToInt16(str, 2);
                        str = value.Substring(24, 8);

                        int blue = Convert.ToInt16(str, 2);

                        btshare[k1].SetPixel(x,y,Color.FromArgb(alpha,red,green,blue));
                      //  bt[k1].SetPixel(x, y, Color.FromArgb(mycolor[x,y,0]));
                   
                    }
                }
                PictureBox pic = new PictureBox();
                pic.Size = new System.Drawing.Size(100, 100);
                pic.Image = btshare[k1];
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                flowLayoutPanel1.Controls.Add(pic);
                btshare[k1].Save(Application.StartupPath+"\\shares\\" + k1+"_"+ btshare[k1].Width+"_"+ btshare[k1].Height + ".jpg");
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("share created");

        }//btclose

        private void button3_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Application.StartupPath + "\\env");
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }

            OpenFileDialog ofd = new OpenFileDialog();
           
            envbt = new Bitmap[k];
            envolope_image = new Bitmap[k];
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            int env_no;
            try
            {
                for (int i = 0; i < k; i++)
               {
                    ofd.ShowDialog();
                    Image img = Bitmap.FromFile(ofd.FileName);
                    envbt[i] = new Bitmap(img);

                    

                    int sz = 4 * imgW * imgH;
                    int sz2 = envbt[i].Width * envbt[i].Height;
                    if (sz2 < sz)
                    {
                        MessageBox.Show("here envolope image size required:4*" + imgW + "*" + imgH);
                        return;
                    }

                   
                }
                
                Cursor.Current = Cursors.WaitCursor;
                string PIX_ST = "";
                en = new MYENV[k];
                for (int i = 0; i < k; i++)
                {
                    en[i] = new MYENV();
                    en[i].ENV = new int[envbt[i].Width, envbt[i].Height, 32];
                    PictureBox pic = new PictureBox();
                    pic.Size = new System.Drawing.Size(100, 100);
                    pic.Image = btshare[i];
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    
                    PictureBox pic1 = new PictureBox();
                    pic1.Size = new System.Drawing.Size(100, 100);
                    pic1.Image = envbt[i];
                    pic1.SizeMode = PictureBoxSizeMode.StretchImage;

                    flowLayoutPanel2.Controls.Add(pic);
                    flowLayoutPanel2.Controls.Add(pic1);


                    for (int y = 0; y < envbt[i].Height; y++)
                    {

                        for (int x = 0; x < envbt[i].Width; x++)
                        {
                            int argb = envbt[i].GetPixel(x, y).ToArgb();
                            PIX_ST = Convert.ToString(argb, 2).ToString();
                            for (int j = 0; j < 32; j++)
                            {
                                en[i].ENV[x, y, j] = int.Parse(PIX_ST[j] + "");

                            }
                        }
                    }
             
                    ORG = new int[imgW * imgH * 32];
                    int M = 0;
                    for (int y = 0; y < imgH; y++)
                    {
                        for (int x = 0; x < imgW; x++)
                        {
                            string value = "";
                            int k2 = y + x;
                            for (int k3 = 0; k3 < 32; k3++)
                            {
                                //  value += IMG_SHARE[k1, k2, k3];
                                ORG[M++] = IMG_SHARE[i, x, y, k3];
                            }
                        }
                    }

                    M = 0;
                    int r = 0;
                    int c = 0;
                    for (int h = 0; M < ORG.Length; h++)
                    {
                            if (r < envbt[i].Height)
                            {
                                if (c < envbt[i].Width)
                                {
                                    en[i].ENV[c, r, 6] = ORG[M++];
                                    en[i].ENV[c, r, 7] = ORG[M++];

                                    en[i].ENV[c, r, 14] = ORG[M++];
                                    en[i].ENV[c, r, 15] = ORG[M++];

                                    en[i].ENV[c, r, 22] = ORG[M++];
                                    en[i].ENV[c, r, 23] = ORG[M++];

                                    en[i].ENV[c, r, 30] = ORG[M++];
                                    en[i].ENV[c, r, 31] = ORG[M++];
                                    c++;
                                }
                                else
                                {
                                    r++;
                                    c = 0;
                                }
                            }          
                    }

                    Bitmap bt = new Bitmap(envbt[i].Width, envbt[i].Height);

                    for (r = 0; r < bt.Height; r++)
                    {
                        int x = 0;
                        for (c = 0; c < bt.Width; c++)
                        {
                            string value = "";
                            for (int k3 = 0; k3 < 32; k3++)
                            {
                                //  value += IMG_SHARE[k1, k2, k3];
                                value += en[i].ENV[c, r, k3];
                            }
                            string str = value.Substring(0, 8);
                            int alpha = Convert.ToInt16(str, 2);
                            str = value.Substring(8, 8);
                            int red = Convert.ToInt16(str, 2);
                            str = value.Substring(16, 8);

                            int green = Convert.ToInt16(str, 2);
                            str = value.Substring(24, 8);
                            int blue = Convert.ToInt16(str, 2);

                            int argb = Convert.ToInt32(value, 2);
                           
                            bt.SetPixel(c, r, Color.FromArgb(alpha,red,green,blue));
                          //  int t = bt.GetPixel(c, r).ToArgb();
                        }
                    }
                    PictureBox pic2 = new PictureBox();
                    pic2.Size = new System.Drawing.Size(100, 100);
                    pic2.Image = bt;
                    envolope_image[i] = bt;
                    pic2.SizeMode = PictureBoxSizeMode.StretchImage;
                    flowLayoutPanel3.Controls.Add(pic2);
                    bt.Save(Application.StartupPath +"\\env\\"+ i +"_"+imgW+"_"+imgH+ ".jpg");
                }

                MessageBox.Show("Envolope created");
                    //--
                }
         
            catch
            {
            }
            Cursor.Current = Cursors.Default;
        }

   
      
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void mail(String to, string msg)
        {
            // this.Cursor = Cursors.WaitCursor;

            MailMessage mMailMessage1 = new MailMessage();

            mMailMessage1.From = new MailAddress("samplevisual58@gmail.com", "Visual cryptography");
            if (to.Contains(','))
            {
                string[] Multi = to.Split(',');
                foreach (string Multiemailid in Multi)
                {
                    mMailMessage1.To.Add(new MailAddress(Multiemailid));
                }
            }
            else
            {
                mMailMessage1.To.Add(new MailAddress(to));
            }
           /* */
            string[] files = Directory.GetFiles(Application.StartupPath + "\\env");
            for (int i = 0; i < files.Length; i++)
            {
                Attachment at = new Attachment(files[i]);

                mMailMessage1.Attachments.Add(at);
            }
           
               
            

            mMailMessage1.Subject = "Visual Cryptography"; // write subject here
            //mMailMessage1.SubjectEncoding = System.Text.Encoding.UTF8;

            string html = msg + "<br/>";
            mMailMessage1.Body = html;


            mMailMessage1.IsBodyHtml = true;

            mMailMessage1.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("samplevisual58@gmail.com", "Visual@cryptography");

            client.Port = 587; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer

            try
            {
                client.Send(mMailMessage1);

                 MessageBox.Show("Mail Sent");
                //  Label1.Text = "Your Mail successfully send";

            }
            catch (Exception ex)
            {
                MessageBox.Show("internet Connection is not available"+ex);
                // Label1.Text = "internet Connection is not available";
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtemail.Text == "")
            {
                MessageBox.Show("Enter email");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            string msg = k + "," + n;
            msg = encrypt(msg,password);
            mail(txtemail.Text, msg);

            Cursor.Current = Cursors.Default;
        }
        public string encrypt(string data, string key)
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int BlockSize = 128;

            byte[] bytes = Encoding.Unicode.GetBytes(data);
            //Encrypt
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }

                string enc = Convert.ToBase64String(memoryStream.ToArray());
                return enc;
            }
        }
    
    }
}
