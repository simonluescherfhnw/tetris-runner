using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private Finish finish;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered == false)
        {
            triggered = true;
            finish.OnAnimationTrigger();
        }
    }
}
