using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace gg
{
    public class galaga : Control
    {
        protected bool enShootStat;
        protected int lifes;
        protected int _currentWidth;
        protected int enemyShootSpeed =1000;
        protected int esa;
        protected int esa2;
        protected int enemiesRemain;
        protected int enemiesCount;
        protected int enemyBulletSpeed;
        protected int playerBulletSpeed;
        protected int playerSpeed;
        protected int enemySpeed;
        protected int min = 5;
        protected int max = 395;
        protected int ds;
        protected int dx;
        protected int playerSize;
        protected int enemySize;
        protected int bulletSize;
        protected int startRectSize;
        protected int _x;
        protected int _y;
        protected int sx;
        protected int sy;
        protected int _size = 0;
        protected int _oldWidth = 0;
        protected int _oldHeight = 0;
        protected bool needReturn;
        protected bool moveELeft;
        protected static bool ShootStat = false;
        protected bool GameStatus = false;
        protected bool moveleft = false;
        protected bool moveRight = false;
        protected static bool AutoAttack = false;
        protected int PlayerLocationX;
        protected int PlayerLocationY;
        protected int PlayerLocationCurrent;
        protected int Shoot;
        protected bool RightRow;
        protected int procdelete;
        protected Color _spaceColor = Color.Black;
        protected Color _playerColor = Color.Red;
        protected Color _enemyColor = Color.Green;
        protected Color _playerBulletColor = Color.White;
        protected Color _enemyBulletColor = Color.Yellow;
        protected Color _startColor = Color.Green;
        static System.Windows.Forms.Timer gTimer = new System.Windows.Forms.Timer();
        protected event EventHandler _lifes;
        protected event EventHandler _enemiesRemain;

        public event EventHandler EnemiesRemain
        {
            add { _enemiesRemain += value; }
            remove { _enemiesRemain -= value; }
        }

        protected void onEnemiesRemain()
        {
            _enemiesRemain?.Invoke(this, new EventArgs());
        }

        public event EventHandler Lifes
        {
            add { _lifes += value; }
            remove { _lifes -= value; }
        }

        protected void onLifes()
        {
            _lifes?.Invoke(this, new EventArgs());
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if(width <200 || height < 300)
            {
                width = 200;
                height = 300;
            }

            if (Created)
            {
               if(width != height / 1.5)
                {
                    if (width > height / 1.5)
                    {
                        height = (int)(width * 1.5);
                    }
                    if (height/1.5> width)
                    {
                        width = (int)(height / 1.5);
                    }
                }
            }
            SetAdaptiveSizeForObjects(height, width);
            Invalidate();
            base.SetBoundsCore(x, y, width, height, specified);
            
        }

        protected void SetAdaptiveSizeForObjects(int currentHeight,int currentWidth)
        {
            PlayerLocationCurrent = currentWidth / 2;
            PlayerLocationY = currentHeight - currentHeight / 4;
            playerSize = currentWidth / 25;
            startRectSize = currentWidth / 2;
            enemySize = currentWidth / 16;
            max = currentWidth;
            min = 0;
            bulletSize = playerSize / 5;
            playerSpeed = currentWidth / 40;
            playerBulletSpeed = currentHeight / 200;
            enemySpeed = currentWidth / 200;
            enemyShootSpeed = currentHeight;
            _currentWidth = currentWidth;
        }

        public int EnemyCount
        {
            get 
            { 
                return enemiesCount; 
            }
            set
            {
                if (enemiesCount != value)
                {
                    if (value > 30) { value = 30;
                        EnemyCount = 30;
                    }
                    if (value < 0) {value = 0;
                        EnemyCount = 0;
                    }
                    enemiesCount = value;
                }
            }
        }

        public Color SpaceColor
        {
            get
            {
                return _spaceColor;
            }
            set
            {
                if(_spaceColor != value)
                {
                    _spaceColor = value;
                    Invalidate();
                }
            }
        }

        public Color PlayerColor
        {
            get
            {
                return _playerColor;
            }
            set
            {
                if(_playerColor != value)
                {
                    _playerColor = value;
                    Invalidate();
                }
            }
        }

        public Color EnemyColor
        {
            get
            {
                return _enemyColor;
            }
            set
            {
                if (_enemyColor != value)
                {
                    _enemyColor = value;
                    Invalidate();
                }
            }
        }

        public Color PlayerBulletColor
        {
            get
            {
                return _playerBulletColor;
            }
            set
            {
                if (_playerBulletColor != value)
                {
                    _playerBulletColor = value;
                    Invalidate();
                }
            }
        }

        public Color EnemyBulletColor
        {
            get
            {
                return _enemyBulletColor;
            }
            set
            {
                if (_enemyBulletColor != value)
                {
                    _enemyBulletColor = value;
                    Invalidate();
                }
            }
        }

        public galaga() : base()
        {
            lifes = 3;
            gTimer.Start();
            _x = 0;
            _y = 0;
            Intik();
            RightRow = false;
            bulletSize = playerSize / 4;
            testTimer = new System.Timers.Timer(10);
            testTimer.Interval = 500;
            testTimer.Elapsed += playerAutoAttack;
            testTimer.Enabled = false;
            enemyTimer = new System.Timers.Timer(10);
            enemyTimer.Interval = 50;
            enemyTimer.Elapsed += enemyMove;
            enemyTimer.Enabled = false;
            enemyShootTimer = new System.Timers.Timer(10);
            enemyShootTimer.Interval = enemyShootSpeed;
            enemyShootTimer.Elapsed += enemyShoot;
            enemyShootTimer.Enabled = false;
            moveELeft = false;
            needReturn = false;
            enemiesRemain = EnemyCount;
        }

        private void showBox()
        {
            MessageBox.Show("Hello, world.");
        }


        //creating autoattack for player
        private void playerAutoAttack(Object Source, ElapsedEventArgs e)
        {
            sy = PlayerLocationY;
            sx = PlayerLocationCurrent;
            PB.Add(new PlayerBullet { x = sx, y = sy, width=bulletSize, height=playerSize });

            //BulletsY.Add(sy);
            //BulletsX.Add(sx);
            Invalidate();
        }
        //new Lists
        List<Enemy> En = new List<Enemy>();
        List<PlayerBullet> PB = new List<PlayerBullet>();
        List<EnemyBullet> EB = new List<EnemyBullet>();

        //old Lists
        List<int> EnemyShootX = new List<int>();
        List<int> EnemyShootY = new List<int>();
        List<int> BulletsY = new List<int>();
        List<int> BulletsX = new List<int>();
        List<int> EnemyCoordsX = new List<int>();
        List<int> EnemyCoordsY = new List<int>();

        //used timers
        private System.Timers.Timer testTimer;
        private System.Timers.Timer enemyTimer;
        private System.Timers.Timer enemyShootTimer;

        //initializing classes
      

        //initial method
        public void Intik()
        {
            enShootStat = true;
            gTimer.Interval = 1;
            gTimer.Tick += new EventHandler(Shooting);
            this.KeyDown += new KeyEventHandler(inputCheck);
            System.Timers.Timer gaTimer = new System.Timers.Timer(10);
            gaTimer.Interval = 1000;
            gaTimer.Elapsed += Shooter;
            gaTimer.Enabled = true;
            Invalidate();
        }
        //creating bunch of enemies
        public void createEnemy(int count)
        {
            ds = 0;
            dx = 10;
            int multiply = 0;
            for (int c= 0; c<count; ++c)
            {
                if (c == 0)
                {
                    multiply = enemySize;
                }
                else
                {
                    multiply = multiply + enemySize+enemySize/2;
                }
                if(multiply >= _currentWidth- enemySize)
                {
                    multiply = enemySize;
                    dx = dx+enemySize*2;
                }
                
                En.Add(new Enemy { x = ds + multiply, y = dx, width=enemySize,height=enemySize/2});
                //EnemyCoordsX.Add(ds + multiply);
                //EnemyCoordsY.Add(dx);
            }
            enShootStat = true;
            enemyTimer.Enabled = true ;
        }

        protected void checkEnemyGotHit()
        {
            if (En.Count == 0)
            {
                shuttering();
            }
            for (int i = PB.Count-1; i >= 0; --i)
            {
                PB[i].y = PB[i].y - Height / 100;
                for (int g = En.Count - 1; g >= 0; --g)
                {
                    if (PB[i].y1 <= En[g].y2 &&
                        PB[i].y2 >= En[g].y1 &&
                        PB[i].x2 >= En[g].x1 &&
                        PB[i].x1 <= En[g].x2
                        )
                    {
                        PB.RemoveAt(i);
                        //BulletsY.RemoveAt(i);
                        //BulletsX.RemoveAt(i);

                        En.RemoveAt(g);
                        

                        //EnemyCoordsX.RemoveAt(g);
                        //EnemyCoordsY.RemoveAt(g);
                        Invalidate();
                        break;
                    }
                }
            }
        }

        //making enemies able to move
        protected void enemyMove(Object Source, ElapsedEventArgs e)
        {
            if (moveELeft)
            {
                for(int i = 0; i < En.Count; i++)
                {
                    En[i].x=En[i].x-enemySpeed*3;
                    if (En[i].x <= min)
                    {
                        needReturn = (En[i].x <= min); 
                    }
                }
            }
            else
                {
                for(int j = 0; j<En.Count; j++)
                {
                    En[j].x = En[j].x + enemySpeed*3;
                    if (En[j].x2>= max)
                    {
                      needReturn = (En[j].x2 >= max);
                    }
                }
            }
            if (needReturn)
            {
                moveELeft = !moveELeft;
                needReturn = false;
            }
            enemyShooter();

            System.Windows.Forms.Application.DoEvents();
            //additional checking for destroying enemies
            if (En.Count == 0)
            {
                shuttering();
            }
            for (int i = PB.Count - 1; i >= 0; --i)
            {
                procdelete = 0;
                PB[i].y = PB[i].y - Height / 100;

                for (int g = En.Count - 1; g >= 0; --g)
                {
                    if (PB[i].y1 <= En[g].y2 &&
                        PB[i].y2 >= En[g].y1 &&
                        PB[i].x2 >= En[g].x1 &&
                        PB[i].x1 <= En[g].x2
                        )
                    {
                        PB.RemoveAt(i);
                        //BulletsY.RemoveAt(i);
                        //BulletsX.RemoveAt(i);

                        En.RemoveAt(g);

                        //EnemyCoordsX.RemoveAt(g);
                        //EnemyCoordsY.RemoveAt(g);
                        Invalidate();
                    }
                }
            }
            Invalidate();   
        }

        //lifes remain check
        private void lifesCheck()
        {
            if (lifes > 1)
            {
                lifes--;
                onLifes();
            }
            else
            {  
                shuttering();
            }
        }

        private void enemyShooter()
        {
            for (int i = En.Count - 1; i > 0; i--)
            {
                if (En[i].x <= PlayerLocationCurrent + playerSize && En[i].x >= PlayerLocationCurrent - playerSize && enShootStat)
                {
                    EB.Add(new EnemyBullet { x = En[i].x1 + En[i].width / 2, y = En[i].y2, height = En[i].height/2, width = En[i].width / 6 });
                    enemyBulletSpeed = EB[0].height / 5;
                    enShootStat = false;
                    enemyShootTimer.Enabled = true;
                    Invalidate();
                    break;       
                }
            }
        }

        //enemy shoots cooldown
        private void enemyShoot(object Source, ElapsedEventArgs e)
        {
            enShootStat = !enShootStat;
            enemyShootTimer.Enabled = false;
        }

        //checking keyboard inputs
        public void inputCheck(object Source, KeyEventArgs e)
        {
            if (GameStatus)
            {
                
                if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && PlayerLocationCurrent >= 0 + playerSize)
                {
                    PlayerLocationCurrent -= playerSpeed;
                    Invalidate();
                }
                else
              if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && PlayerLocationCurrent <= _currentWidth - playerSize)
                {
                    PlayerLocationCurrent += playerSpeed;
                    Invalidate();
                }
                if ((e.KeyCode == Keys.Space) && ShootStat)
                {
                    //Attempt to make semi-auto fire mode
                    //sy = PlayerLocationY;
                    //sx = PlayerLocationCurrent;


                    //PB.Add(new PlayerBullet { x = sx, y = sy, width=bulletSize,height=playerSize });
                    ////BulletsY.Add(sy);
                    ////BulletsX.Add(sx);
                    //Invalidate();
                    //ShootStat = false;
                }

                if (e.KeyCode == Keys.P)
                {
                    testTimer.Enabled = !testTimer.Enabled;
                }
                if(e.KeyCode == Keys.R)
                {
                    shuttering();
                }
            }else
            {
                if(e.KeyCode == Keys.Enter)
                {
                    GameStatus = true;
                    createEnemy(EnemyCount);
                    Invalidate();
                }
            }
        }

        //counting all events about shooting
        protected void Shooting(object Source, EventArgs e)
        {
            //enemies bullets aproach method
            for (int g = 0; g < EB.Count; g++)
            {
                if (g < EB.Count)
                {
                    EB[g].y = EB[g].y + enemyBulletSpeed;
                }
                if (EB[g].y >= Height)
                {
                    EB.RemoveAt(g);
                }
            }
            //enemies bullets catch a player
            for (int j = EB.Count-1; j >= 0; j--)
            {
                /*if (EB[j].x2 >= PlayerLocationCurrent&&
                    EB[j].x1 <= PlayerLocationCurrent+playerSize&&
                    EB[j].y2 <= PlayerLocationY+playerSize&&
                    EB[j].y1 >= PlayerLocationY)*/
                if(EB[j].x1<=PlayerLocationCurrent+playerSize&&
                   EB[j].x2>=PlayerLocationCurrent&&
                   EB[j].y2<=PlayerLocationY&&
                   EB[j].y1>=PlayerLocationY-playerSize
                   )
                {
                    EB.RemoveAt(j);
                    lifesCheck();
                }
            }
            //player bullet catch en enemy
            checkEnemyGotHit();

            //fail attemtp to make enemie`s speed adaptive to number of remain enemies
                //int procent = En.Count / EnemyCount;
                //int speedPercent = 1 - procent;
                //enemySpeed = (int)(enemySpeed * speedPercent) + enemySpeed;
        }

        //game over/restart method
        protected void shuttering()
        {
            onEnemiesRemain();
            lifes = 3;
            enemyTimer.Stop();
            //remove all enemies
            while (En.Count > 0)
            {
                En.RemoveAt(0);
            }
            GameStatus = false;
            testTimer.Stop();
            enemyShootTimer.Stop();
            //removing all enemy bullets
            while (EB.Count > 0)
            {
                EB.RemoveAt(0);
            }
            //removing all player bullets
            while (PB.Count > 0)
            {
                PB.RemoveAt(0);
            }
            PlayerLocationCurrent = Width / 2;
            Invalidate();
        }
        //some weird shit
        protected static void Shooter(Object Source, ElapsedEventArgs e)
        {
            if (ShootStat)
            {
                ShootStat = false;
            }
            else { ShootStat = true; }
            if (AutoAttack)
            {

            }
        }

        //reference
        public void jojo()
        {
            PlayerLocationCurrent++;
            Invalidate();
        }
        //drawing everything on the screen
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush b = new SolidBrush(SpaceColor);
            e.Graphics.FillRectangle(b, ClientRectangle);
            if (GameStatus)
            {
                b = new SolidBrush(PlayerColor);
                e.Graphics.FillRectangle(b, PlayerLocationCurrent, PlayerLocationY, playerSize, playerSize);
                b.Dispose();
                Brush S = new SolidBrush(PlayerBulletColor);

                for (int i = 0; i < PB.Count; ++i)
                {
                    e.Graphics.FillRectangle(S, PB[i].x + playerSize / 2 - bulletSize / 2, PB[i].y - playerSize / 2, PB[i].width, PB[i].height);
                }
                S.Dispose();
                Brush en = new SolidBrush(EnemyColor);
                for (int g = 0; g < En.Count; ++g)
                {
                    e.Graphics.FillRectangle(en, En[g].x1, En[g].y1, En[g].width, En[g].height);
                }
                Brush enS = new SolidBrush(EnemyBulletColor);
                for(int enss=0; enss < EB.Count; enss++)
                {
                    e.Graphics.FillEllipse(enS, EB[enss].x, EB[enss].y, EB[enss].width, EB[enss].height);
                }
            }
            else
            {
                b = new SolidBrush(Color.Green);
                e.Graphics.FillRectangle(b, Width / 2 -startRectSize/2, Height / 2 -startRectSize/4, startRectSize, startRectSize/2); 
            }

        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
