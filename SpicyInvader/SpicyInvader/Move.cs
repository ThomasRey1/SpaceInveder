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
        private Enemy[] _enemies;                           // List of enemies
        private Timer _enemyMovement = new Timer(200);      // Loop to move the enemies ship
        private bool _upDown = false;                       // look if the enemies ship go up or go down
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemies"> List of enemies</param>
        public Move(Enemy[] enemies)
        {
            this._enemies = enemies;
            if(_enemies[0].Difficulty == true)
            {
                _enemyMovement = new Timer(120);
            }
            _enemyMovement.Elapsed += new ElapsedEventHandler(EnemyControl);
            _enemyMovement.Start();
        }

        /// <summary>
        /// Move the enemy from right to left and vice versa
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EnemyControl(object source, ElapsedEventArgs e)
        {
            for (int i = _enemies.Length - 1; i >= 0; i--)
            {
                if (_enemies[i] != null && _upDown == false)
                {
                    // If the lateral position is touching the right border of the window
                    if (_enemies[i].EnemyX + 5 == Console.WindowWidth)
                    {
                        //Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                        //_enemies[i].EnemyY++;
                        //_enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX - 1, enemy.EnemyY + 1);           // descend the ship from one floor
                                enemy.EnemyY++;
                                enemy.EnemyX--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            }
                        }
                        // Speed up the ship
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Change the vertical direction of the ship
                        if(_enemies[i].EnemyY == 12)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Else, move the ship to the right
                    else if (_enemies[i].EnemyDirection == false && _enemies[i].EnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX + 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX++;
                    }
                }

                else if (_enemies[i] != null)
                {
                    // If the lateral position is touching the right border of the window
                    if (_enemies[i].EnemyX + 5 == Console.WindowWidth)
                    {
                        //Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                        //_enemies[i].EnemyY++;
                        //_enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX - 1, enemy.EnemyY - 1);           // descend the ship from one floor
                                enemy.EnemyX--;
                                enemy.EnemyY--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            }
                        }
                        // Speed up the ship
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Change the vertical direction of the ship
                        if (_enemies[i].EnemyY == 3)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Else, move the ship to the right
                    else if (_enemies[i].EnemyDirection == false && _enemies[i].EnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX + 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX++;
                    }
                }
            }


            for (int i = 0; i != _enemies.Length; i++)
            {
                if (_enemies[i] != null && _upDown == false)
                {
                    // If the lateral position is touching the left border of the window
                    if (_enemies[i].EnemyX == Console.WindowLeft)
                    {

                        //Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                        //_enemies[i].EnemyY++;
                        //_enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX + 1, enemy.EnemyY + 1);           // descend the ship from one floor
                                enemy.EnemyY++;
                                enemy.EnemyX++;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            }
                        }
                        // Speed up the ship
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Change the vertical direction of the ship
                        if (_enemies[i].EnemyY == 12)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Else, move the ship to the left
                    else if (_enemies[i].EnemyDirection == true && _enemies[i].EnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX - 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX--;
                    }
                }

                else if (_enemies[i] != null)
                {
                    // If the lateral position is touching the left border of the window
                    if (_enemies[i].EnemyX == Console.WindowLeft)
                    {

                        //Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY + 1);       // descend the ship from one floor
                        //_enemies[i].EnemyY++;
                        //_enemies[i].EnemyDirection = !_enemies[i].EnemyDirection;                                                               // Change the direction of the ship

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX + 1, enemy.EnemyY - 1);           // descend the ship from one floor
                                enemy.EnemyX++;
                                enemy.EnemyY--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Change the direction of the ship
                            }
                        }
                        // Speed up the ship
                        if(_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Change the vertical direction of the ship
                        if (_enemies[i].EnemyY == 3)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Else, move the ship to the left
                    else if (_enemies[i].EnemyDirection == true && _enemies[i].EnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX - 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX--;
                    }
                }
            }
            for (int i = 0; i != _enemies.Length; i++)
            {
                // if the enemy can shoot or not
                if (_enemies[i] != null)
                {
                    // The first ship can shoot
                    if (i == 3 || i == 7 || i == 11 || i == 15 || i == 19)
                    {
                        _enemies[i].Shoot = true;
                    }
                    // For each ship, check if all of ship ahead is dead and if true, then they can shoot
                    else if (i == 2 || i == 6 || i == 10 || i == 14 || i == 18)
                    {
                        if (_enemies[i + 1] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                    else if (i == 1 || i == 5 || i == 9 || i == 13 || i == 17)
                    {
                        if (_enemies[i + 1] == null && _enemies[i + 2] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                    else if (i == 0 || i == 4 || i == 8 || i == 12 || i == 16)
                    {
                        if (_enemies[i + 1] == null && _enemies[i + 2] == null && _enemies[i + 3] == null)
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
        /// <param name="pause">Check if the game is on pause or not</param>
        public void StopMove(bool pause)
        {
            if (pause == true)
            {
                _enemyMovement.Stop();
            }
            else
            {
                _enemyMovement.Start();
            }
        }
        #endregion
    }
}