using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace Quest
{
    public partial class Form1 : Form
    {
        //==Identify the keys that are being pressed BEGIN==========
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);
        byte[] keys = new byte[256];
        //==Identify the keys that are being pressed END============

        Keyboard keyboard = new Keyboard();
        //==Internal Dashboard Setup BEGIN==========================

        private bool randomMode = false;

        //==Movements List BEGIN====
        //==List of Movements required to identify the previous direction of the player
        //==to enable it to shoot arrows left or right when moving up and down based on 
        //==previous last movement left or right.
        List<Player.PlayerMotionState> HeroMotionList = new List<Player.PlayerMotionState>();

        //==Movements List END======

        //==Internal Dashboard Setup END=============================      
        int counter = 0;
        int levelCounter = 1;
      
        Game         game = new Game();
      
        Sword       sword = new Sword();
        Arrow       arrow = new Arrow();
        Maze         maze = new Maze();
        Potion     potion = new Potion();
        Player       hero = new Player();
        Bat           bat = new Bat();
        Ghost       ghost = new Ghost();
        Goul         goul = new Goul();
        Wizard     wizard = new Wizard();
        Fireball fireball = new Fireball();
        Random       rand = new Random();

        List<Enemy> enemiesObjectList = new List<Enemy>();
        List<Weapon> weaponsAvailableObjectList = new List<Weapon>();
        
        List<PictureBox> heroAndWeaponsPicBoxList = new List<PictureBox>();
        List<PictureBox> enemiesPicBoxList = new List<PictureBox>();

        List<Label> labelsPointsList = new List<Label>();
        List<Timer> timersList = new List<Timer>();


        //we set The Movility Status of the Player==
        Player.PlayerMotionState heroMotionStatus = Quest.Player.PlayerMotionState.NotMoving;
        
         //we set The Movility Status of the Fireball Thrown by th Wizard==
        Wizard.WizardMotionState WizardMotionStatus = Quest.Wizard.WizardMotionState.NotMoving;
       ////==Rectangles Drawing BEGIN===
       ////==Rectangle drawing functionality to test the boundaries of each object in the dungeon floor.
       ////==Rectangle(Location-X,Location-Y, width,height)       
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        System.Drawing.Pen pen = new Pen(System.Drawing.Color.Blue);
        System.Drawing.Graphics formGraphics;
       ////==Rectangles Drawing END=====
       

       
        public Form1()
        {                    
            InitializeComponent();
          
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            this.KeyPress +=new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);

           
            //We transfer the characteristics of each Floor pictureBox to the
            //objects pictureBox so the values can be transferred to each 
            //corresponding class and allow Vectors and position management
            //of the objects pictureBoxes within the object class
            hero.PlayerPictureFloor = PlayerPictureFloor;
            heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
            PlayerPointsLabel.Text = hero.HitPoints.ToString();

            //==BAT Begin=====
           // bat.BatPictureBoxFloor = BatPictureBoxFloor;
            bat.EnemyPictureBoxFloor = BatPictureBoxFloor;
            BatPointsLabel.Text = bat.HitPoints.ToString();
            //==BAT End=======
            //==GHOST Begin===

            ghost.EnemyPictureBoxFloor = GhostPictureBoxFloor;     
            GhostPointsLabel.Text = ghost.HitPoints.ToString();
            //==GHOST END=====

            //==GOUL Begin===
            goul.EnemyPictureBoxFloor = GoulPictureBoxFloor;
            GoulPointsLabel.Text = goul.HitPoints.ToString();
            //==GOUL END=====

            //==WIZARD Begin===
            wizard.EnemyPictureBoxFloor = WizardPictureBoxFloor;
            WizardPointsLabel.Text = wizard.HitPoints.ToString();
            //==WIZARD END=====

            //==FIREBALL Begin===
            fireball.EnemyPictureBoxFloor =FireballPictureBoxFloor;
           
            //==FIREBALL END=====
            //==INITIAL LEVEL SELECTED ==

            DialogResult dialogDashboard = MessageBox.Show("Select Yes if you wish your enemies to chase you, otherwise they will move randomly. ", "Select your type of game", MessageBoxButtons.YesNo);
            if (dialogDashboard == DialogResult.No) {randomMode = true; }
            if (dialogDashboard == DialogResult.Yes) { randomMode = false; }

            SelectLevel(levelCounter);
            levelCounter++;

            //we initialize the timer for the Player
            timer1.Enabled = true;
            timer1.Interval = 18;
            timer1.Start();

        }

        private void SelectLevel(int level)
        {
            Random rand = new Random();

            
            switch(level)
            {
                case 1:
                   hero.HitPoints = 20;
                   LevelNumberGamelabel.Text = level.ToString();
                    //==Bat Section BEGIN==
                   bat.HitPoints = 20;
                   if(randomMode==true)
                   {
                       //We set the Random Position of the bat
                       Point p = new Point();
                       p.X = rand.Next(136,895);
                       p.Y = rand.Next(85,300);
                       BatPictureBoxFloor.Location = p;

                       //And we set the random direction of the bat
                       bat.batDirection =  (Bat.BatMotionState)bat.SelectDirection() ;
                       
                   }
                    //==Bat Section END====
                    int totalList = heroAndWeaponsPicBoxList.Count -1;
                    if(heroAndWeaponsPicBoxList.Count>1)
                    {
                        for(int i=totalList;i>0;i--)
                        {
                            heroAndWeaponsPicBoxList.RemoveAt(i);
                        }
                    }
                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true){ timer2.Interval = 500; }
                    else {timer2.Interval = 1000;}
                    timer2.Start();
                    //==Timer for the GHOST END=============
                    //==Timer for the BAT BEGIN=============
                    timer3.Enabled = true;
                    if (randomMode == true){ timer3.Interval = 200;}
                    else{timer3.Interval = 1000;}
                    timer3.Start();
                    //==Timer for the BAT END=============
                    //==Timer for the GOUL BEGIN=============
                    timer4.Enabled = true;
                    if (randomMode == true) {timer4.Interval = 200;}
                    else{ timer4.Interval = 1000;}
                    timer4.Start();
                    //==Timer for the GOUL END=============
                    //==Timer for the WIZARD BEGIN=============
                    timer5.Enabled = true;
                    if (randomMode == true){timer5.Interval = 200; }
                    else{timer5.Interval = 1000; }
                    timer5.Start();
                    //==Timer for the WIZARD END=============

                    //==Timer for the FIREBALL BEGIN=============
                    timer6.Enabled = true;
                    if (randomMode == true){timer6.Interval = 2000;}
                    else{ timer6.Interval = 10000; }
                    //timer6.Start();
                    //==Timer for the FIREBALL END=============
                    weaponsAvailableObjectList.Add(sword);
                    enemiesObjectList.Add(bat);
                    enemiesPicBoxList.Add(bat.EnemyPictureBoxFloor);                
                    labelsPointsList.Add(BatPointsLabel);
                    timersList.Add(timer3);

                    SwordPictureFloor.Show();
                    sword.WeaponPictureFloor.Show();
                    MazePictureFloor.Hide();
                    maze.WeaponPictureFloor.Hide();
                    BowPictureFloor.Hide();
                  
                    arrow.WeaponPictureFloor.Hide();
                    BluePotionPictureFloor.Hide();
                    RedPotionPictureFloor.Hide();

                    ghost.EnemyPictureBoxFloor.Hide();
                    GhostPictureBoxFloor.Hide();
                    timer2.Stop();
                    goul.EnemyPictureBoxFloor.Hide();
                    GoulPictureBoxFloor.Hide();
                    timer4.Stop();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    fireball.EnemyPictureBoxFloor.Hide();
                    FireballPictureBoxFloor.Hide();
                    timer6.Stop();
                    break;

                case 2:
                    hero.HitPoints = 20;
                    LevelNumberGamelabel.Text = level.ToString();
                    ghost.HitPoints = 25;

                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();                
                    labelsPointsList.Clear();
                    timersList.Clear();

                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
                    weaponsAvailableObjectList.Add(sword);
                    enemiesObjectList.Add(ghost);
                    enemiesPicBoxList.Add(ghost.EnemyPictureBoxFloor);                
                    labelsPointsList.Add(GhostPointsLabel);
                    timersList.Add(timer2);
                  
                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true){ timer2.Interval = 500; }
                    else{ timer2.Interval = 1000; }
                    timer2.Start();
                    //==Timer for the GHOST END=============
                   
                    //==Ghost Section BEGIN======
                   
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GhostPictureBoxFloor.Location = p;

                    }
                    ghost.EnemyPictureBoxFloor.Show();
                    GhostPictureBoxFloor.Show();
                    //==Ghost Section END======
                    SwordPictureFloor.Show();
                    sword.WeaponPictureFloor.Show();

                    MazePictureFloor.Hide();
                    maze.WeaponPictureFloor.Hide();
                    BowPictureFloor.Hide();
                  
                    arrow.WeaponPictureFloor.Hide();
                    BluePotionPictureFloor.Hide();
                    RedPotionPictureFloor.Hide();
                    bat.EnemyPictureBoxFloor.Hide();
                    BatPictureBoxFloor.Hide();
                    timer3.Stop();
                    goul.EnemyPictureBoxFloor.Hide();
                    GoulPictureBoxFloor.Hide();
                    timer4.Stop();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    break;

                
                //This is case 3 really
                case 3:
                    hero.HitPoints = 20;
                    LevelNumberGamelabel.Text = level.ToString();
                    goul.HitPoints = 25;

                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();                
                    labelsPointsList.Clear();
                    timersList.Clear();

                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
                    weaponsAvailableObjectList.Add(maze);
                    enemiesObjectList.Add(goul);
                    enemiesPicBoxList.Add(goul.EnemyPictureBoxFloor);                
                    labelsPointsList.Add(GoulPointsLabel);   
                    timersList.Add(timer4);
                    
                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GOUL BEGIN=============
                    timer4.Enabled = true;
                    if (randomMode == true) { timer4.Interval = 300; }
                    else { timer4.Interval = 700; }
                    timer4.Start();
                    //==Timer for the GOUL END=============
                   
                    //==Goul Section BEGIN======
                    if (randomMode == true)
                    {
                        //We set the Random Position of the Goul
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GoulPictureBoxFloor.Location = p;
                        //And we set the random direction of the Goul
                        goul.goulDirection = (Goul.GoulMotionState)goul.SelectGoulDirection();
                    }
                 
                   // GoulPictureBoxFloor= goul.EnemyPictureBoxFloor ;
                    goul.EnemyPictureBoxFloor.Show();
                    GoulPictureBoxFloor.Show();
                  
                    //==Goul Section END======
                    MazePictureFloor.Show();
                    maze.WeaponPictureFloor.Show();
                    SwordPictureFloor.Hide();
                    sword.WeaponPictureFloor.Hide();
                    BowPictureFloor.Hide();
                  
                    arrow.WeaponPictureFloor.Hide();                  
                    BluePotionPictureFloor.Hide();
                    RedPotionPictureFloor.Hide();
                    BluePotionPictureFloor.Hide();
                    RedPotionPictureFloor.Hide();
                    ghost.EnemyPictureBoxFloor.Hide();
                    GhostPictureBoxFloor.Hide();
                    timer2.Stop();
                    bat.EnemyPictureBoxFloor.Hide();
                    BatPictureBoxFloor.Hide();
                    timer3.Stop();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    fireball.EnemyPictureBoxFloor.Hide();
                    FireballPictureBoxFloor.Hide();
                    timer6.Stop();
                    break;                
                case 4:
                    hero.HitPoints = 30;
                    LevelNumberGamelabel.Text = level.ToString();
                    bat.HitPoints = 20;                    
                    ghost.HitPoints = 25;
                    //==========================
                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();
                    labelsPointsList.Clear();
                    timersList.Clear();

                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
                    weaponsAvailableObjectList.Add(sword);
                    weaponsAvailableObjectList.Add(arrow);
                    enemiesObjectList.Add(ghost);
                    enemiesObjectList.Add(bat);
                    enemiesPicBoxList.Add(ghost.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(bat.EnemyPictureBoxFloor);
                    labelsPointsList.Add(GhostPointsLabel);
                    labelsPointsList.Add(BatPointsLabel);
                    timersList.Add(timer2);
                    timersList.Add(timer3);

                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true){timer2.Interval = 500;}
                    else{timer2.Interval = 1000;}
                    timer2.Start();
                    //==Timer for the GHOST END=============
                    //==Timer for the BAT BEGIN=============
                    timer3.Enabled = true;
                    if (randomMode == true) {timer3.Interval = 200;}
                    else { timer3.Interval = 1000;}
                    timer3.Start();
                    //==Timer for the BAT END=============
                    //==Ghost Section BEGIN======
                    ghost.HitPoints = 5;
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GhostPictureBoxFloor.Location = p;

                    }
                    ghost.EnemyPictureBoxFloor.Show();
                    GhostPictureBoxFloor.Show();
                    
                    //==Ghost Section END======            
                    BluePotionPictureFloor.Show();
                    BowPictureFloor.Show();
                    arrow.WeaponPictureFloor.Show();
                    SwordPictureFloor.Show();
                    sword.WeaponPictureFloor.Show();
                    bat.EnemyPictureBoxFloor.Show();
                    BatPictureBoxFloor.Show();
                    //==========================
                    maze.WeaponPictureFloor.Hide();
                    MazePictureFloor.Hide();
                    RedPotionPictureFloor.Hide();
                    goul.EnemyPictureBoxFloor.Hide();
                    GoulPictureBoxFloor.Hide();
                    timer4.Stop();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    break;
                case 5:
                    hero.HitPoints = 35;
                    LevelNumberGamelabel.Text = level.ToString();
                                 
                    ghost.HitPoints = 25;
                    goul.HitPoints = 35;             
                    //==========================
                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();
                    labelsPointsList.Clear();
                    timersList.Clear();
                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);                  
                    weaponsAvailableObjectList.Add(arrow);
                    weaponsAvailableObjectList.Add(maze);
                    enemiesObjectList.Add(ghost);                
                    enemiesObjectList.Add(goul);                 
                    enemiesPicBoxList.Add(ghost.EnemyPictureBoxFloor);                  
                    enemiesPicBoxList.Add(goul.EnemyPictureBoxFloor);
                    labelsPointsList.Add(GhostPointsLabel);                 
                    labelsPointsList.Add(GoulPointsLabel);             
                    timersList.Add(timer2);             
                    timersList.Add(timer4);
                   
                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true){ timer2.Interval = 500; }
                    else{ timer2.Interval = 1000;}
                    timer2.Start();
                    //==Timer for the GHOST END=============
                    
                    //==Timer for the GOUL BEGIN=============
                    timer4.Enabled = true;
                    if (randomMode == true){timer4.Interval = 200;}
                    else{ timer4.Interval = 1000; }
                    timer4.Start();
                    //==Timer for the GOUL END=============
                   
                    //==Ghost Section BEGIN======
                   
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GhostPictureBoxFloor.Location = p;

                    }
                    ghost.EnemyPictureBoxFloor.Show();
                    GhostPictureBoxFloor.Show();
                    
                    //==Ghost Section END======
                    //==Goul Section BEGIN======
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GoulPictureBoxFloor.Location = p;
                        goul.goulDirection = (Goul.GoulMotionState)goul.SelectGoulDirection();
                    }
                    goul.EnemyPictureBoxFloor.Show();
                    GoulPictureBoxFloor.Show();
                    //==Goul Section END==========                
                    MazePictureFloor.Show();
                    maze.WeaponPictureFloor.Show();
                    BowPictureFloor.Show();
                  
                    arrow.WeaponPictureFloor.Show();             
                    RedPotionPictureFloor.Show();
                    //========================== 
                    SwordPictureFloor.Hide();
                    sword.WeaponPictureFloor.Hide();
                    BluePotionPictureFloor.Hide();     
                    bat.EnemyPictureBoxFloor.Hide();
                    BatPictureBoxFloor.Hide();
                    timer3.Stop();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    break;
                case 6:
                    hero.HitPoints = 40;
                    LevelNumberGamelabel.Text = level.ToString();
                    bat.HitPoints = 20;
                    ghost.HitPoints = 25;
                    goul.HitPoints = 35;                   
                    //==========================
                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();
                    labelsPointsList.Clear();
                    timersList.Clear();

                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
                    weaponsAvailableObjectList.Add(arrow);
                    weaponsAvailableObjectList.Add(maze);
                    enemiesObjectList.Add(ghost);
                    enemiesObjectList.Add(bat);
                    enemiesObjectList.Add(goul);                   
                    enemiesPicBoxList.Add(ghost.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(bat.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(goul.EnemyPictureBoxFloor);
                   
                    labelsPointsList.Add(GhostPointsLabel);
                    labelsPointsList.Add(BatPointsLabel);
                    labelsPointsList.Add(GoulPointsLabel);
                  
                    timersList.Add(timer2);
                    timersList.Add(timer3);
                    timersList.Add(timer4);
                  
                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();
                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true){timer2.Interval = 500; }
                    else { timer2.Interval = 1000; }
                    timer2.Start();
                    //==Timer for the GHOST END=============
                    //==Timer for the BAT BEGIN=============
                    timer3.Enabled = true;
                    if (randomMode == true) { timer3.Interval = 200; }
                    else { timer3.Interval = 1000; }
                    timer3.Start();
                    //==Timer for the BAT END=============
                    //==Timer for the GOUL BEGIN=============
                    timer4.Enabled = true;
                    if (randomMode == true) { timer4.Interval = 200; }
                    else { timer4.Interval = 1000;}
                    timer4.Start();
                    //==Timer for the GOUL END=============
                    
                    //==Ghost Section BEGIN======

                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GhostPictureBoxFloor.Location = p;

                    }
                    ghost.EnemyPictureBoxFloor.Show();
                    GhostPictureBoxFloor.Show();

                    //==Ghost Section END======
                    //==BAT Section BEGIN======

                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        BatPictureBoxFloor.Location = p;
                        //And we set the random direction of the bat
                        bat.batDirection = (Bat.BatMotionState)bat.SelectDirection();
                    }
                    bat.EnemyPictureBoxFloor.Show();
                    BatPictureBoxFloor.Show();
                    //==BAT Section END======

                    //==Goul Section BEGIN======
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GoulPictureBoxFloor.Location = p;
                        goul.goulDirection = (Goul.GoulMotionState)goul.SelectGoulDirection();
                    }
                    goul.EnemyPictureBoxFloor.Show();
                    GoulPictureBoxFloor.Show();
                    //==Goul Section END==========
                    
                    MazePictureFloor.Show();
                    maze.WeaponPictureFloor.Show();
                    BowPictureFloor.Show();
                 
                    arrow.WeaponPictureFloor.Show();

                    BluePotionPictureFloor.Show();
                    RedPotionPictureFloor.Show();
                //==========================
                    SwordPictureFloor.Hide();
                    sword.WeaponPictureFloor.Hide();
                    wizard.EnemyPictureBoxFloor.Hide();
                    WizardPictureBoxFloor.Hide();
                    timer5.Stop();
                    break;
                case 7:
                    hero.HitPoints = 45;
                    LevelNumberGamelabel.Text = level.ToString();
                    bat.HitPoints = 20;                    
                    ghost.HitPoints = 25;
                    goul.HitPoints = 35;
                    wizard.HitPoints = 40;
                    //==========================
                    heroAndWeaponsPicBoxList.Clear();
                    weaponsAvailableObjectList.Clear();
                    enemiesObjectList.Clear();
                    enemiesPicBoxList.Clear();
                    labelsPointsList.Clear();
                    timersList.Clear();

                    heroAndWeaponsPicBoxList.Add(hero.PlayerPictureFloor);
                    weaponsAvailableObjectList.Add(sword);
                    weaponsAvailableObjectList.Add(arrow);
                    weaponsAvailableObjectList.Add(maze);
                    enemiesObjectList.Add(ghost);
                    enemiesObjectList.Add(bat);
                    enemiesObjectList.Add(goul);
                    enemiesObjectList.Add(wizard);
                    enemiesPicBoxList.Add(ghost.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(bat.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(goul.EnemyPictureBoxFloor);
                    enemiesPicBoxList.Add(wizard.EnemyPictureBoxFloor);

                    labelsPointsList.Add(GhostPointsLabel);
                    labelsPointsList.Add(BatPointsLabel);
                    labelsPointsList.Add(GoulPointsLabel);
                    labelsPointsList.Add(WizardPointsLabel);
                    timersList.Add(timer2);
                    timersList.Add(timer3);
                    timersList.Add(timer4);
                    timersList.Add(timer5);

                    timer1.Enabled = true;
                    timer1.Interval = 18;
                    timer1.Start();

                    //==Timer for the GHOST BEGIN=============
                    timer2.Enabled = true;
                    if (randomMode == true) { timer2.Interval = 500; }
                    else { timer2.Interval = 1000; }
                    timer2.Start();
                    //==Timer for the GHOST END=============
                    //==Timer for the BAT BEGIN=============
                    timer3.Enabled = true;
                    if (randomMode == true){ timer3.Interval = 200; }
                    else { timer3.Interval = 1000; }
                    timer3.Start();
                    //==Timer for the BAT END=============
                    //==Timer for the GOUL BEGIN=============
                    timer4.Enabled = true;
                    if (randomMode == true) {  timer4.Interval = 200; }
                    else { timer4.Interval = 1000; }
                    timer4.Start();
                    //==Timer for the GOUL END=============
                    //==Timer for the WIZARD BEGIN=============
                    timer5.Enabled = true;
                    if (randomMode == true) { timer5.Interval = 200;}
                    else { timer5.Interval = 1000; }
                    timer5.Start();
                    //==Timer for the WIZARD END=============
                    //==Timer for the FIREBALL BEGIN=========
                    timer6.Enabled = true;
                    timer6.Interval = 100;
                   
                    timer6.Start();
                    //==Timer for the FIREBALL END===========
                    //==Ghost Section BEGIN======
                   
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GhostPictureBoxFloor.Location = p;

                    }
                    ghost.EnemyPictureBoxFloor.Show();
                    GhostPictureBoxFloor.Show();
                    
                    //==Ghost Section END======
                    //==BAT Section BEGIN======

                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        BatPictureBoxFloor.Location = p;
                        //And we set the random direction of the bat
                        bat.batDirection = (Bat.BatMotionState)bat.SelectDirection();
                      

                    }
                    bat.EnemyPictureBoxFloor.Show();
                    BatPictureBoxFloor.Show();
                    //==BAT Section END======

                    //==Goul Section BEGIN======
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        GoulPictureBoxFloor.Location = p;
                        goul.goulDirection = (Goul.GoulMotionState)goul.SelectGoulDirection();
                    }
                    goul.EnemyPictureBoxFloor.Show();
                    GoulPictureBoxFloor.Show();
                    //==Goul Section END==========
                   
                    //==Wizard Section BEGIN======
                    if (randomMode == true)
                    {
                        //We set the Random Position of the ghost
                        Point p = new Point();
                        p.X = rand.Next(136, 895);
                        p.Y = rand.Next(85, 300);
                        WizardPictureBoxFloor.Location = p;
                        wizard.wizardDirection = (Wizard.WizardMotionState)wizard.SelectWizardDirection();
                    }
                    wizard.EnemyPictureBoxFloor.Show();
                    WizardPictureBoxFloor.Show();
                    //==Wizard Section END======


                    //==Fireball Section BEGIN===
                    fireball.fireballDirection = (Fireball.FireballMotionState)fireball.SelectFireballDirection();
                    fireball.EnemyPictureBoxFloor.Hide();
                    FireballPictureBoxFloor.Hide();
                    //==Fireball Section END===
                    SwordPictureFloor.Show();
                    sword.WeaponPictureFloor.Show();
                    MazePictureFloor.Show();
                    maze.WeaponPictureFloor.Show();
                    BowPictureFloor.Show();
                  
                    arrow.WeaponPictureFloor.Show();

                    BluePotionPictureFloor.Show();
                    RedPotionPictureFloor.Show();
                    //==========================
                   
                    break;

            }
        }
       
        //==Timer for HERO BEGIN===
        private void timer1_Tick(object sender,EventArgs e)
        {   
           
            //==Player Motion List BEGIN=======
          
           // int hitCounter = 0;
            Point p = new Point();
            if ((Player.PlayerMotionState)heroMotionStatus != Player.PlayerMotionState.MovingUp
                && (Player.PlayerMotionState)heroMotionStatus != Player.PlayerMotionState.MovingDown
                && (Player.PlayerMotionState)heroMotionStatus != Player.PlayerMotionState.NotMoving
                )
            {
                HeroMotionList.Add((Player.PlayerMotionState)heroMotionStatus);
            }

            int total = HeroMotionList.Count;
            int numberToRemove = total - 3;
            if (HeroMotionList.Count >= 4)
            {
                for (int i = 0; i < numberToRemove; i++)
                {
                    HeroMotionList.RemoveAt(0);
                }

            }
            //==Player Motion List END=========
            //==== PLAYER Movement BEGIN===============  
            if (heroMotionStatus == Player.PlayerMotionState.MovingLeft)
            {
                foreach(PictureBox item in heroAndWeaponsPicBoxList)
                {
                    item.Left -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }               
            }
            if (heroMotionStatus == Player.PlayerMotionState.MovingRight)
            {
                foreach (PictureBox item in heroAndWeaponsPicBoxList)
                {
                    item.Left += 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }
            }
            if (heroMotionStatus == Player.PlayerMotionState.MovingUp)
            {
                foreach (PictureBox item in heroAndWeaponsPicBoxList)
                {
                    item.Top -= 1;
                    hero.DungeonFloorLimitsCheckForPlayer(item);
                }
            }
            if (heroMotionStatus == Player.PlayerMotionState.MovingDown)
            {
                foreach (PictureBox item in heroAndWeaponsPicBoxList)
                {
                   item.Top += 1;
                   hero.DungeonFloorLimitsCheckForPlayer(item);
                }
            }
            //==== PLAYER Movement END===============
            //==Potions Check BEGIN==================
            if(BluePotionPictureFloor.Visible == true)
            {
               potion.PotionUsedCheck(BluePotionPictureFloor, hero, timer1);
            }

            if (RedPotionPictureFloor.Visible == true)
            {
                potion.PotionUsedCheck(RedPotionPictureFloor, hero, timer1);
            }

            //==Potions Check END====================

        }
        //==Timer for HERO END=====
        //==Timer for GHOST BEGIN==
        private void timer2_Tick(object sender, EventArgs e)
        {
            //==GHOST Movement BEGIN===============
            if(randomMode==true)
            {
                ghost.Move(rand);

                ghost.StrikePlayer(hero, randomMode, ghost.PointsDamageCausedToPlayer, ghost.EnemyPictureBoxFloor);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                GhostPointsLabel.Text = ghost.HitPoints.ToString();

               // BoundariesPlayer();
              

                EnemyGameOverCheck(PlayerPointsLabel,hero, heroMotionStatus);
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                if(hero.HitPoints<=0)
                {
                    int value = 0;
                    string valString = value.ToString();
                    PlayerPointsLabel.Text = valString;
                }
                GhostPointsLabel.Text = ghost.HitPoints.ToString();
            }
            else //Targeted mode
            {
                ghost.Move(rand, hero.PlayerPictureFloor);
                ghost.EnemyPictureBoxFloor = GhostPictureBoxFloor;
                ghost.StrikePlayer(hero,randomMode, ghost.PointsDamageCausedToPlayer, ghost.EnemyPictureBoxFloor);
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                GhostPointsLabel.Text = ghost.HitPoints.ToString();

               // BoundariesPlayer();
               
                EnemyGameOverCheck(PlayerPointsLabel, hero, heroMotionStatus);             
               
            }

            //==GHOST Movement END===============
            
        }
        //==Timer for GHOST END====
        //==Timer for BAT BEGIN====
        private void timer3_Tick(object sender, EventArgs e)
        {
            //==BAT Movement BEGIN===============
            if (randomMode == true)
            {
             
                bat.Move(rand);

                bat.StrikePlayer(hero, randomMode, bat.PointsDamageCausedToPlayer, bat.EnemyPictureBoxFloor);
    
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                BatPointsLabel.Text = bat.HitPoints.ToString();
               // BoundariesPlayer();
               
                
                EnemyGameOverCheck(PlayerPointsLabel,hero, heroMotionStatus);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                if (hero.HitPoints <= 0)
                {
                    
                    string valString = Convert.ToString(0);
                    PlayerPointsLabel.Text = valString;
                }
                BatPointsLabel.Text = bat.HitPoints.ToString();

            }
            else //Targeted mode
            {
                
                bat.Move(rand, hero.PlayerPictureFloor);
                bat.StrikePlayer(hero, randomMode, bat.PointsDamageCausedToPlayer, bat.EnemyPictureBoxFloor);
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                BatPointsLabel.Text = bat.HitPoints.ToString();
                //BoundariesPlayer();
               
                EnemyGameOverCheck(PlayerPointsLabel,hero, heroMotionStatus);

            }

            //==BAT Movement END===============

        }
        //==Timer for BAT END======

        //==Timer for GOUL BEGIN======
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (randomMode == true)
            {
               
                goul.Move(rand);
           
                // bat.EnemyPictureBox = bat.BatPictureBoxFloor;

                goul.StrikePlayer(hero, randomMode,goul.PointsDamageCausedToPlayer, goul.EnemyPictureBoxFloor);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                GoulPointsLabel.Text = goul.HitPoints.ToString();
                //BoundariesPlayer();
               
                EnemyGameOverCheck(PlayerPointsLabel, hero,  heroMotionStatus);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                if (hero.HitPoints <= 0)
                {

                    string valString = Convert.ToString(0);
                    PlayerPointsLabel.Text = valString;
                }
                GoulPointsLabel.Text = goul.HitPoints.ToString();
             

            }
             else //Targeted mode
            {
               
                goul.Move(rand, hero.PlayerPictureFloor);
         
                goul.EnemyPictureBoxFloor = GoulPictureBoxFloor;
                goul.StrikePlayer(hero, randomMode, goul.PointsDamageCausedToPlayer, goul.EnemyPictureBoxFloor);
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                GoulPointsLabel.Text = goul.HitPoints.ToString();

                //BoundariesPlayer();
               
                EnemyGameOverCheck(PlayerPointsLabel, hero,  heroMotionStatus);             
               
            }
        }
        //==Timer for GOUL END======
        //==Timer for WIZARD BEGIN======
        private void timer5_Tick(object sender, EventArgs e)
        {

            if (randomMode == true)
            {
               
                wizard.Move(rand);

                // bat.EnemyPictureBox = bat.BatPictureBoxFloor;

                wizard.StrikePlayer(hero, randomMode,wizard.PointsDamageCausedToPlayer, wizard.EnemyPictureBoxFloor);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                WizardPointsLabel.Text = wizard.HitPoints.ToString();
                //BoundariesPlayer();
                
                EnemyGameOverCheck(PlayerPointsLabel, hero,  heroMotionStatus);

                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                if (hero.HitPoints <= 0)
                {

                    string valString = Convert.ToString(0);
                    PlayerPointsLabel.Text = valString;
                }
                WizardPointsLabel.Text = wizard.HitPoints.ToString();
              
            }
            else //Targeted mode
            {

                wizard.Move(rand, hero.PlayerPictureFloor);

                wizard.EnemyPictureBoxFloor = WizardPictureBoxFloor;
                wizard.StrikePlayer(hero, randomMode, wizard.PointsDamageCausedToPlayer, wizard.EnemyPictureBoxFloor);
                PlayerPointsLabel.Text = hero.HitPoints.ToString();
                WizardPointsLabel.Text = wizard.HitPoints.ToString();
                wizard.EnemyBoundariesAdjustment = 10;
                //BoundariesPlayer();
               
                EnemyGameOverCheck(PlayerPointsLabel, hero,  heroMotionStatus);

            }
        }
        //==Timer for WIZARD END======
        //==Timer for FIREBALL BEGIN======
        private void timer6_Tick(object sender, EventArgs e)
        {
           
            //==Starting position of the fireballs.
            //==The fireballs are thrown by the wizard, so their starting point is the wizards position

            //Wizard position
            Point p = new Point();
            p.X = wizard.EnemyPictureBoxFloor.Location.X;
            p.Y = wizard.EnemyPictureBoxFloor.Location.Y;
            if(fireball.EnemyPictureBoxFloor.Visible == false)
            {
                if (wizard.WizardMotionList.Count > 0)
                {
                    fireball.EnemyPictureBoxFloor.Location = p;
                    FireballPictureBoxFloor.Location = p;
                    fireball.EnemyPictureBoxFloor.Show();
                    FireballPictureBoxFloor.Show();
                }

                //we set the direction of the fireball
                fireball.fireballDirection = (Fireball.FireballMotionState)fireball.SelectFireballDirection();

            }

          //Last Left or right Movement Motion Wizard List Index
            int index =wizard.WizardMotionList.Count - 1;
        
            if(wizard.WizardMotionList.Count>0)
            {
                fireball.ShootFireball(randomMode, hero, WizardPictureBoxFloor, wizard.WizardMotionList[wizard.WizardMotionList.Count - 1]);
           
            }
            EnemyGameOverCheck(PlayerPointsLabel, hero,heroMotionStatus);
            
        }
        //==Timer for FIREBALL END======
        #region Key Pressing
        void Form1_KeyPress(object sender, KeyPressEventArgs e)       
        {
            char val = e.KeyChar;
            //==Space key pressed WEAPON==
            if(val ==' ')
            {
               
                int count = 0;
                //==The Weapon is picked Up and placed next to the player BEGIN==
                //==We use a counter to identify if there is any weapon already picked up.
                foreach(PictureBox pic in heroAndWeaponsPicBoxList)
                {
                    //If the weapon is already in the list we break the count
                    if (pic == sword.WeaponPictureFloor){break; }
                    if(pic == arrow.WeaponPictureFloor) { break;}
                    if (pic == maze.WeaponPictureFloor){ break; }
                    count++;
                }
                //when the counted items is equal to the total list
                //it means there is no weapon in the list. so we add it
                if (count == heroAndWeaponsPicBoxList.Count)
                {
                    //==We verify that the weapon is at reach range 
                    //==We define the vectors for the Player ==
                    int aX = hero.PlayerPictureFloor.Location.X;
                    int aY = hero.PlayerPictureFloor.Location.Y;
                    int cX = hero.PlayerPictureFloor.Location.X + PlayerPictureFloor.Width;
                    int cY = hero.PlayerPictureFloor.Location.Y + PlayerPictureFloor.Height;

                    //==We define the Vectors for the Weapon==
                    
                    //==we identify the type of weapon is available on the floor
                   foreach(Weapon item in weaponsAvailableObjectList)
                   {
                       //We make sure only one weapon the player can pick up.
                       if (heroAndWeaponsPicBoxList.Count == 1)
                       {
                           if (item is Sword)
                           {
                               int a1X = SwordPictureFloor.Location.X;
                               int a1Y = SwordPictureFloor.Location.Y;
                               int c1X = SwordPictureFloor.Location.X + SwordPictureFloor.Width;
                               int c1Y = SwordPictureFloor.Location.Y + SwordPictureFloor.Height;
                               //We transfer the characteristic of the Sword to the Weapon PictureBox 
                               sword.WeaponPictureFloor = SwordPictureFloor;
                               //When user is ON top of the Sword, it can pick it up.
                               if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
                               {
                                   Point p = new Point();

                                   p.X = (PlayerPictureFloor.Location.X - 40);
                                   p.Y = (PlayerPictureFloor.Location.Y - 10);

                                   SwordPictureFloor.Image = Properties.Resources.swordPointingUpLeftTranspBknd;//.swordPointingUpLeft;
                                   SwordPictureFloor.Refresh();
                                   SwordPictureFloor.Visible = true;
                                   SwordPictureFloor.Location = p;
                                   PlayerPictureFloor.BringToFront();
                                   heroAndWeaponsPicBoxList.Add(sword.WeaponPictureFloor);
                                   //We indicate that the sword has been picked up by the player
                                   sword.PickedUp = true;
                               }
                           }
                       }
                       //Se review again and We make sure only one weapon the player can pick up.
                       if (heroAndWeaponsPicBoxList.Count == 1)
                       {
                           if (item is Arrow)
                           {
                               int a1X = BowPictureFloor.Location.X;
                               int a1Y = BowPictureFloor.Location.Y;
                               int c1X = BowPictureFloor.Location.X + BowPictureFloor.Width;
                               int c1Y = BowPictureFloor.Location.Y + BowPictureFloor.Height;

                               //We transfer the characteristic of the Arrow to the Weapon PictureBox 
                               arrow.WeaponPictureFloor = BowPictureFloor;
                               //When user is ON top of the Arrow, it can pick it up.
                               if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
                               {
                                   Point p = new Point();

                                   p.X = (PlayerPictureFloor.Location.X - 40);
                                   p.Y = (PlayerPictureFloor.Location.Y - 10);

                                   BowPictureFloor.Image = Properties.Resources.bowFacingDownTranspBknd;//.bowFacingDown;
                                   BowPictureFloor.Refresh();
                                   BowPictureFloor.Visible = true;
                                   BowPictureFloor.Location = p;
                                   PlayerPictureFloor.BringToFront();
                                   heroAndWeaponsPicBoxList.Add(arrow.WeaponPictureFloor);
                                   //We indicate that the sword has been picked up by the player
                                   arrow.PickedUp = true;
                               }
                           }
                       }
                       //We review again and We make sure only one weapon the player can pick up.
                       if (heroAndWeaponsPicBoxList.Count == 1)
                       {
                           if (item is Maze)
                           {
                               int a1X = MazePictureFloor.Location.X;
                               int a1Y = MazePictureFloor.Location.Y;
                               int c1X = MazePictureFloor.Location.X + MazePictureFloor.Width;
                               int c1Y = MazePictureFloor.Location.Y + MazePictureFloor.Height;
                               //We transfer the characteristic of the Sword to the Weapon PictureBox 
                               maze.WeaponPictureFloor = MazePictureFloor;
                               //When user is ON top of the Sword, it can pick it up.
                               if (!(cX < a1X) && !(cY < a1Y) && !(c1X < aX) && !(c1Y < aY))
                               {
                                   Point p = new Point();

                                   p.X = (PlayerPictureFloor.Location.X + 40);
                                   p.Y = (PlayerPictureFloor.Location.Y - 30);
                                   MazePictureFloor.Location = p;
                                   MazePictureFloor.Image = Properties.Resources.mace;
                                   MazePictureFloor.Refresh();
                                   MazePictureFloor.Visible = true;
                                   MazePictureFloor.Location = p;
                                   PlayerPictureFloor.BringToFront();
                                   heroAndWeaponsPicBoxList.Add(maze.WeaponPictureFloor);
                                   //We indicate that the sword has been picked up by the player
                                   maze.PickedUp = true;
                               }
                           }
                       }
                   }
                }
                else //When the Player has grabbed the weapon then uses it to hit the enemies.
                {
                    //==The hero uses the Sword BEGIN==
                    int total = 0;

                    foreach(var element in weaponsAvailableObjectList)
                    {
                        if (heroAndWeaponsPicBoxList[1] == element.WeaponPictureFloor)
                        {
                            if(element is Arrow)
                            {
                                //Previous Movment Motion List Index
                                int index = HeroMotionList.Count - 1;
                                arrow.StrikeEnemy(hero, heroAndWeaponsPicBoxList, weaponsAvailableObjectList, enemiesObjectList, HeroMotionList[index]);
                      
                            }
                            else
                            {
                                element.StrikeEnemy(hero, heroAndWeaponsPicBoxList, weaponsAvailableObjectList, enemiesObjectList);
                            }
                          
                            if (enemiesObjectList.Count > 0)
                            {
                                for (int i = 0; i < enemiesObjectList.Count; i++)
                                {
                                    element.EnemyZeroEnergyCheck(enemiesObjectList[i], labelsPointsList[i], timer1, timersList[i]);
                                }
                            }  
                        }
                    }

                   
                    foreach(Enemy enemy in enemiesObjectList)
                    {
                        Bat batEnemy;
                        Ghost ghostEnemy;
                        Goul goulEnemy;
                        Wizard wizardEnemy;
                           
                        if(enemy is Bat)
                        {
                            batEnemy = (Bat)enemy;
                            total += batEnemy.HitPoints;
                        }
                        if (enemy is Ghost)
                        {
                            ghostEnemy = (Ghost)enemy;
                            total += ghostEnemy.HitPoints;
                        }
                        if (enemy is Goul)
                        {
                            goulEnemy = (Goul)enemy;
                            total += goulEnemy.HitPoints;
                        }
                        if (enemy is Wizard)
                        {
                            wizardEnemy = (Wizard)enemy;
                            total += wizardEnemy.HitPoints;

                            //==The Fireballs depending of the wizard
                            //==must also stop when the wizard has zero energy
                            if(wizard.HitPoints<=0)
                            {
                                FireballPictureBoxFloor.Hide();
                                fireball.EnemyPictureBoxFloor.Hide();
                                timer6.Stop();
                            }

                        }
                    }
                 
                    if(total <= 0)
                    {
                        HeroGameOverCheck();

                      
                     }
                    //==The hero uses the Sword END=======
                    PlayerPointsLabel.Text = hero.HitPoints.ToString();
                    GhostPointsLabel.Text = ghost.HitPoints.ToString();
                    BatPointsLabel.Text = bat.HitPoints.ToString();

                    
                }
                //==The Sword is picked Up and placed next to the player END==

                //==Space Bar pressed - Player hits BEGIN=========
                counter++;
             
                
                //==Space Bar pressed - Player hits END===========


                //==Combined Keys movement: Player[Arrow keys] + Weapon[space bar] BEGIN====
                Quest.Player.PlayerMotionState statusHeroMovement = (Quest.Player.PlayerMotionState)keyboard.CombinedKeysPress(hero, heroMotionStatus, heroAndWeaponsPicBoxList);

                //We move the player and receive the status of the movement for the player-timer1 
                heroMotionStatus = statusHeroMovement;
                //==Combined Keys movement: Player[Arrow keys] + Weapon[space bar] END====              
            
            }

            //If val is X key we drop the weapon

            if(val=='x' || val =='X')
            {
                if(heroAndWeaponsPicBoxList.Count==2)
                {
                    if(heroAndWeaponsPicBoxList[1]== SwordPictureFloor)
                    {
                        heroAndWeaponsPicBoxList.RemoveAt(1);
                        sword.PickedUp = false;
                    }
                    
                }
                if (heroAndWeaponsPicBoxList.Count == 2)
                {
                    if (heroAndWeaponsPicBoxList[1] == BowPictureFloor)
                    {
                        heroAndWeaponsPicBoxList.RemoveAt(1);
                        arrow.PickedUp = false;
                    }
                }
                if (heroAndWeaponsPicBoxList.Count == 2)
                {
                    if (heroAndWeaponsPicBoxList[1] == MazePictureFloor)
                    {
                        heroAndWeaponsPicBoxList.RemoveAt(1);
                        maze.PickedUp = false;
                    }
                }
            }
        }
#endregion    

       
        //==KEYBOARD Keys Event Handler BEGIN===

        //== 1) KEY DOWN==
        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)   
        {    

            ////==Single keys Moves BEGIN=====

            Quest.Player.PlayerMotionState statusHeroMovement = (Quest.Player.PlayerMotionState)keyboard.SingleKeyPress(hero, heroMotionStatus, heroAndWeaponsPicBoxList);

            //We move the player and receive the status of the movement for the player-timer1 
            heroMotionStatus = statusHeroMovement;
            ////==Single keys Moves END=======
                 
          
        }
           
        //==2) KEY UP
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            /******************************************************************************************/       
            //==When any arrow key is Up the status of the player will change to Not Moving, 
            //==so the little player will stop moving.
                

            if(e.KeyCode == Keys.Left && heroMotionStatus== Player.PlayerMotionState.MovingLeft ||
               e.KeyCode == Keys.Right && heroMotionStatus== Player.PlayerMotionState.MovingRight ||
               e.KeyCode == Keys.Up && heroMotionStatus== Player.PlayerMotionState.MovingUp ||
               e.KeyCode == Keys.Down && heroMotionStatus== Player.PlayerMotionState.MovingDown               
             )
            {
                heroMotionStatus = Player.PlayerMotionState.NotMoving;
           
            }
            /******************************************************************************************/

        }
        //==KEYBOARD Keys Event Handler END=== 

     //==GAME OVER Check BEGIN===
        private void HeroGameOverCheck()
        {

            timer1.Stop();
            heroMotionStatus = Player.PlayerMotionState.NotMoving;
            if (levelCounter < 8)
            {
                DialogResult dialog = MessageBox.Show("You have killed all your enemies", "Advance to the Next Level", MessageBoxButtons.OK);
                if (dialog == DialogResult.OK)
                {
                    SelectLevel(levelCounter);
                    levelCounter++;
                }
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                timer5.Stop();
                timer6.Stop();

                DialogResult dialogEndGame = MessageBox.Show("Game Over. Do you want to play again?", " YOU DEFEATED ALL YOUR ENEMIES.", MessageBoxButtons.YesNo);
                if (dialogEndGame == DialogResult.No)
                {

                    Application.ExitThread();
                    Application.Exit();
                }
                if (dialogEndGame == DialogResult.Yes)
                {
                    Application.Restart();
                }
            } 
        }
        private void EnemyGameOverCheck(Label form1Label, Player playerHero, Enum heroMotionStatus)
        {
            //When bat is ON top of the Player, the player and the player has 0 energy left, the player dies.
            if (playerHero.HitPoints <= 0)
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                timer5.Stop();
                timer6.Stop();
              
                heroMotionStatus = Player.PlayerMotionState.NotMoving;

                form1Label.Text = "0";     
                DialogResult dialog = MessageBox.Show("Do you want to play again?", "GAME OVER", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No)
                {
                    Application.ExitThread();
                    Application.Exit();
                }
                if (dialog == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }

     //==GAME OVER Check END===

        private void BoundariesPlayer()
        {
            /**********Uncoment to inspect Player's boundaries***************/
            formGraphics = DungeonPictureBox.CreateGraphics();
            //hero.PlayerBoundaries(randomMode, bat.EnemyBoundariesAdjustment);
            List<int> heroBoundariesVectorslist = hero.PlayerBoundaries(randomMode, wizard.EnemyBoundariesAdjustment);
            int aX = heroBoundariesVectorslist[0]; //Vector a.X
            int aY = heroBoundariesVectorslist[1]; //Vector a.Y
            int cX = heroBoundariesVectorslist[2] - aX; //Wector c.X
            int cY = heroBoundariesVectorslist[3] - aY; //Wector c.Y

            formGraphics.FillRectangle(brush, aX, aY, cX, cY);
            /*****************************END*******************************/
            /*************************End************************************/
        }
    }
}
