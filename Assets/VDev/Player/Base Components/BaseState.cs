using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class BaseState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
