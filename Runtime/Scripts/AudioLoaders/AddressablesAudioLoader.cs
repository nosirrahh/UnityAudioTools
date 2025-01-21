using NosirrahhTools.UnityAddressablesTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Implementation of <see cref="IAudioLoader"/> that loads audio clips using Unity's Addressables system.
    /// </summary>
    public class AddressablesAudioLoader : IAudioLoader
    {
        /// <summary>
        /// Loads an audio clip asynchronously from the Addressables system and invokes a callback upon completion.
        /// </summary>
        /// <param name="audio">The Addressable key for the audio resource to load.</param>
        /// <param name="onCompleted">
        /// A callback that receives the loaded <see cref="AudioClip"/>. 
        /// If the load fails, the parameter will be null.
        /// </param>
        public void LoadAudio (string audio, UnityAction<AudioClip> onCompleted)
        {
            AddressablesHelper.Instance.Exists<AudioClip> (
                audio,
                (status) =>
                {
                    if (status != AsyncOperationStatus.Succeeded)
                        return;

                    AddressablesHelper.Instance.Load (
                        audio,
                        (AsyncOperationStatus status, AudioClip audioClip) =>
                        {
                            onCompleted?.Invoke (audioClip);
                        }
                    );
                }
            );
        }
    }
}