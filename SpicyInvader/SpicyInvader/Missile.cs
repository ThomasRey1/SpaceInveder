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
        //Properties
        private string _missileShap = "|";       // The missile form

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


        public Ennemy[] Ennemies { get; set; }


        public PlayerShip Player { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="missilePause">If the missile is activated or not</param>
        /// <param name="missileLive">If the game is in pause, the missile too</param>
        public Missile(int missileX, int missileY, bool missilePause, bool missileLive, List<int> posXBunker, Ennemy[] ennemies)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissileLive = missileLive;
            this.MissilePause = missilePause;
            this.PosXBunker = posXBunker;
            this.Ennemies = ennemies;
            SetTimerPlayer();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="missileX">The lateral position of the missile</param>
        /// <param name="missileY">The vertical position of the missile</param>
        /// <param name="missilePause">If the game is in pause, the missile too</param>
        public Missile(int missileX, int missileY, bool missilePause)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissilePause = missilePause;
            SetTimerEnnemy();
        }

        /// <summary>
        /// Timer wich call method MissilePlayerMove every 100 milliseconds
        /// </summary>
        public void SetTimerPlayer()
        {
            Timer shootPlayer = new Timer(100);
            shootPlayer.Elapsed += new ElapsedEventHandler(MissilePlayerMove);
            shootPlayer.Enabled = true;
        }

        /// <summary>
        /// Timer which call method MissileEnnemyMove every 250 milliseconds
        /// </summary>
        public void SetTimerEnnemy()
        {
            Timer shootEnnemy = new Timer(100);
            shootEnnemy.Elapsed += new ElapsedEventHandler(MissileEnnemyMove);
            shootEnnemy.Enabled = true;
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
                    }
                }
                // Check if the missile touch a pixel of a ennemy
                for (int i = 0; i != Ennemies.Length; i++)
                {
                    if (Ennemies[i] != null && MissileY == Ennemies[i].EnnemyY)
                    {
                        if (MissileX >= Ennemies[i].EnnemyX && MissileX <= Ennemies[i].EnnemyX + Ennemies[i]._shipForm.Length - 1)
                        {
                            //Ennemies[i].ShipEnnemyLive = false;
                            //Ennemies[i].Shoot = false;
                            Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                            Console.MoveBufferArea(0, 0, 5, 1, Ennemies[i].EnnemyX, Ennemies[i].EnnemyY);
                            Ennemies[i] = null;
                            MissileY = Console.WindowTop;
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
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MissileEnnemyCreate()
        {
            if (MissilePause == false)
            {
                Console.SetCursorPosition(MissileX, MissileY); // Position the cursor in the location of the missile
                Console.Write(_missileShap);                   // Create the missile
            }
        }

        /// <summary>
        /// Move the player missile towards the bottom of the window
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissileEnnemyMove(object source, ElapsedEventArgs e)
        {
            if (MissilePause == false)
            {
                // Check if the missile touch a pixel of a bunker
                for (int i = 0; i < PosXBunker.Count; i++)
                {
                    if (MissileY == 20 && MissileX == PosXBunker[i])
                    {
                        Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                        PosXBunker[i] = Console.WindowTop;
                        MissileY = Console.WindowHeight - 1;
                    }
                }
                // Check if the missile touch a pixel of the player
                if (MissileY == Player.ShipY)
                {
                    if (MissileX >= Player.ShipX && MissileX <= Player.ShipX + Player._shipForm.Length - 1)
                    {
                        Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                        MissileY = Console.WindowHeight - 1;
                    }
                }
                // Move the missile as long as the missile don't touch the bottom
                if (MissileY <= Console.WindowHeight - 2)
                {
                    MissileY++;
                    Console.MoveBufferArea(MissileX, MissileY - 1, 1, 1, MissileX, MissileY);
                }
                // Else destroy the missile
                else
                {
                    Console.MoveBufferArea(MissileX - 1, MissileY, 1, 1, MissileX, MissileY);
                    MissileLive = false;
                }
            }
        }
    }
}