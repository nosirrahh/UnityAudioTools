# UnityAudioTools

...

## Dependencies

Before installing this package, make sure the following dependencies are pre-installed in your Unity project:

- [Addressables](https://docs.unity3d.com/Packages/com.unity.addressables@2.3/manual/index.html) (```com.unity.addressables```) - Version ```1.0.0``` or higher
- [UnityCoreTools](https://github.com/nosirrahh/UnityCoreTools.git) (```com.nosirrahh.unitycoretools```) - Version ```1.0.0``` or higher
- [UnityAddressablesTools](https://github.com/nosirrahh/UnityAddressablesTools.git) (```com.nosirrahh.unityaddressablestools```) - Version ```1.0.0``` or higher

## Installation
1. Open your Unity project.
2. Add the package via the Package Manager:
   ```
   https://github.com/nosirrahh/UnityAudioTools.git
   ```

## How to Use

1. Define which implementation of ```IAudioLoader``` will be used to load audio files in your project.

a. Addressables

Use the ```AddressablesAudioLoader``` to load audio files via Unity's Addressables system.

```csharp
AudioManager.Instance.SetAudioLoader (new AddressablesAudioLoader ());
```

b. Resources

To use the ```ResourcesAudioLoader```, make sure the audio files are located inside a Resources folder in your project.
Here’s an example folder structure:

```
Assets/
└── Resources/
    ├── Click.wav
    └── Music.wav 
```

Then, set the audio loader as follows:

```csharp
AudioManager.Instance.SetAudioLoader (new ResourcesAudioLoader ());
```

c. Scene

To use the ```SceneAudioLoader```, you need to have a ```SceneAudioGroup``` configured in the scenes of your project. This component should manage the audio clips specific to each scene.

```csharp
AudioManager.Instance.SetAudioLoader (new SceneAudioLoader ());
```

2. Play an audio.

a. Using only the audio path.
```csharp
AudioManager.Instance.PlayAudio ("audio_path");
```

b. With optional parameters.
```csharp
AudioManager.Instance.PlayAudio ("audio_path", volume: 1F,  loop: true);
```

c. With an AudioProfile to play an audio.
```csharp
AudioProfile myAudioProfile = new AudioProfile ()
{
    volume = 0.5F,
    mute = false
};

AudioManager.Instance.PlayAudio ("audio_path", volume: myAudioProfile.volume, mute: myAudioProfile.mute);
```