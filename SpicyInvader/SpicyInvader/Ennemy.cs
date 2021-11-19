/*
 *  Auteur : Thomas Rey
 *  Date : 03.09.2021
 *  Lieu : ETML
 *  Description : Code gerant les vaisseaux ennemie
 */

using System;
using System.Timers;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the ennemy ship
    /// </summary>
    public class Ennemy
    {
        //Properties
        public string _shipForm = "■─▬─■";                                  // The shap of the ennemy's ship
        private bool _gamePause = false;                                    // Check if the game is in pause
        private Missile missileEnnemy = new Missile(1, 1, false);           // Create a missile
        

        //Getter - Setter
        /// <summary>
        /// EnnemyDirection property definition
        /// </summary>
        public bool EnnemyDirection { get; set; }                          // The direction of the movement of the ship

        /// <summary>
        /// EnnemyX property definition
        /// </summary>
        public int EnnemyX { get; set; }                                   // The lateral position of the ship

        /// <summary>
        /// EnnemyY property definition
        /// </summary>
        public int EnnemyY { get; set; }                                   // The vertical position of the ship


        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; set; }                                // The sound is ON or OFF

        /// <summary>
        /// ShipEnnemyLive property definition
        /// </summary>
        public bool ShipEnnemyLive { get; set; }                           // The life of the ship

        /// <summary>
        /// PosXBunker property definition
        /// </summary>
        public List<int> PosXBunker { get; set; }                           // Postition of the bunker

        public bool Shoot { get; set; }                                   // If the ennemy can shoot


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ennemyX">The lateral position of the ship</param>
        /// <param name="ennemyY">The vertical position of the ship</param>
        /// <param name="soundGame">The sound is ON or OFF</param>
        public Ennemy(int ennemyX, int ennemyY, bool soundGame, bool ennemyDirection, bool shipEnnemyLive, List<int> posXBunker, PlayerShip player)
        {
            this.EnnemyX = ennemyX;
            this.EnnemyY = ennemyY;
            this.SoundGame = soundGame;
            this.EnnemyDirection = ennemyDirection;
            this.ShipEnnemyLive = shipEnnemyLive;
            missileEnnemy.PosXBunker = posXBunker;
            missileEnnemy.Player = player;

            Console.SetCursorPosition(EnnemyX, EnnemyY);    // Position the cursor to the ship coordinate
            Console.Write(_shipForm);

            Timer ennemyShooting = new Timer(1000);
            ennemyShooting.Elapsed += new ElapsedEventHandler(EnnemyShoot);
            ennemyShooting.Enabled = true;
        }
        public void EnnemyShoot(object source, ElapsedEventArgs e)
        {
            Random shoot = new Random();                // Generate a random number
            
            // If the random number is equal to 0, launch a missile
            if (shoot.Next(0, 6) == 0 && Shoot == true)
            {
                Sound.SoundShipShoot(SoundGame); // Play the shoot sound
                missileEnnemy.MissileLive = true;
                missileEnnemy.MissileY = this.EnnemyY + 1;
                missileEnnemy.MissileX = this.EnnemyX + (3);
                missileEnnemy.MissileEnnemyCreate();
            }
        }
    }
}