using UnityEngine;
using FMODUnity;

public class InventoryItemReference : MonoBehaviour
{
    public GameObject item;
    public PickupItemScript itemRef;
    [SerializeField] int index;
    [SerializeField] EventReference itemplace;

    void OnTriggerEnter(Collider other)
    {
        EnterItem(other.gameObject);
        FMODUnity.RuntimeManager.PlayOneShot(itemplace);
    }
    public void EnterItem(GameObject other)
    {
        if (LayerMask.LayerToName(other.layer) == "Item" && item == null)
        {
            item = other;
            itemRef = item.GetComponent<PickupItemScript>();
            itemRef.slotIndex = index;
            Debug.Log(itemRef.slotIndex);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (item != null && item == other.gameObject)
        {
            itemRef = null;
            item = null;
        }
    }

    void OnDisable()
    {
        if (item != null && itemRef != null)
        {
            if (itemRef.slotIndex == index)
            {
                item.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        Debug.Log(itemRef.slotIndex);
        if (item != null)
        {
            GameObject oldObj = item;

            if (itemRef != null)
            {
                item = Instantiate(itemRef.itemRef.prefab, transform.position, transform.rotation);
            }

            Destroy(oldObj);
            Debug.Log(item);
        }
    }
}
