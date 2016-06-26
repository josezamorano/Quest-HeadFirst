using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Windows.Forms;

namespace Quest
{
    class Wizard: Enemy
    {
        //List to store all the movements to left and right done by the wizard
        public List<Wizard.WizardMotionState> WizardMotionList = new List<Wizard.WizardMotionState>();
        Random random = new Random();
        public enum WizardMotionState
        {
            NotMoving = 0,
            MovingLeft = 1,
            MovingRight = 2,
            MovingUp = 3,
            MovingDown = 4,
        }

        public Wizard.WizardMotionState wizardDirection;
        public Enum SelectWizardDirection()
        {
            Enum direction;
            int index = random.Next(1, 5);
            direction = (WizardMotionState)index;
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
        public Wizard(): base()
        {

        }

        //Random Movement BEGIN====
        public override void Move(Random random)
        {
            /*******************************************************************
                The Wizard moves randomly in the dungeon up and down,
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

            
            //==We create a general Direction Selector==

            //we store all the information of the direction of the wizard in a list
            if ((Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.MovingUp
               && (Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.MovingDown
               && (Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.NotMoving
               )
            {
                WizardMotionList.Add((Wizard.WizardMotionState)wizardDirection);
            }

            int total = WizardMotionList.Count;
            int numberToRemove = total - 3;
            if (WizardMotionList.Count >= 4)
            {
                for (int i = 0; i < numberToRemove; i++)
                {
                    WizardMotionList.RemoveAt(0);
                }

            }


            //==We define the Vectors for the Wizard==
            Point p = new Point();

            //Bat Initial Location
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y = EnemyPictureBoxFloor.Location.Y;


            int randomVariationVectorX = random.Next(1, 16);
            //int randomVariationVectorY;
            //Movement to the Right BEGIN===
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector d => d.X = 961
            //Wizard Width = 50 pixels
            //Maximum Random increase in movement to the right = 16 pixels
            //Therefore the bat image Must not be further right than 961 - 16 = 945 pixels
            //**Calculation END=***********
            if ((p.X) < 945 && wizardDirection == WizardMotionState.MovingRight)
            {
                WizardMoveRight(p);

            }
            int randomPositionRightX = random.Next(136, 980);

            if (p.X >= 630 && p.X <= 640) //At any random X horizontal location the Wizard changes direction
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }
            if (p.X >= 360 && p.X <= 370) //At any random X horizontal location the Wizard changes direction
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }


            if (p.X >= 945) //When the bat hits the right corner it selects another direction to move
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }
            //Movement to the Right END=====


            //Movement to the Left BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum right location dungeon floor - Vector a => a.X = 138

            //Maximum Random decrease in movement to the left = 16 pixels
            //Therefore the bat image Must not be further left than 138 pixels because after
            //the calculation we reduce the X location vector by the random value
            //**Calculation END=***************************************************************

            if ((p.X) > 138 && wizardDirection == WizardMotionState.MovingLeft)
            {
                WizardMoveLeft(p);

            }
            /*
            int randomPositionLeftX = random.Next(136, 980);

            if (p.X <= randomPositionLeftX) //At any random X horizontal location the Wizard changes direction
            {
                WizardDirection = (wizard.WizardMotionState)SelectWizardDirection();
            }
            */

            if (p.X <= 138)
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }
            //Movement to the Left END====

            //Movement Up BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum up location dungeon floor - Vector a => a.Y = 85

            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 85 pixels because after the
            //calculation we reduce the Y Location Vector by the random value
            //**Calculation END=***************************************************************

            if ((p.Y) > 85 && wizardDirection == WizardMotionState.MovingUp)
            {
                WizardMoveUp(p);

            }
          
            if (p.Y <= 85) //When the bat hits the top, it selects another direction to move
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }
            //Movement Up END====

            //Movement Down BEGIN====
            //**Calculation BEGIN====**********************************************************
            //Maximum Down location dungeon floor - Vector b => b.Y = 365
            //Wizard Width = 50 pixels
            //Maximum Random decrease in movement up = 16 pixels
            //Therefore the bat image Must not be further up than 365 - 50 - 16 = 299 pixels
            //**Calculation END=***************************************************************

            if ((p.Y) < 299 && wizardDirection == WizardMotionState.MovingDown)
            {
                WizardMoveDown(p);
            }
          
            if (p.Y >= 299) //When the bat hits the bottom of the dungeon it selets another direction
            {
                wizardDirection = (Wizard.WizardMotionState)SelectWizardDirection();
            }
            //Movement Down END====

        }

        //Random Movement END======
        //==Targeted Movement BEGIN=============
        public override void Move(Random random, PictureBox target)
        {

            if ((Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.MovingUp
               && (Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.MovingDown
               && (Wizard.WizardMotionState)wizardDirection != Wizard.WizardMotionState.NotMoving
               )
            {
                WizardMotionList.Add((Wizard.WizardMotionState)wizardDirection);
            }

            int total = WizardMotionList.Count;
            int numberToRemove = total - 3;
            if (WizardMotionList.Count >= 4)
            {
                for (int i = 0; i < numberToRemove; i++)
                {
                    WizardMotionList.RemoveAt(0);
                }

            }




            Point p = new Point();
            //The Wizard move towards the player which is its target
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

            //==We define the Vectors for the wizard==  
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;

            int randomNumberPointX = random.Next(1, 20);
            int randomNumberPointY = random.Next(1, 20);
            //wizard Move Right
            if (aX > c1X)
            {
                wizardDirection = Wizard.WizardMotionState.MovingRight;
                p.X = EnemyPictureBoxFloor.Location.X + randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;

            }
            //wizard Move Left
            if (a1X > cX)
            {
                wizardDirection = Wizard.WizardMotionState.MovingLeft;
                p.X = EnemyPictureBoxFloor.Location.X - randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;

            }
            //wizard Move Up
            if (a1Y > cY)
            {
                wizardDirection = Wizard.WizardMotionState.MovingUp;
                p.Y = EnemyPictureBoxFloor.Location.Y - randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;

            }
            //wizard Move Down
            if (c1Y < aY)
            {
                wizardDirection = Wizard.WizardMotionState.MovingDown;
                p.Y = EnemyPictureBoxFloor.Location.Y + randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;

            }
            //When wizard is ON top of the Player, the player dies if the player has no energy left.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
                EnemyPictureBoxFloor.Location = target.Location;

            }
            /*****/

        }
        //==Targeted Movement END===============

        //==Movement Auxiliaries BEGIN==========
        private void WizardMoveRight(Point p)
        {

            int randomVariationVectorX = random.Next(1, 21);
            p.X += randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;
        }
        private void WizardMoveLeft(Point p)
        {

            int randomVariationVectorX = random.Next(1, 21);
            p.X -= randomVariationVectorX;
            p.Y = EnemyPictureBoxFloor.Location.Y;
            EnemyPictureBoxFloor.Location = p;
        }
        private void WizardMoveUp(Point p)
        {

            int randomVariationVectorY = random.Next(1, 21);
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y -= randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }
        private void WizardMoveDown(Point p)
        {
            int randomVariationVectorY = random.Next(1, 21);
            p.X = EnemyPictureBoxFloor.Location.X;
            p.Y += randomVariationVectorY;
            EnemyPictureBoxFloor.Location = p;
        }

        //==Movement Auxiliaries END============

       

    }
}
