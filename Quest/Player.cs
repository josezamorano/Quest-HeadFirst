using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quest
{
    class Player: Mover
    {   
        public enum PlayerMotionState
        {
            NotMoving,
            MovingLeft,
            MovingRight,
            MovingUp,
            MovingDown,
        }

        public PictureBox PlayerPictureFloor = new PictureBox();
        private Weapon equippedWeapon;
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

        private List<Weapon> inventory = new List<Weapon>();
        public List<string> Weapons
        {
            get
            {
                List<string> names = new List<string>();
                foreach(Weapon weapon in inventory)
                {
                    names.Add(weapon.Name);
                }
                return names;
            }
        }

        //=Constructors BEGIN===
        public Player(Game game, Point location): base(game,location)
        {
            hitPoints = 10;

        }      
        public Player() : base()
        {

        }
        //==Constructors END====

        public void Hit(int maxDamage,Random random)
        {
            hitPoints -= random.Next(1,maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            hitPoints += random.Next(1,health);
        }
        public void Equip(string weaponName)
        {
            foreach(Weapon weapon in inventory)
            {
                if(weapon.Name == weaponName)
                {
                    equippedWeapon = weapon;
                }
            }
        }

        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            if(!game.weaponInRoom.PickedUp)
            {
                //See if the weapon is nearby and if its possible to pick it up
            }
        }
        public void Attack(Direction direction, Random random)
        {
            //Your code goes here

        }

        public void AttackEnemy()
        {
            //Your code goes here

        }


        //============================================================
        //====Extra Functionalities BEGIN=============

        //==Edges Bounce BEGIN========================================
        //Making the player bounce on the edges of the dungeon
        public void DungeonFloorLimitsCheckForPlayer(PictureBox pictureBox)
        {
            /****Dungeon Floor Boundaries*************
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

            *******************************************/
           
            //pictureBox Vector a
            int aX = pictureBox.Location.X;
            int aY = pictureBox.Location.Y;
            //pictureBox Vector c
            int cX = pictureBox.Location.X + pictureBox.Width;
            int cY = pictureBox.Location.Y + pictureBox.Height;

            //The weapon must appear at all times on the Player's right hand so it has to be 
            //moved from the players centre point

            /*
            //Weapon Vector a 
            int a1X = (pictureBox.Location.X - 40);
            int a1Y = (pictureBox.Location.Y - 10);
            //Weapon Vector c
            int c1X = a1X + pictureBox.Width;
            int c1Y = a1Y + pictureBox.Height;
            */

            if (pictureBox == PlayerPictureFloor)
            {
               //Moving UP
               if(aY<=85)
               {
                   //==We reverse the movement to counter act the arrow key down
                   //==And avoid going beyond the boundaries.
                   pictureBox.Top +=1;
               }
               //Moving DOWN
               if (cY >= 365)
               {
                   //==We reverse the movement to counter act the arrow key down
                   //==And avoid going beyond the boundaries.
                   pictureBox.Top -= 1;
               }
                //Moving LEFT
               if (aX <= 138)
               {
                   //==We reverse the movement to counter act the arrow key down
                   //==And avoid going beyond the boundaries.
                   pictureBox.Left += 1;
               }
                //Moving RIGHT
               if (cX >= 961)
               {
                   //==We reverse the movement to counter act the arrow key down
                   //==And avoid going beyond the boundaries.
                   pictureBox.Left -= 1;
               }
            }

            if (pictureBox != PlayerPictureFloor)
            {
                //Moving UP
                if (aY <= 75)
                {
                    //==We reverse the movement to counter act the arrow key down
                    //==And avoid going beyond the boundaries.
                    pictureBox.Top += 1;
                }
                //Moving DOWN
                if (cY >= 355)
                {
                    //==We reverse the movement to counter act the arrow key down
                    //==And avoid going beyond the boundaries.
                    pictureBox.Top -= 1;
                }
                //Moving LEFT
                if (aX <= 98)
                {
                    //==We reverse the movement to counter act the arrow key down
                    //==And avoid going beyond the boundaries.
                    pictureBox.Left += 1;
                }
                //Moving RIGHT
                if (cX >= 921)
                {
                    //==We reverse the movement to counter act the arrow key down
                    //==And avoid going beyond the boundaries.
                    pictureBox.Left -= 1;
                }
            }

        }
        //==Edges Bounce END==========================================

       /// <summary>
        /// This method Defines the The position of the player on the coordinates X and Y for the player PictureBox
        /// as well as its width and height added on defining the exact boundaries of the player pictureBox
       /// </summary>
       /// <param name="modeRandom">Defines the extra width and Height of the player's boundaries according to the
       /// type of mode "Random" or "Targeted". On random mode the boundaries are increased 2 times.</param>
       /// <param name="boundariesAdjustment">Defines the value of increased pixels that define the boundaries of the player.</param>
        /// <returns>Returns a list of integer values for the player boundaries representing the vectors
        /// aX = X,aY = Y ,cX = X+Width, cY = Y+Height.</returns>
        public List<int> PlayerBoundaries(bool modeRandom, int boundariesAdjustment)
        {
           
            List<int> playerBoundariesVectors = new List<int>();
            //Player Vectors==
            //==We define the vectors for the Player ==
            int aX = PlayerPictureFloor.Location.X;
            int aY = PlayerPictureFloor.Location.Y;
            int cX = PlayerPictureFloor.Location.X + PlayerPictureFloor.Width;
            int cY = PlayerPictureFloor.Location.Y + PlayerPictureFloor.Height;

            //==We increase the boundaries of the Player Vectors to make it easier for the 
            //==Ghost to catch it
           
            if (modeRandom == true)
            {
                boundariesAdjustment = boundariesAdjustment * 3 ;
            }

            aX = aX - boundariesAdjustment;
            aY = aY - boundariesAdjustment;
            cX += boundariesAdjustment;
            cY += boundariesAdjustment;

            playerBoundariesVectors.Add(aX);
            playerBoundariesVectors.Add(aY);
            playerBoundariesVectors.Add(cX);
            playerBoundariesVectors.Add(cY);

            return playerBoundariesVectors;
        }
        //====Extra Functionalities END=============
        
       
    }
}
