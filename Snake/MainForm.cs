using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// Todays end summary, tested adding everything
/// todo tmrw: make collider and everytime snake eats mouse increase size and if snake touches itself make it die
/// </summary>
/// 
///Todo: make snek move, done
///todo tmrw: make snake head rotate proper and add eating mechanism, done
///tode :make the rat die and respawn everytime the snake eats it
///done: made the snake move properly and the pieces get added properly
///next: make snake die when touched by head, make mouse move and add the othere difficulty items its easy going now, done
///.
///Todo : add main menu, moving saws and incrementally increasing mines and sounds
namespace Snake
{
    class MainForm : Form//our view/controller
    {
        //Declarations
        #region
        Image backGround = Image.FromFile(@"../../sprites/BG.png");
        Point ratLocation;
        Point mineLocation;
        Point ratApparentLocation;
        Point snakeLocation;
        private static Snake snakeObj;

        public Form theForm;
        Control.ControlCollection FormAdder;
        private static Random rng;
        bool alive = true;
        bool isFirstTime = true;
        private int size=3;
        private Rat rat;
        private int timer;
        PrivateFontCollection bitFont = new PrivateFontCollection();
        Label scoreBoard = new Label();
        Mine mine;
        List<PictureBox> mineList = new List<PictureBox>();
        float mineCount=0.0f;
        Blade blade;

        //Score increase event

        public delegate void ScoreIncrease();
        public event ScoreIncrease ScoreEvent;

        //List<PictureBox> snek;
        protected override CreateParams CreateParams//renders the forms externally and shows later, i guess, it reduces lag, but lag later down the line is imminent due to a lot of picture frames
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public int SnekSize
        {
            get=>size; set=>size = value;
        }

        int RatX
        {
            get => ratLocation.X;
            set => ratLocation.X = value;
        }
        int RatY
        {
            get => ratLocation.Y;
            set => ratLocation.Y = value;
        }
        int MineX
        {
            get => mineLocation.X;
            set => mineLocation.X = value;
        }
        int MineY
        {
            get => mineLocation.Y;
            set => mineLocation.Y = value;
        }
        int RatApparentX
        {
            get => ratApparentLocation.X;
            set => ratApparentLocation.X = value;
        }
        int RatApparentY
        {
            get => ratApparentLocation.Y;
            set => ratApparentLocation.Y = value;
        }

        int SnekX
        {
            get => snakeLocation.X;
            set => snakeLocation.X = value;
        }
        int SnekY
        {
            get => snakeLocation.Y;
            set => snakeLocation.Y = value;
        }
        int Stage
        {
            get;
            set;
        }


        //blade related
        #region
        bool respawnUB = true;
        bool respawnDB = true;
        bool respawnLB = true;
        bool respawnRB = true;

        PictureBox uB= new PictureBox();
        PictureBox dB = new PictureBox();
        PictureBox lB = new PictureBox();
        PictureBox rB = new PictureBox();

        #endregion
        #endregion
        public MainForm()
        {

            //lets initialize our font collection, private font collecttion it is a font family ie it is basically a list of ttf files
            bitFont.AddFontFile(@"../../fonts/bitFont.ttf");

            //setting execution priorities
            Thread first = new Thread(UpdateTimer);//we are just prioratizing this method as we want it to keep running before others,
                                                   //as if it is not high priority the await is almost always overlooked and overtaken by other processes and it will not run
            first.Priority = ThreadPriority.Highest;

            UpdateTimer();
            //define the forms properties
            this.Height = 500;
            this.Width = 500;
            this.BackgroundImage = backGround;
            this.Text = "Snake";
            this.Icon = new Icon(@"../../sprites/Icon.ico");
            this.KeyPreview = true;//allows us to read key strokes
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;
            theForm = this;
            FormAdder = theForm.Controls;

            //initialize the scoreboard
            scoreBoard.SetBounds(200, 30, 100, 40);
            scoreBoard.Text = "0";
            scoreBoard.Image = Image.FromFile(@"../../sprites/ScoreBoard.png");
            scoreBoard.BackColor = Color.Transparent;
            scoreBoard.Font = new Font(bitFont.Families[0], 15);
            scoreBoard.TextAlign = ContentAlignment.MiddleCenter;

            //adding the border walls
            Walls wall = new Walls(theForm);


            //init the randomizer
            rng = new Random();


            //lets place the uninitiable objects off reach
            uB.Location = new Point(-100, -100);
            dB.Location = new Point(-100, -100);
            lB.Location = new Point(-100, -100);
            rB.Location = new Point(-100, -100);
            //init the snake
            snakeObj = new Snake(SnekSize);//gets called and updated when ever the snake is called
            SnakeInitializer();

            RatLocationUpdater();
            ////tasks for the other components


            this.Paint += new PaintEventHandler(RatMover);
            this.Paint += (s, e) => {
                scoreBoard.Text = (SnekSize-3).ToString();
            };

            //this.MouseClick += new MouseEventHandler(debugger);

            ScoreEvent += new ScoreIncrease(MineCountUpdater);
            ScoreEvent += new ScoreIncrease(BladeUpdater);
            SpawnRatChecker();
            MineSpawner();
            this.KeyDown += new KeyEventHandler(MoveSnek);
            //debugger();

            FormAdder.Add(scoreBoard);
            
        }

        //blade related
        #region
        private async void UpBladeSpawner()
        {
            while (respawnUB)
            {
                blade = new Blade();
                (PictureBox,PictureBox) up = blade.InitUpBlade();
                PictureBox upShooter = up.Item2;
                PictureBox upBlade = up.Item1;
                theForm.Controls.Add(upShooter);
                theForm.Controls.Add(upBlade);
                respawnUB = false;
                theForm.Paint += new PaintEventHandler(blade.MoveForward);
                uB = upBlade;
                await Task.Delay(7000);
                theForm.Controls.Remove(upBlade);
                upBlade.Dispose();
                respawnUB = true;
            }
        }
        private async void DownBladeSpawner()
        {
            while (respawnDB)
            {
                blade = new Blade();
                (PictureBox, PictureBox) down = blade.InitDownBlade();
                PictureBox downShooter = down.Item2;
                PictureBox downBlade = down.Item1;
                theForm.Controls.Add(downShooter);
                theForm.Controls.Add(downBlade);
                respawnDB = false;
                theForm.Paint += new PaintEventHandler(blade.MoveBackward);
                dB = downBlade;
                await Task.Delay(7000);
                theForm.Controls.Remove(downBlade);
                downBlade.Dispose();
                respawnDB = true;
            }
        }
        private async void LeftBladeSpawner()
        {
            while (respawnLB)
            {
                blade = new Blade();
                (PictureBox, PictureBox) left = blade.InitLeftBlade();
                PictureBox leftShooter = left.Item2;
                PictureBox leftBlade = left.Item1;
                theForm.Controls.Add(leftShooter);
                theForm.Controls.Add(leftBlade);
                respawnLB = false;
                theForm.Paint += new PaintEventHandler(blade.MoveUp);
                lB = leftBlade;
                await Task.Delay(7000);
                theForm.Controls.Remove(leftBlade);
                leftBlade.Dispose();
                respawnLB = true;
            }
        }
        private async void RightBladeSpawner()
        {
            while (respawnRB)
            {
                blade = new Blade();
                (PictureBox, PictureBox) right = blade.InitRightBlade();
                PictureBox rightShooter = right.Item2;
                PictureBox rightBlade = right.Item1;
                theForm.Controls.Add(rightShooter);
                theForm.Controls.Add(rightBlade);
                respawnRB = false;
                theForm.Paint += new PaintEventHandler(blade.MoveDown);
                rB = rightBlade;
                await Task.Delay(7000);
                theForm.Controls.Remove(rightBlade);
                rightBlade.Dispose();
                respawnRB = true;
            }
        }
        private void BladeUpdater()//this method may look wierdd and funny but this way all of them will execute without having to type in extra 5 line, so fair is fair
        {
            int Score = SnekSize - 3;
            if (Score >= 5)
            {
                UpBladeSpawner();
                if (Score >= 10)
                {
                    DownBladeSpawner();
                    if (Score >= 15)
                    {
                        LeftBladeSpawner();
                        if (Score >= 20)
                        {
                            RightBladeSpawner();
                        }
                    }
                }
            }
        }
        #endregion

        private void MoveSnek(object sender, KeyEventArgs e)
        {
            string a = e.KeyCode.ToString();
            var movedObject= snakeObj.MoveSnake(a);
            List<PictureBox> moved = movedObject.Item1;
            snakeLocation = movedObject.Item2;
            PictureBox[] movedArray = moved.ToArray();
            FormAdder.AddRange(movedArray);
            KillRat();
            KillSnake(moved);
        }

        private void SpawnRatChecker()
        {
            if (mineList.Count == 0)
            {
                SpawRat();
            }
            else
            {
                foreach (var mine in mineList)
                {
                    if (!mine.Bounds.Contains(RatX, RatY))
                    {
                        SpawRat();
                    }
                    else
                    {
                        RatLocationUpdater();
                    }
                }
            }
        }

        private void SpawRat()
        {
            while (!alive || isFirstTime)
            {
                rat = new Rat(ratLocation, out ratApparentLocation);
                FormAdder.Add(rat.InitRatBox());
                RatLocationUpdater();
                alive = true;
                isFirstTime = false;
                //await Task.Delay(3000);//Since we currently dont have a snake to kill the rat we will just keep spawning one every 3 sec after cliick.
            }
        }

        private void RatLocationUpdater()
        {
            RatX = rng.Next(30, 420);
            RatY = rng.Next(30, 420);
        } 
        private void MineLocationUpdater()
        {
            MineX = rng.Next(30, 420);
            MineY = rng.Next(30, 420);
        }

        //public async void debugger(object Sender, MouseEventArgs e)
        //{
        //    Console.WriteLine(uB.Location);
        //}

        public void KillRat()
        {
            bool thresX1 = SnekX >= RatApparentX && SnekX <= RatApparentX + 20;
            bool thresX2 = SnekX+20 >= RatApparentX && SnekX +20 <= RatApparentX + 20;
            bool thresY1 = SnekY >= RatApparentY && SnekY <= RatApparentY + 20;
            bool thresY2 = SnekY +20 >= RatApparentY && SnekY+20 <= RatApparentY + 20;
            if ((thresX1 || thresX2) && (thresY1 || thresY2))
            {
                
                FormAdder.Remove(rat.InitRatBox());
                rat.InitRatBox().Dispose();
                alive = false;
                SnekSize++;
                ScoreEvent();
                PictureBox[] snek = snakeObj.UpdateSize(SnekSize).ToArray(); ;
                FormAdder.AddRange(snek);
                SpawnRatChecker();

            }

        }
        private void SnakeInitializer()//spawns the snake
        {
            PictureBox[] snek = snakeObj.SnakeInit().ToArray(); ;
            FormAdder.AddRange(snek);
           
        }

        private void RatMover(object sender, PaintEventArgs e)
        {
            if (alive && timer == 5)
            {
                    
                    FormAdder.Remove(rat.InitRatBox());
                    rat.InitRatBox().Dispose();
                    alive = false;
                    SpawnRatChecker();
                    timer = 0;
                
            }
        }

        private async void UpdateTimer()
        {
            while (alive && timer < 6)
            {
                timer++;
                await Task.Delay(1000);
                if (timer == 6)
                {
                    timer = 0;//self feeding loop
                }
            }
        }

        private void MineSpawner()
        {
            int i = (int)mineCount;
            Console.WriteLine(i);
            while (i > 0)
            {
                mine = new Mine(mineLocation);
                PictureBox mineItem = mine.InitMineBox();
                mineList.Add(mineItem);
                FormAdder.Add(mineItem);
                MineLocationUpdater();
                i--;
            }
        }

        private void KillSnake(List<PictureBox> pieces)
        {
            pieces.Reverse();//reversing to get head on the top
            PictureBox head = pieces[0];
            int i = 0;
            foreach (PictureBox piece in pieces)
            {
                if (i > 0)
                {
                    if (head.Bounds.IntersectsWith(piece.Bounds))
                    {
                        GameOver();
                    }
                }
                i++;
            }
            Point headsLocation = head.Location;
            if (headsLocation.X > 466 || headsLocation.X < 20 || headsLocation.Y > 445 || headsLocation.Y<20)
            {
                GameOver();
            }
            foreach(var mine in mineList)
            {
                if (head.Bounds.IntersectsWith(mine.Bounds))
                {
                    GameOver();
                }
            }
            if(head.Bounds.IntersectsWith(uB.Bounds)|| head.Bounds.IntersectsWith(dB.Bounds) || head.Bounds.IntersectsWith(lB.Bounds) || head.Bounds.IntersectsWith(rB.Bounds))
            {
                    GameOver();
            }
        }

        private void MineCountUpdater()
        {
            mineCount+=0.5f;
            MineSpawner();
        }

        private void GameOver()
        {
            string message = $"You Died\nYou Scored: {SnekSize - 3}\nDo you want to retry?";
            string title = "Oh No!";
            DialogResult result= MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.Yes:
                    Application.Restart();
                    break;
                case DialogResult.No:
                    Application.Exit();
                    break;
            }
        }
    }
}
