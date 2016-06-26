using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Quest
{
    class Game
    {
        public Dictionary<string, object> dictionaryGame = new Dictionary<string, object>();
        public List<Enemy> Enemies;
        public Weapon weaponInRoom;

        public Player hero;
        public Bat bat;
        public Ghost ghost;
        public Goul goul;
        public Wizard wizard;
        public Arrow arrow;
        public Sword sword;
        public Maze maze;
        public Potion potion;


        public Point PlayerLocation
        {
            get
            {
                return hero.Location;
            }
        }

        public int PlayerHitPoints
        {
            get
            {
                return hero.HitPoints;
            }
        }


        public List<string> PlayerWeapons
        {
            get
            {
                return hero.Weapons;
            }
        }

        private int level = 0;
        public int Level
        {
            get
            {
                return level;
            }
        }

        private Rectangle boundaries;
        public Rectangle Boundaries
        {
            get
            {
                return boundaries;
            }
        }





        //==Constructor BEGIN==
        public Game(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            hero = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70));
        }

        public Game()
        {

        }
        //==Constructor END====


        
        public void Move (Direction direction, Random random)
        {
            hero.Move(direction);
                foreach(Enemy enemy in Enemies)
                {
                    enemy.Move(random);
                }
        }


        public void Equip(string weaponName)
        {
            hero.Equip(weaponName);
        }

        public bool CheckPlayerInventory(string weaponName)
        {
            return hero.Weapons.Contains(weaponName);
        }


        public void HitPlayer(int maxDamage, Random random)
        {
            hero.Hit(maxDamage, random);
        }


        public void IncreasePlayerHealth(int health, Random random)
        {
            hero.IncreaseHealth(health, random);
        }

        public void Attack(Direction direction, Random random)
        {
            hero.Attack(direction, random);
            
                foreach(Enemy enemy in Enemies)
                {
                    enemy.Move(random);
                }
            
        }

        private Point GetRandomLocation (Random random)
        {
            return new Point(boundaries.Left + random.Next(boundaries.Right / 10 - boundaries.Left/10) *10,
                boundaries.Top +
                random.Next(boundaries.Bottom/10 - boundaries.Top/10)*10);
        }

      

    }
}
