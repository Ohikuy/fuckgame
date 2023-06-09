using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    public FloatEventSO volumeEvent;
    public VoidEventSO pauseEvent;

    [Header("广播")]
    public FloatEventSO syncVolumeEvent;

    [Header("组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer mixer;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
        volumeEvent.OnEventRaised += OnvolumeEvent;
        pauseEvent.OnEventRaised += OnpauseEvent;
    }



    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
        volumeEvent.OnEventRaised -= OnvolumeEvent;
        pauseEvent.OnEventRaised -= OnpauseEvent;
    }

    private void OnpauseEvent()
    {
        float amount;
        mixer.GetFloat("MasterVolume",out amount);
        syncVolumeEvent.RaiseEvent(amount);
    }

    private void OnvolumeEvent(float amount)
    {
        mixer.SetFloat("MasterVolume", amount*100-80);
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
