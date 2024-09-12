using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    private bool _passed = false;
    private Renderer[] _renderers;

    public event EventHandler GatePassed;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
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
        foreach (var renderer in _renderers)
        {
            renderer.enabled = false;
        }
        OnGatePassed();
    }

    private void OnGatePassed()
    {
        GatePassed?.Invoke(this, EventArgs.Empty);
    }
}
