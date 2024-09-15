using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public event EventHandler Passed;

    private void OnTriggerEnter(Collider other)
    {
        Passed?.Invoke(this, EventArgs.Empty);
    }
}
