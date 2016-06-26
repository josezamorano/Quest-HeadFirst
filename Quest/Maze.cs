using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace Quest
{
    class Maze : Weapon
    {
        //=====Constructor BEGIN=====
        public Maze(): base()
        {

        }
        //=====Constructor END=======

        public override string Name
        {
            get { return "Maze"; }
        }

         private int pointsDamageCausedToEnemy = 5;
         public int PointsDamageCausedToEnemy
         {
             get
             {
                 return pointsDamageCausedToEnemy;
             }
         }



        public override void Attack(Direction direction, Random random)
        {
            //throw new NotImplementedException();
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


        public bool SwingMaze(PictureBox heroFloor, PictureBox enemyPictureBox)
        {
            bool swingSuccessHit = false;
            //==Generate The weapon Movement BEGIN====                 
            int hitCounter = 0;
            Point p = new Point();
            for (int i = 0; i <= 10; i++)
            {
                WeaponPictureFloor.Image = Properties.Resources.FlailPointingRightTranspBkgr;
                WeaponPictureFloor.Width = 100;
                WeaponPictureFloor.Height = 50;
                p.X = heroFloor.Location.X + 50;
                p.Y = heroFloor.Location.Y - 10;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;

                
                bool result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                WeaponPictureFloor.Image = Properties.Resources.FlailPointingAngleRightTranspBackgr;
                WeaponPictureFloor.Width = 96;
                WeaponPictureFloor.Height = 86;
                p.X = heroFloor.Location.X + 48;
                p.Y = heroFloor.Location.Y - 60;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
               
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                p.X = heroFloor.Location.X + 55;
                p.Y = heroFloor.Location.Y - 80;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }
                WeaponPictureFloor.Image = Properties.Resources.FlailPointingUpTranspBkgr;
                WeaponPictureFloor.Width = 44;
                WeaponPictureFloor.Height = 98;
                p.X = heroFloor.Location.X + 10;
                p.Y = heroFloor.Location.Y - 80;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }
                WeaponPictureFloor.Image = Properties.Resources.FlailPointingAngleLeftTranspBackgr;
                WeaponPictureFloor.Width = 88;
                WeaponPictureFloor.Height = 98;
                p.X = heroFloor.Location.X - 75;
                p.Y = heroFloor.Location.Y - 70;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }
               
                p.X = (heroFloor.Location.X - 85);
                p.Y = (heroFloor.Location.Y - 75);
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Location = p;
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                WeaponPictureFloor.Image = Properties.Resources.FlailPointingLeftTranspBkgr;
                WeaponPictureFloor.Width = 100;
                WeaponPictureFloor.Height = 50;
                p.X = (heroFloor.Location.X - 115);
                p.Y = (heroFloor.Location.Y - 20);
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                
                result = Attack(enemyPictureBox);
                if (result)
                {
                    hitCounter++;
                }

                WeaponPictureFloor.Image = Properties.Resources.mace;
                WeaponPictureFloor.Width = 50;
                WeaponPictureFloor.Height = 50;
                p.X = (heroFloor.Location.X + 40);
                p.Y = (heroFloor.Location.Y - 30);
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;          
            }

            if (hitCounter > 0)
            {
                swingSuccessHit = true;
            }

            return swingSuccessHit;
            //==Generate The weapon Movement END====       
        }


    }
}
