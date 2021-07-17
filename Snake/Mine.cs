using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{ 
    class Mine//mine model
    {
        PictureBox mine = new PictureBox();
        Image mineImage = Image.FromFile(@"../../sprites/Mine.png");
        Point _location = new Point();
        private Point Location
        {
            get => _location;
        }

        public Mine(Point location)
        {
            _location = location;//we are taking in location in controller so that we can spawn the mine and kill it in our Controller,
                                 //As doing it from Model will be a problem while linking with smake models location and will make it harder in our view part as well
        }
        private Point mineLocation { get => _location; }

        public PictureBox InitMineBox()
        {
            mine.Height = 20;
            mine.Width = 20;
            mine.Location = mineLocation;
            mine.BackColor = Color.Transparent;
            mine.Image = mineImage;
            return mine;

        }
    }
}
