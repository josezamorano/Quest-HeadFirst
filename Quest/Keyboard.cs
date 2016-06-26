using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Quest;

namespace Quest
{
    class Keyboard
    {
        //==Identify the keys that are being pressed BEGIN==========
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);
        byte[] keys = new byte[256];
        //==Identify the keys that are being pressed END============


        /// <summary>
        /// The keyboard motion setup allows for a combination of keys to enable movement and give freedom
        /// to the player to press the arrow keys (Player movement) and the space bar (weapon movement)
        /// creating independency of motion between those two elements.
        /// </summary>
        /// <param name="hero">Player that is moving in the dungeon</param>
        /// <param name="heroMotionStatus">Enumeration that controls the motion status of the player</param>
        /// <param name="picturesList">List of images that are going to be moved at the same time.
        /// It can be player only or player and weapon.</param>
        /// <returns>The status of the player movement according to the key pressed as enumerator.</returns>
         public Enum CombinedKeysPress(Player hero, Enum heroMotionStatus, List<PictureBox> picturesList)      
        {
            //==Combined keys Moves BEGIN=====
            GetKeyboardState(keys);

            int spaceKey = keys[(int)Keys.Space];
            int arrowUpKey = keys[(int)Keys.Up];
            int arrowDownKey = keys[(int)Keys.Down];
            int arrowLeftKey = keys[(int)Keys.Left];
            int arrowRightKey = keys[(int)Keys.Right];


            if ((spaceKey == 129 || spaceKey == 128) && (arrowLeftKey == 129 || arrowLeftKey == 128))
            {

                heroMotionStatus = Player.PlayerMotionState.MovingLeft;
                foreach (PictureBox item in picturesList)
                {
                    item.Left -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }
            }
            if ((spaceKey == 129 || spaceKey == 128) && (arrowRightKey == 129 || arrowRightKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingRight;
                foreach (PictureBox item in picturesList)
                {
                    item.Left += 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            if ((spaceKey == 129 || spaceKey == 128) && (arrowUpKey == 129 || arrowUpKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingUp;
                foreach (PictureBox item in picturesList)
                {
                    item.Top -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            if ((spaceKey == 129 || spaceKey == 128) && (arrowDownKey == 129 || arrowDownKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingDown;
                foreach (PictureBox item in picturesList)
                {
                    item.Top += 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            //==Combined keys Moves END=====
            return heroMotionStatus;
        }

        /// <summary>
        /// This keyboard motion setup allows the player freedom of movement giving them the
        /// possibility to move UP, DOWN, LEFT, RIGHT, DIAGONAL UP LEFT, DIAGONAL UP RIGHT,
        /// DIAGONAL DOWN LEFT and DIAGONAL DOWN RIGHT.
        /// </summary>
        /// <param name="e">Key events Argument</param>
        /// <param name="hero">Player that is moving in the dungeon</param>
        /// <param name="heroMotionStatus">Enumeration that controls the motion status of the player</param>
        /// <param name="picturesList">List of images that are going to be moved at the same time.
        /// It can be player only or player and weapon.</param>
        /// <returns>The status of the player movement according to the key pressed as enumerator.</returns>
      
        public Enum SingleKeyPress(Player hero ,Enum heroMotionStatus,List<PictureBox> picturesList)
        {
            //==Single keys Moves BEGIN=====
            GetKeyboardState(keys);

            int spaceKey = keys[(int)Keys.Space];
            int arrowUpKey = keys[(int)Keys.Up];
            int arrowDownKey = keys[(int)Keys.Down];
            int arrowLeftKey = keys[(int)Keys.Left];
            int arrowRightKey = keys[(int)Keys.Right];


            if ((arrowLeftKey == 129 || arrowLeftKey == 128))
            {

                heroMotionStatus = Player.PlayerMotionState.MovingLeft;
                foreach (PictureBox item in picturesList)
                {
                    item.Left -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }
            }
            if ((arrowRightKey == 129 || arrowRightKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingRight;
                foreach (PictureBox item in picturesList)
                {
                    item.Left += 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            if ((arrowUpKey == 129 || arrowUpKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingUp;
                foreach (PictureBox item in picturesList)
                {
                    item.Top -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            if ((arrowDownKey == 129 || arrowDownKey == 128))
            {
                heroMotionStatus = Player.PlayerMotionState.MovingDown;
                foreach (PictureBox item in picturesList)
                {
                    item.Top += 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }

            }
            //==Single keys Moves END=====
            return heroMotionStatus;
        }

        /// <summary>
        /// Player can move only UP, DOWN, LEFT, RIGHT. There is no Diagonal movement
        /// </summary>
        /// <param name="e">Key events Argument</param>
        /// <param name="hero">Player that is moving in the dungeon</param>
        /// <param name="heroMotionStatus">Enumeration that controls the motion status of the player</param>
        /// <param name="picturesList">List of images that are going to be moved at the same time.
        /// It can be player only or player and weapon.</param>
        /// <returns>The status of the player movement according to the key pressed as enumerator.</returns>
       public Enum SingleKeyPress(System.Windows.Forms.KeyEventArgs e,Player hero, Enum heroMotionStatus, List<PictureBox> picturesList)
        {
            /***/
            switch (e.KeyData)
            {
                case Keys.Space:

                    heroMotionStatus = Player.PlayerMotionState.NotMoving;
                    break;


                case Keys.Left:
                    heroMotionStatus = Player.PlayerMotionState.MovingLeft;
                    foreach (PictureBox item in picturesList)
                    {
                        item.Left -= 1;
                        hero.DungeonFloorLimitsCheckForPlayer(item);
                    }
                    break;
                case Keys.Right:

                    heroMotionStatus = Player.PlayerMotionState.MovingRight;
                    foreach (PictureBox item in picturesList)
                    {
                        item.Left += 1;
                        hero.DungeonFloorLimitsCheckForPlayer(item);
                    }
                    break;
                case Keys.Up:
                    heroMotionStatus = Player.PlayerMotionState.MovingUp;
                    foreach (PictureBox item in picturesList)
                    {
                        item.Top -= 1;
                        hero.DungeonFloorLimitsCheckForPlayer(item);
                    }

                    break;

                case Keys.Down:

                    heroMotionStatus = Player.PlayerMotionState.MovingDown;

                    foreach (PictureBox item in picturesList)
                    {
                        item.Top += 1;
                        hero.DungeonFloorLimitsCheckForPlayer(item);
                    }
                    break;


            }
            /***/
            return heroMotionStatus;
        }

    }
}
