using System;
using UnityEngine;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Stores information related to audio playback.
    /// </summary>
    [Serializable]
    public class AudioPlayback
    {
        #region Fields

        /// <summary>
        /// The delay (in seconds) before the audio starts playing.
        /// </summary>
        public float delay;
        /// <summary>
        /// The <see cref="AudioClip"/> to be played.
        /// </summary>
        public AudioClip audioClip;
        /// <summary>
        /// The <see cref="AudioSource"/> responsible for playing the audio.
        /// </summary>
        public AudioSource audioSource;

        #endregion

        #region Properties

        /// <summary>
        /// A unique identifier for the audio playback session.
        /// </summary>
        public string Id { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPlayback"/> class with a unique Id.
        /// </summary>
        /// <param name="id">The unique identifier for this audio playback session.</param>
        public AudioPlayback (string id)
        {
            Id = id;
            delay = 0;
            audioClip = null;
            audioSource = null;
        }

        #endregion
    }
}