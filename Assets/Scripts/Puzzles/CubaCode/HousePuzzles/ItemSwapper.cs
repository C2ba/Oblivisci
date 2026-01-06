using UnityEngine;
using System.Collections.Generic;

public class ItemSwapper : MonoBehaviour
{
    public List<Dictionary<string, GameObject>> Items = new List<Dictionary<string, GameObject>> { }; //This is for the items and their rotten versions
    public string ItemName;
    private GameObject Item;

    void Start()
    {
        Item = transform.parent.gameObject;
    }

    public void SwapItem() //Swaps out base food for the rotten version
    {
        GameObject baseItem = Items[0][ItemName];
        GameObject newItem = Items[1][ItemName];

        GameObject itemClone = Instantiate(newItem, Item.transform.position, Item.transform.rotation);
        Destroy(Item);
        Item = itemClone;
    }
}
