using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : SingletonBehaviour<GameSoundManager>
{
    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private const float BGM_DEFAULT_VOLUME = 0.5f;
    private const float SFX_DEFAULT_VOLUME = 1f;


    protected override void Init ()
    {
        base.Init();

        sfxSource = new GameObject("SFXSource").AddComponent<AudioSource>();
        sfxSource.transform.SetParent(transform);

        bgmSource = new GameObject("BgmSource").AddComponent<AudioSource>();
        bgmSource.transform.SetParent(transform);
    }

    private void Start ()
    {
        GameManager.inst.SoundOnAction += OnSoundEnable;
        GameManager.inst.PlaySfxAction += OnPlaySfx;

        sfxSource.volume = SFX_DEFAULT_VOLUME;
        bgmSource.volume = BGM_DEFAULT_VOLUME;

        bgmSource.clip = ResourceCacheManager.inst.bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private void OnSoundEnable(bool isEnable)
    {
        if(isEnable)
        {
            sfxSource.volume = SFX_DEFAULT_VOLUME;
            bgmSource.volume = BGM_DEFAULT_VOLUME;
        }
        else
        {
            sfxSource.volume = 0;
            bgmSource.volume = 0;
        } 
    }

    private void OnPlaySfx(SFXSounds sound)
    {
        sfxSource.clip = ResourceCacheManager.inst.sfxDic[sound];
        sfxSource.Play();
    }

    public void SetSfxPitch(float pitch)
    {
        sfxSource.pitch = pitch;
    }

    public void ResetSfxPitch()
    {
        sfxSource.pitch = 1;
    }
}
