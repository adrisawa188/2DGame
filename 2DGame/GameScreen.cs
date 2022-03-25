using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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

       public static int score = 0;

        Random randGen = new Random();

        List<Enemy> downEnemies = new List<Enemy>();
        List<Enemy> upEnemies = new List<Enemy>();

        Size screenSize;

        int spawnRate = 3;

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
        
        public void NewDownEnemy()
        {        
            int randValue = randGen.Next(0, 101);

            if (randValue < spawnRate)
            {
                int x = randGen.Next(150, screenSize.Width - 30);
                Enemy en = new Enemy(x, -50, 0, 8);
                downEnemies.Add(en);  
            }
        }

        public void NewUpEnemy()
        {
            int randValue = randGen.Next(0, 101);

            if (score > 4)
            {
                if (randValue < spawnRate - 5)
                {
                    int x = randGen.Next(150, screenSize.Width - 30);
                    Enemy en = new Enemy(x, 650, 0, -8);
                    upEnemies.Add(en);
                }
            }
        }

        public void DifficultyUp()
        {
            // If player touches far wall, set them back to defualt position and increase spawn rate of enemy 
            if (hero.x == screenSize.Width - 20)
            {
                hero = new Player(20, 300);
                spawnRate++;
                score++;
                scoreLabel.Text = $"Score: {score}";
            }
        }

        public void EnemeyWallColision()
        {
            //chech if enemy has reached bottom edge 
            foreach (Enemy enemy in downEnemies)
            {
                if (enemy.y > this.Width)
                {
                    downEnemies.Remove(enemy);
                    break;
                }
            }

            foreach (Enemy enemy in upEnemies)
            {
                if (enemy.y < -50)
                {
                    upEnemies.Remove(enemy);
                    break;
                }
            }
        }

        public void HeroEnemyColision()
        {
            foreach (Enemy en in downEnemies)
            {
                if (en.Collision(hero))
                {
                    Thread.Sleep(1000); 
                    gameTimer.Enabled = false;
                    Form1.ChangeScreen(this, new ScoreScreen());      
                }
            }
            foreach (Enemy en in upEnemies)
            {
                if (en.Collision(hero))
                {
                    Thread.Sleep(1000);
                    gameTimer.Enabled = false;
                    Form1.ChangeScreen(this, new ScoreScreen());
                }
            }
        }

        public void PlayerMovement()
        {
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
        }

        public void EnemyMovement()
        {
            foreach (Enemy en in downEnemies)
            {
                en.Move(screenSize);
            }

            foreach (Enemy en in upEnemies)
            {
                en.Move(screenSize);
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            NewDownEnemy();
            
            NewUpEnemy();

            HeroEnemyColision();

            PlayerMovement();

            EnemyMovement();

            DifficultyUp();

            EnemeyWallColision();

            Refresh();
        }

 
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DodgerBlue, hero.x, hero.y, hero.width, hero.height);

            foreach (Enemy en in downEnemies)
            {
                e.Graphics.FillRectangle(Brushes.White, en.x, en.y, en.xSize, en.ySize);
            }

            foreach (Enemy en in upEnemies)
            {
                e.Graphics.FillRectangle(Brushes.White, en.x, en.y, en.xSize, en.ySize);
            }
        }
    }
}
