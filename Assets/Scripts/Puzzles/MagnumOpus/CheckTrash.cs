using UnityEngine;

public class CheckTrash : MonoBehaviour
{
    private int fruitCount = 0;
    [SerializeField] DoorState door;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FruitCheck"))
        {
            fruitCount++;
            if (fruitCount >= 5)
            {
                door.SetUnlocked();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        door.ResetDoorPos();
        door.SetLocked();
    }
}
