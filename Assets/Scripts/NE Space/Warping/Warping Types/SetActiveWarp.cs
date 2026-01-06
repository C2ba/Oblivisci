using System.Collections;
using UnityEngine;

public class SetActiveWarp : AbstractWarp
{
    [Tooltip("Through each cycle, the objects will be set this bool (I really dont know why im adding this but if it makes for a good puzzles go ahead)")]
    [SerializeField] bool[] isActive;
    [SerializeField] GameObject switchObject;


    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = isActive.Length;
        wType = WarpType.SetActive;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        if (switchObject != gameObject)
        {
            switchObject.SetActive(isActive[numOfSwitch]);
        }
        else
        {
            StartCoroutine(SetSelfInactive(numOfSwitch));
        }
    }

    IEnumerator SetSelfInactive(int numOfSwitch)
    {
        yield return null;
        yield return null; //Set it after player warp

        gameObject.SetActive(isActive[numOfSwitch]);
    }
}
