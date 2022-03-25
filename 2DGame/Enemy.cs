using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2DGame
{
    internal class Enemy
    {
        public int xSize = 15;
        public int ySize = 50;
        public int xSpeed, ySpeed;
        public int x, y;

        public Enemy(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        public void Move(Size ss)
        {
            y += ySpeed;
        }

        public bool Collision(Player p)
        {
            //chaseball collion with player
            Rectangle enemyRec = new Rectangle(x, y, xSize, ySize);
            Rectangle playerRec = new Rectangle(p.x, p.y, p.width, p.height);

            if (enemyRec.IntersectsWith(playerRec))
            {
                playerRec.X = x;    
                playerRec.Y = y;    
                return true;
            }
            return false;
        }
    }
}
