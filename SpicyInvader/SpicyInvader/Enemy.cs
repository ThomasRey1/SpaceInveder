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
        private bool _soundGame;                                            // The sound is ON or OFF
        private Random shoot = new Random();                                 // Generate a random number
        private Timer _enemyShooting = new Timer(250);                      // Loop to shoot a missile
        #endregion

        #region Getter - Setter
        //Getter - Setter
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
            this._soundGame = soundGame;
            this.EnemyDirection = enemyDirection;
            this.Difficulty = difficulty;
            this.MissileEnemy = new Missile(1, 29, posXBunker, player);

            Console.SetCursorPosition(EnemyX, EnemyY);    // Position the cursor to the ship coordinate
            Console.Write(ShipForm);

            _enemyShooting.Elapsed += new ElapsedEventHandler(EnemyShoot);
            _enemyShooting.Start();
        }
        public void EnemyShoot(object source, ElapsedEventArgs e)
        {
            if(Difficulty == false)
            {
                // If the random number is equal to 0, launch a missile
                if (shoot.Next(0, 11) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    Sound.SoundShipShoot(_soundGame); // Play the shoot sound
                    MissileEnemy.MissileLive = true;
                    MissileEnemy.MissileY = this.EnemyY + 1;
                    MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                    MissileEnemy.MissileEnemyCreate();
                }
            }
            else
            {
                // If the random number is equal to 0, launch a missile
                if (shoot.Next(0, 6) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    Sound.SoundShipShoot(_soundGame); // Play the shoot sound
                    MissileEnemy.MissileLive = true;
                    MissileEnemy.MissileY = this.EnemyY + 1;
                    MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                    MissileEnemy.MissileEnemyCreate();
                }
            }
        }
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

        public void Dead()
        {
            _enemyShooting.Stop();
            Console.MoveBufferArea(0, 0, 1, 1, MissileEnemy.MissileX, MissileEnemy.MissileY);
            MissileEnemy = null;
        }
        #endregion
    }
}