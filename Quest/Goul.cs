using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace Quest
{
    class Goul : Enemy
    {
        Random random = new Random();
        public enum GoulMotionState
        {
            NotMoving = 0,
            MovingLeft = 1,
            MovingRight = 2,
            MovingUp = 3,
            MovingDown = 4,
        }

        public Goul.GoulMotionState goulDirection;
        public Enum SelectGoulDirection()
        {
            Enum direction;
            int index = random.Next(1, 5);
            direction = (GoulMotionState)index;
            return direction;
        }

        private int pointsDamageCausedToPlayer = 6;
        public int PointsDamageCausedToPlayer
        {
            get
            {
                return pointsDamageCausedToPlayer;
            }
        }

       // public PictureBox GoulPictureBoxFloor = new PictureBox();
        public Goul(): base()
        {

        }

        //Random Movement BEGIN====
        public override void Move(Random random)
        {
            /*******************************************************************
                The Goul moves randomly in the dungeon up and down,
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


            //==We define the Vectors for the Goul==
            Point p = new Point();

            //Goul Initial Location
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
            if ((p.X) < 945 && goulDirection == GoulMotionState.MovingRight)
            {
                GoulMoveRight(p);

            }
        

            if(p.X >=310 && p.X <=320) //At any random X horizontal location the goul changes direction
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            if (p.X >= 610 && p.X <= 630) //At any random X horizontal location the goul changes direction
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            if (p.X >= 945) //When the bat hits the right corner it selects another direction to move
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            //Movement to the Right END=====


            //Movement to the Left BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector a => a.X = 138

            //Maximum Random decrease in movement to the left = 16 pixels
            //Therefore the bat image Must not be further left than 138 pixels because after
            //the calculation we reduce the X location vector by the random value
            //**Calculation END=***************************************************************

            if ((p.X) > 138 && goulDirection == GoulMotionState.MovingLeft)
            {
                GoulMoveLeft(p);

            }
           
            if (p.X <= 138)
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            //Movement to the Left END====

            //Movement Up BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum up location dungeon floor - Vector a => a.Y = 85

            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 85 pixels because after the
            //calculation we reduce the Y Location Vector by the random value
            //**Calculation END=***************************************************************
           
            if ((p.Y) > 85 && goulDirection == GoulMotionState.MovingUp)
            {
                GoulMoveUp(p);

            }
         
            if (p.Y <= 85) //When the bat hits the top, it selects another direction to move
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            //Movement Up END====

            //Movement Down BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum Down location dungeon floor - Vector b => b.Y = 365
            //Bat Width = 50 pixels
            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 365 - 50 - 16 = 299 pixels
            //**Calculation END=***************************************************************

            if ((p.Y) < 299 && goulDirection == GoulMotionState.MovingDown)
            {
                GoulMoveDown(p);
            }
         
            if (p.Y >= 299) //When the bat hits the bottom of the dungeon it selets another direction
            {
                goulDirection = (Goul.GoulMotionState)SelectGoulDirection();
            }
            //Movement Down END====
        }

        //Random Movement END======
        //==Targeted Movement BEGIN=============
        public override void Move(Random random, PictureBox target)
        {
            Point p = new Point();
            //The goul move towards the player which is its target
            /****/
            //==We define the vectors for the Player ==
            int aX = target.Location.X;
            int aY = target.Location.Y;
            int cX = target.Location.X + target.Width;
            int cY = target.Location.Y + target.Height;

            aX = aX - 10;
            aY = aY - 10;
            cX += 10;
            cY += 10;

            //==We define the Vectors for the goul==  
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;
           
            int randomNumberPointX = random.Next(1, 20);
            int randomNumberPointY = random.Next(1, 20);
            //Goul Move Right
            if (aX > c1X)
            {
                p.X = EnemyPictureBoxFloor.Location.X + randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;
               
            }
            //Goul Move Left
            if (a1X > cX)
            {
                p.X = EnemyPictureBoxFloor.Location.X - randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;
              
            }
            //Goul Move Up
            if (a1Y > cY)
            {
                p.Y = EnemyPictureBoxFloor.Location.Y - randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;
               
            }
            //Goul Move Down
            if (c1Y < aY)
            {
                p.Y = EnemyPictureBoxFloor.Location.Y + randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;
                
            }
            //When Goul is ON top of the Player, the player dies if the player has no energy left.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
                EnemyPictureBoxFloor.Location = target.Location;
                            
            }
         
        }
        //==Targeted Movement END===============

        //==Auxiliary Movements BEGIN==========
        private void GoulMoveRight(Point p)
        {    
            int randomVariationVectorX = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;
        }
        private void GoulMoveLeft(Point p)
        {          
            int randomVariationVectorX = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;
        }
        private void GoulMoveUp(Point p)
        {       
            int randomVariationVectorY = random.Next(1, 21);
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }
        private void GoulMoveDown(Point p)
        {          
            int randomVariationVectorY = random.Next(1, 21);
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;   
        }

        //==Auxiliary Movements END============

       
    }
}
