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

Define which IAudioLoader will be used to load audios.
```csharp
AudioManager.Instance.SetAudioLoader (new AddressablesAudioLoader ());
```

Play an audio.
```csharp
AudioManager.Instance.PlayAudio ("audio_path");
```

Play an audio with optional parameters.
```csharp
AudioManager.Instance.PlayAudio ("audio_path", volume: 1F,  loop: true);
```

Use an AudioProfile to play an audio.
```csharp
AudioProfile myAudioProfile = new AudioProfile ()
{
    volume = 0.5F,
    mute = false
};

AudioManager.Instance.PlayAudio ("audio_path", volume: myAudioProfile.volume, mute: myAudioProfile.mute);
```