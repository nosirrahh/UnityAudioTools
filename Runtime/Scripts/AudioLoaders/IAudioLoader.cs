using UnityEngine;
using UnityEngine.Events;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Defines a contract for loading audio clips asynchronously.
    /// </summary>
    public interface IAudioLoader
    {
        /// <summary>
        /// Loads an audio clip asynchronously and invokes a callback upon completion.
        /// </summary>
        /// <param name="audio">The name or path of the audio asset to load.</param>
        /// <param name="onCompleted">
        /// A callback that receives the loaded <see cref="AudioClip"/>. 
        /// </param>
        public void LoadAudio (string audio, UnityAction<AudioClip> onCompleted);
    }
}