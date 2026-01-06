using UnityEngine;

public class ChangeObjectWarp : AbstractWarp
{
    [Tooltip("These are the objects that you want to change")]
    [SerializeField] GameObject oldObject;
    [Tooltip("This is the object that you want your object to change to")]
    [SerializeField] GameObject[] newObjects;


    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = newObjects.Length;
        wType = WarpType.ChangeObject;
    }
    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        Debug.Log("CHANGING");
        GameObject original = oldObject;

        oldObject = Instantiate(newObjects[numOfSwitch], original.transform.position, original.transform.rotation);
        ChangeWarpObject(original, oldObject);

        Destroy(original);
    }
}