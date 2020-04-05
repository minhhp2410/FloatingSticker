using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace sticker
{
    public partial class main : Form
    {
        static Image im;
        bool Mousedown = false;
        int x, y;
        public main()
        {
            InitializeComponent();
            this.TopMost = true;
            this.KeyDown += Main_KeyDown;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Load += Main_Load;

        }
        void resize(Image im)
        {
            int res = im.Width - 200;
            if (res >= 0)
            {
                this.Width -= res;
                if (this.Height - res <= 0)
                {
                    this.Width += res * 2;
                    this.Height += res;
                }
                this.Height -= res;

            }
            else
            {
                this.Width += res;
                this.Height += res;
            }
        }

        void start()
        {
            try
            {
                im = Image.FromFile(File.ReadAllText("lastgif.txt"));
                pictureBox1.Image = im;
                //String[] s = File.ReadAllLines("size.txt");
                this.Size = im.Size;
                resize(im);
            }
            catch
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Gif|*.gif";
                op.InitialDirectory = Application.StartupPath + "\\Resource";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    im = Image.FromFile(op.FileName);
                    pictureBox1.Image = im;
                    this.Size = im.Size;
                    resize(im);
                    File.WriteAllText("lastgif.txt", op.FileName);
                }
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                start();
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 0);
            }
            catch
            {
                Application.Exit();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Gif|*.gif";
                op.InitialDirectory = Application.StartupPath + "\\Resource";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    im = Image.FromFile(op.FileName);
                    pictureBox1.Image = im;
                    this.Size = im.Size;
                    resize(im);
                    File.WriteAllText("lastgif.txt", op.FileName);
                }
            }
            if (e.Control && e.KeyCode == Keys.End)
            {
                Application.Exit();
            }
            if (e.Control && e.KeyCode == Keys.PageDown)
            {
                this.TopMost = !this.TopMost;
            }
            if (e.Control && e.KeyCode == Keys.Subtract)
            {
                this.Width--;
                this.Height--;
                this.Width--;
                this.Height--;
                //string[] a = { this.Width.ToString(), this.Height.ToString() };
                //File.WriteAllLines("size.txt", a);
            }
            if (e.Control && e.KeyCode == Keys.Add)
            {
                this.Width++;
                this.Height++;
                this.Width++;
                this.Height++;
                //string[] a = { this.Width.ToString(), this.Height.ToString() };
                //File.WriteAllLines("size.txt", a);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Mousedown)
            {
                Mousedown = true;
                x = e.X;
                y = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Mousedown = !Mousedown;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mousedown == true)
            {
                this.Location = new Point(Control.MousePosition.X - x, Control.MousePosition.Y - y);
            }
        }
    }
}
