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
        private Timer shootEnemy = new Timer(50);      // Loop to lower the enemy missiles
        private Timer shootPlayer = new Timer(50);     // Loop to lower the player missiles
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

        /// <summary>
        /// MissilePause property definition
        /// </summary>
        public bool MissilePause { get; set; }              // The missile is in pause

        /// <summary>
        /// PosXBunker property definition
        /// </summary>
        public List<int> PosXBunker { get; set; }           // Postition of the bunker

        /// <summary>
        /// Enemies property definition
        /// </summary>
        public Enemy[] Enemies { get; set; }              // List of enemies

        /// <summary>
        /// Player property definition
        /// </summary>
        public PlayerShip Player { get; set; }              // The player ship
        #endregion

        #region Method
        /// <summary>
        /// Constructor player
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="missilePause">If the missile is activated or not</param>
        /// <param name="missileLive">If the game is in pause, the missile too</param>
        public Missile(int missileX, int missileY, bool missilePause, bool missileLive, List<int> posXBunker, Enemy[] enemies)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissileLive = missileLive;
            this.MissilePause = missilePause;
            this.PosXBunker = posXBunker;
            this.Enemies = enemies;
            shootEnemy = null;
            SetTimerPlayer();
        }

        /// <summary>
        /// Constructor enemy
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="missilePause">If the game is in pause, the missile too</param>
        public Missile(int missileX, int missileY, bool missilePause)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissilePause = missilePause;
            shootPlayer = null;
            SetTimerEnemy();
        }

        /// <summary>
        /// Timer wich call method MissilePlayerMove every 100 milliseconds
        /// </summary>
        public void SetTimerPlayer()
        {
            shootPlayer.Elapsed += new ElapsedEventHandler(MissilePlayerMove);
        }

        /// <summary>
        /// Timer which call method MissileEnemyMove every 250 milliseconds
        /// </summary>
        public void SetTimerEnemy()
        {
            shootEnemy.Elapsed += new ElapsedEventHandler(MissileEnemyMove);
        }

        /// <summary>
        /// Creates a player missile
        /// </summary>
        public void MissilePlayerCreate()
        {
            if (MissilePause == false)
            {
                Console.SetCursorPosition(MissileX, MissileY);  // Position the cursor in the location of the missile
                Console.Write(_missileShap);                    // Create the missile
                shootPlayer.Start();
            }
        }

        /// <summary>
        /// Move the player missile towards the top of the window
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissilePlayerMove(object source, ElapsedEventArgs e)
        {
            // If the game is not in pause
            if (MissilePause == false)
            {
                // Check if the missile touch a pixel of a bunker
                for (int i = 0; i < PosXBunker.Count; i++)
                {
                    if (MissileY == 20 && MissileX == PosXBunker[i])
                    {
                        Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                        PosXBunker[i] = Console.WindowTop;
                        MissileY = Console.WindowTop;
                        PosXBunker.Remove(PosXBunker[i]);
                        shootPlayer.Stop();
                    }
                }
                
                for (int i = 0; i != Enemies.Length; i++)
                {
                    if (Enemies[i] != null && MissileY == Enemies[i].EnemyY)
                    {
                        // Check if the missile touch a pixel of a enemy
                        if (MissileX >= Enemies[i].EnemyX && MissileX <= Enemies[i].EnemyX + Enemies[i]._shipForm.Length - 1)
                        {
                            Console.MoveBufferArea(0, 0, 1, 1, this.MissileX, this.MissileY);
                            Console.MoveBufferArea(0, 0, 5, 1, Enemies[i].EnemyX, Enemies[i].EnemyY);
                            Enemies[i].MissileEnemy.shootEnemy.Stop();
                            Enemies[i].Dead();
                            Enemies[i] = null;
                            MissileY = Console.WindowTop;
                            Player.Score += 100;
                            Console.SetCursorPosition(Console.WindowWidth / 3 + 8, Console.WindowHeight - 3);
                            Console.Write(Player.Score);
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
                    shootPlayer.Stop();
                }
            }
        }

        /// <summary>
        /// Creates a enemy missile
        /// </summary>
        public void MissileEnemyCreate()
        {
            if (MissilePause == false)
            {
                Console.SetCursorPosition(MissileX, MissileY); // Position the cursor in the location of the missile
                Console.Write(_missileShap);                   // Create the missile
                Console.MoveBufferArea(1, 1, 1, 1, MissileX - 1, MissileY);
                Console.MoveBufferArea(1, 1, 1, 1, MissileX + 1, MissileY);
                shootEnemy.Start();
            }
        }

        /// <summary>
        /// Move the player missile towards the bottom of the window
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissileEnemyMove(object source, ElapsedEventArgs e)
        {
            if (MissilePause == false)
            {
                // Check if the missile touch a pixel of a bunker
                for (int i = 0; i < PosXBunker.Count; i++)
                {
                    if (MissileY == 20 && MissileX == PosXBunker[i])
                    {
                        Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                        PosXBunker[i] = Console.WindowTop;
                        MissileY = Console.WindowHeight - 1;
                        PosXBunker.Remove(PosXBunker[i]);
                        shootEnemy.Stop();
                    }
                }
                // Check if the missile touch a pixel of the player
                if (MissileY == Player.ShipY - 1)
                {
                    if (MissileX >= Player.ShipX && MissileX <= Player.ShipX + Player._shipForm.Length - 1)
                    {
                        Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                        MissileY = Console.WindowHeight;
                        Console.MoveBufferArea(1, 1, 2, 1, Console.WindowLeft + 11 - (6 / Player.ShipLife), Console.WindowHeight - 3);
                        Player.ShipLife -= 1;
                        shootEnemy.Stop();
                    }
                }

                // Check if the missile touch a another player's missile
                if (MissileX == Player.MissilePlayer.MissileX && MissileY >= Player.MissilePlayer.MissileY)
                {
                    Console.MoveBufferArea(0, 0, 1, 1, MissileX, MissileY);
                    MissileLive = false;
                    shootEnemy.Stop();
                    Console.MoveBufferArea(1, 1, 1, 1, Player.MissilePlayer.MissileX, Player.MissilePlayer.MissileY);
                    Player.MissilePlayer.MissileLive = false;
                    Player.MissilePlayer.shootPlayer.Stop();
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
                    shootEnemy.Stop();
                }
            }
        }

        /// <summary>
        /// Start or stop the shoot
        /// </summary>
        public void StopShoot()
        {
            if(shootPlayer == null)
            {
                if (shootEnemy.Enabled == true)
                {
                    shootEnemy.Stop();
                }
                else
                {
                    shootEnemy.Start();
                }
            }
            else
            {
                if (shootPlayer.Enabled == true)
                {
                    shootPlayer.Stop();
                }
                else
                {
                    shootPlayer.Start();
                }
            }
        }
        #endregion
    }
}