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
    [SerializeField]
    private Camera mainCamera;

    private float laneGap = 1f;
    private Lane currentLane;
    private float width = 0;
    private float speed = 0.05f;

    private void Start()
    {
        currentLane = Lane.Middle;

        var renderers = GetComponentsInChildren<Renderer>();
        var x1 = renderers.Select(r => r.bounds.min.x).Min();
        var x2 = renderers.Select(r => r.bounds.max.x).Max();
        width = x2 - x1;
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
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + speed);
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
    }

    enum Lane
    {
        Left,
        Middle,
        Right
    }
}
