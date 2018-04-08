using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files(*.png,*.jpg) | *.png; *.jpg";
            openDialog.InitialDirectory = @"C:\User\quang\Desktop";
            if(openDialog.ShowDialog()==DialogResult.OK)
            {
                textBoxFilePath.Text = openDialog.FileName.ToString();
                pictureBox1.ImageLocation = textBoxFilePath.Text;
            }
            //open file image
        }
        Bitmap img;
        String Enc = "10011011"; // Key encryption

        private void EnCode_Click(object sender, EventArgs e)
        {
            img = new Bitmap(textBoxFilePath.Text);
            String bin = "";
            
            int dem = 0;
            int k;
            for (int i = 0; i < textBoxMessage.TextLength;i++)
            {
                char letter = Convert.ToChar(textBoxMessage.Text.Substring(i, 1));
                int temp = Convert.ToInt32(letter);
                String temp1 = Convert.ToString(temp, 2);
                k = (8 - temp1.Length);
                for (int h = 0; h < k; h++)
                {
                    temp1 = "0" + temp1;
                }
                bin += temp1;
            }
            String xxxxxxxx = "";
            for (int aaa = 0; aaa < (bin.Length / 8); aaa++)
            {
                String catchuoi = bin.Substring(aaa*8, 8);
                String afterS = "";
                for (int bbb = 0; bbb < 8; bbb++)
                {
                    Char biti = Convert.ToChar(catchuoi.Substring(bbb, 1));
                    Char keyi = Convert.ToChar(Enc.Substring(bbb, 1));

                    int bitx;
                    int keyx;
                    if (biti == '0')
                    {
                        bitx = 0;
                    }
                    else bitx = 1;

                    if (keyi == '0')
                    {
                        keyx = 0;
                    }
                    else keyx = 1;
                    bitx = bitx ^ keyx;
                    if (bitx == 0) afterS += '0';
                    else afterS += '1';
                }
                xxxxxxxx += afterS;
            }
            bin = xxxxxxxx;
            Color lastp = img.GetPixel(img.Width -1 , img.Height -1);
            img.SetPixel(img.Width -1, img.Height -1, Color.FromArgb(lastp.R, lastp.G, (int)bin.Length));
            int aa = img.GetPixel(img.Width - 1, img.Height - 1).B;
            System.Console.WriteLine("Set bit pixel cuoi: " + aa);
            for (int i=0 ; i<img.Width ; i++)
            {
                for (int j = 0 ; j < img.Height ; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    
                    if (dem < bin.Length)
                    {
                        char x;
                        int a=0;
                        int b=0;
                        int c=0;
                        if (dem< bin.Length)  // lay 3 bit tu trai qua phai
                        {
                            x = Convert.ToChar(bin.Substring(dem++, 1));
                            a = (int)Char.GetNumericValue(x);
                            System.Console.WriteLine(" a== " + a);
                        }
                        if(dem< bin.Length)
                        {
                            x = Convert.ToChar(bin.Substring(dem++, 1));
                            b = (int)Char.GetNumericValue(x);
                            System.Console.WriteLine(" b== " + b);
                        }

                        
                        if(dem< bin.Length)
                        {
                            x = Convert.ToChar(bin.Substring(dem++, 1));
                            c = (int)Char.GetNumericValue(x);
                            System.Console.WriteLine(" c== " + c);
                        }

                        
                        String sr = Convert.ToString(pixel.R, 2); //lay cac bit rgb pixel
                        String sg = Convert.ToString(pixel.G, 2);
                        String sb = Convert.ToString(pixel.B, 2);
                        System.Console.WriteLine(" SR ==== " + sr);
                        System.Console.WriteLine(" SG ==== " + sg);
                        System.Console.WriteLine(" SB ==== " + sb);
                        k = (8 - sr.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sr = "0" + sr;
                        }
                        k = (8 - sg.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sg= "0" + sg;
                        }
                        k = (8 - sb.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sb = "0" + sb;
                        }

                        

                        int pr = Convert.ToInt32(sr,2); System.Console.WriteLine(" Day la Sr === " + pr);
                        int pg = Convert.ToInt32(sg, 2); System.Console.WriteLine(" Day la Sg === " + pg);
                        int pb = Convert.ToInt32(sb, 2); System.Console.WriteLine(" Day la Sb === " + pb);


                        if (sr.Substring(7, 1) == "0")
                        {
                            pr = pr | a;
                        }else
                        {
                            if (a == 1) pr = pr & 0xff;
                            else pr = pr & 0xfe;
                        }
                        if (sg.Substring(7, 1) == "0")
                        {
                            pg = pg | b;
                        }
                        else
                        {
                            if (b == 1) pg = pg & 0xff;
                            else pg = pg & 0xfe;
                        }
                        if (sb.Substring(7, 1) == "0")
                        {
                            pb = pb | c;
                        }
                        else
                        {
                            if (c == 1) pb = pb & 0xff;
                            else pb = pb & 0xfe;
                        }
                        System.Console.WriteLine(" PR after ==== " + Convert.ToInt32(pr));
                        System.Console.WriteLine(" PG after ==== " + Convert.ToString(pg));
                        System.Console.WriteLine(" PB after ==== " + Convert.ToString(pb));
                        System.Console.WriteLine(" pixel.R" + pixel.R);
                        img.SetPixel(i, j, Color.FromArgb(pr, pg, pb));
                        

                    }
                    /*
                    if(i==img.Width-1 && j==img.Height)
                    {
                        img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, textBoxMessage.TextLength));
                    } */
                }  
            }
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "Image files(*.png,*.jpg) | *.png; *.jpg";
            saveFile.InitialDirectory = @"C:\User\quang\Desktop";
            if (saveFile.ShowDialog() == DialogResult.OK)  
            {
                textBoxFilePath.Text = saveFile.FileName.ToString();
                pictureBox1.ImageLocation = textBoxFilePath.Text;
                Bitmap afterBmp = new Bitmap(img);
                afterBmp.Save(textBoxFilePath.Text);
            } // Lưu file
            lastp = img.GetPixel(img.Width - 1, img.Height - 1); //pixel cuối của ảnh gốc
            System.Console.WriteLine(" Last pixel = " + lastp.B);

            Color lastpixel11 = img.GetPixel(img.Width - 1, img.Height - 1); 
            int msglength = lastpixel11.B;


            Bitmap imgNew = new Bitmap(textBoxFilePath.Text); 
            Color la = img.GetPixel(imgNew.Width - 1, img.Height - 1); 
            int xx = la.B; System.Console.WriteLine("xasssss" + la.B);
            textBoxFilePath.Text = "";
            textBoxMessage.Text = "";


        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            
            String message = "";
            int dem = 0;
            Bitmap imgNew = new Bitmap(textBoxFilePath.Text);
            Color lastpixel = imgNew.GetPixel(imgNew.Width - 1, imgNew.Height - 1);

            int msglength = lastpixel.B;
            for (int i = 0; i < imgNew.Width; i++)
            {
                for (int j = 0; j < imgNew.Height; j++)
                {
                    Color pixel = imgNew.GetPixel(i, j);
                    if (dem < msglength)
                    {
                        int a = pixel.R;
                        int b = pixel.G;
                        int c = pixel.B;
                        String sr = Convert.ToString(a,2);
                        String sg = Convert.ToString(b, 2);
                        String sb = Convert.ToString(c, 2);

                        int k = (8 - sr.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sr = "0" + sr;
                        }
                        k = (8 - sg.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sg = "0" + sg;
                        }
                        k = (8 - sb.Length);
                        for (int h = 0; h < k; h++)
                        {
                            sb = "0" + sb;
                        }

                        char ch = Convert.ToChar(sr.Substring(7, 1));
                        message = message + ch; dem++;  if (dem >= msglength) break;
                        ch = Convert.ToChar(sg.Substring(7, 1));
                        message = message + ch; dem++; if (dem >= msglength) break;
                        ch = Convert.ToChar(sb.Substring(7, 1));
                        message = message + ch; dem++; if (dem >= msglength) break;
                        //String letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });



                        System.Console.WriteLine(" message === " + message);

                    }
                }
            }
            //Giai ma

            String xxxxxxxx = "";
            for (int aaa = 0; aaa < (message.Length / 8); aaa++)
            {
                String catchuoi = message.Substring(aaa*8, 8);
                String afterS = "";
                for (int bbb = 0; bbb < 8; bbb++)
                {
                    Char biti = Convert.ToChar(catchuoi.Substring(bbb, 1));
                    Char keyi = Convert.ToChar(Enc.Substring(bbb, 1));

                    int bitx;
                    int keyx;
                    if (biti == '0')
                    {
                        bitx = 0;
                    }
                    else bitx = 1;

                    if (keyi == '0')
                    {
                        keyx = 0;
                    }
                    else keyx = 1;
                    bitx = bitx ^ keyx;
                    if (bitx == 0) afterS += '0';
                    else afterS += '1';
                }
                xxxxxxxx += afterS;
            }
            message = xxxxxxxx;

            //Giai ma stop

            System.Console.WriteLine(" Length message === " + message.Length);
            String result = "";
            int length = message.Length /8;
            for (int i = 0; i < length; i++)
            {
                String temp = message.Substring(i*8,8);
                int number = 0;
                number = Convert.ToInt32(temp, 2);
                result += Convert.ToString((char)number);
            }
            textBoxMessage.Text = result;
        }
    }
}



