using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    class Ghost: Enemy
    {
        Random rand = new Random();
        //==PictureBox to hold the ghost image==
      //  public PictureBox GhostPictureBoxFloor = new PictureBox();

        int counter = 0;

        private int hitPoints;
        public int HitPoints 
        {
            get { 
                  return hitPoints;
                }

            set { 
                  hitPoints = value; 
                } 
        }
        private int pointsDamageCausedToPlayer = 5;
        public int PointsDamageCausedToPlayer
        {
            get
            {
                return pointsDamageCausedToPlayer;
            }
        }

       
        //==Ghost constructors BEGIN====
        //==Empty Constructor==       
        public Ghost(): base()
        {
        }
        public Ghost(Game game, Point location): base(game,location,6)
        {
        }
        //==Ghost constructors END====

        //==Methods BEGIN=============
        //==Random Movement BEGIN===============
        public override void Move(Random random)
        {
        /*******************************************************************
            The ghost moves randomly in the dungeon appearing and 
            dissappearing, it eventually hits the Target.
                              
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
            //==We define the Vectors for the ghost==
            Point p = new Point();
            p.X = random.Next(0, 1095);
            p.Y = random.Next(0, 450);       
            //==The ghost can only be once at a time outside the dungeon boundaries,
            //==The second time will go back to the dungeon boundaries because the 
            //==player cannot be outside the dungeon boundaries.          
            int distanceToReturnToTheDungeonX = random.Next(138, 825);
            int distanceToReturnToTheDungeonY = random.Next(83,285);

            if(counter==0)
            {
                EnemyPictureBoxFloor.Location = p;
            
            }
            if(p.X <138 && counter>=1)
            {
                //Distance to return to the Dungeon boundaries
                p.X =p.X + distanceToReturnToTheDungeonX;
                EnemyPictureBoxFloor.Location = p;
            
                counter = 0;
            }
            if (p.X > 961 && counter >= 1)
            {
                //Distance to return to the Dungeon boundaries
                p.X = p.X - distanceToReturnToTheDungeonX;
                EnemyPictureBoxFloor.Location = p;
               // GhostPictureBoxFloor.Location = p;
                counter = 0;
            }
            if (p.Y < 85 && counter >= 1)
            {
                //Distance to return to the Dungeon boundaries
                p.Y = p.Y + distanceToReturnToTheDungeonY;
                EnemyPictureBoxFloor.Location = p;
               // GhostPictureBoxFloor.Location = p;
                counter = 0;
            }
            if (p.Y > 365 && counter >= 1)
            {
                //Distance to return to the Dungeon boundaries
                p.Y = p.Y - distanceToReturnToTheDungeonY;
                EnemyPictureBoxFloor.Location = p;
             
                counter = 0;
            }
            if (EnemyPictureBoxFloor.Location.X < 138 || EnemyPictureBoxFloor.Location.X > 961
              || EnemyPictureBoxFloor.Location.Y < 85 || EnemyPictureBoxFloor.Location.Y > 365)
            {
                counter++;

            }           
            //==Ghost vectors (Just for information purposes)====
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;  
           
        }
        //==Random Movement END=================

        //==Targeted Movement BEGIN=============
        public override void Move(Random random, PictureBox target)
        {
            Point p = new Point();
            //The ghost move towards the player which is its target
            /****/
            //==We define the vectors for the Player ==
            int aX =target.Location.X;
            int aY = target.Location.Y;
            int cX = target.Location.X + target.Width;
            int cY = target.Location.Y + target.Height;

            aX = aX - 10;
            aY = aY - 10;
            cX += 10;
            cY += 10;

            //==We define the Vectors for the ghost==  
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;
           
            int randomNumberPointX = random.Next(1, 20);
            int randomNumberPointY = random.Next(1, 20);
            //Ghost Move Right
            if(aX >c1X)
            {
                p.X = EnemyPictureBoxFloor.Location.X + randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;
               
            }
            //Ghost Move Left
            if (a1X > cX)
            {
                p.X = EnemyPictureBoxFloor.Location.X - randomNumberPointX;
                p.Y = EnemyPictureBoxFloor.Location.Y;
                EnemyPictureBoxFloor.Location = p;
              
            }
            //Ghost Move Up
            if (a1Y > cY)
            {
                p.Y = EnemyPictureBoxFloor.Location.Y - randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;
              
            }
            //Ghost Move Down
            if (c1Y < aY)
            {
                p.Y = EnemyPictureBoxFloor.Location.Y + randomNumberPointY;
                p.X = EnemyPictureBoxFloor.Location.X;
                EnemyPictureBoxFloor.Location = p;
              
            }     
            //When Ghost is ON top of the Player, the player dies if the player has no energy left.
            if(!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
                EnemyPictureBoxFloor.Location = target.Location;
               // GhostPictureBoxFloor.Location = target.Location;             
            }
            /*****/

        }
        //==Targeted Movement END===============
      
        //==Methods END==
    }
}
