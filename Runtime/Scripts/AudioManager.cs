using NosirrahhTools.UnityCoreTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NosirrahhTools.UnityAudioTools
{
    /// <summary>
    /// Manages audio playback and resources in the game.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        #region Fields

        /// <summary>
        /// The audio loader used to load audio assets.
        /// </summary>
        private IAudioLoader audioLoader;
        /// <summary>
        /// Factory for managing and recycling AudioSource objects.
        /// </summary>
        private Factory<AudioSource> factory;
        /// <summary>
        /// Dictionary to track currently playing audio by their unique Ids.
        /// </summary>
        private Dictionary<string, AudioPlayback> playingAudios;

        #endregion

        #region Singleton Methods

        /// <summary>
        /// Initializes the AudioManager, building the required hierarchy and setting up internal structures.
        /// </summary>
        public override void Initialize ()
        {
            playingAudios = new Dictionary<string, AudioPlayback> ();
            BuildHierarchy ();
            base.Initialize ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the audio loader responsible for loading audio assets.
        /// </summary>
        /// <param name="audioLoader">The audio loader instance.</param>
        public void SetAudioLoader (IAudioLoader audioLoader)
        {
            this.audioLoader = audioLoader;
        }

        /// <summary>
        /// Plays an audio clip with specified parameters.
        /// </summary>
        /// <param name="audio">The name or path of the audio asset to play.</param>
        /// <param name="volume">The volume at which the audio should play (default is 1.0).</param>
        /// <param name="loop">Indicates whether the audio should loop (default is false).</param>
        /// <param name="delay">The delay before playback starts in seconds (default is 0).</param>
        /// <param name="mute">Indicates whether the audio should be muted (default is false).</param>
        /// <returns>A <see cref="AudioPlayback"/> object containing playback details.</returns>
        public AudioPlayback PlayAudio (string audio, float volume = 1F, bool loop = false, float delay = 0, bool mute = false)
        {
            try
            {
                AudioPlayback playAudioInfo = new AudioPlayback (Guid.NewGuid ().ToString ());
                playingAudios.Add (playAudioInfo.Id, playAudioInfo);

                audioLoader.LoadAudio (
                    audio,
                    (audioClip) =>
                    {
                        playAudioInfo.delay = delay;
                        playAudioInfo.audioClip = audioClip;
                        playAudioInfo.audioSource = GetAudioSource ();
                        playAudioInfo.audioSource.clip = audioClip;
                        playAudioInfo.audioSource.volume = volume;
                        playAudioInfo.audioSource.loop = loop;
                        playAudioInfo.audioSource.mute = mute;

                        if (delay > 0)
                            playAudioInfo.audioSource.PlayDelayed (delay);
                        else
                            playAudioInfo.audioSource.Play ();

                        StartCoroutine (WaitForAudioCompletion (playAudioInfo));
                    }
                );

                return playAudioInfo;
            }
            catch (Exception exception)
            {
                Debug.LogError ($"[{nameof (AudioManager)}] {nameof (PlayAudio)} - Exception: {exception}");
                return null;
            }
        }

        /// <summary>
        /// Stops a currently playing audio using its playback information.
        /// </summary>
        /// <param name="playAudioInfo">The playback information of the audio to stop.</param>
        public void StopAudio (AudioPlayback playAudioInfo)
        {
            if (playAudioInfo != null)
                StopAudio (playAudioInfo.Id);
        }

        /// <summary>
        /// Stops a currently playing audio using its unique Id.
        /// </summary>
        /// <param name="id">The unique Id of the audio to stop.</param>
        public void StopAudio (string id)
        {
            try
            {
                if (playingAudios.Remove (id, out AudioPlayback playAudioInfo))
                {
                    playAudioInfo.audioSource.Stop ();
                    factory.DisableElement (playAudioInfo.audioSource);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError ($"[{nameof (AudioManager)}] {nameof (StopAudio)} - Exception: {exception}");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the internal hierarchy and initializes the factory if not already set.
        /// </summary>
        private void BuildHierarchy ()
        {
            if (factory == null)
            {
                GameObject templateGameObject = new GameObject ("AudioSourceTemplate");
                templateGameObject.transform.SetParent (transform);
                templateGameObject.SetActive (false);
                AudioSource template = templateGameObject.AddComponent<AudioSource> ();
                template.playOnAwake = false;
                factory ??= new Factory<AudioSource> (template);
            }
        }

        /// <summary>
        /// Retrieves an available AudioSource from the factory, or creates a new one if none are available.
        /// </summary>
        /// <returns>A reusable or newly instantiated AudioSource.</returns>
        private AudioSource GetAudioSource ()
        {
            return factory.GetElement (transform);
        }

        /// <summary>
        /// Waits for the audio to complete playback and then stops it.
        /// </summary>
        /// <param name="playAudioInfo">The playback information of the audio.</param>
        /// <returns>An enumerator for coroutine execution.</returns>
        private IEnumerator WaitForAudioCompletion (AudioPlayback playAudioInfo)
        {
            yield return new WaitForSecondsRealtime (playAudioInfo.delay);
            yield return new WaitForSecondsRealtime (playAudioInfo.audioClip.length);
            yield return new WaitWhile (() => playAudioInfo.audioSource.isPlaying);
            StopAudio (playAudioInfo);
        }

        #endregion
    }
}