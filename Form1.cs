using System.Diagnostics;
using System.Runtime.InteropServices;

namespace compareImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] historyCMP = new string[10];
        int indexHistoryCMP = 9;
        string[] historyADJ = new string[10];
        int indexHistoryADJ = 9;

        private void button1_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new(); //open raster image from DialodWindow
            openFileDialog.Filter = "Raster images|*.png;*.jpg;*.bmp;*.gif;*.jpeg;*.ico";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(@openFileDialog.FileName);
                for (int i = 1; i < 10; i++)
                {
                    historyCMP[i - 1] = historyCMP[i];
                }
                historyCMP[9] = @openFileDialog.FileName;
                indexHistoryCMP = 9;
                if (historyCMP[8] != null) button9.Visible = true;
                button8.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = @folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Select original Image!");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Select Folder to compare!");
            }
            else
            {
                Stopwatch stopWatch = new();
                stopWatch.Start();
                byte[,,] originalBytes = RGBfromBMP(pictureBox1.Image);
                string[] allfiles = Directory.GetFiles(textBox1.Text);
                foreach (string filename in allfiles)
                {
                    string ext = filename[^4..];
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == "jpeg" || ext.ToLower() == ".bmp" || ext.ToLower() == ".ico")
                    {
                        using (Image compared = Image.FromFile(filename))
                        {
                            if (pictureBox1.Image.Width == compared.Width && pictureBox1.Image.Height == compared.Height)
                            {
                                byte[,,] comparedBytes = RGBfromBMP(compared);
                                float MAErgb8 = compareMAErgb8(originalBytes, comparedBytes);
                                textBox2.Text += filename + " : MAE = " + (1 - MAErgb8).ToString() + "\r\n";
                                textBox2.SelectionStart = textBox2.Text.Length;
                                textBox2.ScrollToCaret();
                                Application.DoEvents();
                            }
                        }
                    }
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show("All images in folder with same size have been compared to original in " + String.Format("{0:0.000} ", ts.TotalSeconds) + "seconds!");
            }
        }

        private static float compareMAErgb8(byte[,,] original, byte[,,] compared)
        {
            int w = original.GetLength(0);
            int h = original.GetLength(1);
            int sumEr = 0;

            Parallel.For(0, h, y =>
            {
                for (int x = 0; x < w; x++)
                {
                    Interlocked.Add(ref sumEr, Math.Abs(original[x, y, 0] - compared[x, y, 0]) + Math.Abs(original[x, y, 1] - compared[x, y, 1]) + Math.Abs(original[x, y, 2] - compared[x, y, 2]));
                }
            });

            return (float)sumEr / w / h / 765;
        }

        static byte[,,] RGBfromBMP(Image img)
        {
            int w = img.Width;
            int h = img.Height;
            Bitmap bmp = (Bitmap)img;
            byte[,,] sr = new byte[w, h, 3];
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            int stride = Math.Abs(bmpData.Stride);
            int bytes = stride * bmp.Height;
            IntPtr ptr = bmpData.Scan0;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                int w3 = w * 3;
                int stridew3 = stride - w3;
                Parallel.For(0, rgbValues.Length, c =>
                {
                    if (c % stride < w3)
                    {
                        int realc = c - stridew3 * (c / stride);
                        int realc3 = realc / 3;
                        int i = realc3 % w;
                        int s = realc3 / w;
                        sr[i, s, 2 - realc % 3] = rgbValues[c];
                    }

                });
            }
            else
            {
                int w4 = w * 4;
                int stridew4 = stride - w4;
                Parallel.For(0, rgbValues.Length, c =>
                {
                    if (c % stride < w4)
                    {
                        int realc = c - stridew4 * (c / stride);
                        int realc4 = realc / 4;
                        int i = realc4 % w;
                        int s = realc4 / w;
                        int realcm4 = realc % 4;
                        if (realcm4 < 3) sr[i, s, 2 - realcm4] = rgbValues[c];
                    }

                });
            }
            bmp.UnlockBits(bmpData);
            return sr;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new(); //open raster image from DialodWindow
            openFileDialog.Filter = "Raster images|*.png;*.jpg;*.bmp;*.gif;*.jpeg;*.ico";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(@openFileDialog.FileName);
                for (int i = 1; i < 10; i++)
                {
                    historyADJ[i - 1] = historyADJ[i];
                }
                historyADJ[9] = @openFileDialog.FileName;
                indexHistoryADJ = 9;
                if (historyADJ[8] != null) button10.Visible = true;
                button11.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = @folderBrowserDialog.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Select original Image!");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Select Folder to adjust!");
            }
            else
            {
                string al = "All";
                byte[,,] originalBytes = RGBfromBMP(pictureBox2.Image);
                int wo = pictureBox2.Image.Width;
                int ho = pictureBox2.Image.Height;
                float aspect = (float)wo / ho;
                string[] allfiles = Directory.GetFiles(textBox3.Text);
                Stopwatch allS = new();
                allS.Start();
                foreach (string filename in allfiles)
                {
                    string ext = filename[^4..];
                    if ((ext == ".png" || ext == ".jpg" || ext == ".gif" || ext == "jpeg" || ext == ".bmp" || ext == ".ico") && !(filename.Contains("Adj") && filename.Contains("Lanc")))
                    {
                        Image adjusted = Image.FromFile(filename);
                        int w = adjusted.Width;
                        int h = adjusted.Height;
                        if (aspect == (float)w / h && wo < w)
                        {
                            Stopwatch stopWatch = new();
                            stopWatch.Start();
                            byte[,,] adjustedBytes = RGBfromBMP(adjusted);
                            string newname = filename[..^4] + "AdjLanc1.png";
                            if (File.Exists(newname)) try
                                {
                                    File.Delete(newname);
                                }
                                catch
                                {
                                    al = "Some";
                                };
                            try
                            {
                                ReverseAdjustmentLanc1(originalBytes, adjustedBytes, w, h, wo, ho).Save(newname);
                                stopWatch.Stop();
                                TimeSpan ts = stopWatch.Elapsed;
                                textBox4.Text += newname + " done in " + String.Format("{0:0.000} ", ts.TotalSeconds) + "seconds\r\n";
                                textBox4.SelectionStart = textBox4.Text.Length;
                                textBox4.ScrollToCaret();
                            }
                            catch
                            {
                                textBox4.Text += newname + " Error\r\n";
                            }

                            Application.DoEvents();
                        }
                    }
                }
                allS.Stop();
                TimeSpan allTs = allS.Elapsed;
                MessageBox.Show(al + " images in Folder with bigger size and same aspect ratio have been adjusted to original Image in " + String.Format("{0:0.000} ", allTs.TotalSeconds) + "seconds!");
            }
        }

        private static Bitmap ReverseAdjustmentLanc1(byte[,,] originalBytes, byte[,,] adjustedBytes, int w, int h, int wo, int ho)
        {
            float decrease = (float)w / wo;

            (float[,,] mpf, float[,,] add) = AdjustCalc(originalBytes, ExactMean(adjustedBytes, w, h, wo, ho, decrease), adjustedBytes, decrease, w, h, wo, ho);

            return BMPfromRGB(DoAdjust(adjustedBytes, scaleLanczos1(mpf, wo, ho, decrease), scaleLanczos1(add, wo, ho, decrease), w, h), w, h);
        }

        private static byte[,,] DoAdjust(byte[,,] adjustedBytes, float[,,] lancMPs, float[,,] lancAds, int w, int h)
        {
            Parallel.For(0, h, y =>
            {
                for (int x = 0; x < w; x++)
                {
                    adjustedBytes[x, y, 0] = (byte)Math.Clamp(adjustedBytes[x, y, 0] * lancMPs[x, y, 0] + lancAds[x, y, 0] + 0.5f, 0, 255);
                    adjustedBytes[x, y, 1] = (byte)Math.Clamp(adjustedBytes[x, y, 1] * lancMPs[x, y, 1] + lancAds[x, y, 1] + 0.5f, 0, 255);
                    adjustedBytes[x, y, 2] = (byte)Math.Clamp(adjustedBytes[x, y, 2] * lancMPs[x, y, 2] + lancAds[x, y, 2] + 0.5f, 0, 255);
                }
            });
            return adjustedBytes;
        }

        private static (float[,,] mpf, float[,,] add) AdjustCalc(byte[,,] originalBytes, byte[,,] miniAdj, byte[,,] adjustedBytes, float decrease, int w, int h, int wo, int ho)
        {
            float[,,] mpf = new float[wo, ho, 3];
            float[,,] add = new float[wo, ho, 3];

            Parallel.For(0, ho, y =>
            {
                for (int x = 0; x < wo; x++)
                {
                    int rmin = 256;
                    int rmax = -1;
                    int gmin = 256;
                    int gmax = -1;
                    int bmin = 256;
                    int bmax = -1;
                    for (int fx = 0; fx < decrease; fx++)
                    {
                        int xx = (int)(x * decrease + fx + 0.5f);
                        if (xx < w)
                        {
                            for (int fy = 0; fy < decrease; fy++)
                            {
                                int yy = (int)(y * decrease + fy + 0.5f);
                                if (yy < h)
                                {
                                    if (adjustedBytes[xx, yy, 0] < rmin) rmin = adjustedBytes[xx, yy, 0];
                                    if (adjustedBytes[xx, yy, 0] > rmax) rmax = adjustedBytes[xx, yy, 0];
                                    if (adjustedBytes[xx, yy, 1] < gmin) gmin = adjustedBytes[xx, yy, 1];
                                    if (adjustedBytes[xx, yy, 1] > gmax) gmax = adjustedBytes[xx, yy, 1];
                                    if (adjustedBytes[xx, yy, 2] < bmin) bmin = adjustedBytes[xx, yy, 2];
                                    if (adjustedBytes[xx, yy, 2] > bmax) bmax = adjustedBytes[xx, yy, 2];
                                }
                            }
                        }
                    }

                    if (miniAdj[x, y, 0] == 0)
                    {
                        add[x, y, 0] = originalBytes[x, y, 0];
                    }
                    else if (rmax == rmin)
                    {
                        mpf[x, y, 0] = 1;
                        add[x, y, 0] = originalBytes[x, y, 0] - miniAdj[x, y, 0];
                    }
                    else if (rmax * originalBytes[x, y, 0] / (float)miniAdj[x, y, 0] > 255)
                    {
                        float t1x = (float)(miniAdj[x, y, 0] - rmin) / (rmax - rmin);
                        float a2 = (255 - originalBytes[x, y, 0]) / (1 - t1x);
                        mpf[x, y, 0] = a2 / (rmax - rmin);
                        add[x, y, 0] = originalBytes[x, y, 0] - a2 * t1x - rmin * mpf[x, y, 0];
                    }
                    else
                    {
                        mpf[x, y, 0] = originalBytes[x, y, 0] / (float)miniAdj[x, y, 0];
                    }

                    if (miniAdj[x, y, 1] == 0)
                    {
                        add[x, y, 1] = originalBytes[x, y, 1];
                    }
                    else if (gmax == gmin)
                    {
                        mpf[x, y, 1] = 1;
                        add[x, y, 1] = originalBytes[x, y, 1] - miniAdj[x, y, 1];
                    }
                    else if (gmax * originalBytes[x, y, 1] / (float)miniAdj[x, y, 1] > 255)
                    {
                        float t1x = (float)(miniAdj[x, y, 1] - gmin) / (gmax - gmin);
                        float a2 = (255 - originalBytes[x, y, 1]) / (1 - t1x);
                        mpf[x, y, 1] = a2 / (gmax - gmin);
                        add[x, y, 1] = originalBytes[x, y, 1] - a2 * t1x - gmin * mpf[x, y, 1];
                    }
                    else
                    {
                        mpf[x, y, 1] = originalBytes[x, y, 1] / (float)miniAdj[x, y, 1];
                    }

                    if (miniAdj[x, y, 2] == 0)
                    {
                        add[x, y, 2] = originalBytes[x, y, 2];
                    }
                    else if (bmax == bmin)
                    {
                        mpf[x, y, 2] = 1;
                        add[x, y, 2] = originalBytes[x, y, 2] - miniAdj[x, y, 2];
                    }
                    else if (bmax * originalBytes[x, y, 2] / (float)miniAdj[x, y, 2] > 255)
                    {
                        float t1x = (float)(miniAdj[x, y, 2] - bmin) / (bmax - bmin);
                        float a2 = (255 - originalBytes[x, y, 2]) / (1 - t1x);
                        mpf[x, y, 2] = a2 / (bmax - bmin);
                        add[x, y, 2] = originalBytes[x, y, 2] - a2 * t1x - bmin * mpf[x, y, 2];
                    }
                    else
                    {
                        mpf[x, y, 2] = originalBytes[x, y, 2] / (float)miniAdj[x, y, 2];
                    }
                }
            });

            return (mpf, add);
        }

        private static float[,,] scaleLanczos1(float[,,] RGB, int wo, int ho, float scale)
        {
            int h = (int)(ho * scale);
            float w = wo * scale;
            float[,,] lnc = new float[(int)w, h, 3];
            int scaleHalf = (int)(scale / 2 + 0.5f);
            float ascale = 1f / scale;

            Parallel.For(-scaleHalf, h - scaleHalf, y =>
            {
                int yc = y + scaleHalf;
                float l;
                int yy = (int)(y * ascale);
                for (int x = -scaleHalf; x < w - scaleHalf; x++)
                {
                    int xc = x + scaleHalf;
                    int xx = (int)(x * ascale);
                    float suml = 0;
                    float sumr = 0;
                    float sumg = 0;
                    float sumb = 0;
                    for (int xf = -1; xf <= 1; xf++)
                    {
                        int xxx = xx + xf;
                        if (xxx >= 0 && xxx < wo)
                        {
                            for (int yf = -1; yf <= 1; yf++)
                            {
                                int yyy = yy + yf;
                                if (yyy >= 0 && yyy < ho)
                                {
                                    l = Lanczos1(xxx - x * ascale) * Lanczos1(yyy - y * ascale);
                                    suml += l;
                                    sumr += l * RGB[xxx, yyy, 0];
                                    sumg += l * RGB[xxx, yyy, 1];
                                    sumb += l * RGB[xxx, yyy, 2];
                                }
                            }
                        }
                    }

                    lnc[xc, yc, 0] = sumr / suml;
                    lnc[xc, yc, 1] = sumg / suml;
                    lnc[xc, yc, 2] = sumb / suml;
                }
            });

            return lnc;
        }

        private static float[,,] scaleLanczos250(float[,,] RGB, int wo, int ho, float scale)
        {
            int h = (int)(ho * scale);
            float w = wo * scale;
            float[,,] lnc = new float[(int)w, h, 3];
            int scaleHalf = (int)(scale / 2 + 0.5f);
            float ascale = 1f / scale;

            Parallel.For(-scaleHalf, h - scaleHalf, y =>
            {
                int yc = y + scaleHalf;
                float l;
                int yy = (int)(y * ascale);
                for (int x = -scaleHalf; x < w - scaleHalf; x++)
                {
                    int xc = x + scaleHalf;
                    int xx = (int)(x * ascale);
                    float suml = 0;
                    float sumr = 0;
                    float sumg = 0;
                    float sumb = 0;
                    for (int xf = -3; xf <= 3; xf++)
                    {
                        int xxx = xx + xf;
                        if (xxx >= 0 && xxx < wo)
                        {
                            for (int yf = -3; yf <= 3; yf++)
                            {
                                int yyy = yy + yf;
                                if (yyy >= 0 && yyy < ho)
                                {
                                    l = Lanczos250(xxx - x * ascale) * Lanczos250(yyy - y * ascale);
                                    suml += l;
                                    sumr += l * RGB[xxx, yyy, 0];
                                    sumg += l * RGB[xxx, yyy, 1];
                                    sumb += l * RGB[xxx, yyy, 2];
                                }
                            }
                        }
                    }

                    lnc[xc, yc, 0] = sumr / suml;
                    lnc[xc, yc, 1] = sumg / suml;
                    lnc[xc, yc, 2] = sumb / suml;
                }
            });

            return lnc;
        }

        private static float Lanczos250(float v)
        {
            if (v == 0) return 1;
            else if (v >= 2.5f || v <= -2.5f) return 0;//
            else
            {
                float px = MathF.PI * v;
                return 2.5f * MathF.Sin(px) * MathF.Sin(px / 2.5f) / px / px;
            }
        }

        private static float Lanczos1(float v)
        {
            if (v == 0) return 1;
            else if (v >= 1 || v <= -1) return 0;
            else
            {
                float px = MathF.PI * v;
                return MathF.Pow(MathF.Sin(px) / px, 2f);
            }
        }

        private static byte[,,] ExactMean(byte[,,] colorBytes, int w, int h, int wo, int ho, float decrease)
        {
            int decreaseI = (int)(decrease + 0.5f);
            float decrease2 = 1f / (decreaseI * decreaseI);
            byte[,,] miniPic = new byte[wo, ho, 3];

            Parallel.For(0, ho, y =>
            {
                int ys = (int)(y * decrease + 0.5f);
                while (ys + decreaseI > h) ys--;
                for (int x = 0; x < wo; x++)
                {
                    int xs = (int)(x * decrease + 0.5f);
                    while (xs + decreaseI > w) xs--;
                    int red = 0;
                    int green = 0;
                    int blue = 0;
                    for (int fx = 0; fx < decreaseI; fx++)
                    {
                        int xx = xs + fx;
                        for (int fy = 0; fy < decreaseI; fy++)
                        {
                            int yy = ys + fy;
                            red += colorBytes[xx, yy, 0];
                            green += colorBytes[xx, yy, 1];
                            blue += colorBytes[xx, yy, 2];
                        }
                    }
                    miniPic[x, y, 0] = (byte)(red * decrease2 + 0.5f);
                    miniPic[x, y, 1] = (byte)(green * decrease2 + 0.5f);
                    miniPic[x, y, 2] = (byte)(blue * decrease2 + 0.5f);
                }
            });

            return miniPic;
        }

        private static float[,,] ExactMeanFloat(byte[,,] colorBytes, int w, int h, int wo, int ho, float decrease)
        {
            int decreaseI = (int)(decrease + 0.5f);
            float decrease2 = 1f / (decreaseI * decreaseI);
            float[,,] miniPic = new float[wo, ho, 3];

            Parallel.For(0, ho, y =>
            {
                int ys = (int)(y * decrease + 0.5f);
                while (ys + decreaseI > h) ys--;
                for (int x = 0; x < wo; x++)
                {
                    int xs = (int)(x * decrease + 0.5f);
                    while (xs + decreaseI > w) xs--;
                    int red = 0;
                    int green = 0;
                    int blue = 0;
                    for (int fx = 0; fx < decreaseI; fx++)
                    {
                        int xx = xs + fx;
                        for (int fy = 0; fy < decreaseI; fy++)
                        {
                            int yy = ys + fy;
                            red += colorBytes[xx, yy, 0];
                            green += colorBytes[xx, yy, 1];
                            blue += colorBytes[xx, yy, 2];
                        }
                    }
                    miniPic[x, y, 0] = red * decrease2;
                    miniPic[x, y, 1] = green * decrease2;
                    miniPic[x, y, 2] = blue * decrease2;
                }
            });

            return miniPic;
        }

        static Bitmap BMPfromRGB(byte[,,] sr, int w, int h)
        {
            Bitmap bmp = new(w, h);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            int bytes = w * h * 4;
            IntPtr ptr = bmpData.Scan0;
            byte[] rgbValues = new byte[bytes];

            Parallel.For(0, rgbValues.Length / 4, c =>
            {
                int c4 = c * 4;
                int i = c % w;
                int s = c / w;
                rgbValues[c4] = sr[i, s, 2];
                rgbValues[c4 + 1] = sr[i, s, 1];
                rgbValues[c4 + 2] = sr[i, s, 0];
                rgbValues[c4 + 3] = 255;
            });
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Select original Image!");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Select Folder to adjust!");
            }
            else
            {
                string al = "All";
                byte[,,] originalBytes = RGBfromBMP(pictureBox2.Image);
                int wo = pictureBox2.Image.Width;
                int ho = pictureBox2.Image.Height;
                float aspect = (float)wo / ho;
                string[] allfiles = Directory.GetFiles(textBox3.Text);
                Stopwatch allS = new();
                allS.Start();
                foreach (string filename in allfiles)
                {
                    string ext = filename[^4..];
                    if ((ext == ".png" || ext == ".jpg" || ext == ".gif" || ext == "jpeg" || ext == ".bmp" || ext == ".ico") && !(filename.Contains("Adj") && filename.Contains("Lanc")))
                    {
                        Image adjusted = Image.FromFile(filename);
                        int w = adjusted.Width;
                        int h = adjusted.Height;
                        if (aspect == (float)w / h && wo < w)
                        {
                            Stopwatch stopWatch = new();
                            stopWatch.Start();
                            byte[,,] adjustedBytes = RGBfromBMP(adjusted);
                            string newname = filename[..^4] + "AdjMagLanc1.png";
                            if (File.Exists(newname)) try
                                {
                                    File.Delete(newname);
                                }
                                catch
                                {
                                    al = "Some";
                                };
                            try
                            {
                                ReverseAdjustmentMagLanc1(originalBytes, adjustedBytes, w, h, wo, ho).Save(newname);
                                stopWatch.Stop();
                                TimeSpan ts = stopWatch.Elapsed;
                                textBox4.Text += newname + " done in " + String.Format("{0:0.000} ", ts.TotalSeconds) + "seconds\r\n";
                                textBox4.SelectionStart = textBox4.Text.Length;
                                textBox4.ScrollToCaret();
                            }
                            catch
                            {
                                textBox4.Text += newname + " Error\r\n";
                            }

                            Application.DoEvents();
                        }
                    }
                }
                allS.Stop();
                TimeSpan allTs = allS.Elapsed;
                MessageBox.Show(al + " images in Folder with bigger size and same aspect ratio have been adjusted to original Image in " + String.Format("{0:0.000} ", allTs.TotalSeconds) + "seconds!");
            }
        }

        private static Bitmap ReverseAdjustmentMagLanc1(byte[,,] originalBytes, byte[,,] adjustedBytes, int w, int h, int wo, int ho)
        {
            float decrease = (float)w / wo;

            (float[,,] mpf, float[,,] add) = AdjustCalc(originalBytes, ExactMean(adjustedBytes, w, h, wo, ho, decrease), adjustedBytes, decrease, w, h, wo, ho);

            return BMPfromRGB(DoAdjust(adjustedBytes, scaleMagLanczos1(mpf, wo, ho, decrease), scaleMagLanczos1(add, wo, ho, decrease), w, h), w, h);
        }

        private static float[,,] scaleMagLanczos1(float[,,] RGB, int wo, int ho, float scale)
        {
            int h = (int)(ho * scale);
            float w = wo * scale;
            float[,,] lnc = new float[(int)w, h, 3];
            int scaleHalf = (int)(scale / 2 + 0.5f);
            float ascale = 1f / scale;

            Parallel.For(-scaleHalf, h - scaleHalf, y =>
            {
                int yc = y + scaleHalf;
                float l;
                int yy = (int)(y * ascale);
                for (int x = -scaleHalf; x < w - scaleHalf; x++)
                {
                    int xc = x + scaleHalf;
                    int xx = (int)(x * ascale);
                    float suml = 0;
                    float sumr = 0;
                    float sumg = 0;
                    float sumb = 0;
                    for (int xf = -1; xf <= 1; xf++)
                    {
                        int xxx = xx + xf;
                        if (xxx >= 0 && xxx < wo)
                        {
                            for (int yf = -1; yf <= 1; yf++)
                            {
                                int yyy = yy + yf;
                                if (yyy >= 0 && yyy < ho)
                                {
                                    l = Lanczos1(MathF.Sqrt(MathF.Pow(xxx * scale - x, 2f) + MathF.Pow(yyy * scale - y, 2f)) * ascale);
                                    suml += l;
                                    sumr += l * RGB[xxx, yyy, 0];
                                    sumg += l * RGB[xxx, yyy, 1];
                                    sumb += l * RGB[xxx, yyy, 2];
                                }
                            }
                        }
                    }

                    lnc[xc, yc, 0] = sumr / suml;
                    lnc[xc, yc, 1] = sumg / suml;
                    lnc[xc, yc, 2] = sumb / suml;
                }
            });

            return lnc;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (indexHistoryCMP > 0) indexHistoryCMP--;
            pictureBox1.Image = Image.FromFile(historyCMP[indexHistoryCMP]);
            button8.Visible = true;
            if (indexHistoryCMP > 0)
            {
                if (historyCMP[indexHistoryCMP - 1] == null) button9.Visible = false;
            }
            else
            {
                button9.Visible = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (indexHistoryCMP < 9) indexHistoryCMP++;
            pictureBox1.Image = Image.FromFile(historyCMP[indexHistoryCMP]);
            button9.Visible = true;
            if (indexHistoryCMP > 8)
            {
                button8.Visible = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (indexHistoryADJ > 0) indexHistoryADJ--;
            pictureBox2.Image = Image.FromFile(historyADJ[indexHistoryADJ]);
            button11.Visible = true;
            if (indexHistoryADJ > 0)
            {
                if (historyADJ[indexHistoryADJ - 1] == null) button10.Visible = false;
            }
            else
            {
                button10.Visible = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (indexHistoryADJ < 9) indexHistoryADJ++;
            pictureBox2.Image = Image.FromFile(historyADJ[indexHistoryADJ]);
            button10.Visible = true;
            if (indexHistoryADJ > 8)
            {
                button11.Visible = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)//2.5
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Select original Image!");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Select Folder to adjust!");
            }
            else
            {
                string al = "All";
                byte[,,] originalBytes = RGBfromBMP(pictureBox2.Image);
                int wo = pictureBox2.Image.Width;
                int ho = pictureBox2.Image.Height;
                float aspect = (float)wo / ho;
                string[] allfiles = Directory.GetFiles(textBox3.Text);
                Stopwatch allS = new();
                allS.Start();
                foreach (string filename in allfiles)
                {
                    string ext = filename[^4..];
                    if ((ext == ".png" || ext == ".jpg" || ext == ".gif" || ext == "jpeg" || ext == ".bmp" || ext == ".ico") && !(filename.Contains("Adj") && filename.Contains("Lanc")))
                    {
                        Image adjusted = Image.FromFile(filename);
                        int w = adjusted.Width;
                        int h = adjusted.Height;
                        if (aspect == (float)w / h && wo < w)
                        {
                            Stopwatch stopWatch = new();
                            stopWatch.Start();
                            byte[,,] adjustedBytes = RGBfromBMP(adjusted);
                            string newname = filename[..^4] + "AdjLanc250.png";
                            if (File.Exists(newname)) try
                                {
                                    File.Delete(newname);
                                }
                                catch
                                {
                                    al = "Some";
                                };
                            try
                            {
                                ReverseAdjustmentLanc250(originalBytes, adjustedBytes, w, h, wo, ho).Save(newname);
                                stopWatch.Stop();
                                TimeSpan ts = stopWatch.Elapsed;
                                textBox4.Text += newname + " done in " + String.Format("{0:0.000} ", ts.TotalSeconds) + "seconds\r\n";
                                textBox4.SelectionStart = textBox4.Text.Length;
                                textBox4.ScrollToCaret();
                            }
                            catch
                            {
                                textBox4.Text += newname + " Error\r\n";
                            }

                            Application.DoEvents();
                        }
                    }
                }
                allS.Stop();
                TimeSpan allTs = allS.Elapsed;
                MessageBox.Show(al + " images in Folder with bigger size and same aspect ratio have been adjusted to original Image in " + String.Format("{0:0.000} ", allTs.TotalSeconds) + "seconds!");
            }
        }

        private static Bitmap ReverseAdjustmentLanc250(byte[,,] originalBytes, byte[,,] adjustedBytes, int w, int h, int wo, int ho)
        {
            float decrease = (float)w / wo;

            (float[,,] mpf, float[,,] add) = AdjustCalc(originalBytes, ExactMean(adjustedBytes, w, h, wo, ho, decrease), adjustedBytes, decrease, w, h, wo, ho);

            return BMPfromRGB(DoAdjust(adjustedBytes, scaleLanczos250(mpf, wo, ho, decrease), scaleLanczos250(add, wo, ho, decrease), w, h), w, h);
        }

        private void button13_Click(object sender, EventArgs e) //MinMax
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Select original Image!");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Select Folder to compare!");
            }
            else
            {
                Stopwatch stopWatch = new();
                stopWatch.Start();
                byte[,,] originalBytes = RGBfromBMP(pictureBox1.Image);
                string[] allfiles = Directory.GetFiles(textBox1.Text);
                foreach (string filename in allfiles)
                {
                    string ext = filename[^4..];
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == "jpeg" || ext.ToLower() == ".bmp" || ext.ToLower() == ".ico")
                    {
                        using (Image compared = Image.FromFile(filename))
                        {
                            if ((float)pictureBox1.Image.Width / pictureBox1.Image.Height == (float)compared.Width / compared.Height)
                            {
                                byte[,,] comparedBytes = RGBfromBMP(compared);
                                (float avg, float max) = compareAvgMaxRGB8(originalBytes, comparedBytes);
                                textBox2.Text += filename + " : AvgError = "+ ((int)(avg*100+0.5f)/100f).ToString() + ", MaxError = " + ((int)(max * 100 + 0.5f) / 100f).ToString() + "\r\n";
                                textBox2.SelectionStart = textBox2.Text.Length;
                                textBox2.ScrollToCaret();
                                Application.DoEvents();
                            }
                        }
                    }
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show("All images in folder with same size have been compared to original in " + String.Format("{0:0.000} ", ts.TotalSeconds) + "seconds!");
            }
        }

        private static (float avg, float max) compareAvgMaxRGB8(byte[,,] original, byte[,,] compared)
        {
            int w = original.GetLength(0);
            int h = original.GetLength(1);
            float[,,] Floats;
            byte[,,] Bytes;
            if (w < compared.GetLength(0))
            {
                Floats = ExactMeanFloat(compared, compared.GetLength(0), compared.GetLength(1), w, h, (float)compared.GetLength(0) / w);
                Bytes = original;
            } else
            {
                Floats = ExactMeanFloat(original, w, h, compared.GetLength(0), compared.GetLength(1), (float)w/compared.GetLength(0));
                Bytes = compared;
                w = compared.GetLength(0);
                h = compared.GetLength(1);
            }

            int sumEr = 0;
            float[] maxEr = new float[h];
            
            Parallel.For(0, h, y =>
            {
                maxEr[y] = 0;
                for (int x = 0; x < w; x++)
                {
                    float er0 = Math.Abs(Floats[x, y, 0] - Bytes[x, y, 0]);
                    float er1 = Math.Abs(Floats[x, y, 1] - Bytes[x, y, 1]);
                    float er2 = Math.Abs(Floats[x, y, 2] - Bytes[x, y, 2]);

                    Interlocked.Add(ref sumEr, (int)(er0 + er1 + er2+0.5f));

                    if (er0 > maxEr[y]) 
                        maxEr[y] = er0;
                    if (er1 > maxEr[y]) 
                        maxEr[y] = er1;
                    if (er2 > maxEr[y]) 
                        maxEr[y] = er2;
                }
            });

            for (int y = 1; y < h; y++)
            {
                if (maxEr[y] > maxEr[0]) maxEr[0] = maxEr[y];
            }

            return ((float)sumEr/h/w/3, maxEr[0]);
        }
    }
}
