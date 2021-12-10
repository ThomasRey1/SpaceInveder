/*
 *  Auteur : Thomas Rey
 *  Date : 15.10.2021
 *  Lieu : ETML
 *  Description : Code gerant les mouvements des ennemies
 */

using System;
using System.Timers;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing movement of the enemies
    /// </summary>
    public class Move
    {
        #region Properties
        // Properties
        private Enemy[] _enemies;                         // List of enemies
        private Timer enemyMovement = new Timer(200);      // Loop to move the enemies ship
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemies"> List of enemies</param>
        public Move(Enemy[] enemies)
        {
            this._enemies = enemies;
            enemyMovement.Elapsed += new ElapsedEventHandler(EnnemyControl);
            enemyMovement.Start();
        }

        /// <summary>
        /// Move the enemy from right to left and vice versa
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EnnemyControl(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i != _enemies.Length; i++)
            {
                if (_enemies[i] != null && _enemies[i].ShipEnemyLive == true)
                {
                    try
                    {
                        // If the lateral position is touching the left border of the window
                        if (_enemies[i].EnemyX == Console.WindowLeft)
                        {
                            Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                            _enemies[i].EnemyY++;
                            _enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                            //foreach (Enemy enemy in _enemies)
                            //{
                            //    Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX + 1, enemy.EnemyY + 1);           // descend the ship from one floor
                            //    enemy.EnemyY++;
                            //    enemy.EnemyX++;
                            //    enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            //}
                        }

                        // If the lateral position is touching the right border of the window
                        else if (_enemies[i].EnemyX + 5 == Console.WindowWidth)
                        {
                            Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                            _enemies[i].EnemyY++;
                            _enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                            //foreach (Enemy enemy in _enemies)
                            //{
                            //    Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX - 1, enemy.EnemyY + 1);           // descend the ship from one floor
                            //    enemy.EnemyY++;
                            //    enemy.EnemyX--;
                            //    enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            //}
                        }

                        // Else, move the ship to the left
                        if (_enemies[i].EnemyDirection == true && _enemies[i].EnemyX != Console.WindowLeft)
                        {
                            Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX - 1, _enemies[i].EnemyY);
                            _enemies[i].EnemyX--;
                        }

                        // Else, move the ship to the right
                        else if (_enemies[i].EnemyDirection == false && _enemies[i].EnemyX + 5 != Console.WindowWidth)
                        {
                            Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX + 1, _enemies[i].EnemyY);
                            _enemies[i].EnemyX++;
                        }
                    }
                    catch
                    {

                    }
                }
                if(_enemies[i] != null)
                {
                    if (i != 19 && i != 0 &&  (_enemies[i + 1] != null || _enemies[i - 1] != null))
                    {
                        if (i >= 15)
                        {
                            _enemies[i].Shoot = true;
                        }
                        // If the ship forward is destroy, so the ship beward can shoot
                        else if (_enemies[i + 5] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Start or stop the enemy movement
        /// </summary>
        public void StopMove()
        {
            if (enemyMovement.Enabled == true)
            {
                enemyMovement.Stop();
            }
            else
            {
                enemyMovement.Start();
            }
        }
        #endregion
    }
}