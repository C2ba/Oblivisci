using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<InventoryItemReference> refs;

    [SerializeField] GameObject keyPrefab;


    public static ItemManager instance;

    void Awake()
    {
        // if the current instance hasn't been assigned, that means this object is the first instance of this class.
        if (instance == null)
        {
            // Set the instance to this object.
            instance = this;
        }
        else
        {
            // if the instance already exist and it's not this object
            if (instance != this)
            {
                // destroy this one
                Destroy(gameObject);
            }
        }
    }
    public void AddKey()
    {
        for (int i = 0; i < refs.Count; i++)
        {
            Debug.Log("CHECKING FOR KEY");
            InventoryItemReference Ref = refs[i];

            if (Ref != null)
            {
                if (Ref.item == null)
                {
                    GameObject newKey = Instantiate(keyPrefab, Ref.transform.position, Ref.transform.rotation);
                    newKey.SetActive(false);

                    Ref.item = newKey;
                    Ref.itemRef = Ref.item.GetComponent<PickupItemScript>();

                    return;
                }
            }
        }
    }
}
