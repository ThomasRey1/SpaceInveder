/*
 *  Auteur : Thomas Rey
 *  Date : 15.10.2021
 *  Lieu : ETML
 *  Description : Code de test
 */

using System;
using System.Collections.Generic;

namespace SpicyInvader
{
    class test
    {
        public List<int> posXBunker = new List<int>();
        public test()
        {
            for (int i = 1; i != 5; i++)
            {
                Bunker bunker = new Bunker(i);                                                              // Create a bunker

                for (int j = 0; j != 18; j++)
                {
                    posXBunker.Add(Console.WindowWidth / 4 * i - 23 + j);
                }
            }
            Enemy[] ennemies = new Enemy[20];
            int x = 0;
            for (int i = 0; i != 5; i++)
            {
                //Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 2, false, true, true, posXBunker);      // Create an ennemy
                //ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                //Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 4, false, true, true, posXBunker);      // Create an ennemy
                //ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                //Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 6, false, true, true, posXBunker);      // Create an ennemy
                //ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                //Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 8, false, true, true, posXBunker);      // Create an ennemy
                //ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            Move move = new Move(ennemies);
            // Changement de direction pour un groupe d'ennemies
            // Gerer les missiles que les ennemies lance pour que se sois seulement ceux d'en bas qui puissent tiré et aussi faire en sorte qu'ils puissent tiré de manière aléa
        }
    }
}