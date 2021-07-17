using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Blade
    {
        //Blades
        PictureBox upBladePrimary = new PictureBox();
        PictureBox upBladeShooter = new PictureBox();
        PictureBox downBladePrimary = new PictureBox();
        PictureBox downBladeShooter = new PictureBox();
        PictureBox leftBladePrimary = new PictureBox();
        PictureBox leftBladeShooter = new PictureBox();
        PictureBox rightBladePrimary = new PictureBox();
        PictureBox rightBladeShooter = new PictureBox();

        //Blade images
        Image up = Image.FromFile(@"../../sprites/BladeUp.png");
        Image down = Image.FromFile(@"../../sprites/BladeDown.png");
        Image left = Image.FromFile(@"../../sprites/BladeLeft.png");
        Image right = Image.FromFile(@"../../sprites/BladeRight.png");
        Image upShooter = Image.FromFile(@"../../sprites/ShooterUp.png");
        Image downShooter = Image.FromFile(@"../../sprites/ShooterDown.png");
        Image leftShooter = Image.FromFile(@"../../sprites/ShooterLeft.png");
        Image rightShooter = Image.FromFile(@"../../sprites/ShooterRight.png");

        //list of Blades
        List<PictureBox> BladeList = new List<PictureBox>();

        private int _stage = 0;//stages when Blades spawn
        /// <summary>
        /// in stage 0 no Blade
        /// in stage 1 upBladePrimary
        /// in stage 2 downBladePrimary
        /// in stage 3 leftBladePrimary
        /// in stage 4 rightBladePrimary
        /// in stage 5 upBladeSecondary
        /// in stage 6 downBladeSecondary
        /// in stage 7 leftBladeSecondary
        /// in stage 8 rightBladeSecondary
        /// </summary>


        //misc stuff
        #region

        int upBladePX=-50;
        int upBladePY=50;
        int downBladePX = 420;
        int downBladePY = 405;
        int leftBladePX = 50;
        int leftBladePY = 400;
        int rightBladePX = 420;
        int rightBladePY = -30;
        #endregion
        public Blade()
        {
        }

        public (PictureBox, PictureBox) InitUpBlade()
        {
            upBladeShooter.SetBounds(19, 40, 20, 30);
            upBladeShooter.Image = upShooter;
            upBladeShooter.BackColor = Color.Transparent;
            upBladeShooter.BringToFront();
            upBladePrimary.Image = up;//setting the image
            upBladePrimary.Height = 10;//setting height
            upBladePrimary.Width = 150;//setting width
            upBladePrimary.Location = new Point(upBladePX, upBladePY);
            upBladePrimary.BackColor = Color.Transparent;
            return (upBladePrimary,upBladeShooter);//returns the picturebox that can be added to the form
        }

        public async void MoveForward(object Sender, PaintEventArgs e)
        {
            while (upBladePX < 440)
            {
                upBladePX ++;
                await Task.Delay(1000);
                upBladePrimary.Location = new Point(upBladePX, upBladePY);

            }
        }
        public (PictureBox, PictureBox) InitDownBlade()
        {
            downBladeShooter.SetBounds(447, 395, 20, 30);
            downBladeShooter.Image = downShooter;
            downBladeShooter.BackColor = Color.Transparent;
            downBladeShooter.BringToFront();
            downBladePrimary.Image = down;//setting the image
            downBladePrimary.Height = 10;//setting height
            downBladePrimary.Width = 150;//setting width
            downBladePrimary.Location = new Point(downBladePX, downBladePY);
            downBladePrimary.BackColor = Color.Transparent;
            return (downBladePrimary, downBladeShooter);//returns the picturebox that can be added to the form
        }

        public async void MoveBackward(object Sender, PaintEventArgs e)
        {
            while (downBladePX > -100)
            {
                downBladePX--;
                await Task.Delay(1000);
                downBladePrimary.Location = new Point(downBladePX, downBladePY);

            }
        }
        public (PictureBox, PictureBox) InitLeftBlade()
        {
            leftBladeShooter.SetBounds(40, 425, 30, 20);
            leftBladeShooter.Image = leftShooter;
            leftBladeShooter.BackColor = Color.Transparent;
            leftBladeShooter.BringToFront();
            leftBladePrimary.Image = left;//setting the image
            leftBladePrimary.Height = 150;//setting height
            leftBladePrimary.Width = 10;//setting width
            leftBladePrimary.Location = new Point(leftBladePX, leftBladePY);
            leftBladePrimary.BackColor = Color.Transparent;
            return (leftBladePrimary, leftBladeShooter);//returns the picturebox that can be added to the form
        }

        public async void MoveUp(object Sender, PaintEventArgs e)
        {
            while (leftBladePY > -100)
            {
                leftBladePY--;
                await Task.Delay(1000);
                leftBladePrimary.Location = new Point(leftBladePX, leftBladePY);

            }
        }
        public (PictureBox, PictureBox) InitRightBlade()
        {
            rightBladeShooter.SetBounds(410, 20, 30, 20);
            rightBladeShooter.Image = rightShooter;
            rightBladeShooter.BackColor = Color.Transparent;
            rightBladeShooter.BringToFront();
            rightBladePrimary.Image = right;//setting the image
            rightBladePrimary.Height = 150;//setting height
            rightBladePrimary.Width = 10;//setting width
            rightBladePrimary.Location = new Point(rightBladePX, rightBladePY);
            rightBladePrimary.BackColor = Color.Transparent;
            return (rightBladePrimary, rightBladeShooter);//returns the picturebox that can be added to the form
        }

        public async void MoveDown(object Sender, PaintEventArgs e)
        {
            while (rightBladePY < 450)
            {
                rightBladePY++;
                await Task.Delay(1000);
                rightBladePrimary.Location = new Point(rightBladePX, rightBladePY);

            }
        }



    }
}
