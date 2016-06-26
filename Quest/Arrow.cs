using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace Quest
{
    class Arrow: Weapon
    {
        List<Player.PlayerMotionState> MotionList = new List<Player.PlayerMotionState>();
        //==Constructors==
        public Arrow():base()
        {

        }

        private int pointsDamageCausedToEnemy = 6;
        public int PointsDamageCausedToEnemy
        {
            get
            {
                return pointsDamageCausedToEnemy;
            }
        }

        public override string Name
        {
            get { return "Arrow"; }
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
                               
            //==We define the Vectors for the Weapon==          
           
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


        public bool ShootArrow(PictureBox heroFloor, PictureBox enemyPictureBox, Enum HeroMotionStatus)
        {
            bool shootSuccessHit = false;
            //==Generate The weapon Movement BEGIN====                 
            int hitCounter = 0;
            Point p = new Point();

            if ((Player.PlayerMotionState)HeroMotionStatus == Player.PlayerMotionState.MovingRight)
            {
                 WeaponPictureFloor.Image = Properties.Resources.arrowPointingRight;             
                p.X = heroFloor.Location.X + 50;
                p.Y = heroFloor.Location.Y - 5;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                while(p.X <1100)           
                {
                    p.X += 15;
                    p.Y = p.Y;

                    WeaponPictureFloor.Location = p;
             
                    bool result = Attack(enemyPictureBox);
                    if (result)
                    {
                        hitCounter++;
                    }
                     
                }
            }
           
            if ((Player.PlayerMotionState)HeroMotionStatus == Player.PlayerMotionState.MovingLeft)
            {
                WeaponPictureFloor.Image = Properties.Resources.arrowPointingLeft;
                
                p.X = heroFloor.Location.X + 50;
                p.Y = heroFloor.Location.Y - 5;
                WeaponPictureFloor.Location = p;
                WeaponPictureFloor.Refresh();
                WeaponPictureFloor.Visible = true;
                while (p.X > 50)
               
                {
                    p.X -= 15;
                    p.Y = p.Y;

                    WeaponPictureFloor.Location = p;
                   
                    bool result = Attack(enemyPictureBox);
                    if (result)
                    {
                        hitCounter++;
                    }

                }
            }
           
             WeaponPictureFloor.Image = Properties.Resources.bowFacingDownTranspBknd;
            
            p.X = heroFloor.Location.X - 40;
            p.Y = heroFloor.Location.Y - 10;
            WeaponPictureFloor.Location = p;
            WeaponPictureFloor.Refresh();
            WeaponPictureFloor.Visible = true;
          
            if (hitCounter > 0)
            {
                shootSuccessHit = true;
            }
            return shootSuccessHit;
            //==Generate The weapon Movement END====       
        }


       
    }
}
