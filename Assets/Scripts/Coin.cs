using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Game game;

    private void Start()
    {
        game = FindAnyObjectByType<Game>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>().CompareTag("Player"))
        {
            Debug.Log("Player hit coin");
        }
    }
}
