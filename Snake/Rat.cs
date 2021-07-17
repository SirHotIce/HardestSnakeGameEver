using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{ 
    class Rat//rat model
    {
        PictureBox rat = new PictureBox();
        Image ratImage = Image.FromFile(@"../../sprites/rat.png");
        Point _location = new Point();
        private Point Location
        {
            get => _location;
        }

        public Rat(Point location, out Point apparentLocation )
        {
            _location = location;//we are taking in location in controller so that we can spawn the rat and kill it in our Controller,
                                 //As doing it from Model will be a problem while linking with smake models location and will make it harder in our view part as well
            apparentLocation = Location;
        }
        private Point RatLocation { get => _location; }

        public PictureBox InitRatBox()
        {
            rat.Height = 20;
            rat.Width = 20;
            rat.Location = RatLocation;
            rat.BackColor = Color.Transparent;
            rat.Image = ratImage;
            return rat;

        }
        public void KillRat()
        {
            rat.Hide();
        }
    }
}
