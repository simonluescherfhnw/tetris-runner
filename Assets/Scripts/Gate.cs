using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [Space, Header("Audio")]
    [SerializeField]
    private AudioClip passedSound;

    private bool _passed = false;
    private Renderer[] _renderers;
    private AudioSource _passedSoundAudioSource;

    public event EventHandler GatePassed;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _passedSoundAudioSource = gameObject.AddComponent<AudioSource>();
        _passedSoundAudioSource.clip = passedSound;
        _passedSoundAudioSource.volume = 0.4f;
        _passedSoundAudioSource.Play();
        _passedSoundAudioSource.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_passed)
        {
            return;
        }
        Debug.Log("Trigger " + other.name);
        _passed = true;
        _particleSystem.Play();
        _passedSoundAudioSource.Play();

        foreach (var renderer in _renderers)
        {
            if (renderer.gameObject == _particleSystem.gameObject)
            {
                continue;
            }
            renderer.gameObject.SetActive(false);
        }
        OnGatePassed();
    }

    private void OnGatePassed()
    {
        GatePassed?.Invoke(this, EventArgs.Empty);
    }
}
