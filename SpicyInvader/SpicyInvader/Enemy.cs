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
    /// Code managing the enemy ship
    /// </summary>
    public class Enemy
    {
        #region Properties
        //Properties
        public string _shipForm = "■─▬─■";                                  // The shap of the enemy's ship
        private bool _gamePause = false;                                    // Check if the game is in pause
        private Missile missileEnemy = new Missile(1, 29, false);          // Create a missile
        private Timer enemyShooting = new Timer(1000);                     // Loop to shoot a missile
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// EnemyDirection property definition
        /// </summary>
        public bool EnemyDirection { get; set; }                          // The direction of the movement of the ship

        /// <summary>
        /// EnemyX property definition
        /// </summary>
        public int EnemyX { get; set; }                                   // The lateral position of the ship

        /// <summary>
        /// EnemyY property definition
        /// </summary>
        public int EnemyY { get; set; }                                   // The vertical position of the ship


        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; set; }                                // The sound is ON or OFF

        /// <summary>
        /// ShipEnemyLive property definition
        /// </summary>
        public bool ShipEnemyLive { get; set; }                           // The life of the ship

        /// <summary>
        /// PosXBunker property definition
        /// </summary>
        public List<int> PosXBunker { get; set; }                           // Postition of the bunker

        /// <summary>
        /// Shoot property definition
        /// </summary>
        public bool Shoot { get; set; }                                   // If the enemy can shoot
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyX">The lateral position of the ship</param>
        /// <param name="enemyY">The vertical position of the ship</param>
        /// <param name="soundGame">The sound is ON or OFF</param>
        public Enemy(int enemyX, int enemyY, bool soundGame, bool enemyDirection, bool shipEnemyLive, List<int> posXBunker, PlayerShip player)
        {
            this.EnemyX = enemyX;
            this.EnemyY = enemyY;
            this.SoundGame = soundGame;
            this.EnemyDirection = enemyDirection;
            this.ShipEnemyLive = shipEnemyLive;
            missileEnemy.PosXBunker = posXBunker;
            missileEnemy.Player = player;

            Console.SetCursorPosition(EnemyX, EnemyY);    // Position the cursor to the ship coordinate
            Console.Write(_shipForm);

            enemyShooting.Elapsed += new ElapsedEventHandler(EnemyShoot);
            enemyShooting.Start();
        }
        public void EnemyShoot(object source, ElapsedEventArgs e)
        {
            Random shoot = new Random();                // Generate a random number
            
            if(EnemyX > 17 && EnemyY < Console.WindowWidth - 17)
            {
                // If the random number is equal to 0, launch a missile
                if (shoot.Next(0, 5) == 0 && Shoot == true)
                {
                    Sound.SoundShipShoot(SoundGame); // Play the shoot sound
                    missileEnemy.MissileLive = true;
                    missileEnemy.MissileY = this.EnemyY + 1;
                    missileEnemy.MissileX = this.EnemyX + _shipForm.Length / 2;
                    missileEnemy.MissileEnemyCreate();
                }
            }
        }
        public void StopShoot()
        {
            if(enemyShooting.Enabled == true)
            {
                enemyShooting.Stop();
            }
            else
            {
                enemyShooting.Start();
            }
            missileEnemy.StopShoot();
        }

        public void Dead()
        {
            enemyShooting.Stop();
            missileEnemy.StopShoot();
            Console.MoveBufferArea(1, 1, 1, 1, missileEnemy.MissileX, missileEnemy.MissileY);
            missileEnemy = null;
        }
        #endregion
    }
}