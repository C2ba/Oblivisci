using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectMatWarp : AbstractWarp
{
    [Tooltip("The amount of arrays is the amount of warps this can do. It will cycle through them")]
    [SerializeField] Material[] mats;
    [Tooltip("The rendere to change the material of")]
    [SerializeField] Renderer rend;
    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = mats.Length;
        wType = WarpType.ChangeMaterial;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        rend.material = mats[numOfSwitch];
    }
}