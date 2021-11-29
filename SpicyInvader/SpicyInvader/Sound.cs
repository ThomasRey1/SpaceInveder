/*
 *  Auteur : Thomas Rey
 *  Date : 24.09.2021
 *  Lieu : ETML
 *  Description : Code gerant le son du jeu
 */

using System.Media;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the sound of the game
    /// </summary>
    public class Sound
    {
        #region Method
        /// <summary>
        /// Play the main music
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void SoundMenu(bool sound)
        {
            SoundPlayer musicMenu = new SoundPlayer("MusicMenu.wav");
            if (sound == true)
            {
                musicMenu.PlayLooping();
            }
            else
            {
                musicMenu.Stop();
            }
        }

        /// <summary>
        /// play the shoot sound
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void SoundShipShoot(bool sound)
        {
            SoundPlayer soundShoot = new SoundPlayer("LaserShoot.wav");
            if (sound == true)
            {
                soundShoot.Play();
            }
            else
            {
                
            }
        }
        #endregion
    }
}