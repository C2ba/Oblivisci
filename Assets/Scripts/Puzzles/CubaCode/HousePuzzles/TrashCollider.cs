using UnityEngine;

public class TrashCollider : MonoBehaviour
{

    private TaskHandler HousePuzzle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HousePuzzle = transform.parent.gameObject.GetComponent<TaskHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Food)
    {
     //   Debug.Log("Touched");
       // Debug.Log(Food.gameObject.name.Split(' ')[0]);
        if (Food.gameObject.tag == "SwappableItem") //Temp Code
        {
            HousePuzzle.FoodtoTrash(Food.gameObject);
        }
    }
}
