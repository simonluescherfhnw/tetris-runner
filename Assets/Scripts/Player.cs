using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [Space, Header("Audio")]
    [SerializeField]
    private AudioClip move;
    [SerializeField]
    private AudioClip rotate;
    [SerializeField]
    private AudioClip crash;


    private float laneGap = 1f;
    private Lane currentLane;
    private float width = 0;
    private float speed = 0.05f;
    private Renderer[] _renderers;

    private AudioSource _moveAudioSource;
    private AudioSource _rotateAudioSource;
    private AudioSource _crashAudioSource;

    public event EventHandler Collided;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        currentLane = Lane.Middle;
        mainCamera = FindObjectOfType<Camera>();

        var renderers = GetComponentsInChildren<Renderer>();
        var x1 = renderers.Select(r => r.bounds.min.x).Min();
        var x2 = renderers.Select(r => r.bounds.max.x).Max();
        width = x2 - x1;

        _moveAudioSource = gameObject.AddComponent<AudioSource>();
        _moveAudioSource.clip = move;
        _moveAudioSource.loop = false;
        _moveAudioSource.volume = 0.5f;
        _moveAudioSource.Play();
        _moveAudioSource.Pause();

        _rotateAudioSource = gameObject.AddComponent<AudioSource>();
        _rotateAudioSource.clip = rotate;
        _rotateAudioSource.loop = false;
        _rotateAudioSource.volume = 0.4f;
        _rotateAudioSource.Play();
        _rotateAudioSource.Pause();

        _crashAudioSource = gameObject.AddComponent<AudioSource>();
        _crashAudioSource.clip = crash;
        _crashAudioSource.loop = false;
        _crashAudioSource.volume = 0.4f;
        _crashAudioSource.Play();
        _crashAudioSource.Pause();
    }
    public void Stop()
    {
        if (speed > 0f)
        {
            speed = 0f;
            _particleSystem.Play();
            _crashAudioSource?.Play();

            foreach (var renderer in _renderers)
            {
                if (renderer.gameObject == _particleSystem.gameObject)
                {
                    continue;
                }
                renderer.gameObject.SetActive(false);
            }
        }
    }

    public virtual void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();

        if (input.x > 0) // Right arrow key
        {
            MoveRight();
        }
        else if (input.x < 0) // Left arrow key
        {
            MoveLeft();
        }
        _moveAudioSource?.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision();
    }

    public void OnCollision()
    {
        Stop();

        Collided?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate()
    {
        if (speed > 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + speed);
        }
    }

    private void MoveLeft()
    {
        var currentPosition = transform.position;
        switch (currentLane)
        {
            case Lane.Left:
                break;
            case Lane.Middle:
            case Lane.Right:
                transform.position = new Vector3(currentPosition.x - laneGap - width, currentPosition.y, currentPosition.z);
                currentLane -= 1;
                break;
        }
    }

    private void MoveRight()
    {
        var currentPosition = transform.position;
        switch (currentLane)
        {
            case Lane.Left:
            case Lane.Middle:
                transform.position = new Vector3(currentPosition.x + laneGap + width, currentPosition.y, currentPosition.z);
                currentLane += 1;
                break;
            case Lane.Right:
                break;
        }
    }

    public virtual void OnRotate(InputValue value)
    {
        var input = value.Get<Vector2>();


        if (input.x > 0) // Right arrow key
        {
            transform.Rotate(Vector3.back, 90, Space.Self);

            //var rotationSpeed = 0.1f;
            //var targetRotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            //var targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90f, transform.rotation.eulerAngles.z);
            //var targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 90);
        }
        else if (input.x < 0) // Left arrow key
        {
            transform.Rotate(Vector3.forward, 90, Space.Self);
        }
        _rotateAudioSource?.Play();
    }

    enum Lane
    {
        Left,
        Middle,
        Right
    }
}
