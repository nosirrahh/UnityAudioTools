using System.Collections.Generic;
using UnityEngine;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Represents a group of audio clips for a specific scene, allowing you to 
    /// retrieve audio clips by their name at runtime.
    /// </summary>
    public class SceneAudioGroup : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// A list of audio clips available in this audio group.
        /// These clips are managed and accessed using their names.
        /// </summary>
        [SerializeField]
        private List<AudioClip> audios;

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves an audio clip from the group by its name.
        /// </summary>
        /// <param name="audio">
        /// The name of the audio clip to retrieve.
        /// </param>
        /// <returns>
        /// The audio clip with the matching name if found; otherwise, null.
        /// </returns>
        public AudioClip GetAudio (string audio)
        {
            try
            {
                return audios.Find((AudioClip clip) =>  clip.name == audio);
            }
            catch (System.Exception exception)
            {
                Debug.LogError (exception.Message);
                return null;
            }
        }

        #endregion
    }
}