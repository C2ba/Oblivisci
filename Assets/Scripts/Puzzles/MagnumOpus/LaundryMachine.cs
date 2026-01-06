using UnityEngine;

public class LaundryMachine : MonoBehaviour
{
    [SerializeField] GameObject doorGameObject;
    [SerializeField] GameObject fakeWall;
    [SerializeField] DoorState door;

    private int clothCount = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Item")
        {
            clothCount++;
            Destroy(collision.gameObject);
            if (clothCount >= 3)
            {
                fakeWall.SetActive(false);
                doorGameObject.SetActive(true);
                door.SetUnlocked();
            }
        }
    }
}
