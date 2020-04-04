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
        static  Image im = Image.FromFile(File.ReadAllText("lastgif.txt"));
        bool Mousedown = false;
        static int x, y = (int)(250 * (float)(im.Height/im.Width));
        Point point = new Point(new Size(200,y));
        public main()
        {
            InitializeComponent();
            this.TopMost = true;
            this.KeyDown += Main_KeyDown;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Load += Main_Load;
            try
            {
                pictureBox1.Image = im;
                this.Size = new Size(point);
            }
            catch
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Gif|*.gif";
                op.InitialDirectory=Application.StartupPath + "\\Resource";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(op.FileName);
                    this.Size = pictureBox1.Image.Size;
                    File.WriteAllText("lastgif.txt", op.FileName);
                }
            }
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width- pictureBox1.Image.Width, 0);
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode== Keys.O)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Gif|*.gif";
                op.InitialDirectory = Application.StartupPath + "\\Resource";
                if (op.ShowDialog()== DialogResult.OK)
                {
                    im = Image.FromFile(op.FileName);
                    pictureBox1.Image = im;
                    this.Size = new Size(point);
                    File.WriteAllText("lastgif.txt", op.FileName);
                }
            }
            if(e.Control && e.KeyCode== Keys.End)
            {
                Application.Exit();
            }
            if (e.Control && e.KeyCode == Keys.PageDown)
            {
                this.TopMost = !this.TopMost;
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
            if (Mousedown==true)
            {
                this.Location = new Point(Control.MousePosition.X-x,Control.MousePosition.Y-y);
            }
        }
    }
}
