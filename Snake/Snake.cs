using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Urgent fix rotation problem
    /// </summary>
    class Snake//snake model
    {
        PictureBox head = new PictureBox();
        PictureBox body;
        Image BODYIMG = Image.FromFile(@"../../sprites/Body.png");
        Image left = Image.FromFile(@"../../sprites/HeadLeft.png");
        Image right = Image.FromFile(@"../../sprites/HeadRight.png");
        Image up = Image.FromFile(@"../../sprites/HeadUp.png");
        Image down = Image.FromFile(@"../../sprites/HeadDown.png");

        public List<PictureBox> snakeList = new List<PictureBox>();
        public List<PictureBox> MovemenList = new List<PictureBox>();
        public List<PictureBox> TransitionList = new List<PictureBox>();
        private int _size;
        private bool moving = false;
        private string _orientation = "right";
        private int headPosX = 65;
        private int headPosY = 200;

        public int Size { get => _size; set => _size = value; }
        public int HeadPosX { get => headPosX; set => headPosX = value; }
        public int HeadPosY { get => headPosY; set => headPosY = value; }

        public int BodyPosX { get; set; }
        public int BodyPosY { get; set; }
        public string Orientation { get => _orientation; set => _orientation = value; }
        public int Speed { get; private set; }

        public Snake(int size)
        {
            _size = size;
            Speed = 21;
            BodyPosX = HeadPosX;
            BodyPosY = HeadPosY;
            //ImageRestorer(HEADIMG);

        }

        public List<PictureBox> SnakeInit()
        {
            head.Name = "head";
            head.Location = new Point(HeadPosX, HeadPosY);
            head.Height = 20;
            head.Width = 20;
            head.Image = right;
            snakeList.Add(head);
            CreateBody(snakeList);
            return snakeList;


        }
        public List<PictureBox> UpdateSize(int newSize)
        {
            int i = snakeList.Count - 1;
            Point addTO = snakeList[i].Location;
            body = new PictureBox();// we need to create a new instance for it
            body.Name = "body";
            body.Location = new Point(BodyPosX - 25, BodyPosY);//starts out of screen so we dont see it, and when the snake moves it is added to the snakes back, CLEVER Workaround XD
            body.Height = 20;
            body.Width = 20;
            body.Image = BODYIMG;
            snakeList.Add(body);
            return snakeList;
        }

        private void CreateBody(List<PictureBox> list)
        {
            for (int i = 0; i < Size; i++)
            {
                BodyPosX -= 21;
                body = new PictureBox();//need to create the new instance here as if we create it before every time we run the loop
                                        //it will get updated and the last one will only have the value, if we crreate it here every time we run the loop a new picture box is created,
                                        //and if we create it above we are just creating a single box once and everytime the loop runs we are updating its value
                body.Name = "body";
                body.Location = new Point(BodyPosX, BodyPosY);
                body.Height = 20;
                body.Width = 20;
                body.Image = BODYIMG;
                list.Add(body);

            }
        }


        internal (List<PictureBox>, Point) MoveSnake(string key)
        {
            Point ittr1 = head.Location;
            Point ittr2 = new Point();
            Point position = head.Location;
            MovemenList.Clear();//we clear the list everytime we move as the data from the prev ious movements is also stored in there
                                //even though it is not displayed and everytime we move it multiplies, so doing this will make the game lag free,
                                //uptill about 150+, which no one will ever get to
            if (key.Equals("W") && !Orientation.Equals("down"))
            {
                Orientation = "up";
                HeadPosY -= Speed;
                BodyMovement(ref ittr1, ref ittr2);//this causes the bodies to be added before the head to the list
            }
            if (key.Equals("S") && !Orientation.Equals("up"))
            {
                Orientation = "down";
                HeadPosY += Speed;
                BodyMovement(ref ittr1, ref ittr2);//this causes the bodies to be added before the head to the list
            }
            if (key.Equals("A") && !Orientation.Equals("right"))
            {
                Orientation = "left";
                HeadPosX -= Speed;
                BodyMovement(ref ittr1, ref ittr2);//this causes the bodies to be added before the head to the list
            }
            if (key.Equals("D") && !Orientation.Equals("left"))
            {
                Orientation = "right";
                HeadPosX += Speed;
                BodyMovement(ref ittr1, ref ittr2);//this causes the bodies to be added before the head to the list
            }
            head.Image = rotater();
            head.Location = new Point(HeadPosX, HeadPosY);
            MovemenList.Add(head);//here head is added after the body to the list so head is at the bottom of the list
            return (MovemenList, position);
        }


        private void BodyMovement(ref Point ittr1, ref Point ittr2)
        {
            int itter = 0;
            //WARNING convoluted code ahead
            foreach (PictureBox part in snakeList)
            {
                if (itter > 0)
                {
                    ittr2 = part.Location;
                    part.Location = ittr1;
                    ittr1 = ittr2;
                    MovemenList.Add(part);//this causes the bodies to be added before the head to the list
                }
                itter++;
            }
        }

        //embarrissing workaround dont look
        private Image rotater()
        {
            if (Orientation.Equals("up"))
            {
                return up;
            }
            else if (Orientation.Equals("down"))
            {
                return down;
            }
            else if (Orientation.Equals("left"))
            {
                return left;
            }
            else if (Orientation.Equals("right")) { return right; }

            return right;

        }

        

    }

}
