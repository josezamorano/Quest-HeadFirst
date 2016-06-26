using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    class Sword : Weapon
    {
        //==Constructors BEGIN=====
        public Sword():base()
        {

        }
        public Sword(Game game,Point location ) : base(game, location)
        {

        }
        //==Constructors END=======
        private int pointsDamageCausedToEnemy = 3;
        public int PointsDamageCausedToEnemy
        {
            get
            {
                return pointsDamageCausedToEnemy;
            }
        }

        public override string Name
        {
            get { return "Sword"; }
        }

        public override void Attack(Direction direction, Random random)
        {
           
        }

        public override bool Attack(PictureBox target)
        {
            bool hitEnemy = false;
             //==We define the vectors for the Enemy target ==
            int aX = target.Location.X;
            int aY = target.Location.Y;
            int cX = target.Location.X + target.Width;
            int cY = target.Location.Y + target.Height;
                               
            //==We define the Vectors for the Sword Weapon==          
           
            int a1X = WeaponPictureFloor.Location.X;
            int a1Y = WeaponPictureFloor.Location.Y;
            int c1X = WeaponPictureFloor.Location.X + WeaponPictureFloor.Width;
            int c1Y = WeaponPictureFloor.Location.Y + WeaponPictureFloor.Height;
            //When the weapon is in the boundaries of the enemy, the image stops moving and dissapear.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
               
                hitEnemy = true;
            }

            return hitEnemy;
        }


        public bool SwingSword(PictureBox heroFloor, PictureBox enemyPictureBox)
        {
            bool swingSuccessHit = false;
            //==Generate The weapon Movement BEGIN====                 
            int hitCounter = 0;
            Point p = new Point();
            for (int i = 0; i <= 10; i++)
            {
                WeaponPictureFloor.Image = Properties.Resources.swordPointingUpRightTranspBknd;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                p.X = heroFloor.Location.X + 50;
                p.Y = heroFloor.Location.Y - 10;
                WeaponPictureFloor.Location = p;
                bool result = Attack(enemyPictureBox);
                if(result)
                {
                    hitCounter++;
                }

                p.X = heroFloor.Location.X + 48;
                p.Y = heroFloor.Location.Y - 30;
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                p.X = heroFloor.Location.X + 25;
                p.Y = heroFloor.Location.Y - 40;
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                p.X = heroFloor.Location.X + 5;
                p.Y = heroFloor.Location.Y - 40;
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                p.X = heroFloor.Location.X - 20;
                p.Y = heroFloor.Location.Y - 40;
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }
                WeaponPictureFloor.Image = Properties.Resources.swordPointingUpLeftTranspBknd;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;

                p.X = (heroFloor.Location.X - 30);
                p.Y = (heroFloor.Location.Y - 25);
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                p.X = (heroFloor.Location.X - 40);
                p.Y = (heroFloor.Location.Y - 10);
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

            }
            if(hitCounter>0)
            {
                swingSuccessHit = true;
            }
            return swingSuccessHit;
            //==Generate The weapon Movement END====       
        }


    }
}
