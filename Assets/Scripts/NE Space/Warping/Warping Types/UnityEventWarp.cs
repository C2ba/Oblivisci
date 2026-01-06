using UnityEngine;

public class UnityEventWarp : AbstractWarp
{
    protected override void Awake()
    {
        base.Awake();
        wType = WarpType.UnityEvent;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        return;
    }
}
