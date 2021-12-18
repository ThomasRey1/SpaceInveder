/*
 *  Auteur : Thomas Rey
 *  Date : 10.09.2021
 *  Lieu : ETML
 *  Description : Code gerant les parametre des missiles
 */

using System;
using System.Timers;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the missiles parameters
    /// </summary>
    public class Missile
    {
        #region Properties
        //Properties
        private string _missileShap = "|";              // The missile form
        private Timer _shootEnemy = new Timer(50);      // Loop to lower the enemy missiles
        private Timer _shootPlayer = new Timer(50);     // Loop to lower the player missiles
        private List<int> _posXBunker;                  // Postition of the bunker
        private Enemy[] _enemies;                       // List of enemies
        private PlayerShip _player;                     // The player ship
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// MissileX property definition
        /// </summary>
        public int MissileX { get; set; }                   // The lateral position of the missile

        /// <summary>
        /// MissileY property definition
        /// </summary>
        public int MissileY { get; set; }                   // The vertical position of the missile

        /// <summary>
        /// MissileLive property definition
        /// </summary>
        public bool MissileLive { get; set; }               // The missile is activated or not
        #endregion

        #region Method
        /// <summary>
        /// Constructor player
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="missileLive">The missile is activated or not</param>
        /// <param name="posXBunker">Postition of the bunker</param>
        /// <param name="enemies">List of enemies</param>
        /// <param name="player">The player ship</param>
        public Missile(int missileX, int missileY, bool missileLive, List<int> posXBunker, Enemy[] enemies, PlayerShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissileLive = missileLive;
            this._posXBunker = posXBunker;
            this._enemies = enemies;
            this._player = player;
            _shootEnemy = null;
            SetTimerPlayer();
        }

        /// <summary>
        /// Constructor enemy
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="posXBunker">Postition of the bunker</param>
        /// <param name="player">The player ship</param>
        public Missile(int missileX, int missileY, List<int> posXBunker, PlayerShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this._posXBunker = posXBunker;
            this._player = player;
            _shootPlayer = null;
            SetTimerEnemy();
        }

        /// <summary>
        /// Timer wich call method MissilePlayerMove every 100 milliseconds
        /// </summary>
        public void SetTimerPlayer()
        {
            _shootPlayer.Elapsed += new ElapsedEventHandler(MissilePlayerMove);
        }

        /// <summary>
        /// Timer which call method MissileEnemyMove every 250 milliseconds
        /// </summary>
        public void SetTimerEnemy()
        {
            _shootEnemy.Elapsed += new ElapsedEventHandler(MissileEnemyMove);
        }

        /// <summary>
        /// Creates a player missile
        /// </summary>
        public void MissilePlayerCreate()
        {
                Console.SetCursorPosition(MissileX, MissileY);  // Position the cursor in the location of the missile
                Console.Write(_missileShap);                    // Create the missile
                _shootPlayer.Start();
        }

        /// <summary>
        /// Move the player missile towards the top of the window
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissilePlayerMove(object source, ElapsedEventArgs e)
        {
                // Check if the missile touch a pixel of a bunker
                for (int i = 0; i < _posXBunker.Count; i++)
                {
                    if (MissileY == 20 && MissileX == _posXBunker[i])
                    {
                        Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                        _posXBunker[i] = Console.WindowTop;
                        MissileY = Console.WindowTop;
                        _posXBunker.Remove(_posXBunker[i]);
                        _shootPlayer.Stop();
                    }
                }
                
                for (int i = 0; i != _enemies.Length; i++)
                {
                    if (_enemies[i] != null && MissileY == _enemies[i].EnemyY)
                    {
                        // Check if the missile touch a pixel of a enemy
                        if (MissileX >= _enemies[i].EnemyX && MissileX <= _enemies[i].EnemyX + _enemies[i].ShipForm.Length - 1)
                        {
                            Sound.SoundShipExplosed(_enemies[i].SoundGame);
                            Console.MoveBufferArea(0, 0, 1, 1, this.MissileX, this.MissileY);
                            Console.MoveBufferArea(0, 0, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY);
                            _enemies[i].MissileEnemy._shootEnemy.Stop();
                            _enemies[i].Dead();
                            _enemies[i] = null;
                            MissileY = Console.WindowTop;
                            _player.Score += 100;
                            Console.SetCursorPosition(Console.WindowWidth / 2 + 3, Console.WindowHeight - 3);
                            Console.Write(_player.Score);
                        }
                    }
                }


                // Move the missile as long as the missile don't touch the top
                if (MissileY != Console.WindowTop)
                {
                    MissileY--;
                    Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);

                }
                // Else destroy the missile
                else
                {
                    Console.MoveBufferArea(MissileX - 1, MissileY, 1, 1, MissileX, MissileY);
                    MissileLive = false;
                    _shootPlayer.Stop();
                }
        }

        /// <summary>
        /// Creates a enemy missile
        /// </summary>
        public void MissileEnemyCreate()
        {
                Console.SetCursorPosition(MissileX, MissileY); // Position the cursor in the location of the missile
                Console.Write(_missileShap);                   // Create the missile
                _shootEnemy.Start();
        }

        /// <summary>
        /// Move the player missile towards the bottom of the window
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissileEnemyMove(object source, ElapsedEventArgs e)
        {
                // Check if the missile touch a pixel of a bunker
                for (int i = 0; i < _posXBunker.Count; i++)
                {
                    if (MissileY == 20 && MissileX == _posXBunker[i])
                    {
                        Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                        _posXBunker[i] = Console.WindowTop;
                        MissileY = Console.WindowHeight - 1;
                        _posXBunker.Remove(_posXBunker[i]);
                        _shootEnemy.Stop();
                    }
                }
                // Check if the missile touch a pixel of the player
                if (MissileY == _player.ShipY - 1)
                {
                    if (MissileX >= _player.ShipX && MissileX <= _player.ShipX + _player.ShipForm.Length - 1)
                    {
                        Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                        MissileY = Console.WindowHeight;
                        Console.MoveBufferArea(1, 1, 2, 1, Console.WindowLeft + 21 - (6 / _player.ShipLife), Console.WindowHeight - 3);
                        _player.ShipLife -= 1;
                        _shootEnemy.Stop();
                    }
                }

                // Check if the missile touch a another player's missile
                if (MissileX == _player.MissilePlayer.MissileX && MissileY >= _player.MissilePlayer.MissileY)
                {
                    Console.MoveBufferArea(0, 0, 1, 1, MissileX, MissileY);
                    MissileLive = false;
                    _shootEnemy.Stop();
                    Console.MoveBufferArea(1, 1, 1, 1, _player.MissilePlayer.MissileX, _player.MissilePlayer.MissileY);
                    _player.MissilePlayer.MissileLive = false;
                    _player.MissilePlayer._shootPlayer.Stop();
                }

                // Move the missile as long as the missile don't touch the bottom
                if (MissileY < Console.WindowHeight - 8)
                {
                    MissileY++;
                    Console.MoveBufferArea(MissileX, MissileY - 1, 1, 1, MissileX, MissileY);
                }
                // Else destroy the missile
                else
                {
                    Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                    MissileLive = false;
                    _shootEnemy.Stop();
                }
        }

        /// <summary>
        /// Start or stop the shoot
        /// </summary>
        /// <param name="pause">Check if the game is on pause or not</param>
        public void StopShoot(bool pause)
        {
            if (pause == true)
            {
                if (_shootPlayer != null)
                {
                    _shootPlayer.Stop();
                }
                else
                {
                    _shootEnemy.Stop();
                }
            }
            else
            {
                if (_shootPlayer != null)
                {
                    _shootPlayer.Start();
                }
                else
                {
                    _shootEnemy.Start();
                }
            }
        }
        #endregion
    }
}