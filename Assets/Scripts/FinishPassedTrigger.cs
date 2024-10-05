using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPassedTrigger : MonoBehaviour
{
    [SerializeField]
    private Finish finish;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered == false)
        {
            triggered = true;
            finish.OnFinishTrigger();
        }
    }
}
