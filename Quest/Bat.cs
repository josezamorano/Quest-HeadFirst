using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.Drawing;

namespace Quest
{
    class Bat: Enemy
    {
        Random random = new Random();
        public enum BatMotionState
        {
            NotMoving=0,
            MovingLeft=1,
            MovingRight=2,
            MovingUp=3,
            MovingDown=4,
        }

        public Bat.BatMotionState batDirection;
        public Enum SelectDirection()
        {
            Enum direction;
            int index = random.Next(1,5);
            direction = (BatMotionState)index;
            return direction;
        }

        private int pointsDamageCausedToPlayer = 3;
        public int PointsDamageCausedToPlayer
        {
            get
            {
                return pointsDamageCausedToPlayer;
            }
        }


        private int hitPoints;
        public int HitPoints
        {
            get
            {
                return hitPoints;
            }
            set
            {
                hitPoints = value;
            }
        }

        //==Constructors BEGIN=============
        public Bat(Game game, Point location): base(game,location,6)
        {
        }
        public Bat():base()
        {
        }
        //==Constructors END===============

        //==Methods BEGIN==================
        public void BatMethodChecking()
        {

        }


        //Random Movement BEGIN====
        public override void Move(Random random)
        {
            /*******************************************************************
                The bat moves randomly in the dungeon up and down,
                it eventually hits the Target.
                    
                   
                //==Maximum Dungeon boundaries/Coordinate
                //==Points
                * a_______________________d
                * |                       |
                * |                       |
                * |                       |
                * |                       |
                * |_______________________|
                * b                       c
                * 
                *
                a.X = 138 , a.Y =  85
                b.X = 138 , b.Y = 365
                c.X = 961 , c.Y = 365
                d.X = 961 , d.Y =  85
                       
                ***************************************************************/
           
            //==We create a general Direction Selector

           
            //==We define the Vectors for the bat==
            Point p = new Point();
           
            //Bat Initial Location
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y = EnemyPictureBoxFloor.Location.Y;

           
            int randomVariationVectorX = random.Next(1,16);
            int randomVariationVectorY;
            //Movement to the Right BEGIN===
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector d => d.X = 961
            //Bat Width = 50 pixels
            //Maximum Random increase in movement to the right = 16 pixels
            //Therefore the bat image Must not be further right than 961 - 16 = 945 pixels
            //**Calculation END=***********
            if ((p.X ) < 945 && batDirection == BatMotionState.MovingRight)
            {
                BatMoveRight(p, randomVariationVectorX);

            }
            if (p.X >= 945) //When the bat hits the right corner it selects another direction to move
            {
                batDirection = (Bat.BatMotionState)SelectDirection();
            }
            //Movement to the Right END=====


            //Movement to the Left BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector a => a.X = 138
            
            //Maximum Random decrease in movement to the left = 16 pixels
            //Therefore the bat image Must not be further left than 138 pixels because after
            //the calculation we reduce the X location vector by the random value
            //**Calculation END=***************************************************************

            if ((p.X) > 138 && batDirection == BatMotionState.MovingLeft)
            {
                BatMoveLeft(p, randomVariationVectorX);

            }
            if (p.X <=138)
            {
                batDirection = (Bat.BatMotionState)SelectDirection();
            }
            //Movement to the Left END==========

            //Movement Up BEGIN=================
            //**Calculation BEGIN====**********************************************************
            //Maximum up location dungeon floor - Vector a => a.Y = 85

            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 85 pixels because after the
            //calculation we reduce the Y Location Vector by the random value
            //**Calculation END=***************************************************************
            randomVariationVectorY = random.Next(1, 17);
            if ((p.Y) > 85 && batDirection == BatMotionState.MovingUp)
            {
                BatMoveUp(p, randomVariationVectorY);

            }
            if(p.Y <=85) //When the bat hits the top, it selects another direction to move
            {
                batDirection = (Bat.BatMotionState)SelectDirection();
            }
            //Movement Up END===================

            //Movement Down BEGIN===============
            //**Calculation BEGIN====**********************************************************
            //Maximum Down location dungeon floor - Vector b => b.Y = 365
            //Bat Width = 50 pixels
            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 365 - 50 - 16 = 299 pixels
            //**Calculation END=***************************************************************
          
            if ((p.Y ) < 299 && batDirection == BatMotionState.MovingDown)
            {
                BatMoveDown(p, randomVariationVectorY);
            }
            if(p.Y>=299) //When the bat hits the bottom of the dungeon it selets another direction
            {
                batDirection = (Bat.BatMotionState)SelectDirection();
            }
            //Movement Down END=================

        }

        //Random Movement END===================
      
        //==Targeted Movement BEGIN=============
        public override void Move(Random random, PictureBox target)
        {
            Point p = new Point();

            //The bat move towards the player which is its target
            /* */
            //==We define the vectors for the Player ==
            int aX = target.Location.X;
            int aY = target.Location.Y;
            int cX = target.Location.X + target.Width;
            int cY = target.Location.Y + target.Height;


            aX = aX - 10;
            aY = aY - 10;
            cX += 10;
            cY += 10;

            //==We define the Vectors for the Bat==    
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;

            //Bat Initial Location
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y = EnemyPictureBoxFloor.Location.Y;

            int randomVariationVectorX = random.Next(1, 16);
            int randomVariationVectorY;
            //Bat Move to the Right
            if (aX > c1X){ BatMoveRight(p, randomVariationVectorX); }
            //Bat Move to the left
            if (a1X > cX) { BatMoveLeft(p, randomVariationVectorX); }
            randomVariationVectorY = random.Next(1, 17);
            //Bat Move Up
            if (a1Y > cY) {BatMoveUp(p, randomVariationVectorY); }           
            //Bat Move Down
            if (c1Y < aY){ BatMoveDown(p, randomVariationVectorY); }


            //When Ghost is ON top of the Player, the player dies if the player has no energy left.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
                EnemyPictureBoxFloor.Location = target.Location;
            }
            /**/

        }
        //==Targeted Movement END===============

        //==Movement Auxiliaries BEGIN==========
        private void BatMoveRight(Point p, int randomVariationVectorX)
        {
            //==ZigZag Up
            int randomVariationVectorY = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;

            //==ZigZag Down
            randomVariationVectorY = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;

        }
        private void BatMoveLeft(Point p, int randomVariationVectorX)
        {
            //==ZigZag Up
            int randomVariationVectorY = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;

            //==ZigZag Down
            randomVariationVectorY = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }
        private void BatMoveUp(Point p, int randomVariationVectorY)
        {
            //==ZigZag Left
            int randomVariationVectorX = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;

            //==ZigZag Right
            randomVariationVectorX = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }
        private void BatMoveDown(Point p, int randomVariationVectorY)
        {
            //==ZigZag Left
            int randomVariationVectorX = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;

            //==ZigZag Right
            randomVariationVectorX = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }

        //==Movement Auxiliaries END============

       
        //==Methods END=====================
    }
}
