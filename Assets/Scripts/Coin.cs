using System;
using System.ComponentModel;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [Space, Header("Audio")]
    [SerializeField]
    private AudioClip collection;

    [SerializeField, Range(1, 1000), DefaultValue(100)]
    private int _value;

    private bool _isCollected;

    private AudioSource _collectionAudioSource;

    public bool IsCollected => _isCollected;
    public event EventHandler Collected;

    public int Value => _value;

    private void Start()
    {
        _collectionAudioSource = gameObject.AddComponent<AudioSource>();
        _collectionAudioSource.clip = collection;
        _collectionAudioSource.volume = 0.4f;
        _collectionAudioSource.Play();
        _collectionAudioSource.Pause();
    }

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
            _collectionAudioSource.Play();
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
