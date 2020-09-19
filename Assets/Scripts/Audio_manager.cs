using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : MonoBehaviour
{
    private static Audio_manager _instance;

    public static Audio_manager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        isMute = PlayerPrefs.GetInt("mute",0) == 0 ? false : true;
        Do_mute();
    }
    private bool isMute = false;
    public bool IsMute
    {
        get;
    }

    public AudioSource bgm_audio_source;

    public AudioClip sea_wave_clip;
    public AudioClip gold_clip;
    public AudioClip reward_clip;
    public AudioClip fire_clip;
    public AudioClip gun_switch_clip;
    public AudioClip levelup_clip;

    void Do_mute()
    {
        if (isMute)
        {
            bgm_audio_source.Pause();
        }
        else
        {
            bgm_audio_source.Play();
        }
    }

    public void Mute_state_change()
    {
        isMute = !isMute;
        Do_mute();
    }

    public void Play_clip(AudioClip clip)
    {
        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
    }
}
