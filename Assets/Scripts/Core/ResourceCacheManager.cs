using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class ResourceCacheManager : SingletonBehaviour<ResourceCacheManager>
{
    public SFXDictionaryBase sfxDic;
    public AudioClip bgmClip;
}

[Serializable]
public enum SFXSounds { SFX_NA, SFX_ANNA }
[Serializable]
public class SFXDictionaryBase : SerializableDictionaryBase<SFXSounds, AudioClip> { }

