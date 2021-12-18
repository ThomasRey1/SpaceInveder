/*
 *  Auteur : Thomas Rey
 *  Date : 24.09.2021
 *  Lieu : ETML
 *  Description : Code gerant le son du jeu
 */

using System.Media;
using WMPLib;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the sound of the game
    /// </summary>
    public class Sound
    {
        #region Method
        static WindowsMediaPlayer musicMenu = new WindowsMediaPlayer();
        static WindowsMediaPlayer musicGame = new WindowsMediaPlayer();

        /// <summary>
        /// Play the main music
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void MusicMenu(bool sound)
        {
            try
            {
                if (sound == true)
                {
                    musicMenu.URL = "Resource/MusicMenu.mp3";
                    musicGame.controls.stop();
                }
                else
                {
                    musicMenu.controls.stop();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// play when the player start a game
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void MusicGame(bool sound)
        {
            try
            {
                if (sound == true)
                {
                    musicMenu.controls.stop();
                    musicGame.URL = "Resource/MusicGame.mp3";
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// play when the player select an option
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void SelectOption(bool sound)
        {
            try
            {
                WindowsMediaPlayer option = new WindowsMediaPlayer();
                if (sound == true)
                {
                    option.URL = "Resource/SelectOption.mp3";
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// play when the player move the cursor
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void ChoosOption(bool sound)
        {
            try
            {
                WindowsMediaPlayer option = new WindowsMediaPlayer();
                if (sound == true)
                {
                    option.URL = "Resource/ChoosOption.mp3";
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// play the shoot sound
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void SoundShipShoot(bool sound)
        {
            try
            {
                WindowsMediaPlayer soundShoot = new WindowsMediaPlayer();
                if (sound == true)
                {
                    soundShoot.URL = "Resource/LaserShoot.mp3";
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// play when a ship is destroy
        /// </summary>
        /// <param name="sound">check if the sound is activated or not</param>
        public static void SoundShipExplosed(bool sound)
        {
            try
            {
                WindowsMediaPlayer soundExplosed = new WindowsMediaPlayer();
                if (sound == true)
                {
                    soundExplosed.URL = "Resource/Explosion.mp3";
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}