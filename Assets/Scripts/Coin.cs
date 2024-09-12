using JetBrains.Rider.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField, Range(1, 1000), DefaultValue(100)]
    private int _value;

    private bool _isCollected;

    public bool IsCollected => _isCollected;
    public event EventHandler Collected;

    public int Value => _value;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollected)
        {
            return;
        }

        if (other.GetComponentInParent<Rigidbody>().CompareTag("Player"))
        {
            Debug.Log("Player hit coin");
            _particleSystem.Play();
            _meshRenderer.enabled = false;
            _isCollected = true;
            OnCollected();
        }
    }

    private void OnCollected()
    {
        Collected?.Invoke(this, EventArgs.Empty);
    }
}
