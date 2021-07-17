using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Walls
    {
        Form _theForm;
        Image left = Image.FromFile(@"../../sprites/wallLeft.png");
        Image right = Image.FromFile(@"../../sprites/wallRight.png");
        Image up = Image.FromFile(@"../../sprites/wallTop.png");
        Image down = Image.FromFile(@"../../sprites/wallBottom.png");
        PictureBox leftBox= new PictureBox();
        PictureBox rightBox = new PictureBox();
        PictureBox upBox = new PictureBox();
        PictureBox downBox = new PictureBox();
        private Form TheForm { get => _theForm; }
        public Walls(Form theForm)
        {
            _theForm = theForm;
            WallsInnit();
        }

        private void WallsInnit()
        {
            leftBox.SetBounds(0, 0, 20, 460);
            leftBox.Image = left;
            leftBox.BackColor = Color.Transparent;
            TheForm.Controls.Add(leftBox);

            rightBox.SetBounds(466, 0, 20, 460);
            rightBox.Image = right;
            rightBox.BackColor = Color.Transparent;
            TheForm.Controls.Add(rightBox);

            upBox.SetBounds(5, 0, 460, 20);
            upBox.Image = up;
            upBox.BackColor = Color.Transparent;
            TheForm.Controls.Add(upBox);


            downBox.SetBounds(5, 445, 460, 20);
            downBox.Image = down;
            downBox.BackColor = Color.Transparent;
            TheForm.Controls.Add(downBox);

        }
    }
}
