namespace compareImages
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button9 = new Button();
            button8 = new Button();
            textBox2 = new TextBox();
            button3 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            button12 = new Button();
            button10 = new Button();
            button11 = new Button();
            button7 = new Button();
            button6 = new Button();
            textBox3 = new TextBox();
            button5 = new Button();
            button4 = new Button();
            pictureBox2 = new PictureBox();
            textBox4 = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button9);
            tabPage1.Controls.Add(button8);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Compare";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Font = new Font("Segoe UI", 12F);
            button9.Location = new Point(0, 0);
            button9.Name = "button9";
            button9.Size = new Size(33, 34);
            button9.TabIndex = 8;
            button9.TabStop = false;
            button9.Text = "⯇";
            button9.TextAlign = ContentAlignment.TopCenter;
            button9.UseVisualStyleBackColor = true;
            button9.Visible = false;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 12F);
            button8.Location = new Point(31, 0);
            button8.Name = "button8";
            button8.Size = new Size(33, 34);
            button8.TabIndex = 7;
            button8.TabStop = false;
            button8.Text = "⯈";
            button8.TextAlign = ContentAlignment.TopCenter;
            button8.UseVisualStyleBackColor = true;
            button8.Visible = false;
            button8.Click += button8_Click;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Fill;
            textBox2.Location = new Point(3, 299);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(786, 120);
            textBox2.TabIndex = 6;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Top;
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button3.Location = new Point(3, 267);
            button3.Name = "button3";
            button3.Size = new Size(786, 32);
            button3.TabIndex = 5;
            button3.Text = "Compare by MAE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(3, 244);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(786, 23);
            textBox1.TabIndex = 3;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Top;
            button2.Location = new Point(3, 212);
            button2.Name = "button2";
            button2.Size = new Size(786, 32);
            button2.TabIndex = 2;
            button2.Text = "Select Folder to Compare";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Top;
            button1.Location = new Point(3, 181);
            button1.Name = "button1";
            button1.Size = new Size(786, 31);
            button1.TabIndex = 1;
            button1.Text = "Select original Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(786, 178);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(button12);
            tabPage2.Controls.Add(button10);
            tabPage2.Controls.Add(button11);
            tabPage2.Controls.Add(button7);
            tabPage2.Controls.Add(button6);
            tabPage2.Controls.Add(textBox3);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(pictureBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Reverse Adjustment";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            button12.Dock = DockStyle.Top;
            button12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button12.Location = new Point(3, 314);
            button12.Name = "button12";
            button12.Size = new Size(786, 34);
            button12.TabIndex = 21;
            button12.Text = "Adjust by raLanc2.5 - another 130% slower and 0.1255% more accurate";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button10
            // 
            button10.Font = new Font("Segoe UI", 12F);
            button10.Location = new Point(0, 0);
            button10.Name = "button10";
            button10.Size = new Size(33, 34);
            button10.TabIndex = 20;
            button10.TabStop = false;
            button10.Text = "⯇";
            button10.TextAlign = ContentAlignment.TopCenter;
            button10.UseVisualStyleBackColor = true;
            button10.Visible = false;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.Font = new Font("Segoe UI", 12F);
            button11.Location = new Point(31, 0);
            button11.Name = "button11";
            button11.Size = new Size(33, 34);
            button11.TabIndex = 19;
            button11.TabStop = false;
            button11.Text = "⯈";
            button11.TextAlign = ContentAlignment.TopCenter;
            button11.UseVisualStyleBackColor = true;
            button11.Visible = false;
            button11.Click += button11_Click;
            // 
            // button7
            // 
            button7.Dock = DockStyle.Top;
            button7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button7.Location = new Point(3, 280);
            button7.Name = "button7";
            button7.Size = new Size(786, 34);
            button7.TabIndex = 17;
            button7.Text = "Adjust by raMagLanc1 - 27% slower, 0.004% more accurate";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Dock = DockStyle.Top;
            button6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button6.Location = new Point(3, 246);
            button6.Name = "button6";
            button6.Size = new Size(786, 34);
            button6.TabIndex = 15;
            button6.Text = "Adjust by raLanc1";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button4_Click;
            // 
            // textBox3
            // 
            textBox3.Dock = DockStyle.Top;
            textBox3.Location = new Point(3, 223);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(786, 23);
            textBox3.TabIndex = 14;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Top;
            button5.Location = new Point(3, 189);
            button5.Name = "button5";
            button5.Size = new Size(786, 34);
            button5.TabIndex = 13;
            button5.Text = "Select Folder to adjust";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Top;
            button4.Location = new Point(3, 155);
            button4.Name = "button4";
            button4.Size = new Size(786, 34);
            button4.TabIndex = 12;
            button4.Text = "Select original Image";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button6_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Top;
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(786, 152);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            // 
            // textBox4
            // 
            textBox4.Dock = DockStyle.Fill;
            textBox4.Location = new Point(3, 348);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ScrollBars = ScrollBars.Both;
            textBox4.Size = new Size(786, 71);
            textBox4.TabIndex = 22;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "compareImages";
            WindowState = FormWindowState.Maximized;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private Button button3;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox3;
        private Button button5;
        private Button button4;
        private Button button6;
        private Button button7;
        private Button button9;
        private Button button8;
        private Button button10;
        private Button button11;
        private Button button12;
        private TextBox textBox4;
    }
}
