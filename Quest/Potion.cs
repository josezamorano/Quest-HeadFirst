using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    class Potion : Weapon
    {
        //==Constructors BEGIN============
        public Potion(): base()
        {

        }
         public Potion(Game game,Point location ) : base(game, location)
        {

        }
         //==Constructors END============
        public override string Name
        {
            get { return "Potion"; }
        }

        public override void Attack(Direction direction, Random random)
        {
            //throw new NotImplementedException();
        }

        public override bool Attack(PictureBox enemy)
        {
            return true;

        }

        public void PotionUsedCheck(PictureBox potion, Player player, Timer timer1)
        {
           
            //==We define the vectors for the potion target ==
            int aX = potion.Location.X;
            int aY = potion.Location.Y;
            int cX = potion.Location.X + potion.Width;
            int cY = potion.Location.Y + potion.Height;

            //==We define the Vectors for the Player==          

            int a1X = player.PlayerPictureFloor.Location.X;
            int a1Y = player.PlayerPictureFloor.Location.Y;
            int c1X = player.PlayerPictureFloor.Location.X + player.PlayerPictureFloor.Width;
            int c1Y = player.PlayerPictureFloor.Location.Y + player.PlayerPictureFloor.Height;
            //When the potion is in the boundaries of the Player, the potion provide new features and dissapear.
            if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
            {
 
                if (potion.Name == "BluePotionPictureFloor")
                {
                    player.HitPoints += 30;
                    timer1.Interval = 10;
                    potion.Hide();
               
                }
                if (potion.Name == "RedPotionPictureFloor")
                {
                    player.HitPoints += 45;
                    timer1.Interval = 5;
                    potion.Hide();
                
                }
            }

     
        }

    }
}
