using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quest
{
    abstract class Enemy : Mover
    {
        Random random = new Random();
        public PictureBox EnemyPictureBoxFloor = new PictureBox();
        private const int NearPlayerDistance = 25;

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

        public bool Dead
        {
            get
            {
                if(hitPoints<=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private int enemyBoundariesAdjustment;
        public int EnemyBoundariesAdjustment
        {
            get
            {
               return enemyBoundariesAdjustment;
            }
            set
            {
                enemyBoundariesAdjustment = value;
            }
        }
       


        //==Constructors BEGIN==
        public Enemy(Game game, Point location, int hitPoints): base(game,location)
        {
            this.hitPoints = hitPoints;
        }
        public Enemy(): base()
        {
        }
        //==Constructors END====
      
        public abstract void Move(Random random);

        public abstract void Move(Random random, PictureBox target); 
        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1,maxDamage);
        }
        public void Hit(int maxDamage)
        {
            hitPoints -= (maxDamage);
        }
        //==Strike player BEGIN====
        public void StrikePlayer(Player hero, bool modeRandom, int pointsDamageCausedToPlayer, PictureBox enemyPictureBox)
        {
            //we define the player boundaries
            //Player Vectors==
            List<int> heroBoundariesList = hero.PlayerBoundaries(modeRandom, enemyBoundariesAdjustment);
            int aX = heroBoundariesList[0];
            int aY = heroBoundariesList[1];
            int cX = heroBoundariesList[2];
            int cY = heroBoundariesList[3];


            //Enemy vectors==
            int a1X = EnemyPictureBoxFloor.Location.X;
            int a1Y = EnemyPictureBoxFloor.Location.Y;
            int c1X = EnemyPictureBoxFloor.Location.X + EnemyPictureBoxFloor.Width;
            int c1Y = EnemyPictureBoxFloor.Location.Y + EnemyPictureBoxFloor.Height;


            //==Calculation Ghost Strike BEGIN==
            //When Ghost is on hitting area close to the Player, the ghost strikes and goes
            //on top of the player.
            //the player reduces his Energy Level by 5 Points.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
                //Only the Fireball goes through the hero
                if (enemyPictureBox.Name.Equals("FireballPictureBoxFloor"))
                {
                    //Hero reduces its strenght.
                    hero.HitPoints -= pointsDamageCausedToPlayer;
                }
                //all the rest of the enemies stand on the player
                else
                {
                    //Enemy goes to the same position as the player. and stays there for 10 miliseconds
                    EnemyPictureBoxFloor.Location = hero.PlayerPictureFloor.Location;
                    System.Threading.Thread.Sleep(200);
                    //Hero reduces its strenght.
                    hero.HitPoints -= pointsDamageCausedToPlayer;
                    //Enemy moves to a new random location to start over
                    Point p = new Point();
                    p.X = random.Next(138, 962);
                    p.Y = random.Next(85, 366);
                    EnemyPictureBoxFloor.Location = p;
                }             
            }
            //==Calculation Enemy Strike END==
        }
        //==Strike player END======
        protected bool NearPlayer()
        {
            return (Nearby(game.PlayerLocation, NearPlayerDistance));
        }

        protected Direction FindPlayerDirection (Point playerLocation)
        {
            Direction directionToMove;

            if (playerLocation.X > location.X + 10)
            {
                directionToMove = Direction.Right;
            }
            else if(playerLocation.X < location.X -10)
            {
                directionToMove = Direction.Left;
            }

            else if (playerLocation.Y < location.Y - 10)
            {
                directionToMove = Direction.Up;
            }

            else 
            {
                directionToMove = Direction.Down;
            }

            return directionToMove;
        }

       
    }
}
