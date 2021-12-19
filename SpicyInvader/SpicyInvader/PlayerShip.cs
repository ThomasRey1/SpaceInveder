/*
 *  Auteur : Thomas Rey
 *  Date : 03.09.2021
 *  Lieu : ETML
 *  Description : Code gerant le vaisseau du joueur
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the player's ship
    /// </summary>
    public class PlayerShip
    {
        #region Properties
        //Properties
        private bool _gamePause = false;        // Check if the game is in pause
        private bool _over = false;             // Check if the game is over or not
        private bool _soundGame;                 // The sound is ON or OFF
        private const byte _SHIPSPEED = 1;      // The ship movement speed
        private Enemy[] _enemies;               // List of enemies
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// ShipForm property definition
        /// </summary>
        public string ShipForm { get; }                     // The shap of the player's ship

        /// <summary>
        /// ShipX property definition
        /// </summary>
        public int ShipX { get; set; }                      // The lateral position of the ship

        /// <summary>
        /// ShipY property definition
        /// </summary>
        public int ShipY { get; }                           // The vertical position of the ship

        /// <summary>
        /// ShipLife property definition
        /// </summary>
        public byte ShipLife { get; set; }                  // The limit life of the player

        /// <summary>
        /// Score property definition
        /// </summary>
        public int Score { get; set; }                      // Score of the game

        /// <summary>
        /// MissilePlayer preperty definition
        /// </summary>
        public Missile MissilePlayer { get; set; }          // Create a missile
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shipX">The lateral position of the ship</param>
        /// <param name="shipY">The vertical position of the ship</param>
        /// <param name="soundGame">The sound is ON or OFF</param>
        /// <param name="posXBunker">Postition of the bunker</param>
        /// <param name="enemies">List of enemies</param>
        public PlayerShip(int shipX, int shipY, bool soundGame, List<int> posXBunker, Enemy[] enemies)
        {
            ShipForm = "├─┴─┤";
            this.ShipX = shipX;
            this.ShipY = shipY;
            this._soundGame = soundGame;
            this._enemies = enemies;
            this.ShipLife = 3;
            this.Score = 0;
            this.MissilePlayer = new Missile(ShipX, ShipY, false, posXBunker, _enemies, this);
        }

        [DllImport("User32.dll")]                       // Import the User32.dll
        static extern short GetAsyncKeyState(int key);  // keyboard key pressed

        /// <summary>
        /// Allows the player to move and shoot with the ship and to pause the game
        /// </summary>
        /// <param name="move">The enemies movement</param>
        public void ShipAction(Move move)
        {
            _over = false;
            short GetAsyncKeyStateResult = GetAsyncKeyState(32); // Result of the keyboard key pressed
            do
            {
                // If the result of the keyboard key pressed is positiv and he's equal to the left arrow key
                if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(37))
                {
                    // If the game is not in pause
                    if (_gamePause == false)
                    {
                        // If the lateral position is not touching the left border of the window
                        if (ShipX != Console.WindowLeft)
                        {
                            ShipX--;
                            Console.MoveBufferArea(ShipX + 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); // Move the ship to the left
                        }
                        // Else do nothing
                        else { }
                        System.Threading.Thread.Sleep(_SHIPSPEED);
                    }
                }
                // Else if the result of the keyboard key pressed is positiv and he's equal to the right arrow key
                else if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(39))
                {
                    // If the game is not in pause
                    if (_gamePause == false)
                    {
                        // If the lateral position plus the lenght of the ship is not touching the right border of the window
                        if (ShipX + ShipForm.Length != Console.WindowWidth)
                        {
                            ShipX++;
                            Console.MoveBufferArea(ShipX - 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); // Move the ship to the right
                        }
                        // Else do nothing
                        else { }
                        System.Threading.Thread.Sleep(_SHIPSPEED);
                    }
                }
                // If a key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true); // Read the key pressed
                    // If the key pressed is the left arrow
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(37); // Initialize the GetAsynKeyState in left arrow
                    }
                    // If the key pressed is the right arrow
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(39); // Initialize the GetAsynKeyState in right arrow
                    }
                    // If the key pressed is the space bar
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Check if a missile is already launched
                        if (MissilePlayer.MissileLive == false)
                        {
                            Sound.SoundShipShoot(_soundGame); // Play the shoot sound
                            MissilePlayer.MissileLive = true;
                            // Reposition the location of the missile
                            MissilePlayer.MissileY = ShipY - 1;
                            MissilePlayer.MissileX = ShipX + (ShipForm.Length / 2);
                            MissilePlayer.MissilePlayerCreate();
                        }
                    }
                    // If the key pressed is the P or escape
                    else if (key.Key == ConsoleKey.P || key.Key == ConsoleKey.Escape)
                    {
                        // The game is in pause
                        _gamePause = !_gamePause;
                        MissilePlayer.StopShoot(_gamePause);
                        move.StopMove(_gamePause);
                        foreach(Enemy x in _enemies)
                        {
                            if(x != null)
                            {
                                x.StopShoot(_gamePause);
                            }
                        }
                    }
                }
                // if the player is dead, the game is over
                if(ShipLife == 0)
                {
                    _over = true;
                }
                byte i = 0;
                foreach(Enemy x in _enemies)
                {
                    if (x != null)
                    {
                        i++;
                    }
                }
                // if there is no more of enemies the game is over but it restart immediatly
                if(i == 0)
                {
                    _over = true;
                }
                i = 0;
            }
            while (_over == false);
            MissilePlayer.StopShoot(_gamePause);
        }
        #endregion
    }
}