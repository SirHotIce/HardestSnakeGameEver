using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu form = new MainMenu();
        }
        class MainMenu : Form
        {
            public MainMenu()//fix this window being main issue and we are done
            {
                this.Height = 220;
                this.Width = 220;
                this.DesktopLocation = new Point(500, 500);
                this.BackgroundImage = Image.FromFile(@"../../sprites/TitleScreen.png");
                this.Text = "Snake";
                this.Icon = new Icon(@"../../sprites/Icon.ico");
                this.KeyPreview = true;//allows us to read key strokes
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.DoubleBuffered = true;

                MainForm form;


                PictureBox start = new PictureBox();
                start.Height = 20;
                start.Width = 50;
                start.Location = new Point(75, 110);
                start.Image = Image.FromFile(@"../../sprites/StartNormal.png");
                start.BackColor = Color.Transparent;
                this.Controls.Add(start);

                PictureBox exit = new PictureBox();
                exit.Height = 20;
                exit.Width = 50;
                exit.Location = new Point(75, 140);
                exit.Image = Image.FromFile(@"../../sprites/ExitNormal.png");
                exit.BackColor = Color.Transparent;
                this.Controls.Add(exit);


                start.MouseHover += (s, e) =>
                {
                    start.Image = Image.FromFile(@"../../sprites/StartPressed.png");
                    start.Cursor = Cursors.Hand;
                };
                start.MouseLeave += (s, e) => {
                    start.Image = Image.FromFile(@"../../sprites/StartNormal.png");
                };

                exit.MouseHover += (s, e) =>
                {
                    exit.Image = Image.FromFile(@"../../sprites/ExitPressed.png");
                    exit.Cursor = Cursors.Hand;
                };
                exit.MouseLeave += (s, e) => {
                    exit.Image = Image.FromFile(@"../../sprites/ExitNormal.png");
                };
                start.Click += (s, e) =>
                {
                    form = new MainForm();
                    if (!form.Created)
                    {
                        form.Show();
                        this.Hide();
                        form.FormClosing += (sender, eve) =>
                        {
                            this.Show();
                        };
                    }
                };
                exit.Click += (s, e) =>
                {
                    Application.Exit();
                };

                this.Show();
                Application.Run(this);
               
            }
        }
    }
}
