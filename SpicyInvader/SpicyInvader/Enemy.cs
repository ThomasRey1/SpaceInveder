﻿/*
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
    /// Code managing the enemy ship
    /// </summary>
    public class Enemy
    {
        #region Properties
        //Properties
        private Random _shoot = new Random();                                 // Generate a random number
        private Timer _enemyShooting = new Timer(250);                      // Loop to shoot a missile
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; }                                     // The sound is ON or OFF

        /// <summary>
        /// ShipForm property definition
        /// </summary>
        public string ShipForm { get; }                                    // The shap of the enemy's ship

        /// <summary>
        /// EnemyDirection property definition
        /// </summary>
        public bool EnemyDirection { get; set; }                            // The direction of the movement of the ship

        /// <summary>
        /// EnemyX property definition
        /// </summary>
        public int EnemyX { get; set; }                                     // The lateral position of the ship

        /// <summary>
        /// EnemyY property definition
        /// </summary>
        public int EnemyY { get; set; }                                     // The vertical position of the ship

        /// <summary>
        /// Shoot property definition
        /// </summary>
        public bool Shoot { get; set; }                                     // If the enemy can shoot

        /// <summary>
        /// MissileEnemy property definition
        /// </summary>
        public Missile MissileEnemy { get; set; }                           // Create a missile

        /// <summary>
        /// Difficulty property definition
        /// </summary>
        public bool Difficulty { get; }                                     // Choose the difficulty
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyX">The lateral position of the ship</param>
        /// <param name="enemyY">The vertical position of the ship</param>
        /// <param name="soundGame">The sound is ON or OFF</param>
        /// <param name="enemyDirection">The direction of the movement of the ship</param>
        /// <param name="posXBunker">Postition of the bunker</param>
        /// <param name="player">The player ship</param>
        /// <param name="difficulty">Choose the difficulty</param>
        public Enemy(int enemyX, int enemyY, bool soundGame, bool enemyDirection, List<int> posXBunker, PlayerShip player, bool difficulty)
        {
            ShipForm = "■─▬─■";
            this.EnemyX = enemyX;
            this.EnemyY = enemyY;
            this.SoundGame = soundGame;
            this.EnemyDirection = enemyDirection;
            this.Difficulty = difficulty;
            this.MissileEnemy = new Missile(EnemyX, EnemyY, posXBunker, player);

            _enemyShooting.Elapsed += new ElapsedEventHandler(EnemyShoot);
            _enemyShooting.Start();
        }

        /// <summary>
        /// Shoot a enemy missile
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EnemyShoot(object source, ElapsedEventArgs e)
        {
            // Depending on the difficulty, the ship shoot more often
            if(Difficulty == false)
            {
                // If the random number is equal to 0, launch a missile
                if (_shoot.Next(0, 11) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    try
                    {
                        Sound.SoundShipShoot(SoundGame); // Play the shoot sound
                        MissileEnemy.MissileLive = true;
                        MissileEnemy.MissileY = this.EnemyY + 1;
                        MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                        MissileEnemy.MissileEnemyCreate();
                    }
                    catch (ArgumentNullException)
                    {

                    }
                }
            }
            else
            {
                // If the random number is equal to 0, launch a missile
                if (_shoot.Next(0, 6) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    try
                    {
                        Sound.SoundShipShoot(SoundGame); // Play the shoot sound
                        MissileEnemy.MissileLive = true;
                        MissileEnemy.MissileY = this.EnemyY + 1;
                        MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                        MissileEnemy.MissileEnemyCreate();
                    }
                    catch (ArgumentNullException)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Start or stop the enemy movement
        /// </summary>
        /// <param name="pause">Check if the game is on pause or not</param>
        public void StopShoot(bool pause)
        {
            if (pause == true)
            {
                _enemyShooting.Stop();
            }
            else
            {
                _enemyShooting.Start();
            }
            MissileEnemy.StopShoot(pause);
        }

        /// <summary>
        /// Destroy the object missile and the ship
        /// </summary>
        public void Dead()
        {
            _enemyShooting.Stop();
            Console.MoveBufferArea(0, 0, 1, 1, MissileEnemy.MissileX, MissileEnemy.MissileY);
            MissileEnemy = null;
        }
        #endregion
    }
}