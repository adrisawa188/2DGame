using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGame
{
    public partial class GameScreen : UserControl
    {
        Player hero;
        Enemy enemy;

        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        Random randGen = new Random();

        List<Enemy> enemies = new List<Enemy>();

        Size screenSize;

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            gameTimer.Enabled = true;

            hero = new Player(20, 300);
            screenSize = new Size(this.Width, this.Height);      
          
        }
        
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }   
        
        public void NewEnemy()
        {
         


            int randValue = randGen.Next(0, 101);

            if (randValue < 10)
            {
                int x = randGen.Next(150, screenSize.Width - 150);
                Enemy en = new Enemy(x, 30, 0, 8);
                enemies.Add(en);

                //x = randGen.Next(10, this.Height - 60);                             
                //leftBalls.Add(new Rectangle(0, x, ballSize, ballSize));


            }
        }

        

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            NewEnemy(); 

            foreach (Enemy en in enemies)
            {
                en.Move(screenSize);
            }

            if (leftArrowDown == true)
            {
                hero.Move("left", screenSize);
            }

            if (rightArrowDown == true)
            {
                hero.Move("right", screenSize);
            }

            if (upArrowDown == true)
            {
                hero.Move("up", screenSize);
            }

            if (downArrowDown == true)
            {
                hero.Move("down", screenSize);
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DodgerBlue, hero.x, hero.y, hero.width, hero.height);

            foreach (Enemy en in enemies)
            {
                e.Graphics.FillRectangle(Brushes.White, en.x, en.y, en.xSize, en.ySize);
            }
        }
    }
}
