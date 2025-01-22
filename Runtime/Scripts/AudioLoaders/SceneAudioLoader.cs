using UnityEngine;
using UnityEngine.Events;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// An implementation of IAudioLoader that loads audio clips using a SceneAudioGroup.
    /// This loader retrieves audio clips based on the active scene's configuration.
    /// </summary>
    public class SceneAudioLoader : IAudioLoader
    {
        #region Fields

        /// <summary>
        /// Reference to the SceneAudioGroup component in the current scene.
        /// </summary>
        private SceneAudioGroup sceneAudioGroup;

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads an audio clip by name from the active scene's SceneAudioGroup.
        /// </summary>
        /// <param name="audio">The name of the audio clip to load.</param>
        /// <param name="onCompleted">
        /// A callback that is invoked when the audio clip is successfully loaded.
        /// Passes the loaded AudioClip as a parameter.
        /// </param>
        public void LoadAudio (string audio, UnityAction<AudioClip> onCompleted)
        {
            AudioClip audioClip = null;

            try
            {
                SetSceneAudioGroup ();
                audioClip = sceneAudioGroup.GetAudio (audio);
            }
            catch (System.Exception exception)
            {
                Debug.LogError ($"[{nameof (SceneAudioLoader)}] {nameof (LoadAudio)} - Exception: {exception}");
                throw exception;
            }

            onCompleted?.Invoke (audioClip);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Finds and assigns the SceneAudioGroup in the active scene if not already set.
        /// </summary>
        private void SetSceneAudioGroup ()
        {
            if (sceneAudioGroup == null)
                sceneAudioGroup = Object.FindObjectOfType<SceneAudioGroup> ();
        }

        #endregion
    }
}