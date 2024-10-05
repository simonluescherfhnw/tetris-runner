using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public event EventHandler Passed;
    public event EventHandler AnimationTriggerPassed;

    internal void OnAnimationTrigger()
    {
        AnimationTriggerPassed?.Invoke(this, EventArgs.Empty);
    }
    internal void OnFinishTrigger()
    {
        Passed?.Invoke(this, EventArgs.Empty);
    }
}
