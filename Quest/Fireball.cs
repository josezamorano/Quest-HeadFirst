using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    class Fireball : Enemy
    {
        Random random = new Random();
        public enum FireballMotionState
        {
            NotMoving = 0,
            MovingLeft = 1,
            MovingRight = 2,
        }

        public Fireball.FireballMotionState fireballDirection;
        public Enum SelectFireballDirection()
        {
            Enum direction;
            int index = random.Next(1, 3);
            direction = (FireballMotionState)index;
            return direction;
        }


        private int pointsDamageCausedToPlayer = 1;
        public int PointsDamageCausedToPlayer
        {
            get
            {
                return pointsDamageCausedToPlayer;
            }
        }

        public Fireball()
        {
        }

        public override void Move(Random random)
        {
        }

        public override void Move(Random random, PictureBox target)
        {
        }

        public bool ShootFireball(bool modeRandom, Player heroFloor, PictureBox wizardPictureBox, Enum WizardMotionStatus)
        {
            bool shootSuccessHit = false;

            //Random Movement BEGIN====
            //==We define the Vectors for the Goul==
            Point p = new Point();

            //Fireball Initial Location
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y = EnemyPictureBoxFloor.Location.Y;

            int randomVariationVectorX = random.Next(1, 16);
        
            //Movement to the Right BEGIN===
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector d => d.X = 961
            //Bat Width = 50 pixels
            //Maximum Random increase in movement to the right = 16 pixels
            //Therefore the bat image Must not be further right than 961 - 16 = 945 pixels
            //**Calculation END=***********
            if ((p.X) < 945 && fireballDirection == FireballMotionState.MovingRight)
            {
                FireballMoveRight(p);
                StrikePlayer(heroFloor, modeRandom, pointsDamageCausedToPlayer, EnemyPictureBoxFloor);
            }
          
            
            if (p.X >= 945) //When the Fireball hits the right corner it disappears
            {
                EnemyPictureBoxFloor.Hide();
            }
            //Movement to the Right END=====


            //Movement to the Left BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector a => a.X = 138

            //Maximum Random decrease in movement to the left = 16 pixels
            //Therefore the bat image Must not be further left than 138 pixels because after
            //the calculation we reduce the X location vector by the random value
            //**Calculation END=***************************************************************

            if ((p.X) > 138 && fireballDirection == FireballMotionState.MovingLeft)
            {
                FireballMoveLeft(p);
                StrikePlayer(heroFloor, modeRandom,pointsDamageCausedToPlayer,EnemyPictureBoxFloor);
            }
            

            if (p.X <= 138)
            {
              EnemyPictureBoxFloor.Hide();
            }
            //Movement to the Left END====

           //Random Movement END======
         
            return shootSuccessHit;
            //==Generate The weapon Movement END====       
        }

        //==Auxiliary Movements BEGIN==========
        private void FireballMoveRight(Point p)
        {
            int randomVariationVectorX = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;    
        }
        private void FireballMoveLeft(Point p)
        {
            int randomVariationVectorX = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;
        }
        //==Auxiliary Movements END============
    }
}
