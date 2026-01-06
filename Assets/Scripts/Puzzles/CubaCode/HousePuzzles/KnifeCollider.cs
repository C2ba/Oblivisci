using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class KnifeCollider : MonoBehaviour
{
    [SerializeField] EventReference slice;
    public GameObject HalfApple;
    public GameObject QuarterApple;
    private List<GameObject> InteractedObjects = new List<GameObject> { };

    private int HalfAppleUses;
    private int QuarterAppleUses;
    private bool CuttingBoard;
    private bool CutDebounce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HalfAppleUses = 2;
        QuarterAppleUses = 2;
        CuttingBoard = false;
    }

    void OnTriggerEnter(Collider Collision)
    {
        GameObject Object = Collision.gameObject;
        string ObjectName = Object.name.Split(' ')[0];
        Debug.Log(ObjectName);
        
        if (InteractedObjects.Contains(Object) || CuttingBoard || CutDebounce) { return; }
        InteractedObjects.Add(Object);

        if (Object.tag == "SwappableItem")
        {
            Vector3 ObjectPos = Object.transform.position;
            Quaternion ObjectRot = Object.transform.rotation;

            if (ObjectName == "Apple" && HalfAppleUses > 0)
            {
                HalfAppleUses -= 1;
                CutDebounce = true;
                FMODUnity.RuntimeManager.PlayOneShot(slice);

                Debug.Log("Cut Half");
                GameObject LHalf = Instantiate(HalfApple, ObjectPos, ObjectRot);
                LHalf.transform.Rotate(0, -0.025f, 0);
                GameObject RHalf = Instantiate(HalfApple, new Vector3(ObjectPos.x, ObjectPos.y, ObjectPos.z + 0.075f), ObjectRot);
                RHalf.transform.Rotate(0, 0.025f, 0);
                Destroy(Object);

                LHalf.gameObject.name = "HalfApple (Clone)";
                RHalf.gameObject.name = "HalfApple (Clone)";

                LHalf.SetActive(true);
                RHalf.SetActive(true);
            }
            else if (ObjectName == "HalfApple" && QuarterAppleUses > 0)
            {
                CutDebounce = true;

                QuarterAppleUses -= 1;
                Debug.Log("Cut Quarter");
                FMODUnity.RuntimeManager.PlayOneShot(slice);
                GameObject LQuarter = Instantiate(QuarterApple, ObjectPos, ObjectRot);
                LQuarter.transform.Rotate(0, -0.05f, 0);
                GameObject RQuarter = Instantiate(QuarterApple, new Vector3(ObjectPos.x, ObjectPos.y, ObjectPos.z + 0.075f), ObjectRot);
                RQuarter.transform.Rotate(0, 0.05f, 0);

                RQuarter.gameObject.name = "QuarterApple (Clone)";
                LQuarter.gameObject.name = "QuarterApple (Clone)";
                Destroy(Object);

                LQuarter.SetActive(true);
                RQuarter.SetActive(true);
            }
        }
        else if (Object.name == "CuttingBoard")
        {
            CuttingBoard = true;
            Debug.Log("Activated");
        }

    }

    void OnTriggerExit(Collider Collision)
    {
        Debug.Log("UnRan");
        GameObject Object = Collision.gameObject;

        if (Object.name == "CuttingBoard")
        {
            CuttingBoard = false;
            Debug.Log("Deactivated");
        }
        else if (Object.tag == "SwappableItem")
        {
            Debug.Log("Cooldown Off");
            CutDebounce = false;
        }
    }
}
