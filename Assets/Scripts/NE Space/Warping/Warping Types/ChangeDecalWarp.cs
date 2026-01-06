using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeDecalWarp : AbstractWarp
{
    [Tooltip("The amount of arrays is the amount of warps this can do. It will cycle through them")]
    [SerializeField] Material[] decals;
    [Tooltip("The projector to change the material of")]
    [SerializeField] DecalProjector projector;
    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = decals.Length;
        wType = WarpType.ChangeDecal;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        projector.material = decals[numOfSwitch];
    }
}
