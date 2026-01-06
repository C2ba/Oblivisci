using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectMoveWarp : AbstractWarp
{
    [Tooltip("The place to send the obejct to")]
    [SerializeField] Transform[] newTransforms;
    [Tooltip("The object to move")]
    [SerializeField] GameObject warpGameObject;

    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = newTransforms.Length;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        warpGameObject.transform.position = newTransforms[numOfSwitch].position;
        warpGameObject.transform.rotation = newTransforms[numOfSwitch].rotation;
    }
}
