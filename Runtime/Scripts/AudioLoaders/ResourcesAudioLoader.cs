using UnityEngine;
using UnityEngine.Events;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Implementation of <see cref="IAudioLoader"/> that loads audio clips using Unity's Resources API.
    /// </summary>
    public class ResourcesAudioLoader : IAudioLoader
    {
        /// <summary>
        /// Loads an audio clip asynchronously from the Resources folder and invokes a callback upon completion.
        /// </summary>
        /// <param name="audio">The name or path of the audio resource to load (relative to the Resources folder).</param>
        /// <param name="onCompleted">
        /// A callback that receives the loaded <see cref="AudioClip"/>. 
        /// If the load fails, the parameter will be null.
        /// </param>
        public void LoadAudio (string audio, UnityAction<AudioClip> onCompleted)
        {
            ResourceRequest request = Resources.LoadAsync<AudioClip> (audio);
            request.completed += (operation) => onCompleted?.Invoke (request.asset as AudioClip);
        }
    }
}