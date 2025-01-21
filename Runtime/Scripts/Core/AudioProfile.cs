using System;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Represents an audio profile that defines playback settings.
    /// </summary>
    [Serializable]
    public struct AudioProfile
    {
        /// <summary>
        /// The playback volume, ranging from 0.0 (silent) to 1.0 (full volume).
        /// </summary>
        public float volume;
        /// <summary>
        /// Indicates whether the audio is muted.
        /// </summary>
        public bool mute;
    }
}