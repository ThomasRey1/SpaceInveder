/*
 *  Auteur : Thomas Rey
 *  Date : 15.10.2021
 *  Lieu : ETML
 *  Description : Code gerant les bunker
 */

using System;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the bunker
    /// </summary>
    class Bunker
    {
        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decalage">Offset between each bunker</param>
        public Bunker(int decalage)
        {
            Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 20);
            Console.WriteLine("██████████████████");
            
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 20);
            //Console.WriteLine("      █████");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 21);
            //Console.WriteLine("    ██     ██");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 22);
            //Console.WriteLine("  ██         ██");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 23);
            //Console.WriteLine("██            ██");
        }
        #endregion
    }
}