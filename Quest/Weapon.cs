using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    abstract class Weapon : Mover
    {
        //==Extra Variable PictureBox
        public PictureBox WeaponPictureFloor = new PictureBox();
        protected Game game;
        private bool pickedUp;
        public bool PickedUp
        { 
            get
            {
                return pickedUp;
            }
            set
            {
                pickedUp = value;
            }
        }
        private Point location;
        public Point Location
        {
            get
            {
                return location;
            }
        }
        //==Constructors BEGIN===
        public Weapon():base(){}
        public Weapon(Game game, Point location): base(game, location)
        {
            this.game = game;
            this.location = location;
            pickedUp = false;
        }

        //==Constructors END
        public void PickUpWeapon() { pickedUp = true; }
        public abstract string Name {get; }
        public abstract void Attack(Direction direction, Random random);
        public virtual bool Attack(PictureBox enemyPictureBox, Enemy enemy) {  return true; }
        public virtual bool Attack(PictureBox enemy) { return true; }

        public void StrikeEnemy(Player hero, List<PictureBox> heroAndWeaponsPicBoxList, List<Weapon> weaponsList, List<Enemy> enemiesObjectList,Enum heroPosition)
        {
             bool shootWeapon=false;
             foreach (var weapon in weaponsList)
             {           
                if(weapon is Arrow)
                {
                    //We check if it has been picked up
                    if (weapon.pickedUp == true)
                    {
                        //The specific situation of the Sword
                        Arrow arrow = (Arrow)weapon;
                        foreach (var enemyObject in enemiesObjectList)
                        {
                            if (arrow.PickedUp == true)
                            {
                                //we shoot the arrow.If we hit the enemy, we know wich enemy got striken
                                shootWeapon = arrow.ShootArrow(hero.PlayerPictureFloor, enemyObject.EnemyPictureBoxFloor, heroPosition);
                            }
                            //If we hit the enemy, the enemy reduces its energy

                            if (shootWeapon == true)
                            {
                                if (enemyObject is Bat)
                                {
                                    Bat batEnemy = (Bat)enemyObject;

                                    batEnemy.HitPoints -= arrow.PointsDamageCausedToEnemy;
                                }

                                if (enemyObject is Ghost)
                                {
                                    Ghost ghostEnemy = (Ghost)enemyObject;
                                    ghostEnemy.HitPoints -= arrow.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Goul)
                                {
                                    Goul goulEnemy = (Goul)enemyObject;
                                    goulEnemy.HitPoints -= arrow.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Wizard)
                                {
                                    Wizard wizardEnemy = (Wizard)enemyObject;
                                    wizardEnemy.HitPoints -= arrow.PointsDamageCausedToEnemy;
                                }

                            }
                        }

                        

                    }
                }

             }
        }
        public void StrikeEnemy(Player hero, List<PictureBox> heroAndWeaponsPicBoxList, List<Weapon> weaponsList, List<Enemy> enemiesObjectList)
        {
            bool shootWeapon=false;
            foreach(var weapon in weaponsList)
            {
                //We check if the weapon is a sword
                if(weapon is Sword)
                {
                    //We check if it has been picked up
                    if(weapon.pickedUp==true)
                    {
                        //The specific situation of the Sword
                        Sword sword = (Sword)weapon;
                        foreach(var enemyObject in enemiesObjectList)
                        {
                            if(sword.PickedUp==true)
                            {
                                //we swing the sword.If we hit the enemy, we know wich enemy got striken
                                 shootWeapon  = sword.SwingSword(hero.PlayerPictureFloor, enemyObject.EnemyPictureBoxFloor);        
                            }
                            //If we hit the enemy, the enemy reduces its energy
                          
                            if (shootWeapon == true)
                            {
                                if (enemyObject is Bat)
                                {
                                    Bat batEnemy = (Bat)enemyObject;

                                    batEnemy.HitPoints -= sword.PointsDamageCausedToEnemy;
                                }

                                if (enemyObject is Ghost)
                                {
                                    Ghost ghostEnemy = (Ghost)enemyObject;
                                    ghostEnemy.HitPoints -= sword.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Goul)
                                {
                                    Goul goulEnemy = (Goul)enemyObject;
                                    goulEnemy.HitPoints -= sword.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Wizard)
                                {
                                    Wizard wizardEnemy = (Wizard)enemyObject;
                                    wizardEnemy.HitPoints -= sword.PointsDamageCausedToEnemy;
                                }

                            }
                        }


                    }
                }
                //We check if the weapon is a Maze
                if (weapon is Maze)
                {
                    //We check if it has been picked up
                    if (weapon.pickedUp == true)
                    {
                        //The specific situation of the Sword
                        Maze maze = (Maze)weapon;
                        foreach (var enemyObject in enemiesObjectList)
                        {
                            if (maze.PickedUp == true)
                            {
                                //we swing the sword.If we hit the enemy, we know wich enemy got striken
                                shootWeapon = maze.SwingMaze(hero.PlayerPictureFloor, enemyObject.EnemyPictureBoxFloor);
                            }
                            //If we hit the enemy, the enemy reduces its energy

                            if (shootWeapon == true)
                            {
                                if (enemyObject is Bat)
                                {
                                    Bat batEnemy = (Bat)enemyObject;

                                    batEnemy.HitPoints -= maze.PointsDamageCausedToEnemy;
                                }

                                if (enemyObject is Ghost)
                                {
                                    Ghost ghostEnemy = (Ghost)enemyObject;
                                    ghostEnemy.HitPoints -= maze.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Goul)
                                {
                                    Goul goulEnemy = (Goul)enemyObject;
                                    goulEnemy.HitPoints -= maze.PointsDamageCausedToEnemy;
                                }
                                if (enemyObject is Wizard)
                                {
                                    Wizard wizardEnemy = (Wizard)enemyObject;
                                    wizardEnemy.HitPoints -= maze.PointsDamageCausedToEnemy;
                                }

                            }
                        }
                       
                    }
                }

            }
        }


        public void EnemyZeroEnergyCheck(Enemy enemy,Label labelsPoints,Timer timerPlayer, Timer timerEnemy)
            {
            //When bat is ON top of the Player, the player has 0 energy left, the player dies.
            
            if (enemy is Bat)
            {
                Bat batEnemy = (Bat)enemy;
                if (batEnemy.HitPoints <= 0)
                {
                    batEnemy.HitPoints = 0;
                    labelsPoints.Text = Convert.ToString(0);
                
                    batEnemy.EnemyPictureBoxFloor.Hide();
                    timerEnemy.Stop();
                }
            }
            if (enemy is Ghost)
            {
                Ghost ghostEnemy = (Ghost)enemy;
                if (ghostEnemy.HitPoints <= 0)
                {
                    ghostEnemy.HitPoints = 0;
                    labelsPoints.Text = Convert.ToString(0);
                 
                    ghostEnemy.EnemyPictureBoxFloor.Hide();
                    timerEnemy.Stop();
                }
            }
            if (enemy is Goul)
            {
                Goul goulEnemy = (Goul)enemy;
                if (goulEnemy.HitPoints <= 0)
                {
                    goulEnemy.HitPoints = 0;
                    labelsPoints.Text = Convert.ToString(0);
                  
                    goulEnemy.EnemyPictureBoxFloor.Hide();
                    timerEnemy.Stop();
                }
            }
            if (enemy is Wizard)
            {
                Wizard wizardEnemy = (Wizard)enemy;
                if (wizardEnemy.HitPoints <= 0)
                {
                    wizardEnemy.HitPoints = 0;
                    labelsPoints.Text = Convert.ToString(0);
               
                    wizardEnemy.EnemyPictureBoxFloor.Hide();

                    timerEnemy.Stop();
                }
            }  
        }

        protected bool DamageEnemy(Direction direction, int radius, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance<radius ; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if(Nearby(enemy.Location, target, radius))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }           
                }
                target = Move(direction, target, game.Boundaries);
            }        
            return false;
        }        

    }
}
